using System;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells
{
	public class PourritureDEspritSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
				"Pourriture D'esprit", "Wis An Ben",
				1,
				203,
				9031,
				Reagent.BlackPearl,
				Reagent.SulfurousAsh,
				Reagent.Ginseng
            );

        public PourritureDEspritSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( HasMindRotScalar( m ) )
			{
				Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

                SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

				/* Attempts to place a curse on the Target that increases the mana cost of any spells they cast,
				 * for a duration based off a comparison between the Caster's Spirit Speak skill and the Target's Resisting Spells skill.
				 * The effect lasts for ((Spirit Speak skill level - target's Resist Magic skill level) / 50 ) + 20 seconds.
				 */

				m.PlaySound( 0x1FB );
				m.PlaySound( 0x258 );
				Effects.SendTargetParticles(m, 0x373A, 1, 17, 9903, 15, 4, EffectLayer.Head );

                TimeSpan duration = TimeSpan.FromSeconds(0);

                double scalar = SpellHelper.AdjustValue(Caster, 1 + (Caster.Skills[SkillName.ArtMagique].Value) / 1000);
                SetMindRotScalar(Caster, m, scalar, duration);
			}

			FinishSequence();
		}

		private static Hashtable m_Table = new Hashtable();

		public static void ClearMindRotScalar( Mobile m )
		{
			m_Table.Remove( m );
		}

		public static bool HasMindRotScalar( Mobile m )
		{
			return m_Table.Contains( m );
		}

		public static double GetMindRotScalar( Mobile m )
		{
			object obj = m_Table[m];

			if ( obj == null )
				return 1;

			return (double)obj;
		}

		public static void SetMindRotScalar( Mobile caster, Mobile target, double scalar, TimeSpan duration )
		{
			m_Table[target] = scalar;
			new ExpireTimer( caster, target, duration ).Start();
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
				if ( m_Target.Deleted || !m_Target.Alive || DateTime.Now >= m_End )
				{
					m_Target.SendLocalizedMessage( 1060872 ); // Your mind feels normal again.
					ClearMindRotScalar( m_Target );
					Stop();
				}
			}
		}

		private class InternalTarget : Target
		{
            private PourritureDEspritSpell m_Owner;

            public InternalTarget(PourritureDEspritSpell owner)
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