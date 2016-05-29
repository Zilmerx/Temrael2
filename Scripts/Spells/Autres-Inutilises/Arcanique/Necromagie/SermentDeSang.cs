using System;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells
{
	public class SermentDeSangSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
				"Blood Oath", "In Jux Mani Xen",
				7,
				203,
				9031,
				Reagent.DaemonBlood,
                Reagent.Garlic
            );

        public SermentDeSangSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( Caster == m || !(m is PlayerMobile || m is BaseCreature) ) // only PlayerMobile and BaseCreature implement blood oath checking
			{
				Caster.SendLocalizedMessage( 1060508 ); // You can't curse that.
			}
			else if ( m_OathTable.Contains( Caster ) )
			{
				Caster.SendLocalizedMessage( 1061607 ); // You are already bonded in a Blood Oath.
			}
			else if ( m_OathTable.Contains( m ) )
			{
				if ( m.Player )
					Caster.SendLocalizedMessage( 1061608 ); // That player is already bonded in a Blood Oath.
				else
					Caster.SendLocalizedMessage( 1061609 ); // That creature is already bonded in a Blood Oath.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

                Disturb(m);

                SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

				/* Temporarily creates a dark pact between the caster and the target.
				 * Any damage dealt by the target to the caster is increased, but the target receives the same amount of damage.
				 * The effect lasts for ((Spirit Speak skill level - target's Resist Magic skill level) / 80 ) + 8 seconds.
				 * 
				 * NOTE: The above algorithm must be fixed point, it should be:
				 * ((ss-rm)/8)+8
				 */

				m_OathTable[Caster] = Caster;
				m_OathTable[m] = Caster;

				Caster.PlaySound( 0x175 );

				Effects.SendTargetParticles(Caster, 0x375A, 1, 17, 9919, 33, 7, EffectLayer.Waist );
				Effects.SendTargetParticles(Caster, 0x3728, 1, 13, 9502, 33, 7, (EffectLayer)255 );

				Effects.SendTargetParticles(m, 0x375A, 1, 17, 9919, 33, 7, EffectLayer.Waist );
				Effects.SendTargetParticles(m, 0x3728, 1, 13, 9502, 33, 7, (EffectLayer)255 );

                TimeSpan duration = TimeSpan.FromSeconds(0);

				new ExpireTimer( Caster, m, duration ).Start();
			}

			FinishSequence();
		}

		public static Hashtable m_OathTable = new Hashtable();

		public static Mobile GetBloodOath( Mobile m )
		{
			if ( m == null )
				return null;

			Mobile oath = (Mobile)m_OathTable[m];

			if ( oath == m )
				oath = null;

			return oath;
		}

		private class ExpireTimer : Timer
		{
			private Mobile m_Caster;
			private Mobile m_Target;
			private DateTime m_End;

			public ExpireTimer( Mobile caster, Mobile target, TimeSpan delay ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Caster = caster;
				m_Target = target;
				m_End = DateTime.Now + delay;

				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				if ( m_Caster.Deleted || m_Target.Deleted || !m_Caster.Alive || !m_Target.Alive || DateTime.Now >= m_End )
				{
					m_Caster.SendLocalizedMessage( 1061620 ); // Your Blood Oath has been broken.
					m_Target.SendLocalizedMessage( 1061620 ); // Your Blood Oath has been broken.

					m_OathTable.Remove( m_Caster );
					m_OathTable.Remove( m_Target );

					Stop();
				}
			}
		}

		private class InternalTarget : Target
		{
            private SermentDeSangSpell m_Owner;

            public InternalTarget(SermentDeSangSpell owner)
                : base(12, false, TargetFlags.Harmful)
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile) o );
				else
					from.SendLocalizedMessage( 1060508 ); // You can't curse that.
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}