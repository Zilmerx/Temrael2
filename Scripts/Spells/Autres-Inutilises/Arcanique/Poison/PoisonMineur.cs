using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
	public class PoisonMineurSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
				"Poison mineur", "Nox",
				1,
				203,
				9051,
				Reagent.Nightshade
            );

        public PoisonMineurSpell(Mobile caster, Item scroll)
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
            else if (CheckHSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                Disturb(m);

                SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

                m.ApplyPoison(Caster, Poison.GetPoison(0));

                Effects.SendTargetParticles(m,0x374A, 10, 15, 5021, EffectLayer.Waist);
                m.PlaySound(0x474);
            }

			FinishSequence();
		}

		private class InternalTarget : Target
		{
            private PoisonMineurSpell m_Owner;

            public InternalTarget(PoisonMineurSpell owner)
                : base(12, false, TargetFlags.Harmful)
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