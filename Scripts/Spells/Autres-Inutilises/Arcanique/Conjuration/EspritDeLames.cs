using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells
{
	public class EspritDeLamesSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
				"Esprit de lames", "In Jux Hur Ylem", 
				5,
				266,
				9040,
				Reagent.BlackPearl,
				Reagent.MandrakeRoot,
				Reagent.Nightshade
            );

        public EspritDeLamesSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
		{
		}

		public override TimeSpan GetCastDelay()
		{
			return base.GetCastDelay() + TimeSpan.FromSeconds( 4.0 );
		}

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			if ( (Caster.Followers + 2) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			Map map = Caster.Map;

			SpellHelper.GetSurfaceTop( ref p );

			if ( map == null || !map.CanSpawnMobile( p.X, p.Y, p.Z ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( /*SpellHelper.CheckTown( p, Caster ) && */CheckSequence() )
			{
                TimeSpan duration = TimeSpan.FromSeconds(0);

                BaseCreature.Summon(new SummonedBladeSpirits(), false, Caster, new Point3D(p), 0x212, duration);
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
            private EspritDeLamesSpell m_Owner;

            public InternalTarget(EspritDeLamesSpell owner)
                : base(12, true, TargetFlags.Harmful)
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
					m_Owner.Target( (IPoint3D)o );
			}

			protected override void OnTargetOutOfLOS( Mobile from, object o )
			{
				from.SendLocalizedMessage( 501943 ); // Target cannot be seen. Try again.
				from.Target = new InternalTarget( m_Owner );
				from.Target.BeginTimeout( from, TimeoutTime - DateTime.Now );
				m_Owner = null;
			}

			protected override void OnTargetFinish( Mobile from )
			{
				if ( m_Owner != null )
					m_Owner.FinishSequence();
			}
        }
	}
}