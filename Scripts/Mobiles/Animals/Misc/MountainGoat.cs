using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a mountain goat corpse" )]
	public class MountainGoat : BaseCreature
	{
		[Constructable]
		public MountainGoat() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a mountain goat";
			Body = 88;
			BaseSoundID = 0x99;

			SetStr( 22, 64 );
			SetDex( 56, 75 );
			SetInt( 16, 30 );

			SetHits( 20, 33 );
			SetMana( 0 );

			SetDamage( 3, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10, 20 );
			SetResistance( ResistanceType.Magical, 10, 15 );

			SetSkill( SkillName.Concentration, 25.1, 30.0 );
			SetSkill( SkillName.Tactiques, 29.3, 44.0 );
			SetSkill( SkillName.Anatomie, 29.3, 44.0 );

			VirtualArmor = 10;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 15.0;
		}

        public override double AttackSpeed { get { return 2.5; } }
		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 2; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay | FoodType.FruitsAndVegies; } }

		public MountainGoat(Serial serial) : base(serial)
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