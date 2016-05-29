﻿using System;
using Server.Items;

namespace Server.Items
{
    public class CuirasseReligieuse : BaseArmor
    {
        //public override int NiveauAttirail { get { return 4; } }

        public override double BasePhysicalResistance { get { return ArmorDivers4.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorDivers4.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorDivers4.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorDivers4.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorDivers4.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorDivers4.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public CuirasseReligieuse()
            : base(0x2876)
        {
            Weight = 2.0;
            Name = "Cuirasse Religieuse";
            Layer = Layer.InnerTorso;
        }

        public CuirasseReligieuse(Serial serial)
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
    public class TuniqueChaine : BaseArmor
    {
        //public override int NiveauAttirail { get { return 3; } }

        public override double BasePhysicalResistance { get { return ArmorDivers3.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorDivers3.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorDivers3.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorDivers3.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorDivers3.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorDivers3.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Ringmail; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public TuniqueChaine()
            : base(0x2877)
        {
            Weight = 2.0;
            Name = "Tunique de Chaine";
            Layer = Layer.InnerTorso;
        }

        public TuniqueChaine(Serial serial)
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
    public class Cuirasse : BaseArmor
    {
        //public override int NiveauAttirail { get { return 5; } }

        public override double BasePhysicalResistance { get { return ArmorDivers5.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorDivers5.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorDivers5.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorDivers5.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorDivers5.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorDivers5.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public Cuirasse()
            : base(0x2881)
        {
            Weight = 2.0;
            Name = "Cuirasse";
            Layer = Layer.InnerTorso;
        }

        public Cuirasse(Serial serial)
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
    public class CuirasseDraconique : BaseArmor
    {
        //public override int NiveauAttirail { get { return 6; } }

        public override double BasePhysicalResistance { get { return ArmorDivers6.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorDivers6.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorDivers6.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorDivers6.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorDivers6.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorDivers6.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public CuirasseDraconique()
            : base(0x2890)
        {
            Weight = 2.0;
            Name = "Cuirasse Draconique";
            Layer = Layer.InnerTorso;
        }

        public CuirasseDraconique(Serial serial)
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
    public class CuirasseBarbare : BaseArmor
    {
        //public override int NiveauAttirail { get { return 4; } }

        public override double BasePhysicalResistance { get { return ArmorDivers4.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorDivers4.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorDivers4.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorDivers4.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorDivers4.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorDivers4.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public CuirasseBarbare()
            : base(0x2891)
        {
            Weight = 2.0;
            Name = "Cuirasse Barbare";
            Layer = Layer.InnerTorso;
        }

        public CuirasseBarbare(Serial serial)
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
    public class CuirasseNordique : BaseArmor
    {
        //public override int NiveauAttirail { get { return 5; } }

        public override double BasePhysicalResistance { get { return ArmorDivers5.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorDivers5.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorDivers5.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorDivers5.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorDivers5.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorDivers5.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public CuirasseNordique()
            : base(0x2BDD)
        {
            Weight = 2.0;
            Name = "Cuirasse Nordique";
            Layer = Layer.InnerTorso;
        }

        public CuirasseNordique(Serial serial)
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
