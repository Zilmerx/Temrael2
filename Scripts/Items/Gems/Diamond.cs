using System;
using Server;

namespace Server.Items
{
    public class Diamond : BaseGem
	{
        public override int m_Couleur
        {
            get { return 2378; }
        }

        public override double m_SkillReq
        {
            get { return 70; }
        }

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public Diamond() : this( 1 )
		{
		}

		[Constructable]
		public Diamond( int amount ) : base( 0xF26 )
		{
            Name = "Diamant";
			Stackable = true;
			Amount = amount;
		}

		public Diamond( Serial serial ) : base( serial )
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