using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "Cauchemar" )]
	public class Cauchemar : BaseMount
	{
		[Constructable]
		public Cauchemar() : this( "Cauchemar" )
		{
		}

		[Constructable]
		public Cauchemar( string name ) : base( name, 116, 0x3EA7, AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 0xA8;

			SetStr( 496, 525 );
			SetDex( 86, 105 );
			SetInt( 86, 125 );

			SetHits( 298, 315 );

			SetDamage( 16, 22 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Magical, 20 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Magical, 20, 30 );

			//SetSkill( SkillName.EvalInt, 10.4, 50.0 );
			SetSkill( SkillName.ArtMagique, 10.4, 50.0 );
			SetSkill( SkillName.Concentration, 85.3, 100.0 );
			SetSkill( SkillName.Tactiques, 97.6, 100.0 );
			SetSkill( SkillName.Anatomie, 80.5, 92.5 );

			VirtualArmor = 60;

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 95.0;

			/*switch ( Utility.Random( 3 ) )
			{
				case 0:
				{
					BodyValue = 116;
					ItemID = 16039;
					break;
				}
				case 1:
				{
					BodyValue = 178;
					ItemID = 16041;
					break;
				}
				case 2:
				{
					BodyValue = 179;
					ItemID = 16055;
					break;
				}
			}*/

			PackItem( new SulfurousAsh( Utility.RandomMinMax( 3, 5 ) ) );
		}

		public override int GetAngerSound()
		{
			if ( !Controlled )
				return 0x16A;

			return base.GetAngerSound();
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Demoniaque; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }

        public Cauchemar(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( BaseSoundID == 0x16A )
				BaseSoundID = 0xA8;
		}
	}
}