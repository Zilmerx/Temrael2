using System;
using Server;

namespace Server.Items
{
    public class Emerald : BaseGem
	{
        public override int m_Couleur
        {
            get { return 2389; }
        }

        public override double m_SkillReq
        {
            get { return 65; }
        }

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public Emerald() : this( 1 )
		{
		}

		[Constructable]
		public Emerald( int amount ) : base( 0xF10 )
		{
            Name = "Emeraude";
			Stackable = true;
			Amount = amount;
		}

		public Emerald( Serial serial ) : base( serial )
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