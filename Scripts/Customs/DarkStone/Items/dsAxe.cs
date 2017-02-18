// By Shrike 2/5/17

using System;
using Server.Items;
using Server.Network;
using Server.Engines.Harvest;

namespace Server.Items
{
    [FlipableAttribute(0xF43, 0xF44)]
    public class dsAxe : BaseAxe
	{
		// DG override harvest system with darkstone harvest system
		public override HarvestSystem HarvestSystem { get { return dsLumberjacking.System; } } // make dslumberjack

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 41; } }
		public override float MlSpeed{ get{ return 2.75f; } }

		public override int OldStrengthReq{ get{ return 15; } }
		public override int OldMinDamage{ get{ return 2; } }
		public override int OldMaxDamage{ get{ return 17; } }
		public override int OldSpeed{ get{ return 40; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 80; } }

		[Constructable]
		public dsAxe() : base( 0xF43 )
		{
            Name = "DarkStone Axe";
			Weight = 4.0;
		}

		public dsAxe( Serial serial ) : base( serial )
		{
		}
    
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        /*
        public override void OnDoubleClick(Mobile from)
        {
            if (HarvestSystem == null || Deleted)
                return;

            Point3D loc = GetWorldLocation();

            if (!from.InLOS(loc) || !from.InRange(loc, 2))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3E9, 1019045); // I can't reach that
                return;
            }
            else if (!this.IsAccessibleTo(from))
            {
                this.PublicOverheadMessage(MessageType.Regular, 0x3E9, 1061637); // You are not allowed to access this.
                return;
            }

            from.SendLocalizedMessage(1010018); // What do you want to use this item on?

            HarvestSystem.BeginHarvesting(from, this);
        }
         * */
    }
}