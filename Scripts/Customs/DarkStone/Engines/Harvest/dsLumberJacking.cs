﻿using System;
using Server;
using Server.Items;
using Server.Network;
using System.Collections.Generic;
using DarkStone;

namespace Server.Engines.Harvest
{
    public class dsLumberjacking : HarvestSystem
    {
        private static dsLumberjacking m_System;
        private object m_toHarvest;
        private int m_tileID;

        public static dsLumberjacking System
        {
            get
            {
                if (m_System == null)
                    m_System = new dsLumberjacking();

                return m_System;
            }
        }

        private HarvestDefinition m_Definition;

        public HarvestDefinition Definition
        {
            get { return m_Definition; }
        }

        private dsLumberjacking()
        {
            HarvestResource[] res;
            HarvestVein[] veins;

            #region Lumberjacking
            HarvestDefinition lumber = new HarvestDefinition();

            // Resource banks are every 4x3 tiles
            lumber.BankWidth = 4;
            lumber.BankHeight = 3;

            // Every bank holds from 20 to 45 logs
            lumber.MinTotal = 20;
            lumber.MaxTotal = 45;

            // A resource bank will respawn its content every 20 to 30 minutes
            lumber.MinRespawn = TimeSpan.FromMinutes(20.0);
            lumber.MaxRespawn = TimeSpan.FromMinutes(30.0);

            // Skill checking is done on the Lumberjacking skill
            lumber.Skill = SkillName.Lumberjacking;

            // Set the list of harvestable tiles
            lumber.Tiles = m_TreeTiles;

            // Players must be within 2 tiles to harvest
            lumber.MaxRange = 2;

            // Ten logs per harvest action
            lumber.ConsumedPerHarvest = 10;
            lumber.ConsumedPerFeluccaHarvest = 20;

            // The chopping effect
            lumber.EffectActions = new int[] { 13 };
            lumber.EffectSounds = new int[] { 0x13E };
            lumber.EffectCounts = (Core.AOS ? new int[] { 1 } : new int[] { 1, 2, 2, 2, 3 });
            lumber.EffectDelay = TimeSpan.FromSeconds(1.6);
            lumber.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            lumber.NoResourcesMessage = 500493; // There's not enough wood here to harvest.
            lumber.FailMessage = 500495; // You hack at the tree for a while, but fail to produce any useable wood.
            lumber.OutOfRangeMessage = 500446; // That is too far away.
            lumber.PackFullMessage = 500497; // You can't place any wood into your backpack!
            lumber.ToolBrokeMessage = 500499; // You broke your axe.

            res = new HarvestResource[]
			{
				new HarvestResource(  00.0, 00.0, 100.0, 1072540, typeof( Log ) ),
				new HarvestResource(  65.0, 25.0, 105.0, 1072541, typeof( OakLog ) ),
				new HarvestResource(  80.0, 40.0, 120.0, 1072542, typeof( AshLog ) ),
				new HarvestResource(  95.0, 55.0, 135.0, 1072543, typeof( YewLog ) ),
				new HarvestResource( 100.0, 60.0, 140.0, 1072544, typeof( HeartwoodLog ) ),
				new HarvestResource( 100.0, 60.0, 140.0, 1072545, typeof( BloodwoodLog ) ),
				new HarvestResource( 100.0, 60.0, 140.0, 1072546, typeof( FrostwoodLog ) ),
			};


            veins = new HarvestVein[]
			{
				new HarvestVein( 49.0, 0.0, res[0], null ),	// Ordinary Logs
				new HarvestVein( 30.0, 0.5, res[1], res[0] ), // Oak
				new HarvestVein( 10.0, 0.5, res[2], res[0] ), // Ash
				new HarvestVein( 05.0, 0.5, res[3], res[0] ), // Yew
				new HarvestVein( 03.0, 0.5, res[4], res[0] ), // Heartwood
				new HarvestVein( 02.0, 0.5, res[5], res[0] ), // Bloodwood
				new HarvestVein( 01.0, 0.5, res[6], res[0] ), // Frostwood
			};

            lumber.BonusResources = new BonusHarvestResource[]
			{
				new BonusHarvestResource( 0, 83.9, null, null ),	//Nothing
				new BonusHarvestResource( 100, 10.0, 1072548, typeof( BarkFragment ) ),
				new BonusHarvestResource( 100, 03.0, 1072550, typeof( LuminescentFungi ) ),
				new BonusHarvestResource( 100, 02.0, 1072547, typeof( SwitchItem ) ),
				new BonusHarvestResource( 100, 01.0, 1072549, typeof( ParasiticPlant ) ),
				new BonusHarvestResource( 100, 00.1, 1072551, typeof( BrilliantAmber ) )
			};

            lumber.Resources = res;
            lumber.Veins = veins;

            lumber.RaceBonus = Core.ML;
            lumber.RandomizeVeins = Core.ML;

            m_Definition = lumber;
            Definitions.Add(lumber);
            #endregion
        }

        public override bool CheckHarvest(Mobile from, Item tool)
        {
            if (!base.CheckHarvest(from, tool))
                return false;

            if (tool.Parent != from)
            {
                from.SendLocalizedMessage(500487); // The axe must be equipped for any serious wood chopping.
                return false;
            }

            return true;
        }

        public override bool CheckHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            m_toHarvest = toHarvest; // save harvested object to later determine reward

            if (!base.CheckHarvest(from, tool, def, toHarvest))
                return false;

            if (tool.Parent != from)
            {
                from.SendLocalizedMessage(500487); // The axe must be equipped for any serious wood chopping.
                return false;
            }

            return true;
        }

        public override void OnBadHarvestTarget(Mobile from, Item tool, object toHarvest)
        {
            if (toHarvest is Mobile)
                ((Mobile)toHarvest).PrivateOverheadMessage(MessageType.Regular, 0x3B2, 500450, from.NetState); // You can only skin dead creatures.
            else if (toHarvest is Item)
                ((Item)toHarvest).LabelTo(from, 500464); // Use this on corpses to carve away meat and hide
            else if (toHarvest is Targeting.StaticTarget || toHarvest is Targeting.LandTarget)
                from.SendLocalizedMessage(500489); // You can't use an axe on that.
            else
                from.SendLocalizedMessage(1005213); // You can't do that
        }

        public override void OnHarvestStarted(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            base.OnHarvestStarted(from, tool, def, toHarvest);

            if (Core.ML)
                from.RevealingAction();
        }

        public static void Initialize()
        {
            Array.Sort(m_TreeTiles);
        }

        #region Tile lists
        private static int[] m_TreeTiles = new int[]
			{
				0x4CCA, 0x4CCB, 0x4CCC, 0x4CCD, 0x4CD0, 0x4CD3, 0x4CD6, 0x4CD8,
				0x4CDA, 0x4CDD, 0x4CE0, 0x4CE3, 0x4CE6, 0x4CF8, 0x4CFB, 0x4CFE,
				0x4D01, 0x4D41, 0x4D42, 0x4D43, 0x4D44, 0x4D57, 0x4D58, 0x4D59,
				0x4D5A, 0x4D5B, 0x4D6E, 0x4D6F, 0x4D70, 0x4D71, 0x4D72, 0x4D84,
				0x4D85, 0x4D86, 0x52B5, 0x52B6, 0x52B7, 0x52B8, 0x52B9, 0x52BA,
				0x52BB, 0x52BC, 0x52BD,

				0x4CCE, 0x4CCF, 0x4CD1, 0x4CD2, 0x4CD4, 0x4CD5, 0x4CD7, 0x4CD9,
				0x4CDB, 0x4CDC, 0x4CDE, 0x4CDF, 0x4CE1, 0x4CE2, 0x4CE4, 0x4CE5,
				0x4CE7, 0x4CE8, 0x4CF9, 0x4CFA, 0x4CFC, 0x4CFD, 0x4CFF, 0x4D00,
				0x4D02, 0x4D03, 0x4D45, 0x4D46, 0x4D47, 0x4D48, 0x4D49, 0x4D4A,
				0x4D4B, 0x4D4C, 0x4D4D, 0x4D4E, 0x4D4F, 0x4D50, 0x4D51, 0x4D52,
				0x4D53, 0x4D5C, 0x4D5D, 0x4D5E, 0x4D5F, 0x4D60, 0x4D61, 0x4D62,
				0x4D63, 0x4D64, 0x4D65, 0x4D66, 0x4D67, 0x4D68, 0x4D69, 0x4D73,
				0x4D74, 0x4D75, 0x4D76, 0x4D77, 0x4D78, 0x4D79, 0x4D7A, 0x4D7B,
				0x4D7C, 0x4D7D, 0x4D7E, 0x4D7F, 0x4D87, 0x4D88, 0x4D89, 0x4D8A,
				0x4D8B, 0x4D8C, 0x4D8D, 0x4D8E, 0x4D8F, 0x4D90, 0x4D95, 0x4D96,
				0x4D97, 0x4D99, 0x4D9A, 0x4D9B, 0x4D9D, 0x4D9E, 0x4D9F, 0x4DA1,
				0x4DA2, 0x4DA3, 0x4DA5, 0x4DA6, 0x4DA7, 0x4DA9, 0x4DAA, 0x4DAB,
				0x52BE, 0x52BF, 0x52C0, 0x52C1, 0x52C2, 0x52C3, 0x52C4, 0x52C5,
				0x52C6, 0x52C7
			};
        #endregion

        public override Type GetResourceType(Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource)
        {
            // get tileID
            GetHarvestDetails(from, tool, m_toHarvest, out m_tileID, out map, out loc);

            // this returns a random woodType, we override that in construct
            if (resource.Types.Length > 0)
            {
                return resource.Types[Utility.Random(resource.Types.Length)];
            }
            return null; // thuis use to return a random woodType, instead we will use the tileID to determine the woodType
        }

        public override Item Construct(Type type, Mobile from)
        {
            Dictionary<int, DarkStone.WoodType> trees = new Dictionary<int, DarkStone.WoodType>();
            

            trees.Add(0x4CCA, DarkStone.WoodType.Teakwood);
            trees.Add(0x4CCB, DarkStone.WoodType.Teakwood);
            trees.Add(0x4CCC, DarkStone.WoodType.Teakwood); // could use this as another tree type
            trees.Add(0x4CCD, DarkStone.WoodType.Ashwood);
            trees.Add(0x4CD0, DarkStone.WoodType.Birchwood);
            trees.Add(0x4CD3, DarkStone.WoodType.Hickory);
            trees.Add(0x4CDA, DarkStone.WoodType.Oakwood); 
            trees.Add(0x4CDD, DarkStone.WoodType.Oakwood); 
            trees.Add(0x6224, DarkStone.WoodType.Oakwood); // stange add
            trees.Add(0x624D, DarkStone.WoodType.Oakwood); // strange add
            trees.Add(0x647D, DarkStone.WoodType.Maplewood); // strange add
            trees.Add(0x647E, DarkStone.WoodType.Maplewood); // strange add
            trees.Add(0x4CE0, DarkStone.WoodType.Walnut); 
            trees.Add(0x4CE3, DarkStone.WoodType.Walnut); 
            trees.Add(0x4D42, DarkStone.WoodType.Mahogany); 
            trees.Add(0x4D43, DarkStone.WoodType.Mahogany); 
            trees.Add(0x4D85, DarkStone.WoodType.Mahogany); 
            trees.Add(0x4D59, DarkStone.WoodType.Rosewood); 
            trees.Add(0x4D70, DarkStone.WoodType.Rosewood); 
            trees.Add(0x6476, DarkStone.WoodType.Cherrywood); // stange add
            trees.Add(0x6477, DarkStone.WoodType.Cherrywood); // stange add
            trees.Add(0x52B7, DarkStone.WoodType.Yew); // stange add
            trees.Add(0x52B8, DarkStone.WoodType.Yew); // stange add
            trees.Add(0x52B9, DarkStone.WoodType.Yew); // stange add
            trees.Add(0x52BA, DarkStone.WoodType.Yew); // stange add

            // softwood pines
            trees.Add(0x4CD6, DarkStone.WoodType.Cedarwood);
            trees.Add(0x4CD8, DarkStone.WoodType.Cedarwood);
            trees.Add(0x4CF8, DarkStone.WoodType.Cypress);
            trees.Add(0x4CFB, DarkStone.WoodType.Cypress);
            trees.Add(0x4CFE, DarkStone.WoodType.Cypress);
            trees.Add(0x4D01, DarkStone.WoodType.Cypress);

            // still need to add leaves here appraently you can click them too
            // might have to add regular log sources as well to get ride of things like frost wood
            //0x309C, 0x30A1, 30BD, 30BE 30C3, 30C4, 30C6, 30D4, 30D7, 30DA, 30DD

            try {
                if (m_toHarvest is Targeting.StaticTarget || m_toHarvest is Targeting.LandTarget) {

                    // is this a darkstone compatible tree
                    if (trees.ContainsKey(m_tileID))
                    {
                        DarkStone.WoodType woodType = trees[m_tileID];

                        // is this a softwood pine
                        if (woodType == DarkStone.WoodType.Cedarwood)
                        {
                            // chose a random softwood pine
                            Random rnd = new Random();
                            woodType = (DarkStone.WoodType)((int)woodType + rnd.Next(0, 7));
                        }
                        return new dsLog(woodType) as Item;
                    }
                    else
                    {
                        return Activator.CreateInstance(typeof(Log)) as Item;
                    }
                }
            }
            catch { return null; }

            return null;
        }
    }
}
