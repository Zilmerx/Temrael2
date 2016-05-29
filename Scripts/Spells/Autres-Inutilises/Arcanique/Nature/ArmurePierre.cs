using System;
using Server.Targeting;
using Server.Network;
using Server;
using Server.Mobiles;

namespace Server.Spells
{
	public class ArmurePierreSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
                "Armure de pierre", "Rel Sanct In Ylem",
				6,
				236,
				9031,
                Reagent.Bloodmoss,
                Reagent.Ginseng,
                Reagent.SpidersSilk
			);

        public ArmurePierreSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new ArmurePierreTarget( this );
		}

		private class ArmurePierreTarget : Target
		{
			private Spell m_spell;

            public ArmurePierreTarget(Spell spell)
                : base(12, false, TargetFlags.Beneficial)
			{
				m_spell = spell;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
                if (targeted is PlayerMobile && m_spell.CheckBSequence((Mobile)targeted))
				{
                    PlayerMobile targ = (PlayerMobile)targeted;

                    SpellHelper.Turn(m_spell.Caster, targ);

                    TimeSpan duration = TimeSpan.FromSeconds(0);

                    new ArmurePierreSpell.InternalTimer(targ, duration).Start();
                    //targ.ArmurePierre = true;

                    Effects.SendTargetParticles(targ,6899, 9, 32, 5007, 2302, 0, EffectLayer.LeftFoot);
					targ.PlaySound( 508 );
				}

                m_spell.FinishSequence();
			}

			protected override void OnTargetFinish( Mobile from )
			{
                m_spell.FinishSequence();
			}
		}

        private class InternalTimer : Timer
        {
            private PlayerMobile player;

            public InternalTimer(PlayerMobile pm, TimeSpan duration)
                : base(duration)
            {
                player = pm;

                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                if (player == null || player.Deleted)
                    return;

                //player.ArmurePierre = false;
            }
        }
	}
}
