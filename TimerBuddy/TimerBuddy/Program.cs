﻿using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System;
using System.Collections.Generic;

namespace TimerBuddy
{
    public static class Program
    {
        public static List<Spell> SpellList = new List<Spell>();
        public static List<SpellCaster> CasterList = new List<SpellCaster>();
        public static List<WardCaster> WardCasterList = new List<WardCaster>();
        public static List<SC2Timer> SC2TimerList = new List<SC2Timer>();

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
