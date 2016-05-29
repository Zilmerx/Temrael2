using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Spells
{
	public class FamilierSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
				"Familier", "Kal Xen Bal",
				1,
				203,
				9031,
				Reagent.BatWing,
				Reagent.GraveDust,
				Reagent.DaemonBlood
            );

        public FamilierSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
		{
		}

		private static Hashtable m_Table = new Hashtable();

		public static Hashtable Table{ get{ return m_Table; } }

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				Caster.CloseGump( typeof( FamilierGump ) );
				Caster.SendGump( new FamilierGump( Caster, m_Entries ) );
			}

			FinishSequence();
		}

		private static FamilierEntry[] m_Entries = new FamilierEntry[]
			{
                //new FamilierEntry( typeof( HordeMinionFamiliar ), 1060146,  0,  0 ), // Horde Minion
                //new FamilierEntry( typeof( ShadowWispFamiliar ), 1060142,  15.0,  0 ), // Shadow Wisp
                //new FamilierEntry( typeof( DarkWolfFamiliar ), 1060143,  20.0,  0 ), // Dark Wolf
                //new FamilierEntry( typeof( DeathAdder ), 1060145,  25.0,  0 ), // Death Adder
                //new FamilierEntry( typeof( VampireBatFamiliar ), 1060144, 30.0, 0 )  // Vampire Bat
			};

		public static FamilierEntry[] Entries{ get{ return m_Entries; } }
	}

	public class FamilierEntry
	{
		private Type m_Type;
		private object m_Name;
		private double m_ReqNecromancy;
		private double m_ReqSpiritSpeak;

		public Type Type{ get{ return m_Type; } }
		public object Name{ get{ return m_Name; } }
		public double ReqNecromancy{ get{ return m_ReqNecromancy; } }
		public double ReqSpiritSpeak{ get{ return m_ReqSpiritSpeak; } }

		public FamilierEntry( Type type, object name, double reqNecromancy, double reqSpiritSpeak )
		{
			m_Type = type;
			m_Name = name;
			m_ReqNecromancy = reqNecromancy;
			m_ReqSpiritSpeak = reqSpiritSpeak;
		}
	}

	public class FamilierGump : Gump
	{
		private Mobile m_From;
		private FamilierEntry[] m_Entries;

		private const int  EnabledColor16 = 0x0F20;
		private const int DisabledColor16 = 0x262A;

		private const int  EnabledColor32 = 0x18CD00;
		private const int DisabledColor32 = 0x4A8B52;

		public FamilierGump( Mobile from, FamilierEntry[] entries ) : base( 200, 100 )
		{
			m_From = from;
			m_Entries = entries;

			AddPage( 0 );

			AddBackground( 10, 10, 250, 178, 9270 );
			AddAlphaRegion( 20, 20, 230, 158 );

			AddImage( 220, 20, 10464 );
			AddImage( 220, 72, 10464 );
			AddImage( 220, 124, 10464 );

			AddItem( 188, 16, 6883 );
			AddItem( 198, 168, 6881 );
			AddItem( 8, 15, 6882 );
			AddItem( 2, 168, 6880 );

			AddHtmlLocalized( 30, 26, 200, 20, 1060147, EnabledColor16, false, false ); // Chose thy familiar...

            double necro = from.Skills[SkillName.ArtMagique].Base;
			double spirit = from.Skills[SkillName.Animisme].Base;

			for ( int i = 0; i < entries.Length; ++i )
			{
				object name = entries[i].Name;

				bool enabled = ( necro >= entries[i].ReqNecromancy && spirit >= entries[i].ReqSpiritSpeak );

				AddButton( 27, 53 + (i * 21), 9702, 9703, i + 1, GumpButtonType.Reply, 0 );

				if ( name is int )
					AddHtmlLocalized( 50, 51 + (i * 21), 150, 20, (int)name, enabled ? EnabledColor16 : DisabledColor16, false, false );
				else if ( name is string )
					AddHtml( 50, 51 + (i * 21), 150, 20, String.Format( "<BASEFONT COLOR=#FFFFFF>{1}</BASEFONT>", enabled ? EnabledColor32 : DisabledColor32, name ), false, false );
			}
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int index = info.ButtonID - 1;

			if ( index >= 0 && index < m_Entries.Length )
			{
				FamilierEntry entry = m_Entries[index];

                double necro = m_From.Skills[SkillName.ArtMagique].Base;
				double spirit = m_From.Skills[SkillName.Animisme].Base;

                //BaseCreature check = (BaseCreature)FamilierSpell.Table[m_From];

                //if ( check != null && !check.Deleted )
                //{
                //    m_From.SendLocalizedMessage( 1061605 ); // You already have a familiar.
                //}
                //else 
                if ((m_From.Followers + 2) > m_From.FollowersMax)
                {
                    m_From.SendLocalizedMessage(1049645); // You have too many followers to summon that creature.
                }
                else if ( necro < entry.ReqNecromancy || spirit < entry.ReqSpiritSpeak )
				{
					// That familiar requires ~1_NECROMANCY~ Necromancy and ~2_SPIRIT~ Spirit Speak.
					m_From.SendLocalizedMessage( 1061606, String.Format( "{0:F1}\t{1:F1}", entry.ReqNecromancy, entry.ReqSpiritSpeak ) );

					m_From.CloseGump( typeof( FamilierGump ) );
					m_From.SendGump( new FamilierGump( m_From, FamilierSpell.Entries ) );
				}
				else if ( entry.Type == null )
				{
					m_From.SendMessage( "That familiar has not yet been defined." );

					m_From.CloseGump( typeof( FamilierGump ) );
					m_From.SendGump( new FamilierGump( m_From, FamilierSpell.Entries ) );
				}
				else
				{
					try
					{
						BaseCreature bc = (BaseCreature)Activator.CreateInstance( entry.Type );

						bc.Skills.Concentration = m_From.Skills.Concentration;

						if ( BaseCreature.Summon( bc, m_From, m_From.Location, -1, TimeSpan.FromSeconds(10 + m_From.Skills[SkillName.Animisme].Base * 2.4) ) )
						{
							Effects.SendTargetParticles(m_From, 0x3728, 1, 10, 9910, EffectLayer.Head );
							bc.PlaySound( bc.GetIdleSound() );
							FamilierSpell.Table[m_From] = bc;
						}
					}
					catch
					{
					}
				}
			}
			else
			{
				m_From.SendLocalizedMessage( 1061825 ); // You decide not to summon a familiar.
			}
		}
	}
}