﻿using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using System.Collections;

namespace Server.Spells
{
    public class MontureCelesteMiracle : ReligiousSpell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        public static readonly new SpellInfo Info = new SpellInfo(
                "Monture Celeste", "",
                6,
                17,
                9032
            );

        public MontureCelesteMiracle(Mobile caster, Item scroll)
            : base(caster, scroll, Info)
        {
        }

        public override bool CheckCast()
        {
            if (!base.CheckCast())
                return false;

            if ((Caster.Followers + 1) > Caster.FollowersMax)
            {
                Caster.SendLocalizedMessage(1049645); // You have too many followers to summon that creature.
                return false;
            }

            return true;
        }

        public override void OnCast()
        {
            if (CheckSequence())
            {
                TimeSpan duration = TimeSpan.FromSeconds(0);
                if (Caster is PlayerMobile)
                {
                    SpellHelper.Summon(new Horse(), Caster, 0x217, duration, false, false);
                }
                else
                {
                SpellHelper.Summon(new Horse(), Caster, 0x217, duration, false, false);
                }
            }

            FinishSequence();
        }
    }
}
