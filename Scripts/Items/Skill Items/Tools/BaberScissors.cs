﻿using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
    public class BarberScissors : BaseTool
    {
        public override CraftSystem CraftSystem { get { return DefTailoring.CraftSystem; } }

        [Constructable]
        public BarberScissors()
            : base(0xDFC)
        {
            Weight = 2.0;
            Layer = Layer.OneHanded;
        }

        [Constructable]
        public BarberScissors(int uses)
            : base(uses, 0xDFC)
        {
            Weight = 2.0;
            Layer = Layer.OneHanded;
        }

        public BarberScissors(Serial serial)
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