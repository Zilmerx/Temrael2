using System;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Regions;

namespace Server.Spells
{
	public class MarkSpell : Spell
	{
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        private static short s_Cercle = 0;

		public static readonly new SpellInfo Info = new SpellInfo(
				"Marque", "Kal Por Ylem",
                s_Cercle,
                203,
                9031,
                GetBaseManaCost(s_Cercle),
                TimeSpan.FromSeconds(1),
                SkillName.ArtMagique,
				Reagent.BlackPearl,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot
            );

		public MarkSpell( Mobile caster, Item scroll ) : base( caster, scroll, Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		/*public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			return SpellHelper.CheckTravel( Caster, TravelCheckType.Mark );
		}*/

		public void Target( RecallRune rune )
		{
			if ( !Caster.CanSee( rune ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( !SpellHelper.CheckTravel( Caster, TravelCheckType.Mark ) )
			{
			}
			else if ( SpellHelper.CheckMulti( Caster.Location, Caster.Map, true ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( !rune.IsChildOf( Caster.Backpack ) )
			{
				Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1062422 ); // You must have this rune in your backpack in order to mark it.
			}
			else if ( CheckSequence() )
			{
				rune.Mark( Caster );

				Caster.PlaySound( 0x1FA );
				Effects.SendLocationEffect( Caster, Caster.Map, 14201, 16 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private MarkSpell m_Owner;

			public InternalTarget( MarkSpell owner ) : base( 12, false, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is RecallRune )
				{
					m_Owner.Target( (RecallRune) o );
				}
				else
				{
					from.Send( new MessageLocalized( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 501797, from.Name, "" ) ); // I cannot mark that object.
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}