using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TimerBuddy
{
    public static class Program
    {
        public static List<Spell> SpellList = new List<Spell>();
        public static List<SpellCaster> CasterList = new List<SpellCaster>();
        public static List<WardCaster> WardCasterList = new List<WardCaster>();
        public static List<SC2Timer> SC2TimerList = new List<SC2Timer>();
        public static List<Spell> SpellDB = new List<Spell>();
        public static List<string> Hero = new List<string>();

        static void Main(string[] args)
        {
            try
            {
                Loading.OnLoadingComplete += Loading_OnLoadingComplete;
            }
            catch (Exception e)
            {
                e.ErrorMessage("MAIN");
            }
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            try
            {
                foreach (var h in EntityManager.Heroes.AllHeroes)
                    Hero.Add(h.BaseSkinName);

                foreach (Spell spell in SpellDatabase.Database.Where(d => d.SpellType != SpellType.Spell || (d.SpellType == SpellType.Spell && Hero.Contains(d.ChampionName))))
                    SpellDB.Add(spell);

                Hero.Clear();
                SpellDatabase.Database.Clear();

                Config.Initialize();
                TextureDraw.Initialize();
                ObjectDetector.Initialize();
                SC2TimerManager.Initialize();
                DrawManager.Initialize();
                //Debug.Initialize();
                
                Drawing.OnEndScene += Drawing_OnEndScene;
                
                Chat.Print("<font color='#9400D3'>TimerBuddy</font> <font color='#FFFFFF'>Loaded</font>");
            }
            catch (Exception e)
            {
                e.ErrorMessage("MAIN_INIT");
            }
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            try
            {
                Utility.CloneTracker();
            }
            catch (Exception e)
            {
                e.ErrorMessage("MAIN_DRAW");
            }            
        }
    }
}
