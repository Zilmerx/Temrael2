using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class SummonDaemonScroll : SpellScroll
	{
		[Constructable]
		public SummonDaemonScroll() : this( 1 )
		{
		}

		[Constructable]
		public SummonDaemonScroll( int amount ) : base( SummonCreatureSpell.m_SpellID, 0x1F69, amount )
		{
            Name = "N�cromancie: Conjuration";
		}

		public SummonDaemonScroll( Serial serial ) : base( serial )
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