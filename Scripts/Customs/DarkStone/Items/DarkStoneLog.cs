// By Shrike 2/5/17

using System; 
using Server; 
using Server.Network; 
using System.Collections.Generic;

namespace Server.Items 
{
    [FlipableAttribute(0x1bdd, 0x1be0)]
    public class DarkStoneLog : Server.Item, IAxe
    {
        private DarkStone.WoodType woodType;


        [Constructable]
        public DarkStoneLog(DarkStone.WoodType woodType)
            : base(0x1bdd)
        {
            Dictionary<DarkStone.WoodType, int> hues = new Dictionary<DarkStone.WoodType, int>();

            // softwood pines (red Hues)
            hues.Add(DarkStone.WoodType.Redwood, 141); // done
            hues.Add(DarkStone.WoodType.Sprucewood, 341);
            hues.Add(DarkStone.WoodType.Pinewood, 441);
            hues.Add(DarkStone.WoodType.Firwood, 541);
            hues.Add(DarkStone.WoodType.Cedarwood, 641);
            hues.Add(DarkStone.WoodType.Hemlock, 741);
            hues.Add(DarkStone.WoodType.Cypress, 841);

            // hardwoods (brown hues)
            hues.Add(DarkStone.WoodType.Walnut, 151);
            hues.Add(DarkStone.WoodType.Mahogany, 251);
            hues.Add(DarkStone.WoodType.Cherrywood, 351);
            hues.Add(DarkStone.WoodType.Hickory, 451);
            hues.Add(DarkStone.WoodType.Rosewood, 241); // done
            hues.Add(DarkStone.WoodType.Yew, 551);

            // hardwoods (green hues)
            hues.Add(DarkStone.WoodType.Teakwood, 456);
            hues.Add(DarkStone.WoodType.Ashwood, 556);
            hues.Add(DarkStone.WoodType.Oakwood, 656);
            hues.Add(DarkStone.WoodType.Maplewood, 756);
            hues.Add(DarkStone.WoodType.Birchwood, 856);

            this.woodType = woodType;
            this.Name = woodType.ToString() + " Log";
            this.Weight = 2.0;
            this.Stackable = true;
            this.Hue = hues[woodType];
        }

        public DarkStoneLog(Serial serial) : base(serial)
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

        public override void OnDoubleClick(Mobile from)
        {

        }

        public virtual bool Axe(Mobile from, BaseAxe axe)
        {
            if (!TryCreateBoards(from, 0, new DarkStoneBoard(this.woodType))) return false;
            else return true;
        }

        public virtual bool TryCreateBoards(Mobile from, double skill, Item item)
        {
            if (Deleted || !from.CanSee(this))
                return false;
            else if (from.Skills.Carpentry.Value < skill &&
                from.Skills.Lumberjacking.Value < skill)
            {
                item.Delete();
                from.SendLocalizedMessage(1072652); // You cannot work this strange and unusual wood.
                return false;
            }
            base.ScissorHelper(from, item, 1, false);
            return true;
        }
    } 
}