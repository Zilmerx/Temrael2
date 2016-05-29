﻿using System;
using Server.Items;

namespace Server.Items
{
    public class ElfiqueChaineTunic : BaseArmor
    {
        //public override int NiveauAttirail { get { return ChainElfique_Niveau; } }

        public override double BasePhysicalResistance { get { return ArmorChainElf.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorChainElf.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorChainElf.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorChainElf.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorChainElf.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorChainElf.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Chainmail; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public ElfiqueChaineTunic()
            : base(0x2897)
        {
            Weight = 2.0;
            Name = "Tunique Elfique";
            Layer = Layer.InnerTorso;
        }

        public ElfiqueChaineTunic(Serial serial)
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
    public class ElfiqueChaineLeggings : BaseArmor
    {
        //public override int NiveauAttirail { get { return ChainElfique_Niveau; } }

        public override double BasePhysicalResistance { get { return ArmorChainElf.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorChainElf.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorChainElf.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorChainElf.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorChainElf.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorChainElf.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Chainmail; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public ElfiqueChaineLeggings()
            : base(0x2898)
        {
            Weight = 2.0;
            Name = "Jambieres Elfiques";
            Layer = Layer.Pants;
        }

        public ElfiqueChaineLeggings(Serial serial)
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
}
