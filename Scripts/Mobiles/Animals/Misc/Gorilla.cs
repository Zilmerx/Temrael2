using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a gorilla corpse" )]
	public class Gorilla : BaseCreature
	{
		[Constructable]
		public Gorilla() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a gorilla";
			Body = 0x1D;
			BaseSoundID = 0x9E;

			SetStr( 53, 95 );
			SetDex( 36, 55 );
			SetInt( 36, 60 );

			SetHits( 38, 51 );
			SetMana( 0 );

			SetDamage( 4, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );

			SetSkill( SkillName.Concentration, 45.1, 60.0 );
			SetSkill( SkillName.Tactiques, 43.3, 58.0 );
			SetSkill( SkillName.Anatomie, 43.3, 58.0 );

			VirtualArmor = 20;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 40.0;
		}

        public override double AttackSpeed { get { return 2.5; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 3; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public Gorilla(Serial serial) : base(serial)
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