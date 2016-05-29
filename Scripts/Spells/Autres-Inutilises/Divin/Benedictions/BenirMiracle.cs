﻿using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells
{
    public class BenirMiracle : ReligiousSpell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        public static readonly new SpellInfo Info = new SpellInfo(
                "Bénir", "",
                8,
                17,
                9050
            );

        public BenirMiracle(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckBSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.AddStatBonus(Caster, m, StatType.Str); SpellHelper.DisableSkillCheck = true;
                SpellHelper.AddStatBonus(Caster, m, StatType.Dex);
                SpellHelper.AddStatBonus(Caster, m, StatType.Int); SpellHelper.DisableSkillCheck = false;

                Effects.SendTargetParticles(m,0x373A, 10, 15, 5018, EffectLayer.Waist);
                m.PlaySound(0x1EA);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private BenirMiracle m_Owner;

            public InternalTarget(BenirMiracle owner)
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