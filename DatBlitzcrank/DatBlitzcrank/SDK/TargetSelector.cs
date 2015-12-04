﻿#region LICENSE

/*
 Copyright 2014 - 2014 LeagueSharp
 TargetSelector.cs is part of LeagueSharp.Common.
 
 LeagueSharp.Common is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.
 
 LeagueSharp.Common is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 GNU General Public License for more details.
 
 You should have received a copy of the GNU General Public License
 along with LeagueSharp.Common. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

#region

using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using Color = System.Drawing.Color;

#endregion

namespace DatBlitzcrank.SDK
{
    public class TargetSelector
    {
        #region Enum

        public enum DamageType
        {
            Magical,
            Physical,
            True
        }

        public enum TargetingMode
        {
            AutoPriority,
            LowHP,
            MostAD,
            MostAP,
            Closest,
            NearMouse,
            LessAttack,
            LessCast
        }

        #endregion

        #region Vars

        public static TargetingMode Mode = TargetingMode.AutoPriority;
        private static Menu Menu;
        private static AIHeroClient _selectedTargetObjAiHero;

        private static bool UsingCustom;

        public static bool CustomTS
        {
            get { return UsingCustom; }
            set
            {
                UsingCustom = value;
                if (value)
                {
                    Game.OnWndProc -= GameOnOnWndProc;
                    Drawing.OnDraw -= DrawingOnOnDraw;
                }
                else
                {
                    Game.OnWndProc += GameOnOnWndProc;
                    Drawing.OnDraw += DrawingOnOnDraw;
                }
            }
        }

        #endregion

        #region EventArgs

        private static void DrawingOnOnDraw(EventArgs args)
        {
            if (_selectedTargetObjAiHero.IsValidTarget() &&
                Menu["FocusSelected"].Cast<CheckBox>().CurrentValue)
            {
                Render.Circle.DrawCircle(
                    _selectedTargetObjAiHero.Position, 150, Color.Red, 7,
                    true);
            }

            var a = (Menu["ForceFocusSelectedK"].Cast<KeyBind>().CurrentValue ||
                      Menu["ForceFocusSelectedK2"].Cast<KeyBind>().CurrentValue &&
                      Menu["ForceFocusSelectedKeys"].Cast<CheckBox>().CurrentValue);

            //_configMenu.Item("ForceFocusSelectedKeys").Permashow(SelectedTarget != null && a);
            //_configMenu.Item("ForceFocusSelected").Permashow(_configMenu.Item("ForceFocusSelected").GetValue<bool>());
        }

        private static void GameOnOnWndProc(WndEventArgs args)
        {
            if (args.Msg != (uint)WindowsMessages.WM_LBUTTONDOWN)
            {
                return;
            }
            _selectedTargetObjAiHero =
                HeroManager.Enemies
                    .FindAll(hero => hero.IsValidTarget() && hero.Distance(Game.CursorPos, true) < 40000) // 200 * 200
                    .OrderBy(h => h.Distance(Game.CursorPos, true)).FirstOrDefault();
        }

        #endregion

        #region Functions

        public static AIHeroClient SelectedTarget
        {
            get
            {
                return (Menu["FocusSelected"].Cast<CheckBox>().CurrentValue
                    ? _selectedTargetObjAiHero
                    : null);
            }
        }

        /// <summary>
        ///     Sets the priority of the hero
        /// </summary>
        public static void SetPriority(AIHeroClient hero, int newPriority)
        {
            var p = Menu["Priority" + hero.ChampionName].Cast<Slider>().CurrentValue;
            p = Math.Max(1, Math.Min(5, newPriority));
            Menu["Priority" + hero.ChampionName].Cast<Slider>().CurrentValue = p;
        }

        /// <summary>
        ///     Returns the priority of the hero
        /// </summary>
        public static float GetPriority(AIHeroClient hero)
        {
            var p = 1;
                p = Menu["Priority" + hero.ChampionName].Cast<Slider>().CurrentValue;

            switch (p)
            {
                case 2:
                    return 1.5f;
                case 3:
                    return 1.75f;
                case 4:
                    return 2f;
                case 5:
                    return 2.5f;
                default:
                    return 1f;
            }
        }

        private static int GetPriorityFromDb(string championName)
        {
            string[] p1 =
            {
                "Alistar", "Amumu", "Bard", "Blitzcrank", "Braum", "Cho'Gath", "Dr. Mundo", "Garen", "Gnar",
                "Hecarim", "Janna", "Jarvan IV", "Leona", "Lulu", "Malphite", "Nami", "Nasus", "Nautilus", "Nunu",
                "Olaf", "Rammus", "Renekton", "Sejuani", "Shen", "Shyvana", "Singed", "Sion", "Skarner", "Sona",
                "Soraka", "Taric", "TahmKench", "Thresh", "Volibear", "Warwick", "MonkeyKing", "Yorick", "Zac", "Zyra"
            };

            string[] p2 =
            {
                "Aatrox", "Darius", "Elise", "Evelynn", "Galio", "Gangplank", "Gragas", "Irelia", "Jax",
                "Lee Sin", "Maokai", "Morgana", "Nocturne", "Pantheon", "Poppy", "Rengar", "Rumble", "Ryze", "Swain",
                "Trundle", "Tryndamere", "Udyr", "Urgot", "Vi", "XinZhao", "RekSai"
            };

            string[] p3 =
            {
                "Akali", "Diana", "Ekko", "Fiddlesticks", "Fiora", "Fizz", "Heimerdinger", "Jayce", "Kassadin",
                "Kayle", "Kha'Zix", "Lissandra", "Mordekaiser", "Nidalee", "Riven", "Shaco", "Vladimir", "Yasuo",
                "Zilean"
            };

            string[] p4 =
            {
                "Ahri", "Anivia", "Annie", "Ashe", "Azir", "Brand", "Caitlyn", "Cassiopeia", "Corki", "Draven",
                "Ezreal", "Graves", "Jinx", "Kalista", "Karma", "Karthus", "Katarina", "Kennen", "KogMaw", "Kindred",
                "Leblanc", "Lucian", "Lux", "Malzahar", "MasterYi", "MissFortune", "Orianna", "Quinn", "Sivir", "Syndra",
                "Talon", "Teemo", "Tristana", "TwistedFate", "Twitch", "Varus", "Vayne", "Veigar", "Velkoz", "Viktor",
                "Xerath", "Zed", "Ziggs"
            };

            if (p1.Contains(championName))
            {
                return 1;
            }
            if (p2.Contains(championName))
            {
                return 2;
            }
            if (p3.Contains(championName))
            {
                return 3;
            }
            return p4.Contains(championName) ? 4 : 1;
        }


        internal static void Initialize()
        {
            CustomEvents.Game.OnGameLoad += args =>
            {
                Menu = MenuSDK.Menu.AddSubMenu("Target Selector", "TargetSelector");

                Menu.AddGroupLabel("Focus Target Settings");
                Menu.Add("FocusSelected", new CheckBox("Focus selected target", true));
                Menu.Add("ForceFocusSelected", new CheckBox("Only attack selected target", false));
                Menu.Add("ForceFocusSelectedKeys", new CheckBox("Enable only attack selected Keys", false));
                Menu.Add("ForceFocusSelectedK", new KeyBind("Only attack selected Key", false, KeyBind.BindTypes.HoldActive, 32));
                Menu.Add("ForceFocusSelectedK2", new KeyBind("ForceFocusSelectedK2", false, KeyBind.BindTypes.HoldActive, 32));

                Menu.AddGroupLabel("TargetSelector");
                Menu.AddLabel("5 = Highest Priority");
                foreach (var enemy in EntityManager.Heroes.Enemies)
                {
                    Menu.Add("Priority" + enemy.ChampionName, new Slider(enemy.ChampionName, 1, 5, 1));
                }

                Menu.Add("TargetingMode", new Slider("Target Mode", 1, 8, 1));
                Menu.AddLabel("1: LowHP, 2: MostAD, 3: MostAP, 4: Closest");
                Menu.AddLabel("5: NearMouse, 6: AutoPriority, 7: LessAttack, 8: LessCast");

                /*
                Menu focusMenu = new Menu("Focus Target Settings", "FocusTargetSettings");

                focusMenu.AddItem(new MenuItem("FocusSelected", "Focus selected target").SetShared().SetValue(true));
                focusMenu.AddItem(
                    new MenuItem("SelTColor", "Focus selected target color").SetShared().SetValue(new Circle(true, Color.Red)));
                focusMenu.AddItem(
                    new MenuItem("ForceFocusSelected", "Only attack selected target").SetShared().SetValue(false));
                focusMenu.AddItem(new MenuItem("sep", ""));
                focusMenu.AddItem(
                    new MenuItem("ForceFocusSelectedKeys", "Enable only attack selected Keys").SetShared().SetValue(false));
                focusMenu.AddItem(
                    new MenuItem("ForceFocusSelectedK", "Only attack selected Key"))
                    .SetValue(new KeyBind(32, KeyBindType.Press));
                focusMenu.AddItem(
                    new MenuItem("ForceFocusSelectedK2", "ForceFocusSelectedK2"))
                    .SetValue(new KeyBind(32, KeyBindType.Press));

                config.AddSubMenu(focusMenu);

                var autoPriorityItem =
                    new MenuItem("AutoPriority", "Auto arrange priorities").SetShared().SetValue(false).SetTooltip("5 = Highest Priority");
                    */
                //autoPriorityItem.ValueChanged += autoPriorityItem_ValueChanged;
                /*
                foreach (var enemy in HeroManager.Enemies)
                {
                    config.AddItem(
                        new MenuItem("TargetSelector" + enemy.ChampionName + "Priority", enemy.ChampionName).SetShared()
                            .SetValue(
                                new Slider(
                                    autoPriorityItem.GetValue<bool>() ? GetPriorityFromDb(enemy.ChampionName) : 1, 5, 1)));
                    if (autoPriorityItem.GetValue<bool>())
                    {
                        config.Item("TargetSelector" + enemy.ChampionName + "Priority")
                            .SetValue(
                                new Slider(
                                    autoPriorityItem.GetValue<bool>() ? GetPriorityFromDb(enemy.ChampionName) : 1, 5, 1));
                    }
                }
                config.AddItem(autoPriorityItem);
                config.AddItem(
                    new MenuItem("TargetingMode", "Target Mode").SetShared()
                        .SetValue(new StringList(Enum.GetNames(typeof(TargetingMode)))));

                CommonMenu.Config.AddSubMenu(config);
                */

                Game.OnWndProc += GameOnOnWndProc;
                Drawing.OnDraw += DrawingOnOnDraw;
            };
        }
        /*
        public static void AddToMenu(Menu config)
        {
            config.AddItem(new MenuItem("Alert", "----Use TS in Common Menu----"));
        }
        
        private static void autoPriorityItem_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            if (!e.GetNewValue<bool>())
            {
                return;
            }
            foreach (var enemy in HeroManager.Enemies)
            {
                _configMenu.Item("TargetSelector" + enemy.ChampionName + "Priority")
                    .SetValue(new Slider(GetPriorityFromDb(enemy.ChampionName), 5, 1));
            }
        }
        */
        public static bool IsInvulnerable(Obj_AI_Base target, DamageType damageType, bool ignoreShields = true)
        {
            //Kindred's Lamb's Respite(R)

            if (target.HasBuff("kindredrnodeathbuff") && target.HealthPercent <= 10)
            {
                return true;
            }

            // Tryndamere's Undying Rage (R)
            if (target.HasBuff("Undying Rage") && target.Health <= target.MaxHealth * 0.10f)
            {
                return true;
            }

            // Kayle's Intervention (R)
            if (target.HasBuff("JudicatorIntervention"))
            {
                return true;
            }

            // Poppy's Diplomatic Immunity (R)
            if (target.HasBuff("DiplomaticImmunity") && !ObjectManager.Player.HasBuff("poppyulttargetmark"))
            {
                //TODO: Get the actual target mark buff name
                return true;
            }

            if (ignoreShields)
            {
                return false;
            }

            // Morgana's Black Shield (E)
            if (damageType.Equals(DamageType.Magical) && target.HasBuff("BlackShield"))
            {
                return true;
            }

            // Banshee's Veil (PASSIVE)
            if (damageType.Equals(DamageType.Magical) && target.HasBuff("BansheesVeil"))
            {
                // TODO: Get exact Banshee's Veil buff name.
                return true;
            }

            // Sivir's Spell Shield (E)
            if (damageType.Equals(DamageType.Magical) && target.HasBuff("SivirShield"))
            {
                // TODO: Get exact Sivir's Spell Shield buff name
                return true;
            }

            // Nocturne's Shroud of Darkness (W)
            if (damageType.Equals(DamageType.Magical) && target.HasBuff("ShroudofDarkness"))
            {
                // TODO: Get exact Nocturne's Shourd of Darkness buff name
                return true;
            }

            return false;
        }


        public static void SetTarget(AIHeroClient hero)
        {
            if (hero.IsValidTarget())
            {
                _selectedTargetObjAiHero = hero;
            }
        }

        public static AIHeroClient GetSelectedTarget()
        {
            return SelectedTarget;
        }

        public static AIHeroClient GetTarget(float range,
            DamageType damageType,
            bool ignoreShield = true,
            IEnumerable<AIHeroClient> ignoredChamps = null,
            Vector3? rangeCheckFrom = null)
        {
            return GetTarget(ObjectManager.Player, range, damageType, ignoreShield, ignoredChamps, rangeCheckFrom);
        }

        public static AIHeroClient GetTargetNoCollision(Spell spell,
            bool ignoreShield = true,
            IEnumerable<AIHeroClient> ignoredChamps = null,
            Vector3? rangeCheckFrom = null)
        {
            var t = GetTarget(ObjectManager.Player, spell.Range,
                spell.DamageType, ignoreShield, ignoredChamps, rangeCheckFrom);

            if (spell.Collision && spell.GetPrediction(t).Hitchance != HitChance.Collision)
            {
                return t;
            }

            return null;
        }

        private static bool IsValidTarget(Obj_AI_Base target,
            float range,
            DamageType damageType,
            bool ignoreShieldSpells = true,
            Vector3? rangeCheckFrom = null)
        {
            return target.IsValidTarget() &&
                   target.Distance(rangeCheckFrom ?? ObjectManager.Player.ServerPosition, true) <
                   Math.Pow(range <= 0 ? Orbwalking.GetRealAutoAttackRange(target) : range, 2) &&
                   !IsInvulnerable(target, damageType, ignoreShieldSpells);
        }

        public static AIHeroClient GetTarget(Obj_AI_Base champion,
            float range,
            DamageType type,
            bool ignoreShieldSpells = true,
            IEnumerable<AIHeroClient> ignoredChamps = null,
            Vector3? rangeCheckFrom = null)
        {
            try
            {
                if (ignoredChamps == null)
                {
                    ignoredChamps = new List<AIHeroClient>();
                }

                var damageType = (Damage.DamageType)Enum.Parse(typeof(Damage.DamageType), type.ToString());

                if (IsValidTarget(
                    SelectedTarget, Menu["ForceFocusSelected"].Cast<CheckBox>().CurrentValue ? float.MaxValue : range,
                    type, ignoreShieldSpells, rangeCheckFrom))
                {
                    return SelectedTarget;
                }

                if (IsValidTarget(
                    SelectedTarget, Menu["ForceFocusSelectedKeys"].Cast<CheckBox>().CurrentValue ? float.MaxValue : range,
                    type, ignoreShieldSpells, rangeCheckFrom))
                {
                    if (Menu["ForceFocusSelectedK"].Cast<KeyBind>().CurrentValue ||
                        Menu["ForceFocusSelectedK2"].Cast<KeyBind>().CurrentValue)
                    {
                        return SelectedTarget;
                    }
                }

                if (Mode == TargetingMode.AutoPriority)
                {
                    var menuItem = Menu["TargetingMode"].Cast<Slider>().CurrentValue;
                }

                var targets =
                    EntityManager.Heroes.Enemies
                        .FindAll(
                            hero =>
                                ignoredChamps.All(ignored => ignored.NetworkId != hero.NetworkId) &&
                                IsValidTarget(hero, range, type, ignoreShieldSpells, rangeCheckFrom));
                
                switch (Mode)
                {
                    case TargetingMode.LowHP:
                        return targets.MinOrDefault(hero => hero.Health);

                    case TargetingMode.MostAD:
                        return targets.MaxOrDefault(hero => hero.BaseAttackDamage + hero.FlatPhysicalDamageMod);

                    case TargetingMode.MostAP:
                        return targets.MaxOrDefault(hero => hero.BaseAbilityDamage + hero.FlatMagicDamageMod);

                    case TargetingMode.Closest:
                        return
                            targets.MinOrDefault(
                                hero =>
                                    (rangeCheckFrom.HasValue ? rangeCheckFrom.Value : champion.ServerPosition).Distance(
                                        hero.ServerPosition, true));

                    case TargetingMode.NearMouse:
                        return targets.MinOrDefault(hero => hero.Distance(Game.CursorPos, true));

                    case TargetingMode.AutoPriority:
                        return
                            targets.MaxOrDefault(
                                hero =>
                                    champion.CalcDamage(hero, damageType, 100) / (1 + hero.Health) * GetPriority(hero));

                    case TargetingMode.LessAttack:
                        return
                            targets.MaxOrDefault(
                                hero =>
                                    champion.CalcDamage(hero, Damage.DamageType.Physical, 100) / (1 + hero.Health) *
                                    GetPriority(hero));

                    case TargetingMode.LessCast:
                        return
                            targets.MaxOrDefault(
                                hero =>
                                    champion.CalcDamage(hero, Damage.DamageType.Magical, 100) / (1 + hero.Health) *
                                    GetPriority(hero));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        #endregion
    }

    /// <summary>
    ///     This TS attempts to always lock the same target, useful for people getting targets for each spell, or for champions
    ///     that have to burst 1 target.
    /// </summary>
    public class LockedTargetSelector
    {
        public static AIHeroClient _lastTarget;
        private static TargetSelector.DamageType _lastDamageType;

        public static AIHeroClient GetTarget(float range,
            TargetSelector.DamageType damageType,
            bool ignoreShield = true,
            IEnumerable<AIHeroClient> ignoredChamps = null,
            Vector3? rangeCheckFrom = null)
        {
            if (_lastTarget == null || !_lastTarget.IsValidTarget() || _lastDamageType != damageType)
            {
                var newTarget = TargetSelector.GetTarget(range, damageType, ignoreShield, ignoredChamps, rangeCheckFrom);

                _lastTarget = newTarget;
                _lastDamageType = damageType;

                return newTarget;
            }

            if (_lastTarget.IsValidTarget(range) && damageType == _lastDamageType)
            {
                return _lastTarget;
            }

            var newTarget2 = TargetSelector.GetTarget(range, damageType, ignoreShield, ignoredChamps, rangeCheckFrom);

            _lastTarget = newTarget2;
            _lastDamageType = damageType;

            return newTarget2;
        }

        /// <summary>
        ///     Unlocks the currently locked target.
        /// </summary>
        public static void UnlockTarget()
        {
            _lastTarget = null;
        }
        /*
        public static void AddToMenu(Menu menu)
        {
            TargetSelector.AddToMenu(menu);
        }
        */
    }
}