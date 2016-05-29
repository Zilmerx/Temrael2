using System;
using Server;

namespace Server.Items
{
    public class Tourmaline : BaseGem
	{
        public override int m_Couleur
        {
            get { return 2125; }
        }
        
        public override double m_SkillReq
        {
            get { return 45; }
        }

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public Tourmaline() : this( 1 )
		{
		}

		[Constructable]
		public Tourmaline( int amount ) : base( 0xF2D )
		{
            Name = "Tourmaline";
			Stackable = true;
			Amount = amount;
		}

		public Tourmaline( Serial serial ) : base( serial )
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