using System;
using System.Collections;
using Server.Misc;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
	public class RevealSpell : Spell
	{
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        private static short s_Cercle = 0;

		public static readonly new SpellInfo Info = new SpellInfo(
				"Révélation", "Wis Quas",
                s_Cercle,
                203,
                9031,
                GetBaseManaCost(s_Cercle),
                TimeSpan.FromSeconds(1),
                SkillName.ArtMagique,
				Reagent.Bloodmoss,
				Reagent.SulfurousAsh
            );

		public RevealSpell( Mobile caster, Item scroll ) : base( caster, scroll, Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );

				SpellHelper.GetSurfaceTop( ref p );

				ArrayList targets = new ArrayList();

				Map map = Caster.Map;

				if ( map != null )
				{
                    double tile = 4;

                    tile = SpellHelper.AdjustValue(Caster, tile);

					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), (int)tile );

					foreach ( Mobile m in eable )
					{
						if ( m.Hidden && (m.AccessLevel == AccessLevel.Player || Caster.AccessLevel > m.AccessLevel) && Caster.CanBeHarmful( m, false, true ) && CheckDifficulty( Caster, m ) )
							targets.Add( m );
					}

					eable.Free();
				}

				for ( int i = 0; i < targets.Count; ++i )
				{
					Mobile m = (Mobile)targets[i];

					m.RevealingAction();

					Effects.SendTargetParticles(m, 0x375A, 9, 20, 5049, EffectLayer.Head );
					m.PlaySound( 0x1FD );
				}
			}

			FinishSequence();
		}

		// Reveal uses magery and detect hidden vs. hide and stealth 
		private static bool CheckDifficulty( Mobile from, Mobile m )
		{
			// Reveal always reveals vs. invisibility spell 
			if ( InvisibilitySpell.HasTimer( m ) )
				return true;

			double magery = from.Skills[SkillName.Hallucination].Value;
            double detectHidden = from.Skills[SkillName.ArtMagique].Value;

            double hiding = m.Skills[SkillName.Discretion].Value;
            double stealth = m.Skills[SkillName.Infiltration].Value;

            double chance;

            if (hiding + stealth > 0)
				chance = (magery + detectHidden) - (hiding + stealth);
			else
				chance = 100;

			return chance > Utility.Random( 100 );
		}

		private class InternalTarget : Target
		{
			private RevealSpell m_Owner;

			public InternalTarget( RevealSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}