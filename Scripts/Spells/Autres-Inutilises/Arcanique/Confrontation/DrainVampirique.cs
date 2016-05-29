using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells
{
	public class DrainVampiriqueSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
                "Drain Vampirique", "Ort Sanct",
				6,
				221,
				9032,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
            );

        public DrainVampiriqueSpell(Mobile caster, Item scroll)
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
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

                Disturb(m);

                SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

				int toDrain = 0;

				if ( CheckResisted( m ) )
					m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
				else
					toDrain = (int)(m.Mana * (0.50 + Utility.RandomDouble()));

				if ( toDrain > Caster.Mana )
					toDrain = Caster.Mana;

				m.Mana -= toDrain;
				Caster.Mana += toDrain;

				Effects.SendTargetParticles(m, 0x374A, 10, 15, 5054, EffectLayer.Head );
				m.PlaySound( 0x1F9 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
            private DrainVampiriqueSpell m_Owner;

            public InternalTarget(DrainVampiriqueSpell owner)
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