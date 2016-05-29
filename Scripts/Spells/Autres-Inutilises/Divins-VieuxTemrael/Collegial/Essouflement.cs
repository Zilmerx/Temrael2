using System;
using Server.Targeting;
using Server.Network;
using Server;
using System.Collections;

namespace Server.Spells
{
    public class EssouflementSpell : ReligiousSpell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        public static readonly new SpellInfo Info = new SpellInfo(
                "Essouflement", "Fehu Ehwa",
                3,
                236,
                9031
            );

        public EssouflementSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new EssouflementTarget(this);
        }

        public static Hashtable m_Timers = new Hashtable();

        private class EssouflementTarget : Target
        {
            private EssouflementSpell m_spell;

            public EssouflementTarget(EssouflementSpell spell)
                : base(12, false, TargetFlags.Harmful)
            {
                m_spell = spell;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Mobile && m_spell.CheckHSequence((Mobile)targeted))
                {
                    Mobile targ = (Mobile)targeted;

                    SpellHelper.Turn(m_spell.Caster, targ);

                    Spell.Disturb(targ);

                    SpellHelper.CheckReflect((int)m_spell.Circle, m_spell.Caster, ref targ);

                    DateTime endtime = DateTime.Now + TimeSpan.FromSeconds(0);

                    Timer t = new EssouflementSpell.InternalTimer(targ, endtime);

                    EtouffementsSpell.m_Timers[targ] = t;
                    
                    t.Start();

                    Effects.SendTargetParticles(targ,14154, 9, 18, 5007, 1942, 0, EffectLayer.Waist);
                    targ.PlaySound(497);
                }

                m_spell.FinishSequence();
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_spell.FinishSequence();
            }
        }

        public static void StopTimer(Mobile m)
        {
            Timer t = (Timer)m_Timers[m];

            if (t != null)
            {
                t.Stop();
                m_Timers.Remove(m);

                Effects.SendTargetParticles(m,14154, 9, 18, 5007, 1942, 0, EffectLayer.CenterFeet);
                m.PlaySound(497);
            }
        }

        private class InternalTimer : Timer
        {
            private Mobile m_From;
            private DateTime ending;

            public InternalTimer(Mobile from, DateTime endtime)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(5))
            {
                m_From = from;
                ending = endtime;

                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                if (m_From.Alive && !m_From.Deleted && DateTime.Now < ending)
                {
                    int damage = Utility.Random(0, 10) + 5;
                    m_From.Mana -= damage;

                    Effects.SendTargetParticles(m_From,14154, 9, 18, 5007, 1942, 0, EffectLayer.CenterFeet);

                    if (m_From.Mana <= 20)
                        Stop();
                }
                else
                {
                    Stop();

                    Effects.SendTargetParticles(m_From,14154, 9, 18, 5007, 1942, 0, EffectLayer.CenterFeet);
                    m_From.PlaySound(497);
                }
            }
        }
    }
}
