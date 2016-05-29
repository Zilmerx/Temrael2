﻿using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("Corps de heron")]
    public class Heron : BaseCreature
    {
        [Constructable]
        public Heron()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "Heron";
            Body = 5;
            Hue = 2053;
            BaseSoundID = 0x2EE;

            SetStr(31, 47);
            SetDex(36, 60);
            SetInt(8, 20);

            SetHits(20, 27);
            SetMana(0);

            SetDamage(5, 10);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Magical, 5, 10);

            SetSkill(SkillName.Concentration, 15.3, 30.0);
            SetSkill(SkillName.Tactiques, 18.1, 37.0);
            SetSkill(SkillName.Anatomie, 20.1, 30.0);

            VirtualArmor = 22;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 5.0;
        }

        public override double AttackSpeed { get { return 2.0; } }
        public override int Meat { get { return 1; } }
        public override MeatType MeatType { get { return MeatType.Bird; } }
        public override int Feathers { get { return 12; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat | FoodType.Fish; } }

        public Heron(Serial serial)
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