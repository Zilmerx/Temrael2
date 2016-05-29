using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Spells;

namespace Server.SkillHandlers
{
	public class Tracking
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Poursuite].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.SendLocalizedMessage( 1011350 ); // What do you wish to track?

			m.CloseGump( typeof( TrackWhatGump ) );
			m.CloseGump( typeof( TrackWhoGump ) );
			m.SendGump( new TrackWhatGump( m ) );

			return TimeSpan.FromSeconds( 10.0 ); // 10 second delay before beign able to re-use a skill
		}

		public class TrackingInfo
		{
			public Mobile m_Tracker;
			public Mobile m_Target;
			public Point2D m_Location;
			public Map m_Map;

			public TrackingInfo( Mobile tracker, Mobile target )
			{
				m_Tracker = tracker;
				m_Target = target;
				m_Location = new Point2D( target.X, target.Y );
				m_Map = target.Map;
			}
		}

        public static bool IsTracking(Mobile tracker, Mobile tracked)
        {
            TrackingInfo info = null;
            m_Table.TryGetValue(tracker, out info);

            if (info == null || info.m_Target != tracked)
            {
                return false;
            }
            return true;
        }

		public static Dictionary<Mobile, TrackingInfo> m_Table = new Dictionary<Mobile, TrackingInfo>();

		public static void AddInfo( Mobile tracker, Mobile target )
		{
			TrackingInfo info = new TrackingInfo( tracker, target );
			m_Table[tracker] = info;
		}

		public static double GetStalkingBonus( Mobile tracker, Mobile target )
		{
			TrackingInfo info = null;
			m_Table.TryGetValue( tracker, out info );

			if ( info == null || info.m_Target != target || info.m_Map != target.Map )
				return 0.0;

			int xDelta = info.m_Location.X - target.X;
			int yDelta = info.m_Location.Y - target.Y;

			double bonus = Math.Sqrt( (xDelta * xDelta) + (yDelta * yDelta) );

			m_Table.Remove( tracker );	//Reset as of Pub 40, counting it as bug for Core.SE.

			if( Core.ML )
				return Math.Min( bonus, 10 + tracker.Skills.Poursuite.Value/10 );

			return bonus;
		}


		public static void ClearTrackingInfo( Mobile tracker )
		{
			m_Table.Remove( tracker );
		}
	}

	public class TrackWhatGump : Gump
	{
		private Mobile m_From;
		private bool m_Success;

		public TrackWhatGump( Mobile from ) : base( 20, 30 )
		{
			m_From = from;
			m_Success = from.CheckSkill( SkillName.Poursuite, 0.0, 21.1 );

			AddPage( 0 );

			AddBackground( 0, 0, 440, 135, 5054 );

			AddBackground( 10, 10, 420, 75, 2620 );
			AddBackground( 10, 85, 420, 25, 3000 );

			AddItem( 20, 20, 9682 );
			AddButton( 20, 110, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 20, 90, 100, 20, 1018087, false, false ); // Animals

			AddItem( 120, 20, 8440 );
			AddButton( 120, 110, 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 120, 90, 100, 20, 1018088, false, false ); // Monsters

			AddItem( 220, 20, 8454 );
			AddButton( 220, 110, 4005, 4007, 3, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 220, 90, 100, 20, 1018089, false, false ); // Human NPCs

			AddItem( 320, 20, 8455 );
			AddButton( 320, 110, 4005, 4007, 4, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 320, 90, 100, 20, 1018090, false, false ); // Players
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( info.ButtonID >= 1 && info.ButtonID <= 4 )
				TrackWhoGump.DisplayTo( m_Success, m_From, info.ButtonID - 1 );
		}
	}

	public delegate bool TrackTypeDelegate( Mobile m );

	public class TrackWhoGump : Gump
	{
		private Mobile m_From;
		private int m_Range;

		private static TrackTypeDelegate[] m_Delegates = new TrackTypeDelegate[]
			{
				new TrackTypeDelegate( IsAnimal ),
				new TrackTypeDelegate( IsMonster ),
				new TrackTypeDelegate( IsHumanNPC ),
				new TrackTypeDelegate( IsPlayer )
			};

		private class InternalSorter : IComparer<Mobile>
		{
			private Mobile m_From;

			public InternalSorter( Mobile from )
			{
				m_From = from;
			}

			public int Compare( Mobile x, Mobile y )
			{
				if ( x == null && y == null )
					return 0;
				else if ( x == null )
					return -1;
				else if ( y == null )
					return 1;

				return m_From.GetDistanceToSqrt( x ).CompareTo( m_From.GetDistanceToSqrt( y ) );
			}
		}

		public static void DisplayTo( bool success, Mobile from, int type )
		{
			if ( !success )
			{
				from.SendLocalizedMessage( 1018092 ); // You see no evidence of those in the area.
				return;
			}

			Map map = from.Map;

			if ( map == null )
				return;

			TrackTypeDelegate check = m_Delegates[type];

            double bonus = 0;

            if (BruitSpell.m_BruitTable.Contains(from))
                bonus += (double)BruitSpell.m_BruitTable[from];

            from.CheckSkill(SkillName.Poursuite, 21.1 - bonus, 100.0 - bonus); // Passive gain

            int range = 10 + (int)(from.Skills[SkillName.Poursuite].Value / 3) + (int)bonus;

			List<Mobile> list = new List<Mobile>();

			foreach ( Mobile m in from.GetMobilesInRange( range ) )
			{
				// Ghosts can no longer be tracked 
				if ( m != from && (!Core.AOS || m.Alive) && (!m.Hidden || m.AccessLevel == AccessLevel.Player || from.AccessLevel > m.AccessLevel) && check( m ) && CheckDifficulty( from, m ) )
					list.Add( m );
			}

			if ( list.Count > 0 )
			{
				list.Sort( new InternalSorter( from ) );

				from.SendGump( new TrackWhoGump( from, list, range ) );
				from.SendLocalizedMessage( 1018093 ); // Select the one you would like to track.
			}
			else
			{
				if ( type == 0 )
					from.SendLocalizedMessage( 502991 ); // You see no evidence of animals in the area.
				else if ( type == 1 )
					from.SendLocalizedMessage( 502993 ); // You see no evidence of creatures in the area.
				else
					from.SendLocalizedMessage( 502995 ); // You see no evidence of people in the area.
			}
		}

		// Tracking players uses tracking and detect hidden vs. hiding and stealth 
		private static bool CheckDifficulty( Mobile from, Mobile m )
		{
    		int tracking = from.Skills[SkillName.Poursuite].Fixed;	
			int detectHidden = from.Skills[SkillName.Detection].Fixed;

			int hiding = m.Skills[SkillName.Discretion].Fixed;
			int stealth = m.Skills[SkillName.Infiltration].Fixed;
			int divisor = hiding + stealth;

            // Necromancy forms affect tracking difficulty 
            if (TransformationSpellHelper.UnderTransformation(m, typeof(HorrificBeastSpell)))
                divisor -= 200;
            else if (TransformationSpellHelper.UnderTransformation(m, typeof(VampiricEmbraceSpell)) && divisor < 500)
                divisor = 500;
            else if (TransformationSpellHelper.UnderTransformation(m, typeof(WraithFormSpell)) && divisor <= 2000)
                divisor += 200;

			int chance;

            if (divisor > 0)
                chance = 50 * (tracking * 2 + detectHidden) / divisor;
            else
                chance = 100;

			return chance > Utility.Random( 100 );
		}

		private static bool IsAnimal( Mobile m )
		{
			return ( !m.Player && m.Body.IsAnimal );
		}

		private static bool IsMonster( Mobile m )
		{
			return ( !m.Player && m.Body.IsMonster );
		}

		private static bool IsHumanNPC( Mobile m )
		{
			return ( !m.Player && m.Body.IsHuman );
		}

		private static bool IsPlayer( Mobile m )
		{
			return m.Player;
		}

		private List<Mobile> m_List;

		private TrackWhoGump( Mobile from, List<Mobile> list, int range ) : base( 20, 30 )
		{
			m_From = from;
			m_List = list;
			m_Range = range;

			AddPage( 0 );

			AddBackground( 0, 0, 440, 155, 5054 );

			AddBackground( 10, 10, 420, 75, 2620 );
			AddBackground( 10, 85, 420, 45, 3000 );

			if ( list.Count > 4 )
			{
				AddBackground( 0, 155, 440, 155, 5054 );

				AddBackground( 10, 165, 420, 75, 2620 );
				AddBackground( 10, 240, 420, 45, 3000 );

				if ( list.Count > 8 )
				{
					AddBackground( 0, 310, 440, 155, 5054 );

					AddBackground( 10, 320, 420, 75, 2620 );
					AddBackground( 10, 395, 420, 45, 3000 );
				}
			}

			for ( int i = 0; i < list.Count && i < 12; ++i )
			{
				Mobile m = list[i];

				AddItem( 20 + ((i % 4) * 100), 20 + ((i / 4) * 155), ShrinkTable.Lookup( m ) );
				AddButton( 20 + ((i % 4) * 100), 130 + ((i / 4) * 155), 4005, 4007, i + 1, GumpButtonType.Reply, 0 );

				if ( m.Name != null )
					AddHtml( 20 + ((i % 4) * 100), 90 + ((i / 4) * 155), 90, 40, m.GetNameUsedBy(from), false, false );
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			int index = info.ButtonID - 1;

			if ( index >= 0 && index < m_List.Count && index < 12 )
			{
				Mobile m = m_List[index];

				m_From.QuestArrow = new TrackArrow( m_From, m, m_Range * 2 );

				Tracking.AddInfo( m_From, m );
			}
		}
	}

	public class TrackArrow : QuestArrow
	{
		private Mobile m_From;
		private Timer m_Timer;

		public TrackArrow( Mobile from, Mobile target, int range ) : base( from, target )
		{
			m_From = from;
			m_Timer = new TrackTimer( from, target, range, this );
			m_Timer.Start();
		}

		public override void OnClick( bool rightClick )
		{
			if ( rightClick )
			{
				Tracking.ClearTrackingInfo( m_From );

                m_From.CloseGump(typeof(TrackDistanceGump));

				m_From = null;

				Stop();
			}
		}

		public override void OnStop()
		{
			m_Timer.Stop();

			if ( m_From != null )
			{
				Tracking.ClearTrackingInfo( m_From );

				m_From.SendLocalizedMessage( 503177 ); // You have lost your quarry.

                m_From.CloseGump(typeof(TrackDistanceGump));
			}
		}
	}

    public class TrackDistanceGump : Gump
	{
        private Mobile m_From;
        private Mobile m_Target;
        private string message;
        private double distance;
        
        public TrackDistanceGump( Mobile from, Mobile target) : base( 20, 30 )
		{
			m_From = from;
			m_Target = target;

			AddPage( 0 );

			AddBackground( 0, 0, 400, 60, 5054 );

			AddBackground( 10, 10, 380, 40, 2620 );
			AddBackground( 20, 20, 360, 20, 3000 );

            distance = getDistance(m_From, m_Target);

            if (distance <= 1) message = "Votre cible est � c�t� de vous.";
            else if (distance > 1 && distance <= 5) message = "Votre cible est tr�s proche de vous.";
            else if (distance > 5 && distance <= 10) message = "Votre cible est proche de vous.";
            else if (distance > 10 && distance <= 15) message = "Votre cible est moyennement proche de vous.";
            else if (distance > 15 && distance <= 20) message = "Votre cible est loin de vous.";
            else if (distance > 20 && distance <= 25) message = "Votre cible est moyennement loin de vous.";
            else if (distance > 25 && distance <= 30) message = "Votre cible est tr�s loin de vous.";
            else if (distance > 30 && distance <= 40) message = "Votre cible est � une grande distance de vous.";
            else if (distance > 40 && distance <= 60) message = "Votre cible est � une tr�s grande distance de vous.";
            else message = "Votre cible est � quelques lieux de vous.";

            AddHtml(30, 20, 360, 20, message, false, false);
        }

        private double getDistance(Mobile from, Mobile target)
        {
            return Math.Round(Math.Sqrt(Math.Pow((from.Location.X - target.Location.X), 2) + Math.Pow((from.Location.Y - target.Location.Y), 2)));
        }
	}


	public class TrackTimer : Timer
	{
		private Mobile m_From, m_Target;
		private int m_Range;
		private int m_LastX, m_LastY;
		private QuestArrow m_Arrow;
        private double distance;

		public TrackTimer( Mobile from, Mobile target, int range, QuestArrow arrow ) : base( TimeSpan.FromSeconds( 0.25 ), TimeSpan.FromSeconds( 2.5 ) )
		{
			m_From = from;
			m_Target = target;
			m_Range = range;

			m_Arrow = arrow;
		}

		protected override void OnTick()
		{
            m_From.CloseGump(typeof(TrackDistanceGump));

            if ( !m_Arrow.Running )
			{
				Stop();
				return;
			}
            else if (m_From.NetState == null || m_From.Deleted || m_Target.Deleted || m_From.Map != m_Target.Map || !m_From.InRange(m_Target, m_Range))
            {
                m_Arrow.Stop();
                Stop();
                return;
            }
            else
            {
                m_From.SendGump(new TrackDistanceGump(m_From, m_Target));
            }

			if ( m_LastX != m_Target.X || m_LastY != m_Target.Y )
			{
				m_LastX = m_Target.X;
				m_LastY = m_Target.Y;

				m_Arrow.Update();  
			}
		}
	}
}