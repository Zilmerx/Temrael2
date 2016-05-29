using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Items;

namespace Server.Spells
{
	public class TalismanSpell : ReligiousSpell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
                "Talisman", "Gebo Mann Hiro",
				2,
				212,
				9041
            );

        public TalismanSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return false; } }

        public void Target(IPoint3D p)
        {
            if (!Caster.CanSee(p))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, p);

				SpellHelper.GetSurfaceTop( ref p );

                int ItemID = 7961;
                string name = "Talisman";
                int hue = 2042;

                TotemType type = TotemType.Talisman;
                DateTime delete = DateTime.Now + TimeSpan.FromSeconds(0);
                int range = 1 + (int)(Caster.Skills[CastSkill].Value / 10);
                double bonus = 0.05 + (double)((Caster.Skills[CastSkill].Value + Caster.Skills[DamageSkill].Value) / 800);//5 à 30%

                int effectid = 14201;
                int effectspeed = 10;
                int effectduration = 20;
                EffectLayer layer = EffectLayer.Waist;
                int soundid = 514;

                Totem totem = new Totem(ItemID, name, hue, range, type, delete, Caster, bonus, effectid, effectspeed, effectduration, layer, soundid);

                if (totem != null)
                {
                    totem.MoveToWorld(new Point3D(p), Caster.Map);
                    Effects.SendTargetParticles(totem,effectid, effectspeed, effectduration, 5005, hue, 0, layer);
                    totem.PlaySound(soundid);
                }
            }

            FinishSequence();
        }

		private class InternalTarget : Target
		{
            private TalismanSpell m_Owner;

            public InternalTarget(TalismanSpell owner)
                : base(12, true, TargetFlags.None)
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
				{
                    m_Owner.Target((IPoint3D)o);
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}