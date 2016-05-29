﻿using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
    public class ReposCelesteMiracle : ReligiousSpell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        public static readonly new SpellInfo Info = new SpellInfo(
                "Repos Celeste", "",
                2,
                17,
                9041
            );

        public ReposCelesteMiracle(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public override bool DelayedDamage { get { return false; } }

        public void Target(PlayerMobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, m);

//                m.Fatigue -= 1 + (int)((Caster.Skills[CastSkill].Value + Caster.Skills[DamageSkill].Value)); // 1 à 201
//
//                if (m.Fatigue < 0)
//                    m.Fatigue = 0;

                Effects.SendTargetParticles(m,14170, 10, 20, 5013, 2010, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(586);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private ReposCelesteMiracle m_Owner;

            public InternalTarget(ReposCelesteMiracle owner)
                : base(12, false, TargetFlags.Beneficial)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is PlayerMobile)
                {
                    m_Owner.Target((PlayerMobile)o);
                }
                else
                    from.SendMessage("Vous ne pouvez cibler que les joueurs !");
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}