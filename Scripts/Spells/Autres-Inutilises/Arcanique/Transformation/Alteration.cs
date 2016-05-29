using System;
using System.Collections;
using Server;
using Server.Misc;
using Server.Items;
using Server.Gumps;
using Server.Prompts;
using Server.Spells;
using Server.Mobiles;
using Server.Targeting;
using Server.Scripts.Commands;

namespace Server.Spells
{
    public class AlterationSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

        public static readonly new SpellInfo Info = new SpellInfo(
                "Altération", "Kal Xen Corp",
                1,
                221,
                9002,
                Reagent.Garlic,
                Reagent.Ginseng,
                Reagent.SulfurousAsh
            );

		private int m_NewBody;
        private int m_HueMod;
        private string m_NameMod;

        public static Hashtable m_Mods = new Hashtable();

		public AlterationSpell( Mobile caster, Item scroll, string name, int body, int hue) : base( caster, scroll, Info )
		{
			m_NewBody = body;
            m_NameMod = name;
            m_HueMod = hue;
		}

		public AlterationSpell( Mobile caster, Item scroll ) : this(caster, scroll, null, 0, -1)
		{
		}

		public override void OnCast()
		{
            if (!Caster.CanBeginAction(typeof(AlterationSpell)))
            {
                if (Caster is PlayerMobile)
                {
                    PlayerMobile pm = (PlayerMobile)Caster;
                    pm.Transformation.OnTransformationChange(0, null, -1, true);
                }
                else
                {
                    Caster.BodyMod = 0;
                    Caster.NameMod = null;
                }

                Caster.EndAction(typeof(AlterationSpell));

                Effects.SendTargetParticles(Caster,0x373A, 10, 15, 5036, EffectLayer.Head);
                Caster.PlaySound(0x3BD);

                if (Caster is PlayerMobile)
                    ((PlayerMobile)Caster).CheckRaceSkin();

                BaseArmor.ValidateMobile(Caster);
            }
            else if (m_NewBody == 0)
            {
                ArrayList entries = new ArrayList();
                entries.Add(new MetamorphoseGump.MetamorphoseEntry("Chien", ShrinkTable.Lookup(0xD9), 0xD9, 1015237, 0, 0, 0, 0, 0));
                entries.Add(new MetamorphoseGump.MetamorphoseEntry("Chat", ShrinkTable.Lookup(0xC9), 0xC9, 1015246, 0, 0, 0, 0, 0));
                entries.Add(new MetamorphoseGump.MetamorphoseEntry("Diablotin", ShrinkTable.Lookup(0x4A), 0x4A, 1015246, 0, 0, 0, 0, 0));
                entries.Add(new MetamorphoseGump.MetamorphoseEntry("Morlask", ShrinkTable.Lookup(0x27), 0x27, 1015246, 0, 0, 0, 0, 0));

                Caster.SendGump(new MetamorphoseGump(Caster, Scroll, entries, 1));
            }
            else if (!CheckTransformation(Caster, Caster))
            {
                DoFizzle();
            }
            else if (CheckSequence())
            {
                if (Caster.BeginAction(typeof(AlterationSpell)))
                {
                    if (m_NewBody != 0)
                    {
                        if (!((Body)m_NewBody).IsHuman)
                        {
                            Mobiles.IMount mt = Caster.Mount;

                            if (mt != null)
                                mt.Rider = null;
                        }

                        if (Caster is PlayerMobile)
                        {
                            PlayerMobile pm = (PlayerMobile)Caster;
                            pm.Transformation.OnTransformationChange(m_NewBody, m_NameMod, m_HueMod, true);
                        }
                        else
                        {
                            Caster.BodyMod = m_NewBody;
                            Caster.NameMod = m_NameMod;
                            Caster.HueMod = m_HueMod;
                        }

                        Effects.SendTargetParticles(Caster,0x373A, 10, 15, 5036, EffectLayer.Head);
                        Caster.PlaySound(0x3BD);
                    }
                }
            }

			FinishSequence();
		}

        public static void StopTimer(Mobile m)
        {
            if (!m.CanBeginAction(typeof(AlterationSpell)))
            {
                if (m is PlayerMobile)
                {
                    PlayerMobile pm = (PlayerMobile)m;
                    pm.Transformation.OnTransformationChange(0, null, -1, true);
                }
                else
                {
                    m.BodyMod = 0;
                    m.NameMod = null;
                    m.HueMod = -1;
                }

                m.EndAction(typeof(AlterationSpell));

                Effects.SendTargetParticles(m,0x373A, 10, 15, 5036, EffectLayer.Head);
                m.PlaySound(0x3BD);

                if (m is PlayerMobile)
                    ((PlayerMobile)m).CheckRaceSkin();

                BaseArmor.ValidateMobile(m);
            }
        }
	}
}
