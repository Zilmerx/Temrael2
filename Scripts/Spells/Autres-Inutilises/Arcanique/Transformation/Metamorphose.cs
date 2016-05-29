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
    public class MetamorphoseSpell : Spell
    {
        public static int m_SpellID { get { return 0; } } // TOCHANGE

		public static readonly new SpellInfo Info = new SpellInfo(
				"Metamorphose", "Vas Ylem Rel",
				7,
				221,
				9002,
				Reagent.Bloodmoss,
				Reagent.SpidersSilk,
				Reagent.MandrakeRoot
			);

		private int m_NewBody;
        private int m_StrMod;
        private int m_DexMod;
        private int m_IntMod;
        private int m_HueMod;
        private double m_SkillReq;
        private string m_NameMod;

        public static Hashtable m_Mods = new Hashtable();

		public MetamorphoseSpell( Mobile caster, Item scroll, string name, int body, int StrMod, int DexMod, int IntMod, double SkillReq, int hue) : base( caster, scroll, Info )
		{
			m_NewBody = body;
            m_StrMod = StrMod;
            m_DexMod = DexMod;
            m_IntMod = IntMod;
            m_SkillReq = SkillReq;
            m_NameMod = name;
            m_HueMod = hue;
		}

		public MetamorphoseSpell( Mobile caster, Item scroll ) : this(caster,scroll,null,0, 0, 0, 0, 0, 0)
		{
		}

		public override void OnCast()
		{
            if (m_NewBody == 0)
                Caster.Target = new InternalTarget(this);
            else
                ToogleMetamorphose();
		}

        public void Target(BaseCreature m)
        {
            if (CheckSequence())
            {
                if (m.Alive && Caster.CanSee(m) && m.BodyMod == 0)
                {
                    MetamorphoseGump.MetamorphoseEntry entry = new MetamorphoseGump.MetamorphoseEntry(m.Name, ShrinkTable.Lookup(m.Body), m.Body, 0, (int)(Caster.Skills[SkillName.Hallucination].Base / 4), (int)(Caster.Skills[SkillName.Hallucination].Base / 4), (int)(Caster.Skills[SkillName.Hallucination].Base / 4), 0, m.Hue);

                    if (Caster is PlayerMobile)
                    {
                        PlayerMobile pm = (PlayerMobile)Caster;

                        if (pm.Transformation.MetamorphoseList == null)
                            pm.Transformation.MetamorphoseList = new ArrayList();

                        int max = (int)(pm.Skills[SkillName.Hallucination].Base / 5);

                        if (pm.Transformation.MetamorphoseList.Count >= max)
                            pm.SendMessage("Vous ne pouvez pas avoir plus de " + max.ToString() + " créatures dans votre liste de métamorphoses.");
                        else
                        {
                            pm.Transformation.MetamorphoseList.Add((object)entry);
                            pm.SendMessage("Vous ajoutez avec succès la créature à votre liste de métamorphoses.");

                            Effects.SendTargetParticles(Caster,0x373A, 10, 15, 5036, EffectLayer.Head);
                            Caster.PlaySound(0x3BD);

                            Effects.SendTargetParticles(m,0x373A, 10, 15, 5036, EffectLayer.Head);
                            m.PlaySound(0x3BD);
                        }
                    }
                }
                else
                    Caster.SendMessage("Vous ne pouvez pas cibler des créatures transformées.");
            }

            FinishSequence();
        }

        public void ToogleMetamorphose()
        {
            if (!Caster.CanBeginAction(typeof(MetamorphoseSpell)))
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
                    Caster.HueMod = -1;
                }

                Caster.EndAction(typeof(MetamorphoseSpell));

                if (Caster is PlayerMobile)
                    ((PlayerMobile)Caster).CheckRaceSkin();

                BaseArmor.ValidateMobile(Caster);

                Effects.SendTargetParticles(Caster,0x373A, 10, 15, 5036, EffectLayer.Head);
                Caster.PlaySound(0x3BD);

                string name = String.Format("[Transformation] {0} Offset", StatType.Str);
                StatMod mod = Caster.GetStatMod(name);

                if (mod != null)
                    Caster.RemoveStatMod(name);

                name = String.Format("[Transformation] {0} Offset", StatType.Dex);
                mod = Caster.GetStatMod(name);

                if (mod != null)
                    Caster.RemoveStatMod(name);

                name = String.Format("[Transformation] {0} Offset", StatType.Int);
                mod = Caster.GetStatMod(name);

                if (mod != null)
                    Caster.RemoveStatMod(name);
            }
            else if (m_NewBody == 0)
            {
                ArrayList entries = null;

                if (Caster is PlayerMobile)
                    entries = ((PlayerMobile)Caster).Transformation.MetamorphoseList;

                if(entries != null)
                    Caster.SendGump(new MetamorphoseGump(Caster, Scroll, entries, 5));
            }
            else if (!CheckTransformation(Caster, Caster))
            {
                DoFizzle();
            }
            else if (CheckSequence())
            {
                if (Caster.BeginAction(typeof(MetamorphoseSpell)))
                {
                    if (m_NewBody != 0)
                    {
                        if (!((Body)m_NewBody).IsHuman)
                        {
                            Mobiles.IMount mt = Caster.Mount;

                            if (mt != null)
                                mt.Rider = null;
                        }

                        Caster.AddStatMod(new StatMod(StatType.Str, String.Format("[Transformation] {0} Offset", StatType.Str), m_StrMod, TimeSpan.Zero));
                        Caster.AddStatMod(new StatMod(StatType.Dex, String.Format("[Transformation] {0} Offset", StatType.Dex), m_DexMod, TimeSpan.Zero));
                        Caster.AddStatMod(new StatMod(StatType.Int, String.Format("[Transformation] {0} Offset", StatType.Int), m_IntMod, TimeSpan.Zero));

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
            if (!m.CanBeginAction(typeof(MetamorphoseSpell)))
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

                m.EndAction(typeof(MetamorphoseSpell));

                if (m is PlayerMobile)
                    ((PlayerMobile)m).CheckRaceSkin();

                BaseArmor.ValidateMobile(m);

                Effects.SendTargetParticles(m,0x373A, 10, 15, 5036, EffectLayer.Head);
                m.PlaySound(0x3BD);

                string name = String.Format("[Transformation] {0} Offset", StatType.Str);
                StatMod mod = m.GetStatMod(name);

                if (mod != null)
                    m.RemoveStatMod(name);

                name = String.Format("[Transformation] {0} Offset", StatType.Dex);
                mod = m.GetStatMod(name);

                if (mod != null)
                    m.RemoveStatMod(name);

                name = String.Format("[Transformation] {0} Offset", StatType.Int);
                mod = m.GetStatMod(name);

                if (mod != null)
                    m.RemoveStatMod(name);
            }
        }

        private class InternalTarget : Target
        {
            private MetamorphoseSpell m_Owner;

            public InternalTarget(MetamorphoseSpell owner)
                : base(12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is BaseCreature)
                    m_Owner.Target((BaseCreature)o);
                else if (o is PlayerMobile && from == o)
                    m_Owner.ToogleMetamorphose();
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
	}
}
