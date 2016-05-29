using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class BlessScroll : SpellScroll
	{
		[Constructable]
		public BlessScroll() : this( 1 )
		{
		}

		[Constructable]
		public BlessScroll( int amount ) : base( BlessSpell.m_SpellID, 0x1F3D, amount )
		{
            Name = "Providence: Puissance";
		}

		public BlessScroll( Serial serial ) : base( serial )
		{
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
	}
}