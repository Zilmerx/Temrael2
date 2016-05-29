using System;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using Server.Engines.Combat;

namespace Server.Spells
{
	public class BloodOathSpell : NecromancerSpell
	{
        public static int m_SpellID { get { return 303; } } // TOCHANGE

        private static short s_Cercle = 3;

		public static readonly new SpellInfo Info = new SpellInfo(
				"Serment", "In Jux Mani Xen",
                s_Cercle,
                203,
                9031,
                GetBaseManaCost(s_Cercle),
                TimeSpan.FromSeconds(3),
                SkillName.Alteration,
				Reagent.DaemonBlood
            );

		public BloodOathSpell( Mobile caster, Item scroll ) : base( caster, scroll, Info )
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

				/* Temporarily creates a dark pact between the caster and the target.
				 * Any damage dealt by the target to the caster is increased, but the target receives the same amount of damage.
				 * The effect lasts for ((Spirit Speak skill level - target's Resist Magic skill level) / 80 ) + 8 seconds.
				 * 
				 * NOTE: The above algorithm must be fixed point, it should be:
				 * ((ss-rm)/8)+8
				 */

				m_OathTable[m] = Caster;
                m_OathTableScaling[Caster] = Spell.GetSpellScaling(Caster, Info.skillForCasting);

				Caster.PlaySound( 0x175 );

				Effects.SendTargetParticles(Caster, 0x375A, 1, 17, 9919, 33, 7, EffectLayer.Waist );
				Effects.SendTargetParticles(Caster, 0x3728, 1, 13, 9502, 33, 7, (EffectLayer)255 );

				Effects.SendTargetParticles(m, 0x375A, 1, 17, 9919, 33, 7, EffectLayer.Waist );
				Effects.SendTargetParticles(m, 0x3728, 1, 13, 9502, 33, 7, (EffectLayer)255 );

                double duration = 60;

                duration *= Spell.GetSpellScaling(Caster, Info.skillForCasting);

				new ExpireTimer( Caster, m, TimeSpan.FromSeconds(duration) ).Start();
			}

			FinishSequence();
		}

        public static void GetOnHitEffect(Mobile atk, Mobile def, ref double damage)
        {
            if (GetBloodOath(atk) == def)
            {
                damage = damage * 1.25;    // Augmente de 25% tous les d�g�ts.
                Damage.instance.AppliquerDegatsMagiques(atk, GetBloodOathScaling(def)*damage); // Retourne une partie des degats.
            }
        }

		private static Hashtable m_OathTable = new Hashtable();
		private static Hashtable m_OathTableScaling = new Hashtable();

		public static Mobile GetBloodOath( Mobile m )
		{
			if ( m == null )
				return null;

			Mobile oath = (Mobile)m_OathTable[m];

			if ( oath == m )
				oath = null;

			return oath;
		}

		public static double GetBloodOathScaling( Mobile m )
		{
			if ( m == null )
				return 0;

			double oath = (double)m_OathTableScaling[m];

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

                    m_OathTable.Remove(m_Target);
                    m_OathTableScaling.Remove(m_Caster);

					Stop();
				}
			}
		}

		private class InternalTarget : Target
		{
			private BloodOathSpell m_Owner;

			public InternalTarget( BloodOathSpell owner ) : base( 12, false, TargetFlags.Harmful )
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