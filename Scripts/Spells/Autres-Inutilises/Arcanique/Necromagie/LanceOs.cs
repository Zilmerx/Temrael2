using System;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Spells;
using Server.Mobiles;
using System.Collections;
using Server.Engines.Craft;

namespace Server.Spells
{
    public class LanceOsSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        public static readonly new SpellInfo Info = new SpellInfo(
                "Lance d'Os", "Evo Ylem Corp",
                6,
                212,
                9041,
                Reagent.MandrakeRoot,
                Reagent.Garlic,
                Reagent.PigIron
            );

        public LanceOsSpell(Mobile caster, Item scroll)
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
            else if (CheckHSequence(m))
            {
                Container ourPack = Caster.Backpack;

                if (ourPack != null)
                {
                    if (ourPack.GetAmount(typeof(Bone)) > 0)
                    {
                        ourPack.ConsumeTotal(typeof(Bone), 1);

                        SpellHelper.Turn(Caster, m);

                        Disturb(m);

                        SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

                        //double damage = GetNewAosDamage(15, 1, 6, true);

                        //SpellHelper.Damage(TimeSpan.Zero, m, Caster, damage, 0, 0, 0, 0, 100);

                        Effects.SendMovingParticles(Caster,m, 0x36D4, 7, 0, false, false, 2056, 0, 3006, 4006, 0, 0);
                        Caster.PlaySound(903);
                    }
                    else
                    {
                        Caster.SendMessage(0, "Vous avez besoin d'os pour lancer ce sort.");
                    }
                }
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private LanceOsSpell m_Owner;

            public InternalTarget(LanceOsSpell owner)
                : base(12, false, TargetFlags.Harmful)
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