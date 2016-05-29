using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells
{
	public class TremblementsSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
				"Tremblements", "In Vas Por",
				3,
				233,
				9012,
				Reagent.Bloodmoss,
				Reagent.Ginseng,
				Reagent.SulfurousAsh
            );

        public TremblementsSpell(Mobile caster, Item scroll)
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
                    double tile = SpellHelper.AdjustValue(Caster, 1 + (int)(Caster.Skills[CastSkill].Value / 12.0), true);

                    if (tile > 12)
                        tile = 12;

					foreach ( Mobile m in Caster.GetMobilesInRange( (int)tile ) )
					{
                        if (Caster != m && SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false) && !(Caster.Party == m.Party))
							targets.Add( m );
					}
				}

                Caster.PlaySound(0x2F3);

                //double damage = GetNewAosDamage(8, 1, 6, true);

				for ( int i = 0; i < targets.Count; ++i )
				{
                    Mobile m = (Mobile)targets[i];

                    Disturb(m);

					Caster.DoHarmful( m );
					//SpellHelper.Damage( TimeSpan.Zero, m, Caster, damage, 0, 0, 0, 0, 100 );
				}
			}

			FinishSequence();
		}
	}
}