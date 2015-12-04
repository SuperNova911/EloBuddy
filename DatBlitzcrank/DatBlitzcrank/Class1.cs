using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using DatBlitzcrank.SDK;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace DatBlitzcrank
{
    class Class1
    {
        static Class1()
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            MenuSDK.Initialize();
            SDK.Prediction.Initialize();
            SDK.TargetSelector.Initialize();

            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            var t = EloBuddy.SDK.TargetSelector.GetTarget(Config.SpellSetting.Q.MaxrangeQ, DamageType.Magical);

            SpellManager.Q.Cast()
            
        }

        public static void Initialize()
        {
        }
    }
}
