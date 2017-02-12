// By Shrike 2/5/17

using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
    public class DarkStoneFroe : Froe
    {
        public override CraftSystem CraftSystem { get { return DefCarpentry.CraftSystem; } }

        [Constructable]
        public DarkStoneFroe() : base()
        {
            Name = "DarkStone Froe";
            Weight = 1.0;
        }

        [Constructable]
        public DarkStoneFroe(int uses) : base(uses)
        {
            Weight = 1.0;
        }

        public DarkStoneFroe(Serial serial)
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


    }
}