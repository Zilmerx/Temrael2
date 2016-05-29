using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class PoisonScroll : SpellScroll
	{
		[Constructable]
		public PoisonScroll() : this( 1 )
		{
		}

		[Constructable]
		public PoisonScroll( int amount ) : base( PoisonSpell.m_SpellID, 0x1F40, amount )
		{
            Name = "N�cromancie: Poison";
		}

		public PoisonScroll( Serial serial ) : base( serial )
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