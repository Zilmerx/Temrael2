using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells
{
	public class AgiliteSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
				"Agilite", "Ex Uus",
				1,
				212,
				9061,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot
            );

        public AgiliteSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
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

                SpellHelper.AddStatBonus(Caster, m, StatType.Dex, TimeSpan.FromSeconds(0));

				Effects.SendTargetParticles(m, 0x375A, 10, 15, 5010, EffectLayer.Waist );
				m.PlaySound( 0x28E );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
            private AgiliteSpell m_Owner;

            public InternalTarget(AgiliteSpell owner)
                : base(12, false, TargetFlags.Beneficial)
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