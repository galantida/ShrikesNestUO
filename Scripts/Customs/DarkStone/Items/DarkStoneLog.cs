// By Shrike 2/5/17

using System; 
using Server; 
using Server.Network; 

namespace Server.Items 
{
    public enum DarkStoneWoodType { Sprucewood }


    [FlipableAttribute(0x1bdd, 0x1be0)]
    public class DarkStoneLog : Item, IAxe
    {
        private DarkStoneWoodType woodType;


        [Constructable]
        public DarkStoneLog(DarkStoneWoodType woodType)
            : base(0x1bdd)
        {
            this.woodType = woodType;
            this.Name = woodType.ToString() + " Log";
            this.Weight = 2.0;
            this.Stackable = true;

            // pick hue
            switch (woodType)
            {
                case DarkStoneWoodType.Sprucewood:
                    this.Hue = 0x04EA;
                    break;
                default:
                    // nothing
                    break;
            }
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
            if (!TryCreateBoards(from, 0, new DarkStoneLog(this.woodType))) return false;
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