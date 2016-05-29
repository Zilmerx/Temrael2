using System;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells
{
	public class SerrureSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
				"Serrure", "An Por",
                3,
				215,
				9001,
				Reagent.Garlic,
				Reagent.Bloodmoss,
				Reagent.Ginseng
            );

        public SerrureSpell(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( LockableContainer targ )
		{
			if ( Multis.BaseHouse.CheckLockedDownOrSecured( targ ) )
			{
				// You cannot cast this on a locked down item.
				Caster.LocalOverheadMessage( MessageType.Regular, 0x22, 501761 );
			}
			else if ( targ.Locked/* || targ.LockLevel == 0 */)
			{
				// Target must be an unlocked chest.
				Caster.SendLocalizedMessage( 501762 );
			}
			else if ( CheckSequence() )
			{
				SpellHelper.Turn( Caster, targ );

				Point3D loc = targ.GetWorldLocation();

				Effects.SendLocationParticles(
					EffectItem.Create( loc, targ.Map, EffectItem.DefaultDuration ),
					0x376A, 9, 32, 5020 );

				Effects.PlaySound( loc, targ.Map, 0x1FA );

				// The chest is now locked!
				Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 501763 );

				targ.LockLevel = -255; // signal magic lock
				targ.Locked = true;
                targ.RequiredSkill = (int)Caster.Skills[CastSkill].Value;
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
            private SerrureSpell m_Owner;

            public InternalTarget(SerrureSpell owner)
                : base(12, false, TargetFlags.None)
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is LockableContainer )
					m_Owner.Target( (LockableContainer)o );
				else
					from.SendLocalizedMessage( 501762 ); // Target must be an unlocked chest.
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}