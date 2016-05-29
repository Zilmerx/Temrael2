﻿    using System;
    using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("Flaque putride")]
    public class GeleePutride : BaseCreature
    {
        [Constructable]
        public GeleePutride()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Gelée Putride";
            Body = 139;
            BaseSoundID = 898;

            PlayersAreEnemies = true;

            SetStr(120);
            SetDex(40);
            SetInt(25);

            SetHits(300);
            SetMana(50);
            SetStam(80);
            SetArme(12, 18, 50);

            SetResistance(ResistanceType.Physical, 40);
            SetResistance(ResistanceType.Magical, 30);

            SetSkill(SkillName.ArmureNaturelle, 100);
            SetSkill(SkillName.Tactiques, 70);
            SetSkill(SkillName.Epee, 70);
            SetSkill(SkillName.Anatomie, 50);
            SetSkill(SkillName.ResistanceMagique, 50);
            SetSkill(SkillName.Parer, 70);
        }

        public override void GenerateLoot()
        {

            Amethyst Amethyst = new Amethyst(1);
            AddToBackpack(Amethyst);

        }


        public GeleePutride(Serial serial)
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