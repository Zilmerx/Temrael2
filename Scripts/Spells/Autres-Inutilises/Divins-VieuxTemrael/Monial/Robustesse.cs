using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
    public class RobustesseSpell : ReligiousSpell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        public static Hashtable m_RobustesseTable = new Hashtable();
        public static Hashtable m_Timers = new Hashtable();

        public static readonly new SpellInfo Info = new SpellInfo(
                "Robutesse", "Lagu Kano Toki",
                2,
                212,
                9041
            );

        public RobustesseSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public override bool DelayedDamage { get { return false; } }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, m);

                StopTimer(m);

                TimeSpan duration = TimeSpan.FromSeconds(0);

                m_RobustesseTable[m] = (int)(10 + ((Caster.Skills[CastSkill].Value + Caster.Skills[DamageSkill].Value) / 4)); //10 à 110

                Timer t = new RobustesseTimer(m, DateTime.Now + duration);
                m_Timers[m] = t;
                t.Start();

                m.CheckStatTimers();

                m.Hits -= 1;
                m.Stam -= 1;

                Effects.SendTargetParticles(m,14186, 10, 20, 5013, 0, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(494);
            }

            FinishSequence();
        }

        public void StopTimer(Mobile m)
        {
            Timer t = (Timer)m_Timers[m];

            if (t != null)
            {
                t.Stop();
                m_Timers.Remove(m);
                m_RobustesseTable.Remove(m);

                m.CheckStatTimers();
 
                m.Hits -= 1;
                m.Stam -= 1;

                Effects.SendTargetParticles(m,14186, 10, 20, 5013, 0, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(494);
            }
        }

        public class RobustesseTimer : Timer
        {
            private Mobile m_target;
            private DateTime endtime;

            public RobustesseTimer(Mobile target, DateTime end)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(2))
            {
                m_target = target;
                endtime = end;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if ((DateTime.Now >= endtime && RobustesseSpell.m_RobustesseTable.Contains(m_target)) || m_target == null || m_target.Deleted || !m_target.Alive)
                {
                    RobustesseSpell.m_RobustesseTable.Remove(m_target);
                    RobustesseSpell.m_Timers.Remove(m_target);

                    m_target.CheckStatTimers();

                    m_target.Hits -= 1;
                    m_target.Stam -= 1;

                    Effects.SendTargetParticles(m_target,14186, 10, 20, 5013, 0, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                    m_target.PlaySound(494);

                    Stop();
                }
            }
        }

        private class InternalTarget : Target
        {
            private RobustesseSpell m_Owner;

            public InternalTarget(RobustesseSpell owner)
                : base(12, false, TargetFlags.Beneficial)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}