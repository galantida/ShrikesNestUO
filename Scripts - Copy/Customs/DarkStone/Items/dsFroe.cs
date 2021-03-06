﻿// By Shrike 2/5/17

using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
    public class dsFroe : Froe
    {
        public override CraftSystem CraftSystem { get { return DefCarpentry.CraftSystem; } }

        [Constructable]
        public dsFroe() : base()
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


    }
}