using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells
{
	public class ElementaireTerreSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
				"Elementaire de terre", "Kal Vas Xen Ylem Choma",
				3,
				269,
				9020,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
                Reagent.BlackPearl
			);

        public ElementaireTerreSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
		{
		}

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			if ( (Caster.Followers + 2) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
                TimeSpan duration = TimeSpan.FromSeconds(0);

				SpellHelper.Summon( new SummonedEarthElemental(), Caster, 0x217, duration, false, false );
			}

			FinishSequence();
		}
	}
}