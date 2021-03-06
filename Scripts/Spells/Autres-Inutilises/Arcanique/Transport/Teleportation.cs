using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;

namespace Server.Spells
{
	public class TeleportationSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
				"Teleportation", "Rel Por",
				3,
				215,
				9031,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot
            );

		public TeleportationSpell( Mobile caster, Item scroll ) : base( caster, scroll, Info )
		{
		}

		public override bool CheckCast()
		{
            if (Server.Misc.WeightOverloading.IsOverloaded(Caster))
            {
                Caster.SendLocalizedMessage(502359, "", 0x22); // Thou art too encumbered to move.
                return false;
            }

            return base.CheckCast(); //SpellHelper.CheckTravel( Caster, TravelCheckType.TeleportFrom );
           
		}

		public override void OnCast()
		{
			//Caster.Target = new InternalTarget( this );
            Caster.SendMessage("Sort temporairement désactivé en raison de bug abuse.");
		}

		public void Target( IPoint3D p )
		{
			IPoint3D orig = p;
			Map map = Caster.Map;

			SpellHelper.GetSurfaceTop( ref p );

			if ( Server.Misc.WeightOverloading.IsOverloaded( Caster ) )
			{
				Caster.SendLocalizedMessage( 502359, "", 0x22 ); // Thou art too encumbered to move.
			}
			/*else if ( !SpellHelper.CheckTravel( Caster, TravelCheckType.TeleportFrom ) )
			{
			}*/
			else if ( !SpellHelper.CheckTravel( Caster, map, new Point3D( p ), TravelCheckType.TeleportTo ) )
			{
			}
			else if ( map == null || !map.CanSpawnMobile( p.X, p.Y, p.Z ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( SpellHelper.CheckMulti( new Point3D( p ), map ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
            else if (!Caster.CanSee(p))
            {
                Caster.SendMessage("Vous ne pouvez pas  voir l'endroit où vous désirez vous téléporter.");
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, orig);

                Mobile m = Caster;

                Point3D from = m.Location;
                Point3D to = new Point3D(p);

                m.Location = to;
                m.ProcessDelta();

                if (m.Player)
                {
                    Effects.SendLocationParticles(EffectItem.Create(from, m.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);
                    Effects.SendLocationParticles(EffectItem.Create(to, m.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 5023);
                }
                else
                {
                    Effects.SendTargetParticles(m,0x376A, 9, 32, 0x13AF, EffectLayer.Waist);
                }

                m.PlaySound(0x1FE);
            }

			FinishSequence();
		}

		public class InternalTarget : Target
		{
            private TeleportationSpell m_Owner;

            public InternalTarget(TeleportationSpell owner)
                : base(12, true, TargetFlags.None)
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