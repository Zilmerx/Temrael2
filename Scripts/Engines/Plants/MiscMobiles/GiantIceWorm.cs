using System;
using Server;

namespace Server.Mobiles
{
	[CorpseName( "a giant ice worm corpse" )]
	public class GiantIceWorm : BaseCreature
	{
		public override bool SubdueBeforeTame { get { return true; } }

		[Constructable]
		public GiantIceWorm() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 89;
			Name = "a giant ice worm";
			BaseSoundID = 0xDC;

			SetStr( 216, 245 );
			SetDex( 76, 100 );
			SetInt( 66, 85 );

			SetHits( 130, 147 );

			SetDamage( 7, 17 );

			SetDamageType( ResistanceType.Physical, 10 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Magical, 10, 20 );

			SetSkill( SkillName.Empoisonnement, 75.1, 95.0 );
			SetSkill( SkillName.Concentration, 45.1, 60.0 );
			SetSkill( SkillName.Tactiques, 75.1, 80.0 );
			SetSkill( SkillName.Anatomie, 60.1, 80.0 );

			VirtualArmor = 40;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 71.1;
		}

		public override Poison PoisonImmune { get { return Poison.Greater; } }

		public override Poison HitPoison { get { return Poison.Greater; } }

		public override FoodType FavoriteFood { get { return FoodType.Meat; } }

		public GiantIceWorm( Serial serial ) : base ( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}