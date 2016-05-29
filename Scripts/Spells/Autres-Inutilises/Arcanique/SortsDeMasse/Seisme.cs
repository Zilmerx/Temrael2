using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells
{
	public class SeismeSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
                "Seisme", "In Vas Por Choma An Por",
				5,
				233,
				9012,
				Reagent.Bloodmoss,
                Reagent.Bloodmoss,
				Reagent.SulfurousAsh
			);

        public SeismeSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
		{
		}
      
		public override bool DelayedDamage{ get{ return !Core.AOS; } }

        public override void OnCast()
        {
               ArrayList m_target = new ArrayList();
            if (SpellHelper.CheckTown(Caster, Caster) && CheckSequence())
            {
                   //double damage = GetNewAosDamage(15, 4, 6, true);

                   //double s_damage = damage;

                   m_target.Clear();

                    Map map = Caster.Map;

                    if (map != null)
                    {
                        foreach (Mobile m in Caster.GetMobilesInRange((int)SpellHelper.AdjustValue(Caster, 1 + Caster.Skills[SkillName.Evocation].Value / 15, true)))
                        {
                            if (Caster != m && SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false) && (!Core.AOS || Caster.InLOS(m)) && !(Caster.Party == m.Party))
                                m_target.Add(m);
                        }
                    }

                    for (int i = 0; i < m_target.Count; ++i)
                    {
                        Mobile targ = (Mobile)m_target[i];

                        if (Caster.CanSee(targ))
                        {
                            Caster.DoHarmful(targ);

                            targ.Freeze(TimeSpan.FromSeconds(0.25));

                            //AOS.Damage(targ, Caster, (int)s_damage, 100, 0, 0, 0, 0);
                        }
                    }
       

                    for (int i = 0; i < 12; i++)
                    {
                        Point3D orig = Caster.Location;

                        Point3D p = new Point3D(orig.X + Utility.Random(-5, 10), orig.Y + Utility.Random(-5, 10), orig.Z - 10);

                        Effects.SendLocationEffect(p, Caster.Map, 7020 + Utility.Random(0, 4), 50);
                        Effects.PlaySound(p, Caster.Map, 0x2F3);
                        Caster.Freeze(TimeSpan.FromSeconds(0.5));
                    }
                }

            FinishSequence();
    }
        }
            }