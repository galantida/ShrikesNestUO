// By Shrike 2/5/17

using System;
using Server;
using Server.Network;
using Server.Engines.Craft;
using Server.Targets;

namespace Server.Items
{
    public class dsFroe : Item
    {
        [Constructable]
        public dsFroe()
            : base(0x10E5)
        {
            Name = "DarkStone Froe";
            Weight = 1.0;
        }

        [Constructable]
        public dsFroe(int uses) : base(uses)
        {
            Weight = 1.0;
        }

        public dsFroe(Serial serial)
            : base(serial)
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

            from.Target = new Server.Targets.dsToolTarget(this); // this brings up the crosshair
        }
    }
}