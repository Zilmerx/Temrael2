using System;
using Server;

namespace Server.Items
{
    public class Amber : BaseGem
	{
        public override int m_Couleur
        {
            get { return 2171; }
        }

        public override double m_SkillReq
        {
            get { return 35; }
        }

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public Amber() : this( 1 )
		{
		}

        public override int GoldValue { get { return 6; } }

		[Constructable]
		public Amber( int amount ) : base( 0xF25 )
		{
            Name = "Ambre";
			Stackable = true;
			Amount = amount;
		}

		public Amber( Serial serial ) : base( serial )
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
		}
	}
}