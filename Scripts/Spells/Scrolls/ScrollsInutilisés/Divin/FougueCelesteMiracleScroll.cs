﻿using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class FougueCelesteMiracleScroll : SpellScroll
    {
        [Constructable]
        public FougueCelesteMiracleScroll()
            : this(1)
        {
        }

        [Constructable]
        public FougueCelesteMiracleScroll(int amount)
            : base(2018, 0x227B, amount)
        {
            Name = "Fougue Céleste";
        }

        public FougueCelesteMiracleScroll(Serial serial)
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