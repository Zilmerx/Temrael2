﻿using System;
using Server.Items;

namespace Server.Items
{
    public class DrowHelm : BaseArmor
    {
        //public override int NiveauAttirail { get { return ChainElfeNoir_Niveau; } }

        public override Layer Layer
        {
            get
            {
                return Server.Layer.Helm;
            }
            set
            {
                base.Layer = value;
            }
        }

        public override double BasePhysicalResistance { get { return ArmorChainNoir.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorChainNoir.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorChainNoir.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorChainNoir.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorChainNoir.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorChainNoir.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Ringmail; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public DrowHelm()
            : base(0x2D2D)
        {
            Weight = 2.0;
            Name = "Tiara d'Elfe Noir";
        }

        public DrowHelm(Serial serial)
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
        }
    }
    public class DrowGorget : BaseArmor
    {
        //public override int NiveauAttirail { get { return ChainElfeNoir_Niveau; } }

        public override Layer Layer
        {
            get
            {
                return Server.Layer.Neck;
            }
            set
            {
                base.Layer = value;
            }
        }

        public override double BasePhysicalResistance { get { return ArmorChainNoir.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorChainNoir.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorChainNoir.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorChainNoir.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorChainNoir.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorChainNoir.malus_Dex; } }
        
        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Ringmail; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public DrowGorget()
            : base(0x2D2B)
        {
            Weight = 2.0;
            Name = "Gorget d'Elfe Noir";
        }

        public DrowGorget(Serial serial)
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
        }
    }
    public class DrowArms : BaseArmor
    {
        //public override int NiveauAttirail { get { return ChainElfeNoir_Niveau; } }

        public override Layer Layer
        {
            get
            {
                return Server.Layer.Arms;
            }
            set
            {
                base.Layer = value;
            }
        }

        public override double BasePhysicalResistance { get { return ArmorChainNoir.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorChainNoir.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorChainNoir.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorChainNoir.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorChainNoir.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorChainNoir.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Ringmail; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public DrowArms()
            : base(0x2D29)
        {
            Weight = 2.0;
            Name = "Brassards d'Elfe Noir";
        }

        public DrowArms(Serial serial)
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
        }
    }
    public class DrowLeggings : BaseArmor
    {
        //public override int NiveauAttirail { get { return ChainElfeNoir_Niveau; } }

        public override Layer Layer
        {
            get
            {
                return Server.Layer.Pants;
            }
            set
            {
                base.Layer = value;
            }
        }

        public override double BasePhysicalResistance { get { return ArmorChainNoir.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorChainNoir.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorChainNoir.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorChainNoir.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorChainNoir.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorChainNoir.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Ringmail; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public DrowLeggings()
            : base(0x2D2C)
        {
            Weight = 2.0;
            Name = "Jambieres d'Elfe Noir";
        }

        public DrowLeggings(Serial serial)
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
        }
    }
    public class DrowTunic : BaseArmor
    {
        //public override int NiveauAttirail { get { return ChainElfeNoir_Niveau; } }

        public override Layer Layer
        {
            get
            {
                return Server.Layer.InnerTorso;
            }
            set
            {
                base.Layer = value;
            }
        }

        public override double BasePhysicalResistance { get { return ArmorChainNoir.resistance_Physique; } }
        public override double BaseMagicalResistance { get { return ArmorChainNoir.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorChainNoir.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorChainNoir.max_Durabilite; } }

        public override int BaseStrReq { get { return ArmorChainNoir.force_Requise; } }
        public override int BaseDexBonus { get { return ArmorChainNoir.malus_Dex; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Ringmail; } }
        public override CraftResource DefaultResource { get { return CraftResource.Fer; } }

        [Constructable]
        public DrowTunic()
            : base(0x2D2A)
        {
            Weight = 2.0;
            Name = "Tunique d'Elfe Noir";
        }

        public DrowTunic(Serial serial)
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
        }
    }
}