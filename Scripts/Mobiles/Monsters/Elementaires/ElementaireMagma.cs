﻿    using System;
    using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("Cadavre d'élémentaire de magma")]
    public class ElementaireMagma : BaseCreature
    {
        [Constructable]
        public ElementaireMagma()
            : base(AIType.AI_Mage, FightMode.Closest, 11, 1, 0.2, 0.4)
        {
            Name = "Élémentaire de Magma";
            Body = 253;
            BaseSoundID = 273;

            PlayersAreEnemies = true;

            SetStr(120);
            SetDex(40);
            SetInt(100);

            SetHits(275);
            SetMana(300);
            SetStam(80);
            SetArme(12, 19, 40);

            SetResistance(ResistanceType.Physical, 20);
            SetResistance(ResistanceType.Magical, 20);

            SetSkill(SkillName.ArmureNaturelle, 80);
            SetSkill(SkillName.Tactiques, 60);
            SetSkill(SkillName.Epee, 60);
            SetSkill(SkillName.ArtMagique, 100);
            SetSkill(SkillName.Evocation, 100);
            SetSkill(SkillName.CoupCritique, 50);
            SetSkill(SkillName.MagieDeGuerre, 50);
        }

        public override void GenerateLoot()
        {

            Ruby Ruby = new Ruby(1);
            AddToBackpack(Ruby);

            Ruby = new Ruby(1);
            AddToBackpack(Ruby);

        }


        public ElementaireMagma(Serial serial)
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