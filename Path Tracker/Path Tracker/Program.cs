using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using System;
using System.Linq;
using Color = System.Drawing.Color;

namespace Path_Tracker
{
    public class Program
    {
        public static Menu Menu;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Menu = MainMenu.AddMenu("Path Tracker", "Path Tracker");
            Menu.AddGroupLabel("Drawing");
            Menu.Add("me", new CheckBox("My Path", false));
            Menu.Add("ally", new CheckBox("Ally Path", false));
            Menu.Add("enemy", new CheckBox("Enemy Path", true));
            Menu.AddLabel("Misc");
            Menu.Add("toggle", new KeyBind("Toggle On/Off", true, KeyBind.BindTypes.PressToggle, 'G'));
            Menu.Add("eta", new CheckBox("Estimated time of arrival", false));
            Menu.Add("name", new CheckBox("Champion Name", true));
            Menu.Add("thick", new Slider("Line Thickness", 2, 1, 5));
            Menu.AddGroupLabel("Disable while use orbwalk");
            Menu.Add("combo", new CheckBox("Combo", true));
            Menu.Add("harass", new CheckBox("Harass", true));
            Menu.Add("laneclear", new CheckBox("LaneClear", true));
            Menu.Add("lasthit", new CheckBox("LastHit", true));
            Menu.Add("flee", new CheckBox("Flee", false));

            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (!Menu["toggle"].Cast<KeyBind>().CurrentValue)
                return;

            var ETA = Menu["eta"].Cast<CheckBox>().CurrentValue;
            var Name = Menu["name"].Cast<CheckBox>().CurrentValue;
            var Thickness = Menu["thick"].Cast<Slider>().CurrentValue;

            if (Menu["me"].Cast<CheckBox>().CurrentValue && Enable())
            {
                DrawPath(Player.Instance, Thickness, Color.LawnGreen);

                if (ETA && Player.Instance.Path.Length > 1)
                {
                    Drawing.DrawText(Player.Instance.Path[Player.Instance.Path.Length - 1].WorldToScreen(), Color.NavajoWhite, GetETA(Player.Instance), 10);
                }
            }

            if (Menu["ally"].Cast<CheckBox>().CurrentValue)
            {
                foreach (var ally in EntityManager.Heroes.Allies.Where(a => !a.IsMe && a.IsValid))
                {
                    DrawPath(ally, Thickness, Color.Orange);

                    if (ally.Path.Length > 1)
                    {
                        if (Name)
                        {
                            Drawing.DrawText(ally.Path[ally.Path.Length - 1].WorldToScreen(), Color.LightSkyBlue, ally.BaseSkinName, 10);
                        }
                        if (ETA)
                        {
                            Drawing.DrawText(ally.Path[ally.Path.Length - 1].WorldToScreen() + new Vector2(0, 20), Color.NavajoWhite, GetETA(ally), 10);
                        }
                    }
                }
            }

            if (Menu["enemy"].Cast<CheckBox>().CurrentValue)
            {
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(e => e.IsValid))
                {
                    DrawPath(enemy, Thickness, Color.Red);

                    if (enemy.Path.Length > 1)
                    {
                        if (Name)
                        {
                            Drawing.DrawText(enemy.Path[enemy.Path.Length - 1].WorldToScreen(), Color.LightSkyBlue, enemy.BaseSkinName, 10);
                        }
                        if (ETA)
                        {
                            Drawing.DrawText(enemy.Path[enemy.Path.Length - 1].WorldToScreen() + new Vector2(0, 20), Color.NavajoWhite, GetETA(enemy), 10);
                        }
                    }
                }
            }
        }

        public static void DrawPath(AIHeroClient unit, int thickness, Color color)
        {
            for (var i = 1; unit.Path.Length > i; i++)
            {
                if (unit.Path[i - 1].IsValid() && unit.Path[i].IsValid() && (unit.Path[i - 1].IsOnScreen() || unit.Path[i].IsOnScreen()))
                {
                    Drawing.DrawLine(Drawing.WorldToScreen(unit.Path[i - 1]), Drawing.WorldToScreen(unit.Path[i]), thickness, color);
                }
            }
        }

        public static string GetETA(AIHeroClient unit)
        {
            float Distance = 0;

            if (unit.Path.Length > 1)
            {
                for (var i = 1; unit.Path.Length > i; i++)
                {
                    Distance += unit.Path[i - 1].Distance(unit.Path[i]);
                }
            }
            
            return (Distance / unit.MoveSpeed).ToString("F1");
        }

        public static bool Enable()
        {
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo && Menu["combo"].Cast<CheckBox>().CurrentValue)
                return false;
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass && Menu["harass"].Cast<CheckBox>().CurrentValue)
                return false;
            if ((Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.LaneClear || Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.JungleClear) && Menu["laneclear"].Cast<CheckBox>().CurrentValue)
                return false;
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.LastHit && Menu["lasthit"].Cast<CheckBox>().CurrentValue)
                return false;
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Flee && Menu["flee"].Cast<CheckBox>().CurrentValue)
                return false;
            return true;
        }
    }
}
