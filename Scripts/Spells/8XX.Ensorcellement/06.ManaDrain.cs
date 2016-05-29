using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
	public class ManaDrainSpell : Spell
	{
        public static int m_SpellID { get { return 806; } } // TOCHANGE

        private static short s_Cercle = 3;

		public static readonly new SpellInfo Info = new SpellInfo(
				"Vampirisme", "Ort Rel",
                s_Cercle,
                203,
                9031,
                GetBaseManaCost(s_Cercle),
                TimeSpan.FromSeconds(1),
                SkillName.Ensorcellement,
				Reagent.BlackPearl,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
            );

		public ManaDrainSpell( Mobile caster, Item scroll ) : base( caster, scroll, Info )
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

                SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

                if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

                double manaLost = 0;

                if (CheckResisted(m))
                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                else if (m.Mana >= 100)
                    manaLost = Utility.Random(25, 100);
                else
                    manaLost = Utility.Random(1, m.Mana);

                manaLost = SpellHelper.AdjustValue(Caster, manaLost);

                if (manaLost > 100)
                    manaLost = 100;

                m.Mana -= (int)manaLost;

                Effects.SendTargetParticles(m,0x374A, 10, 15, 5032, EffectLayer.Head);
                m.PlaySound(0x1F8);
            }

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private ManaDrainSpell m_Owner;

			public InternalTarget( ManaDrainSpell owner ) : base( 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}