﻿using System;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("Cadavre d'oiseau")]
    public class Oiseau : BaseCreature
    {
        [Constructable]
        public Oiseau()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "Oiseau";
            Body = 6;
            BaseSoundID = 27;
            Hue = 2113;

            SetStr(5);
            SetDex(15);
            SetInt(5);

            SetHits(3);
            SetMana(0);

            SetArme(1, 3, 40);

            SetResistance(ResistanceType.Physical, 0);
            SetResistance(ResistanceType.Magical, 0);

            SetSkill(SkillName.Concentration, 4.0);
            SetSkill(SkillName.Tactiques, 5.0);
            SetSkill(SkillName.Epee, 5.0);

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 5;
        }

        public override int Meat { get { return 1; } }
        public override MeatType MeatType { get { return MeatType.Bird; } }
        public override FoodType FavoriteFood { get { return FoodType.GrainsAndHay; } }

        public override int Feathers { get { return 10; } }

        public Oiseau(Serial serial)
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