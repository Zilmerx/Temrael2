﻿using Server.Engines.Equitation;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.TechniquesCombat;
using Server.Engines.Durability;
using Server.Spells.TechniquesCombat;
using Server.Mobiles.GuardsVersion2;

namespace Server.Engines.Combat
{
    /// <summary>
    /// Class servant de base au système de combat.
    /// Chaque type de combat (mêlée, distance, haches, créatures) va override cette class et 
    /// l'arme va utiliser ces methods pour engager le combat.
    /// </summary>
    [PropertyObject]
    public abstract class CombatStrategy
    {
        #region Sequence de Combat
        /// <summary>
        /// Détermine la séquence de combat d'une tentative de coup de l'attaquant au défenseur.
        /// </summary>
        /// <param name="atk">L'attaquant</param>
        /// <param name="def">Le défenseur</param>
        /// <returns>Le délai nécessaire avant de pouvoir porter le prochain coup.</returns>
        public int Sequence(Mobile atk, Mobile def)
        {
            GuardVer2.CheckOnHit(atk); // La séquence est activée seulement si le joueur attaquant est "InRange" du défenseur. Donc le warmode ne trigger pas le OnHit.

            if (!BandageContext.IsHealingSelf(atk))
            {
                if (Toucher(atk, def))
                {
                    def = ProtectionTechnique.GetOnHitEffect(def);
                    OnHit(atk, def);
                }
                else
                    OnMiss(atk, def);

                CheckEquitationAttaque(atk);
            }
            else
            {
                atk.SendMessage("Vous ne pouvez pas vous battre tout en vous soignant avec des bandages.");
            }
            
            return ProchaineAttaque(atk);
        }

        public virtual void OnHit(Mobile atk, Mobile def)
        {
            AttaqueAnimation(atk);
            DegatsAnimation(def);

            DurabilityHandler.OnPhysAttack(atk);
            DurabilityHandler.OnPhysDamageReceive(atk, def);

            CheckEquitation(def, EquitationType.BeingAttacked);

            atk.PlaySound(Weapon(atk).GetHitAttackSound(atk, def));
            def.PlaySound(Weapon(def).GetHitDefendSound(atk, def));

            double basedmg = (atk.Weapon as BaseWeapon).MinDamage + (Utility.RandomDouble() * ((atk.Weapon as BaseWeapon).MaxDamage - (atk.Weapon as BaseWeapon).MinDamage));
            double degats = Degats(basedmg, atk, def);

            Assassinat.Instance.OnHit(atk, def, ref degats);

            if (DefStrategy(def).Parer(def))
            {
                Effects.SendTargetEffect(def,0x37B9, 10, 16);
                def.Mana -= ParerCoutMana;
                degats = 0;
            }
            else if (Critique(atk) && atk.Mana > CritiqueManaCost(basedmg))
            {
                degats = CritiqueDegats(atk, degats);
                atk.Mana -= CritiqueManaCost(basedmg);
                atk.SendMessage("Vous effectuez un coup critique.");
                def.SendMessage("Vous recevez un coup critique.");
            }

            AppliquerPoison(atk, def);

            degats = Spell.OnHitEffects(atk, def, degats);

            atk.Stam -= (int)(basedmg * 0.60);

            def.Damage((int)degats, atk);

            atk.RevealingAction();
            def.RevealingAction();
        }

        public virtual void OnMiss(Mobile atk, Mobile def)
        {
            AttaqueAnimation(atk);
            atk.PlaySound(Weapon(atk).GetMissAttackSound(atk, def));
        }

        // Utilise seulement pour les armes a distance.
        public abstract bool OnFired(Mobile atk, Mobile def);

        public virtual void AttaqueAnimation(Mobile atk)
        {
            int action = Weapon(atk).SwingAnimation(atk);
            atk.Animate(action, 7, 1, true, false, 0);
        }
        
        public virtual void DegatsAnimation(Mobile def)
        {
            int action, frames;
            if (Weapon(def).PlayHurtAnimation(def, out action, out frames))
                def.Animate(action, frames, 1, true, false, 0);
        }

        //public delegate void EffetsAuxiliaires(Mobile atk, Mobile def);
        // Note: Should be in mobile.
        #endregion

        #region Equitation Check

        protected abstract void CheckEquitationAttaque(Mobile atk);

        public virtual void CheckEquitation(Mobile m, EquitationType type)
        {
            Equitation.Equitation.CheckEquitation(m, type);
        }
        #endregion

        #region Range
        [CommandProperty(AccessLevel.Batisseur)]
        public virtual int BaseRange { get { return 1; } }

        public virtual int Range(Mobile atk)
        {
            return BaseRange;
        }
        #endregion

        #region Toucher
        [CommandProperty(AccessLevel.Batisseur)]
        public abstract SkillName ToucherSkill { get; }

        public bool Toucher(Mobile atk, Mobile def)
        {
            double chance = ToucherChance(atk, def);

            if (chance < 0.02)
                chance = 0.02;

            if (!def.CanSee(atk))
            {
                CheckSkillGain(atk, SkillName.Poursuite);
                double poursuite = GetBonus(atk.Skills[SkillName.Poursuite].Value, 0.85);
                chance = IncreasedValue(chance, poursuite);
            }

            return atk.CheckSkill(ToucherSkill, chance);
        }

        protected virtual double ToucherChance(Mobile atk, Mobile def)
        {
            double atkvalue = atk.Skills[ToucherSkill].Value;
            double defvalue = def.Skills[(def.Weapon as BaseWeapon).Strategy.ToucherSkill].Value;

            double chance = (atkvalue + 20.0) * 150 / ((defvalue + 20.0) * 200);

            return chance;
        }
        #endregion

        #region Degats
        public virtual double Degats(double basedmg, Mobile atk, Mobile def)
        {
            double dmg = ComputerDegats(atk, basedmg, true);

            return (int)DegatsReduits(atk, def, dmg);
        }

        public int MinDegats(Mobile atk)
        {
            return (int)ComputerDegats(atk, (atk.Weapon as BaseWeapon).MinDamage, false);
        }

        public int MaxDegats(Mobile atk)
        {
            return (int)ComputerDegats(atk, (atk.Weapon as BaseWeapon).MaxDamage, false);
        }

        protected virtual double ComputerDegats(Mobile atk, double basedmg, bool skillup)
        {
            if (skillup)
            {
                CheckSkillGain(atk, SkillName.Tactiques);
                CheckSkillGain(atk, SkillName.Anatomie);
            }

            double strBonus = atk.Str * 0.35 / 100; // pas de bonus a 100 pour la force.
            double tactiqueBonus = GetBonus(atk.Skills[SkillName.Tactiques].Value, 0.55);
            double anatomyBonus = GetBonus(atk.Skills[SkillName.Anatomie].Value, 0.40);

            double exceptBonus = 0;

            if((atk.Weapon as BaseWeapon).Quality == WeaponQuality.Exceptional)
            {
                if (skillup)
                    CheckSkillGain(atk, SkillName.Polissage);
                exceptBonus += GetBonus(atk.Skills[SkillName.Polissage].Value, 0.30);
            }

            return basedmg * (1 + strBonus + tactiqueBonus + anatomyBonus + exceptBonus);
        }

        public virtual double DegatsReduits(Mobile atk, Mobile def, double dmg)
        {
            return Damage.instance.DegatsPhysiquesReduits(atk, def, dmg);
        }

        protected double GetBonus(double value, double scalar)
        {
            double bonus = value * scalar;

            if (value >= 100)
                bonus += scalar * 5; //5% de la valeur a 100 est ajoutee.

            return bonus / 100;
        }

        protected double ReduceValue(double value, double factor)
        {
            return value * (1 - factor);
        }

        protected double IncreasedValue(double value, double factor)
        {
            return value * (1 + factor);
        }

        protected double IncValueDimReturn(double value, double factor)
        {
            if (value >= 1) return 1;
            if (value < 0) value = 0;

            return (1 - value) * factor + value;
        }
        #endregion

        protected abstract void AppliquerPoison(Mobile atk, Mobile def);

        #region Coup Critique
        public virtual int CritiqueManaCost(double degats)
        {
            return (int)(degats * 0.75);
        }

        public bool Critique(Mobile atk)
        {
            return atk.CheckSkill(SkillName.CoupCritique, CritiqueChance(atk));
        }

        public virtual double CritiqueChance(Mobile atk)
        {
            double chance = GetBonus(atk.Skills[SkillName.CoupCritique].Value, 0.25);
            return chance;
        }

        public virtual double CritiqueDegats(Mobile atk, double dmg)
        {
            // Tentative de dégâts % constants.
            return IncreasedValue(dmg, 0.5);
        }
        #endregion

        #region Vitesse
        /// <summary>
        /// Calcule le délai avant la prochaine attaque basé sur la vitesse du personnage.
        /// </summary>
        /// <param name="atk">Le personnage portant l'attaque.</param>
        /// <returns>Le délai en millisecondes.</returns>
        public virtual int ProchaineAttaque(Mobile atk)
        {
            int vitesse = (int)(CalculerVitesse(atk) * 100);

            return vitesse;
        }

        public void ResetAttackAfterCast(Mobile atk)
        {
            double delay = ProchaineAttaque(atk);

            CheckSkillGain(atk, SkillName.MagieDeGuerre);

            double magie = GetBonus(atk.Skills[SkillName.MagieDeGuerre].Value, 0.85);
            delay = ReduceValue(delay, magie);

            long ticks = Core.TickCount + Core.GetTicks(TimeSpan.FromMilliseconds(delay));
            if (atk.NextCombatTime < ticks)
                atk.NextCombatTime = ticks;
        }

        /// <summary>
        /// Calcule la vitesse du personnage en fonction de la vitesse de son arme (celle-ci calculée en dixième de seconde)
        /// </summary>
        /// <param name="atk">Le personnage portant l'attaque.</param>
        /// <returns>Retourne le délai entre deux attaques en dixième de seconde.</returns>
        public double CalculerVitesse(Mobile atk)
        {
            return Vitesse.instance.CalculerVitesse(atk, Weapon(atk).Speed);
        }
        #endregion

        #region Parer

        const int ParerCoutMana = 25;

        /// <summary>
        /// Cette fonction sert à déterminer si le défenseur a paré le coup.
        /// Elle est appelée du module de stratégie du défendeur et utilise donc les informations de la class CombatStrategy courante.
        /// </summary>
        /// <param name="def">Le défendeur de l'attaque.</param>
        /// <returns>true si le défendeur a paré le coup.</returns>
        public bool Parer(Mobile def)
        {
            if (def.Mana <= ParerCoutMana)
                return false;

            double chance = ParerChance(def);

            // On donne la possibilite d'augmenter Parer seulement s'il y a chance de parer
            // Cela exclue donc a mains nues ou a distance
            if (chance > 0 || (def.FindItemOnLayer(Layer.TwoHanded) as BaseShield) != null)
                return def.CheckSkill(SkillName.Parer, ParerChance(def));
            return false;
        }

        protected virtual double BaseParerChance(Mobile def)
        {
            double parry = def.Skills[SkillName.Parer].Value;

            return GetBonus(parry, 0.15);
        }

        protected virtual double ParerChance(Mobile def)
        {
            double chance = BaseParerChance(def);

            if (def.Spell != null && def.Spell.IsCasting)
            {
                double magie = GetBonus(def.Skills[SkillName.Parer].Value, 0.95);

                chance = ReduceValue(chance, ReduceValue(0.75, magie));
            }

            return chance;
        }
        #endregion

        protected CombatStrategy DefStrategy(Mobile def)
        {
            return (def.Weapon as BaseWeapon).Strategy;
        }

        public static BaseWeapon Weapon(Mobile m)
        {
            return m.Weapon as BaseWeapon;
        }

        public static CombatStrategy GetStrategy(Mobile m)
        {
            BaseWeapon w = Weapon(m);
            if (w != null)
                return w.Strategy;

            return null;
        }

        // Test passif pour un gain de skill
        protected void CheckSkillGain(Mobile m, SkillName skill)
        {
            m.CheckSkill(skill, 0.0, m.Skills[skill].Cap);
        }
    }
}
