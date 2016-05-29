using System;
using System.Text;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Spells
{
	public abstract class ReligiousSpell : Spell
	{
        public override StatType DamageStat { get { return StatType.Int; } }

        public ReligiousSpell(Mobile caster, Item scroll, SpellInfo info) : base(caster, scroll, info)
		{
        }

        public static void MiracleEffet(Mobile Caster, Mobile mob, int ID, int speed, int dura, int effect, int hue, int render, EffectLayer layer)
        {
            Effects.SendTargetParticles(mob,ID, speed, dura, effect, layer);

        }
    }
}