﻿using System;
using System.Collections;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells
{
    public class ProtectionCelesteMiracle : ReligiousSpell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        public static readonly new SpellInfo Info = new SpellInfo(
                "Protection Celeste", "",
                8,
                17,
                9031
            );

        public ProtectionCelesteMiracle(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!(m is BaseCreature || m is PlayerMobile))
            {
                Caster.SendLocalizedMessage(1060508); // You can't curse that.
            }
            else if (CheckHSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                Effects.SendTargetParticles(m,14170, 10, 15, 5013, 0, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                Effects.SendTargetParticles(m,14201, 10, 15, 5013, 0, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(514);

                StatMod[] mods = (StatMod[])m_Table[m];

                if (mods == null)
                {
                    mods = new StatMod[]
                    {
                        new StatMod( StatType.Str, "Protection Celeste", (int)(Caster.Skills[CastSkill].Value / 5), TimeSpan.FromSeconds((int)(Caster.Skills[CastSkill].Value / 2)) ),
                        new StatMod( StatType.Dex, "Protection Celeste", (int)(Caster.Skills[CastSkill].Value / 5), TimeSpan.FromSeconds((int)(Caster.Skills[CastSkill].Value / 2)) )
                    };

                    for (int i = 0; i < mods.Length; ++i)
                        m.AddStatMod(mods[i]);

                    m_Table[m] = mods;
                }
            }

            FinishSequence();
        }

        private static Hashtable m_Table = new Hashtable();

        private class InternalTarget : Target
        {
            private ProtectionCelesteMiracle m_Owner;

            public InternalTarget(ProtectionCelesteMiracle owner)
                : base(12, false, TargetFlags.Beneficial)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    m_Owner.Target((Mobile)o);
                else
                    from.SendLocalizedMessage(1060508); // You can't curse that.
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}