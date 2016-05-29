﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Mobiles.NewAnimals
{
    [CorpseName( "Cadavre de chevre" )]
	public class Chevre : BaseCreature
	{
		[Constructable]
		public Chevre() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.3 )
		{
			Name = "Chevre";
			Body = 209;
			BaseSoundID = 0X09A;

			SetStr( 30 );
			SetDex( 15 );
			SetInt( 5 );

			SetHits( 60 );
			SetMana( 0 );
			SetStam( 30 );

            SetArme(1, 5, 30);

			SetResistance( ResistanceType.Physical, 1, 5 );
			SetResistance( ResistanceType.Magical, 1, 5 );

			SetSkill( SkillName.Concentration, 4.0 );
			SetSkill( SkillName.Tactiques, 7.0 );
			SetSkill( SkillName.Epee, 7.0 );

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 5;
        }

        public override int Meat { get { return 1; } }
        public override MeatType MeatType { get { return MeatType.LambLeg; } }
        public override int Hides { get { return 1; } }
        public override HideType HideType { get { return HideType.Regular; } }

		public Chevre(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
