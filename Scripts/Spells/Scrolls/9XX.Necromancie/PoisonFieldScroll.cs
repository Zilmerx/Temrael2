using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class PoisonFieldScroll : SpellScroll
	{
		[Constructable]
		public PoisonFieldScroll() : this( 1 )
		{
		}

		[Constructable]
		public PoisonFieldScroll( int amount ) : base( PoisonFieldSpell.m_SpellID, 0x1F53, amount )
		{
            Name = "N�cromancie: Mur de Poison";
		}

		public PoisonFieldScroll( Serial serial ) : base( serial )
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