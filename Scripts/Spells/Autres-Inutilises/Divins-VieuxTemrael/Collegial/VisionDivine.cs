using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
	public class VisionDivineSpell : ReligiousSpell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
                "Vision Divine", "Perth Kena",
				1,
				212,
				9041
            );

        public VisionDivineSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return false; } }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (Caster.AccessLevel < m.AccessLevel)
            {
                Caster.SendMessage("Vous ne pouvez pas cibler cette personne !");
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

//                Caster.SendMessage(334, "Fatigue de " + m.Name);
//
//                if (m is PlayerMobile)
//                    Caster.SendMessage(334, ((PlayerMobile)m).Fatigue.ToString());
//                else
//                    Caster.SendMessage(m.Stam.ToString() + " / " + m.StamMax.ToString());
                  
                //Caster.SendMessage("Points de vie : " + m.Hits + " / " + m.HitsMax);
                //Caster.SendMessage("Mana : " + m.Mana + " / " + m.ManaMax);
                //Caster.SendMessage("Stamina : " + m.Stam + " / " + m.StamMax);

                //Caster.SendMessage("Force : " + m.Str);
                //Caster.SendMessage("Intelligence : " + m.Int);
                //Caster.SendMessage("Dextérité : " + m.Dex);

                Effects.SendTargetParticles(m,14265, 10, 15, 5013, 2042, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(509);
            }

            FinishSequence();
        }

		private class InternalTarget : Target
		{
            private VisionDivineSpell m_Owner;

            public InternalTarget(VisionDivineSpell owner)
                : base(12, false, TargetFlags.None)
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}