﻿using System;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("corps de canard")]
    public class Canard : BaseCreature
    {
        [Constructable]
        public Canard()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "Canard";
            Body = 0x72;
            BaseSoundID = 0x6E;
            Hue = 2053;

            SetStr(5);
            SetDex(15);
            SetInt(5);

            SetHits(3);
            SetMana(0);

            SetDamage(1);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 1, 5);

            SetSkill(SkillName.Concentration, 4.0);
            SetSkill(SkillName.Tactiques, 5.0);
            SetSkill(SkillName.Anatomie, 5.0);

            VirtualArmor = 2;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 0.0;
        }

        public override double AttackSpeed { get { return 2.0; } }
        public override int Meat { get { return 1; } }
        public override MeatType MeatType { get { return MeatType.Bird; } }
        public override FoodType FavoriteFood { get { return FoodType.GrainsAndHay; } }

        public override int Feathers { get { return 25; } }

        public Canard(Serial serial)
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