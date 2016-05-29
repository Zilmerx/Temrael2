using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
    public class IntrinsequeSpell : ReligiousSpell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        public static ArrayList m_IntrinsequeTable = new ArrayList();
        public static Hashtable m_Timers = new Hashtable();

        public static readonly new SpellInfo Info = new SpellInfo(
                "Intrinseque", "Desu Mann",
                3,
                212,
                9041
            );

        public IntrinsequeSpell(Mobile caster, Item scroll)
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

                m_IntrinsequeTable.Add(m);

                Timer t = new IntrinsequeTimer(m, DateTime.Now + duration);
                m_Timers[m] = t;
                t.Start();

                Effects.SendTargetParticles(m,14276, 10, 15, 5013, 211, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(493);
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
                m_IntrinsequeTable.Remove(m);

                Effects.SendTargetParticles(m,14276, 10, 15, 5013, 211, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(493);
            }
        }

        public class IntrinsequeTimer : Timer
        {
            private Mobile m_target;
            private DateTime endtime;

            public IntrinsequeTimer(Mobile target, DateTime end)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(2))
            {
                m_target = target;
                endtime = end;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if ((DateTime.Now >= endtime && IntrinsequeSpell.m_IntrinsequeTable.Contains(m_target)) || m_target == null || m_target.Deleted || !m_target.Alive)
                {
                    IntrinsequeSpell.m_IntrinsequeTable.Remove(m_target);
                    IntrinsequeSpell.m_Timers.Remove(m_target);

                    Effects.SendTargetParticles(m_target,14276, 10, 15, 5013, 211, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                    m_target.PlaySound(493);

                    Stop();
                }
            }
        }

        private class InternalTarget : Target
        {
            private IntrinsequeSpell m_Owner;

            public InternalTarget(IntrinsequeSpell owner)
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