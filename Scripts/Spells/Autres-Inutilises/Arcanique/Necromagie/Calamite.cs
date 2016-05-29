using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells
{
	public class CalamiteSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
				"Calamite", "An Sanct Gra Char",
				2,
				203,
				9031,
				Reagent.PigIron
            );

        public override bool ClearHandsOnCast { get { return false; } }

        public CalamiteSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
		{
		}

		public override void OnCast()
		{
			BaseWeapon weapon = Caster.Weapon as BaseWeapon;

            if (weapon == null || weapon is Fists/* || weapon is CreatureFists*/)
			{
				Caster.SendLocalizedMessage( 501078 ); // You must be holding a weapon.
			}
			else if ( CheckSequence() )
			{
				/* Temporarily imbues a weapon with a life draining effect.
				 * Half the damage that the weapon inflicts is added to the necromancer's health.
				 * The effects lasts for (Spirit Speak skill level / 75) + 1 seconds.
				 * 
				 * NOTE: Above algorithm is fixed point, should be :
				 * (Spirit Speak skill level / 7.5) + 1
				 * 
				 * TODO: What happens if you curse a weapon then give it to someone else? Should they get the drain effect?
				 */

				Caster.PlaySound( 0x387 );
				Effects.SendTargetParticles(Caster, 0x3779, 1, 15, 9905, 32, 2, EffectLayer.Head );
				Effects.SendTargetParticles(Caster, 0x37B9, 1, 14, 9502, 32, 5, (EffectLayer)255 );
				new SoundEffectTimer( Caster ).Start();

                TimeSpan duration = TimeSpan.FromSeconds(0);

				Timer t = (Timer)m_Table[weapon];

				if ( t != null )
					t.Stop();

				weapon.Cursed = true;

				m_Table[weapon] = t = new ExpireTimer( weapon, duration );

				t.Start();
			}

			FinishSequence();
		}

		private static Hashtable m_Table = new Hashtable();

		private class ExpireTimer : Timer
		{
			private BaseWeapon m_Weapon;

			public ExpireTimer( BaseWeapon weapon, TimeSpan delay ) : base( delay )
			{
				m_Weapon = weapon;
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				m_Weapon.Cursed = false;
				Effects.PlaySound( m_Weapon.GetWorldLocation(), m_Weapon.Map, 0xFA );
				m_Table.Remove( this );
			}
		}

		private class SoundEffectTimer : Timer
		{
			private Mobile m_Mobile;

			public SoundEffectTimer( Mobile m ) : base( TimeSpan.FromSeconds( 0.75 ) )
			{
				m_Mobile = m;
				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				m_Mobile.PlaySound( 0xFA );
			}
		}
	}
}