﻿using System;
using Server.Targeting;
using Server.Network;
using Server;
using Server.Mobiles;
using Server.Misc;

namespace Server.Spells
{
    public class Tempete : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        private static short s_Cercle = 0;

        public static readonly new SpellInfo Info = new SpellInfo(
                "Tempete", "In Ort",
                s_Cercle,
                203,
                9031,
                GetBaseManaCost(s_Cercle),
                TimeSpan.FromSeconds(1),
                SkillName.ArtMagique,
                Reagent.SulfurousAsh
            );

        public Tempete(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new TempeteTarget(this);
        }

        private class TempeteTarget : Target
        {
            private Spell m_Spell;

            public TempeteTarget(Spell spell)
                : base(12, true, TargetFlags.Beneficial)
            {
                m_Spell = spell;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is LandTarget)
                {
                    Server.Misc.Weather weather = Server.Misc.Weather.GetWeather(from.Location);

                    if (!(weather.Cloud == DensityOfCloud.Caverne))
                    {
                        LandTarget targ = (LandTarget)targeted;

                        SpellHelper.Turn(m_Spell.Caster, targ);

                        double value = Utility.RandomMinMax(3, 7);

                        //value = SpellHelper.AdjustValue(m_Spell.Caster, value, NAptitude.Spiritisme);

                        Server.Misc.Weather.RemoveWeather(from.Location);

                        Server.Misc.Weather.AddWeather(weather.Temperature, (DensityOfCloud)value, weather.Wind, false, new Rectangle2D(new Point2D(0, 0), new Point2D(6145, 4097)));

                        from.SendMessage(String.Concat("Le ciel est désormais ", ((DensityOfCloud)value).ToString()));
                    }
                    else
                    {
                        from.SendMessage("Vous ne pouvez pas faire ça sous terre !");
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
