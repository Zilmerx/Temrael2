﻿using System;
using Server.Targeting;
using Server.Network;
using Server;
using Server.Mobiles;

namespace Server.Spells
{
    public class Voile : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        private static short s_Cercle = 0;

        public static readonly new SpellInfo Info = new SpellInfo(
                "Voile", "Des Lor",
                s_Cercle,
                203,
                9031,
                GetBaseManaCost(s_Cercle),
                TimeSpan.FromSeconds(1),
                SkillName.ArtMagique,
                Reagent.SulfurousAsh,
                Reagent.SpidersSilk
            );

        public Voile(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new VoileTarget(this);
        }

        private class VoileTarget : Target
        {
            private Spell m_Spell;

            public VoileTarget(Spell spell)
                : base(12, false, TargetFlags.Beneficial)
            {
                m_Spell = spell;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Mobile && m_Spell.CheckBSequence((Mobile)targeted))
                {
                    Mobile targ = (Mobile)targeted;

                    SpellHelper.Turn(m_Spell.Caster, targ);

                    if (targ.BeginAction(typeof(LightCycle)))
                    {
                        double value = Utility.Random(15, 25);

                        value = SpellHelper.AdjustValue(m_Spell.Caster, value);

                        new LightCycle.NightSightTimer(targ).Start();

                        targ.LightLevel = -100;

                        Effects.SendTargetParticles(targ,0x376A, 9, 32, 5007, EffectLayer.Waist);
                        targ.PlaySound(0x1E3);
                    }
                    else
                    {
                        from.SendMessage("{0} deja le sort de voile applique.", from == targ ? "Vous avez" : "Ils ont");
                    }
                }

                m_Spell.FinishSequence();
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Spell.FinishSequence();
            }
        }
    }
}
