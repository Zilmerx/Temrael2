using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Spells
{
	public class EvasionSpell : Spell
    {
        public static int m_SpellID { get { return 501; } } // TOCHANGE

        private static short s_Cercle = 1;

		private static Hashtable m_Registry = new Hashtable();
		public static Hashtable Registry { get { return m_Registry; } }
		
		public override double CastDelayBase{ get{ return 0; } }
		public override int CastDelayMinimum{ get{ return 0; } }

		public override int CastRecoveryBase{ get{ return 0; } }
		public override int CastRecoveryCircleScalar{ get{ return 0; } }
		public override int CastRecoveryFastScalar{ get{ return 0; } }
		public override int CastRecoveryPerSecond{ get{ return 1; } }
		public override int CastRecoveryMinimum{ get{ return 0; } }

        public static readonly new SpellInfo Info = new SpellInfo(
            "Evasion", "Vas Por",
            s_Cercle,
            203,
            9031,
            GetBaseManaCost(s_Cercle),
            TimeSpan.FromSeconds(0),
            SkillName.Transmutation,
            Reagent.MandrakeRoot,
            Reagent.SpidersSilk
        );

		public EvasionSpell( Mobile caster, Item scroll ) : base( caster, scroll, Info )
		{
		}

        public void GetLocation()
        {
            int X = Utility.Random(-10, 20);
            int Y = Utility.Random(-10, 20);

            Point3D total = Caster.Location;

            IPoint3D p = new Point3D(total.X + X, total.Y + Y, total.Z);

            IPoint3D orig = p;
            Map map = Caster.Map;

            SpellHelper.GetSurfaceTop(ref p);

            point = new Point3D(p);

            if (Server.Misc.WeightOverloading.IsOverloaded(Caster))
            {
                Check = false;
                Count++;
                return;
            }
            else if (!SpellHelper.CheckTravel(Caster, TravelCheckType.TeleportFrom))
            {
                Check = false;
                Count++;
                return;
            }
            else if (!SpellHelper.CheckTravel(Caster, map, new Point3D(p), TravelCheckType.TeleportTo))
            {
                Check = false;
                Count++;
                return;
            }
            else if (map == null || !map.CanSpawnMobile(p.X, p.Y, p.Z))
            {
                Check = false;
                Count++;
                return;
            }
            else if (SpellHelper.CheckMulti(new Point3D(p), map))
            {
                Check = false;
                Count++;
                return;
            }
            else if (!Caster.CanSee(new Point3D(p)) || !Caster.InLOS(new Point3D(p)))
            {
                Check = false;
                Count++;
                return;
            }

            Check = true;
        }

        public Point3D point = new Point3D(0, 0, 0);
        public bool Check = false;
        public int Count = 0;

		public override void OnCast()
		{
            if (CheckSequence())
            {
                if (Server.Misc.WeightOverloading.IsOverloaded(Caster))
                {
                    Caster.SendLocalizedMessage(502359, "", 0x22); // Thou art too encumbered to move.
                }

                while ((!Check || point == Caster.Location || point == new Point3D(0, 0, 0)) && Count < 30 || !SpellHelper.CheckTravel( Caster, Caster.Map, point, TravelCheckType.TeleportTo ) || Caster.Map == null || !Caster.Map.CanSpawnMobile( point.X, point.Y, point.Z ) || ( SpellHelper.CheckMulti( new Point3D( point ), Caster.Map ) ) || ! Caster.CanSee(point))
                {
                    GetLocation();
                }

                SpellHelper.Turn(Caster, point);

                Mobile m = Caster;

                Point3D to = point;

                Point3D from = m.Location;

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

	}
}