using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class SummonFireElementalScroll : SpellScroll
	{
		[Constructable]
		public SummonFireElementalScroll() : this( 1 )
		{
		}

		[Constructable]
		public SummonFireElementalScroll( int amount ) : base( FireElementalSpell.m_SpellID, 0x1F6B, amount )
		{
            Name = "Invocation: Élémental de Feu";
		}

		public SummonFireElementalScroll( Serial serial ) : base( serial )
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

            Name = "Invocation: Élémental de Feu";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new SummonFireElementalScroll( amount ), amount );
		}*/
	}
}