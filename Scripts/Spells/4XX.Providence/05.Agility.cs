using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells
{
	public class AgilitySpell : Spell
	{
        public static int m_SpellID { get { return 405; } } // TOCHANGE

        private static short s_Cercle = 1;
        private static short durationMax = 600;
        private static short bonusMax = 45;

		public static readonly new SpellInfo Info = new SpellInfo(
				"Agilité", "Ex Uus",
                s_Cercle,
                203,
                9031,
                GetBaseManaCost(s_Cercle),
                TimeSpan.FromSeconds(2),
                SkillName.Providence,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot
            );

		public AgilitySpell( Mobile caster, Item scroll ) : base( caster, scroll, Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckBSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

                int bonus = (int)(bonusMax * GetSpellScaling(Caster, Info.skillForCasting));
                TimeSpan duration = TimeSpan.FromSeconds(durationMax * GetSpellScaling(Caster, Info.skillForCasting));

                SpellHelper.AddStatBonus(Caster, m, StatType.Dex, bonus, duration); SpellHelper.DisableSkillCheck = true;

				Effects.SendTargetParticles(m, 0x375A, 10, 15, 5010, EffectLayer.Waist );
				m.PlaySound( 0x28E );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private AgilitySpell m_Owner;

			public InternalTarget( AgilitySpell owner ) : base( 12, false, TargetFlags.Beneficial )
			{
				m_Owner = owner;
			} 

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();

			}
		}
	}
}