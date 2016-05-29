﻿using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;


namespace Server.Custom.CustomSpell
{
    public enum StyleSpell
    {
        Unsetted, // Par défaut, si non défini.

        Targeted,
        TargetedTimer,

        AoE,
        AoETimer,

        Self,
        SelfTimer
    }

    // Classe de base pour les autres sortes de spell. Non-instanciable.
    public abstract class CustomSpell : Server.Spells.Spell
    {
        public InfoSpell m_info;

        private CustomSpell(Mobile caster, Item scroll, InfoSpell info)
            : base(caster, scroll, (Server.Spells.SpellInfo)info)
        {
            m_info = info;
        }

        // Appelle la bonne fonction, dépendant du type de spell. La fonction appellée est virtual ici, et overridée dans les classes spécialisées.
        public override void OnCast()
        {
            switch (m_info.style)
            {
                    // Targeted
                case (StyleSpell.Targeted): UseSpellTargeted();
                    break;

                    // TargetedTimer
                case (StyleSpell.TargetedTimer) : UseSpellTargetedTimer();
                    break;

                    // AoE
                case (StyleSpell.AoE): UseSpellAoE();
                    break;

                    // AoETimer
                case (StyleSpell.AoETimer): UseSpellAoETimer();
                    break;

                    // Self
                case (StyleSpell.Self): UseSpellSelf();
                    break;

                    // SelfTimer
                case (StyleSpell.SelfTimer): UseSpellSelfTimer();
                    break;

                default: throw new Exception("StyleSpell non setté.");
            }
        }


        #region Fonctions virtual, utilisées par les classes spécialisées.
        public virtual void UseSpellTargeted()
        {
        }

        public virtual void UseSpellTargetedTimer()
        {
        }

        public virtual void UseSpellAoE()
        {
        }

        public virtual void UseSpellAoETimer()
        {
        }

        public virtual void UseSpellSelf()
        {
        }

        public virtual void UseSpellSelfTimer()
        {
        }
        #endregion


        // FONCTIONNEL !
        public abstract class CSpellTargeted : CustomSpell
        {
            private new InfoSpell.Targeted m_info;

            // Pourraient être des tableaux --v
            public object   target1 = null,
                            target2 = null, 
                            target3 = null;

            private bool target1rdy = false,
                         target2rdy = false,
                         target3rdy = false;



            public CSpellTargeted(Mobile caster, Item scroll, InfoSpell.Targeted info)
                : base(caster, scroll, (InfoSpell)info)
            {
                m_info = info;
            }

            public override void OnCast()
            {
                base.OnCast();
            }


            // Point d'entrée, lorsque l'on appelle un Spell.Cast();
            public override void UseSpellTargeted()
            {
                if (CheckSequence()) // Si le mana, les ingrédients... etc sont corrects.
                {
                    Caster.Target = new InternalTarget(this, 1);
                }
                else
                {
                    FinishSequence();
                }
            }

            // Appellé à chaque fois que l'utilisateur clique sur un target.
            // Gère aussi quand est-ce que la fonction Effect() est appellée.
            private void OnNewTarget()
            {
                if (target1 != null) // Si le player a cancellé.
                {
                    if (m_info.unEffectParTarget)
                    {
                        // Un effet à chaque target.
                        Effect();
                    }
                    else
                    {
                        if (target1rdy && (target2rdy || m_info.nbTarget <= 1) && (target3rdy || m_info.nbTarget <= 2))
                        {
                            // Appelle l'effet seulement lorsque tous les targets demandés sont faits.
                            Effect();
                        }
                    }

                    // Si on veut plus de targets, on en créée un nouveau.
                    if (target2 == null && m_info.nbTarget >= 2)
                    {
                        Caster.Target = new InternalTarget(this, 2);
                    }
                    else if (target3 == null && m_info.nbTarget >= 3)
                    {
                        Caster.Target = new InternalTarget(this, 3);
                    }
                    else
                    {
                        FinishSequence();
                    }
                }
            }


            // Doit être redéfinie par l'utilisateur de la classe.
            public abstract void Effect();


            // Créer un target et retourner l'objet dans le "target" spécifié par le int.
            private class InternalTarget : Target
            {
                private CSpellTargeted m_Owner;

                private int m_numeroTarget = 1;
                private int numeroTarget { get { return m_numeroTarget; } set { if (value <= 3 && value >= 1) m_numeroTarget = numeroTarget; } }

                public InternalTarget(CSpellTargeted owner, int NumeroTarget)
                    : base(owner.m_info.range, true, TargetFlags.None)
                {
                    numeroTarget = NumeroTarget;
                    m_Owner = owner;

                    switch (numeroTarget)
                    {
                        case 1: m_Owner.target1rdy = false;
                            break;
                        case 2: m_Owner.target2rdy = false;
                            break;
                        case 3: m_Owner.target3rdy = false;
                            break;
                    }
                }

                protected override void OnTarget(Mobile from, object o)
                {
                    switch (numeroTarget)
                    {
                        case 1: m_Owner.target1 = o;
                                m_Owner.target1rdy = true;
                            break;
                        case 2: m_Owner.target2 = o;
                                m_Owner.target2rdy = true;
                            break;
                        case 3: m_Owner.target3 = o;
                                m_Owner.target3rdy = true;
                            break;
                    }

                    m_Owner.OnNewTarget();
                }

                protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
                {
                    // Empêche le cast.
                    m_Owner.target1 = null;
                    m_Owner.target2 = null;
                    m_Owner.target3 = null;

                    base.OnTargetCancel(from, cancelType);
                }
            }

        }

        // FONCTIONNEL !
        public abstract class CSpellTargetedTimer : CustomSpell
        {
            private InfoSpell.TargetedTimer info;

            private EffectTimer effectTimer;

            // Pourraient être des tableaux --v
            public object target1 = null,
                            target2 = null,
                            target3 = null;

            private bool target1rdy = false,
                         target2rdy = false,
                         target3rdy = false;

            public CSpellTargetedTimer(Mobile caster, Item scroll, InfoSpell.TargetedTimer info)
                : base(caster, scroll, (InfoSpell)info)
            {
                this.info = info;
            }

            public override void OnCast()
            {
                base.OnCast();
            }


            // Appellé par .Cast().
            public override void UseSpellTargetedTimer()
            {
                if (CheckSequence()) // Si le mana, les ingrédients... etc sont corrects.
                {
                    Caster.Target = new InternalTarget(this, 1);
                }
                else
                {
                    FinishSequence();
                }
            }

            // Appellé à chaque fois que l'utilisateur clique sur un target.
            // Gère aussi quand est-ce que la fonction Effect() est appellée.
            private void OnNewTarget()
            {
                if (target1 != null) // Si le player a cancellé.
                {

                    if (target1rdy && (target2rdy || info.nbTarget <= 1) && (target3rdy || info.nbTarget <= 2))
                    {
                        // CREATION DU TIMER.
                        effectTimer = new EffectTimer(this, info.duree, info.intervale);
                    }

                    // Si on veut plus de targets, on en créée un nouveau.
                    if (target2 == null && info.nbTarget >= 2)
                    {
                        Caster.Target = new InternalTarget(this, 2);
                    }
                    else if (target3 == null && info.nbTarget >= 3)
                    {
                        Caster.Target = new InternalTarget(this, 3);
                    }
                    else
                    {
                        FinishSequence();
                    }
                }
            }


            // Effet au début du timer.
            public abstract void OnStart();
            // Effet à chaque Tick du timer.
            public abstract void OnTick();
            // Effet a la fin du timer.
            public abstract void OnEnd();


            // Timer qui est utilisé après le temps de cast, et une fois que tous les targets ont été choisis. Appelle les fonctions asbtract OnStart, OnTick, et OnEnd définies par l'utilisteur.
            private class EffectTimer : Timer
            {
                private CSpellTargetedTimer m_owner;
                private DateTime m_End;

                private int m_NumeroTick = 0;
                public int NumeroTick { get { return m_NumeroTick; } }

                public EffectTimer(CSpellTargetedTimer owner, TimeSpan duree, TimerPriority intervale )
                    : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
                {
                    m_End = DateTime.Now + duree;
                    m_owner = owner;

                    Priority = intervale;

                    m_owner.OnStart();
                    Start();
                }

                protected override void OnTick()
                {
                    m_NumeroTick++;
                    // Si le temps est fini.
                    if (DateTime.Now >= m_End)
                    {
                        m_owner.OnEnd();
                        Stop();
                    }
                    else
                    {
                        m_owner.OnTick();
                    }
                }
            }
            public void StopSpell()
            {
                effectTimer.Stop();
            }
            public int NumeroTick
            {
                get { return effectTimer.NumeroTick; }
            }

            // Créer un target et retourner l'objet dans le "target" spécifié par le int.
            private class InternalTarget : Target
            {
                private CSpellTargetedTimer m_Owner;

                private int m_numeroTarget = 1;
                private int numeroTarget { get { return m_numeroTarget; } set { if (value <= 3 && value >= 1) m_numeroTarget = numeroTarget; } }

                public InternalTarget(CSpellTargetedTimer owner, int NumeroTarget)
                    : base(owner.info.range, true, TargetFlags.None)
                {
                    numeroTarget = NumeroTarget;
                    m_Owner = owner;

                    switch (numeroTarget)
                    {
                        case 1: m_Owner.target1rdy = false;
                            break;
                        case 2: m_Owner.target2rdy = false;
                            break;
                        case 3: m_Owner.target3rdy = false;
                            break;
                    }
                }

                protected override void OnTarget(Mobile from, object o)
                {
                    switch (numeroTarget)
                    {
                        case 1: m_Owner.target1 = o;
                            m_Owner.target1rdy = true;
                            break;
                        case 2: m_Owner.target2 = o;
                            m_Owner.target2rdy = true;
                            break;
                        case 3: m_Owner.target3 = o;
                            m_Owner.target3rdy = true;
                            break;
                    }

                    m_Owner.OnNewTarget();
                }

                protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
                {
                    // Empêche le cast.
                    m_Owner.target1 = null;
                    m_Owner.target2 = null;
                    m_Owner.target3 = null;

                    base.OnTargetCancel(from, cancelType);
                }
            }

        }

        // FONCTIONNEL !
        public abstract class CSpellAoE : CustomSpell
        {
            private InfoSpell.AoE info;

            public CSpellAoE(Mobile caster, Item scroll, InfoSpell.AoE info)
                : base(caster, scroll, (InfoSpell)info)
            {
                this.info = info;
            }

            public override void OnCast()
            {
                base.OnCast();
            }

            public override void UseSpellAoE()
            {
                if (CheckSequence()) // Si le mana, les ingrédients... etc sont corrects.
                {
                    UniqueEffect();
                    foreach(Mobile target in Caster.GetMobilesInRange(info.range))
                    {
                        TargetEffect(target);
                    }
                }
                FinishSequence();
            }

            // Un seul effect pour le spell (Ex Animation de feu sur le caster..)
            public abstract void UniqueEffect();
            // Effect sur tous les Mobiles (Ex 10 de damage)
            public abstract void TargetEffect(Mobile target);
        }

        // FONCTIONNEL !
        public abstract class CSpellAoETimer : CustomSpell
        {
            private InfoSpell.AoETimer info;

            private EffectTimer effectTimer;

            private IPooledEnumerable<Mobile> targetsList;

            public CSpellAoETimer(Mobile caster, Item scroll, InfoSpell.AoETimer info)
                : base(caster, scroll, (InfoSpell)info)
            {
                this.info = info;
            }

            public override void UseSpellAoETimer()
            {
                if (CheckSequence()) // Si le mana, les ingrédients... etc sont corrects.
                {
                    // CREATION DU TIMER.
                    effectTimer = new EffectTimer(this, info.duree, info.intervale);
                }
                else
                {
                    FinishSequence();
                }
            }

            // Un seul effect pour le spell (Ex Animation de feu sur le caster..) Avant que le timer débute.
            public abstract void UniqueEffect();

            // Effet au début du timer sur tous les targets.
            public abstract void OnStart(Mobile target);
            // Effet à chaque Tick du timer sur tous les targets.
            public abstract void OnTick(Mobile target);
            // Effet a la fin du timer sur tous les targets.
            public abstract void OnEnd(Mobile target);


            // Timer qui est utilisé après le temps de cast. Appelle les fonctions asbtract OnStart, OnTick, et OnEnd définies par l'utilisteur.
            private class EffectTimer : Timer
            {
                private CSpellAoETimer m_owner;
                private DateTime m_End;

                private int m_NumeroTick = 0;
                public int NumeroTick { get { return m_NumeroTick; } }

                public EffectTimer(CSpellAoETimer owner, TimeSpan duree, TimerPriority intervale)
                    : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
                {
                    m_End = DateTime.Now + duree;
                    m_owner = owner;

                    Priority = intervale;

                    m_owner.targetsList = m_owner.Caster.GetMobilesInRange(m_owner.info.range);

                    m_owner.UniqueEffect();

                    ChoixTarget(m_owner.OnStart);

                    Start();

                    if (!m_owner.info.continueCastDuringTimer)
                    {
                        m_owner.FinishSequence();
                    }
                }

                protected override void OnTick()
                {
                    m_NumeroTick++;
                    // Si le temps n'est pas fini.
                    if (DateTime.Now < m_End)
                    {
                        ChoixTarget(m_owner.OnTick);
                    }
                    else // Si le timer est fini.
                    {
                        ChoixTarget(m_owner.OnEnd);

                        if (m_owner.info.continueCastDuringTimer)
                        {
                            m_owner.FinishSequence();
                        }

                        Stop();
                    }
                }

                // Décide si on doit utiliser la targetlist settée au début, ou si on doit se mettre à jour sur les nouveaux personnages en range.
                private void ChoixTarget(Action<Mobile> Fonction)
                {
                    if (m_owner.info.targetsDebutCast)
                    {
                        foreach (Mobile target in m_owner.targetsList)
                        {
                            Fonction(target);
                        }
                    }
                    else
                    {
                        foreach (Mobile target in m_owner.Caster.GetMobilesInRange(m_owner.info.range))
                        {
                            Fonction(target);
                        }
                    }
                }


                ~EffectTimer()
                {
                    m_owner.FinishSequence();
                }
            }
            public void StopSpell()
            {
                effectTimer.Stop();
            }
            public int NumeroTick
            {
                get { return effectTimer.NumeroTick; }
            }
        }

        // FONCTIONNEL !
        public abstract class CSpellSelf : CustomSpell
        {
            private InfoSpell.Self info;

            public CSpellSelf(Mobile caster, Item scroll, InfoSpell.Self info)
                : base(caster, scroll, (InfoSpell)info)
            {
                this.info = info;
            }

            public override void UseSpellSelf()
            {
                if (CheckSequence()) // Si le mana, les ingrédients... etc sont corrects.
                {
                    Effect(Caster);
                }
                FinishSequence();
            }

            public abstract void Effect(Mobile caster);
        }

        // FONCTIONNEL !
        public abstract class CSpellSelfTimer : CustomSpell
        {
            private InfoSpell.SelfTimer info;

            private EffectTimer effectTimer;

            public CSpellSelfTimer(Mobile caster, Item scroll, InfoSpell.SelfTimer info)
                : base(caster, scroll, (InfoSpell)info)
            {
                this.info = info;
            }

            public override void UseSpellSelfTimer()
            {
                if (CheckSequence()) // Si le mana, les ingrédients... etc sont corrects.
                {
                    // CREATION DU TIMER.
                    effectTimer = new EffectTimer(this, info.duree, info.intervale);
                }
                else
                {
                    FinishSequence();
                }
            }


            // Un seul effect pour le spell (Ex Animation de feu sur le caster..) Avant que le timer débute.
            public abstract void UniqueEffect();

            // Effet au début du timer sur tous les targets.
            public abstract void OnStart(Mobile target);
            // Effet à chaque Tick du timer sur tous les targets.
            public abstract void OnTick(Mobile target);
            // Effet a la fin du timer sur tous les targets.
            public abstract void OnEnd(Mobile target);

            // Timer qui est utilisé après le temps de cast. Appelle les fonctions asbtract OnStart, OnTick, et OnEnd définies par l'utilisteur.
            private class EffectTimer : Timer
            {
                private CSpellSelfTimer m_owner;
                private DateTime m_End;

                private int m_NumeroTick = 0;
                public int NumeroTick { get { return m_NumeroTick; } }

                public EffectTimer(CSpellSelfTimer owner, TimeSpan duree, TimerPriority intervale)
                    : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
                {
                    m_End = DateTime.Now + duree;
                    m_owner = owner;

                    Priority = intervale;

                    m_owner.UniqueEffect();

                    m_owner.OnStart(m_owner.Caster);
                    Start();

                    m_owner.FinishSequence();
                }

                protected override void OnTick()
                {
                    m_NumeroTick++;
                    // Si le temps n'est pas fini.
                    if (DateTime.Now < m_End)
                    {
                        m_owner.OnTick(m_owner.Caster);
                    }
                    else // Si le timer est fini.
                    {
                        m_owner.OnEnd(m_owner.Caster);

                        Stop();
                    }
                }

                ~EffectTimer()
                {
                    m_owner.FinishSequence();
                }
            }
            public void StopSpell()
            {
                effectTimer.Stop();
            }
            public int NumeroTick
            {
                get { return effectTimer.NumeroTick; }
            }

        }
    }
}
