using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class BladeSpiritsScroll : SpellScroll
	{
		[Constructable]
		public BladeSpiritsScroll() : this( 1 )
		{
		}

		[Constructable]
		public BladeSpiritsScroll( int amount ) : base(BladeSpiritsSpell.m_SpellID, 0x1F4D, amount )
		{
            Name = "Invocation: Esprit des Lames";
		}

		public BladeSpiritsScroll( Serial serial ) : base( serial )
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

            Name = "Invocation: Esprit des Lames";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new BladeSpiritsScroll( amount ), amount );
		}*/
	}
}