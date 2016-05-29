using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells
{
	public class StupefactionSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        public static readonly new SpellInfo Info = new SpellInfo(
                "Stupefaction", "Marc Desu",
                3,
                212,
                9041
            );

        public StupefactionSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
		{
        }

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				ArrayList targets = new ArrayList();

				Map map = Caster.Map;

				if ( map != null )
                {
                    double tile = SpellHelper.AdjustValue(Caster, 1 + (int)(Caster.Skills[CastSkill].Value / 33.0), true);

                    if (tile > 5)
                        tile = 5;

					foreach ( Mobile m in Caster.GetMobilesInRange( (int)tile ) )
					{
                        if (Caster != m && SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false) && !(Caster.Party == m.Party))
							targets.Add( m );
					}
				}

                Caster.PlaySound(503);
                Effects.SendTargetParticles(Caster,14154, 10, 15, 5013, 1108, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer

                TimeSpan duration = TimeSpan.FromSeconds(0);

				for ( int i = 0; i < targets.Count; ++i )
				{
                    Mobile m = (Mobile)targets[i];

                    Effects.SendTargetParticles(m,14154, 10, 15, 5013, 1108, 0, EffectLayer.CenterFeet); //ID, speed, dura, effect, hue, render, layer

                    Disturb(m);
                    m.Freeze(duration);
				}
			}

			FinishSequence();
		}
	}
}