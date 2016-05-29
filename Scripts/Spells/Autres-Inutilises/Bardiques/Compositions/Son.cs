﻿using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Engines.PartySystem;

namespace Server.Spells
{
    public class SonSpell : BardeSpell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        public static Hashtable m_SonTable = new Hashtable();
        public static Hashtable m_Timers = new Hashtable();

        //Identification des constantes utilisées (pour modification aisée)
        private const double bonus_donne = 0.0;
        private const int portee = 8;

        public static readonly new SpellInfo Info = new SpellInfo(
                "Son", "",
                1,
                215,
                9041
            );

        public SonSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
        {
        }

        public override void OnCast()
        {
            if (CheckSequence())
            {
                TimeSpan duration = TimeSpan.FromSeconds(0);
                double amount = 1;

                //Calcul du bonus donné par le sort (niveau * bonus_donne)
                if (Caster is PlayerMobile)
                {
                    //amount += (double)(((PlayerMobile)Caster).GetAptitudeValue(Aptitude.Composition) * bonus_donne);
                }

                DateTime endtime = DateTime.Now + duration;

                ArrayList m_target = new ArrayList();

                Map map = Caster.Map;

                Party party = Engines.PartySystem.Party.Get(Caster);

                //Définition des cibles du sort
                m_target.Add(Caster);

                if (map != null)
                {
                    foreach (Mobile m in Caster.GetMobilesInRange(portee))
                    {
                        if (SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false))
                        {
                            if (party != null && party.Count > 0)
                            {
                                for (int k = 0; k < party.Members.Count; ++k)
                                {
                                    PartyMemberInfo pmi = (PartyMemberInfo)party.Members[k];
                                    Mobile member = pmi.Mobile;
                                    if (member.Serial == m.Serial)
                                        m_target.Add(m);
                                }
                            }
                        }
                    }
                }       

                for (int i = 0; i < m_target.Count; ++i)
                {
                    Mobile targ = (Mobile)m_target[i];

                    StopTimer(targ);

                    m_SonTable[targ] = amount;

                    Timer t = new SonTimer(targ, DateTime.Now + duration);
                    m_Timers[targ] = t;
                    t.Start();

                    Effects.SendTargetParticles(targ,14170, 10, 20, 5013, 2144, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                    targ.PlaySound(526);
                }
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
                m_SonTable.Remove(m);

                Effects.SendTargetParticles(m,14170, 10, 20, 5013, 2144, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(526);
            }
        }

        public class SonTimer : Timer
        {
            private Mobile m_target;
            private DateTime endtime;

            public SonTimer(Mobile target, DateTime end)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(2))
            {
                m_target = target;
                endtime = end;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if ((DateTime.Now >= endtime && SonSpell.m_SonTable.Contains(m_target)) || m_target == null || m_target.Deleted || !m_target.Alive)
                {
                    SonSpell.m_SonTable.Remove(m_target);
                    SonSpell.m_Timers.Remove(m_target);

                    Effects.SendTargetParticles(m_target,14170, 10, 20, 5013, 2144, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                    m_target.PlaySound(526);

                    Stop();
                }
            }
        }
    }
}