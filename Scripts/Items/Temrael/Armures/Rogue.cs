﻿using System;
using Server.Items;

namespace Server.Items
{
    public class RoublardLeggings : BaseArmor
    {

        public override double BasePhysicalResistance { get { return ArmorLeather.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorLeather.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorLeather.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorLeather.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorLeather.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorLeather.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Leather; } }
        public override CraftResource DefaultResource { get { return CraftResource.RegularLeather; } }

        [Constructable]
        public RoublardLeggings()
            : base(0x2724)
        {
            Weight = 2.0;
            Name = "Jambieres Roublardes";
            Layer = Layer.Pants;
        }

        public RoublardLeggings(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            Layer = Layer.Pants;
        }
    }
    public class RoublardTunic : BaseArmor
    {

        public override double BasePhysicalResistance { get { return ArmorLeather.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorLeather.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorLeather.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorLeather.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorLeather.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorLeather.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Leather; } }
        public override CraftResource DefaultResource { get { return CraftResource.RegularLeather; } }

        [Constructable]
        public RoublardTunic()
            : base(0x2725)
        {
            Weight = 2.0;
            Name = "Tunique Roublarde";
            Layer = Layer.InnerTorso;
        }

        public RoublardTunic(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            Layer = Layer.InnerTorso;
        }
    }
}
