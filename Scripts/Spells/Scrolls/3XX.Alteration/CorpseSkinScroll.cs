using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class CorpseSkinScroll : SpellScroll
	{
		[Constructable]
		public CorpseSkinScroll() : this( 1 )
		{
		}

		[Constructable]
		public CorpseSkinScroll( int amount ) : base( CorpseSkinSpell.m_SpellID, 0x2262, amount )
		{
            Name = "Alteration: Corps Mortifié";
		}

		public CorpseSkinScroll( Serial serial ) : base( serial )
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