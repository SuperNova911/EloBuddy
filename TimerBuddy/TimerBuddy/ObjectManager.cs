﻿using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TimerBuddy.Properties;
using Color = SharpDX.Color;

namespace TimerBuddy
{
    public enum Team
    {
        None, Ally, Enemy, Neutral
    }

    public enum Importance
    {
        Low, Medium, High, VeryHigh
    }

    public enum DrawType
    {
        Default, Number, NumberLine, HPLine
    }

    public enum SpellType
    {
        SummonerSpell, Item, Trap, Spell, Ward, Blink
    }

    public class SpellCaster
    {
        public Obj_AI_Base Caster { get; set; }
        public SpellType SpellType { get; set; }
        public SpellSlot Slot { get; set; }
        public float EndTime { get; set; }
    }

    public class WardCaster
    {
        public Obj_AI_Base Caster { get; set; }
        public float EndTime { get; set; }
        public string Name { get; set; }
    }
    
    public class Spell
    {
        public SpellType SpellType = SpellType.Spell;
        public Team Team = Team.None;
        public SpellSlot Slot { get; set; }
        public DrawType DrawType = DrawType.Default;
        public Importance Importance = Importance.Medium;
        public Obj_AI_Base Caster { get; set; }
        public Obj_AI_Base Target { get; set; }
        public GameObject Object { get; set; }
        public Vector3 StartPosition { get; set; }
        public Vector3 CastPosition { get; set; }
        public string ChampionName { get; set; }
        public string Name { get; set; }
        public string ObjectName { get; set; }
        public string MenuCode { get; set; }
        public float FullTime { get; set; }
        public float EndTime { get; set; }
        public int NetworkID { get; set; }
        public int Range { get; set; }
        public bool Teleport = false;
        public bool GameObject = false;
        public bool SkillShot = false;
        public bool Targeting = false;
        public bool Cancel = false;
        public bool Buff = false;
        public bool OnlyMe = false;
        public bool Drawing = false;
        public Color Color = Color.White;
        public Bitmap SpriteName { get; set; }
    }

    internal static class SpellDatabase
    {
        public static readonly List<Spell> Database = new List<Spell>();
        private static readonly string[] Hero = EntityManager.Heroes.AllHeroes.Select(d => d.BaseSkinName).ToArray();

        static SpellDatabase()
        {
            Database = new List<Spell>
            {
                                
                #region Item
                new Spell { SpellType = SpellType.Item, Name = "ZhonyasHourglass", EndTime = 2500, MenuCode = "Zhonyas Hourglass", SpriteName = Resources.Zhonya_s_Hourglass, Importance = Importance.VeryHigh, DrawType = DrawType.NumberLine },
                //new Spell { SpellType = SpellType.Item, Name = "shurelyascrest", EndTime = 3000, MenuCode = "Talisman of Ascension", SpriteName = Resources.Talisman_of_Ascension },
                new Spell { SpellType = SpellType.Item, Buff = true, Name = "HealthBomb", MenuCode = "Face of the Mountain", SpriteName = Resources.Face_of_the_Mountain },
                new Spell { SpellType = SpellType.Item, Buff = true, Name = "IronStylusBuff", MenuCode = "Locket of the Iron Solari", SpriteName = Resources.Locket_of_the_Iron_Solari },
                new Spell { SpellType = SpellType.Item, Buff = true, Name = "SpectralFury", MenuCode = "Youmuu's Ghostblade", SpriteName = Resources.Youmuu_s_Ghostblade },
                new Spell { SpellType = SpellType.Item, Buff = true, Name = "HexdrunkEmpowered", MenuCode = "Hexdrinker", SpriteName = Resources.Hexdrinker },
                new Spell { SpellType = SpellType.Item, GameObject = true, Name = "LifeAura.troy", EndTime = 4000, MenuCode = "Guardian Angel", SpriteName = Resources.Guardian_Ange, Importance = Importance.VeryHigh },

                new Spell { SpellType = SpellType.Item, GameObject = true, Name = "Global_Trinket_Spotter.troy", EndTime = 6000, MenuCode = "Sweeping Lens", SpriteName = Resources.Sweeping_Lens__Trinket_ },
                new Spell { SpellType = SpellType.Item, Buff = true, Name = "TrinketSweeperLvl3", MenuCode = "Oracle Alteration", SpriteName = Resources.Oracle_Alteration },

                new Spell { SpellType = SpellType.Item, Buff = true, Name = "Health Potion", MenuCode = "Health Potion", SpriteName = Resources.Health_Potion },
                new Spell { SpellType = SpellType.Item, Buff = true, Name = "ItemMiniRegenPotion", MenuCode = "Biscuit", SpriteName = Resources.Total_Biscuit_of_Rejuvenation },
                new Spell { SpellType = SpellType.Item, Buff = true, Name = "ItemCrystalFlask", MenuCode = "Refillable_Potion", SpriteName = Resources.Refillable_Potion },
                new Spell { SpellType = SpellType.Item, Buff = true, Name = "ItemCrystalFlaskJungle", MenuCode = "Hunter's Potion", SpriteName = Resources.Hunter_s_Potion },
                new Spell { SpellType = SpellType.Item, Buff = true, Name = "ItemDarkCrystalFlask", MenuCode = "Corrupting Potion", SpriteName = Resources.Corrupting_Potion },
                new Spell { SpellType = SpellType.Item, Buff = true, Name = "ElixirOfWrath", MenuCode = "Elixir of Wrath", SpriteName = Resources.Elixir_of_Wrath, Importance = Importance.Low },
                new Spell { SpellType = SpellType.Item, Buff = true, Name = "ElixirOfSorcery", MenuCode = "Elixir of Sorcery", SpriteName = Resources.Elixir_of_Sorcery, Importance = Importance.Low },
                new Spell { SpellType = SpellType.Item, Buff = true, Name = "ElixirOfIron", MenuCode = "Elixir of Iron", SpriteName = Resources.Elixir_of_Iron, Importance = Importance.Low },
                #endregion

                #region Summoner Spell
                new Spell { SpellType = SpellType.SummonerSpell, Buff = true, Name = "Recall", MenuCode = "Recall", SpriteName = Resources.Recall, Importance = Importance.High },
                new Spell { SpellType = SpellType.SummonerSpell, GameObject = true, Teleport = true, Name = "global_ss_teleport_blue.troy", EndTime = 3500, MenuCode = "Summoner Teleport", SpriteName = Resources.Teleport, Importance = Importance.High },
                new Spell { SpellType = SpellType.SummonerSpell, GameObject = true, Teleport = true, Name = "global_ss_teleport_red.troy", EndTime = 3500, MenuCode = "Summoner Teleport", SpriteName = Resources.Teleport, Importance = Importance.High },
                new Spell { SpellType = SpellType.SummonerSpell, GameObject = true, Teleport = true, Name = "global_ss_teleport_target_", EndTime = 3500, MenuCode = "Summoner Teleport", SpriteName = Resources.Teleport, Importance = Importance.High },
                new Spell { SpellType = SpellType.SummonerSpell, GameObject = true, Teleport = true, Name = "global_ss_teleport_turret_", EndTime = 3500, MenuCode = "Summoner Teleport", SpriteName = Resources.Teleport, Importance = Importance.High },
                new Spell { SpellType = SpellType.SummonerSpell, Buff = true, Name = "SummonerIgnite", MenuCode = "Summoner Ignite", SpriteName = Resources.Ignite, Importance = Importance.High },
                new Spell { SpellType = SpellType.SummonerSpell, Buff = true, Name = "SummonerExhaustDebuff", MenuCode = "Summoner Exhaust", SpriteName = Resources.Exhaust, Importance = Importance.High },
                new Spell { SpellType = SpellType.SummonerSpell, Buff = true, Name = "SummonerBarrier", MenuCode = "Summoner Barrier", SpriteName = Resources.Barrier, Importance = Importance.High },
                new Spell { SpellType = SpellType.SummonerSpell, Buff = true, Name = "SummonerHeal", MenuCode = "Summoner Heal", SpriteName = Resources.Heal },
                new Spell { SpellType = SpellType.SummonerSpell, Buff = true, Name = "Cleanse", MenuCode = "Summoner Cleanse", SpriteName = Resources.Cleanse },
                new Spell { SpellType = SpellType.SummonerSpell, Buff = true, Name = "S4SpawnLockSpeed", MenuCode = "Summoner Haste", SpriteName = Resources.Haste },
                new Spell { SpellType = SpellType.SummonerSpell, Buff = true, Name = "SRHomeguardSpeed", MenuCode = "Summoner Haste", SpriteName = Resources.Haste },
                new Spell { SpellType = SpellType.SummonerSpell, Buff = true, Name = "Haste", MenuCode = "Summoner Haste", SpriteName = Resources.Haste },
                #endregion

                #region Blink
                new Spell { SpellType = SpellType.Blink, Name = "summonerflash", EndTime = 2000, Range = 425, SkillShot = true, MenuCode = "Summoner Flash", SpriteName = Resources.Flash, Color = Color.Yellow },
                new Spell { SpellType = SpellType.Blink, Name = "RiftWalk", EndTime = 2000, Range = 500, SkillShot = true, MenuCode = "Kassadin R", SpriteName = Resources.KassadinR, Color = Color.HotPink },
                new Spell { SpellType = SpellType.Blink, Name = "Deceive", EndTime = 2000, Range = 400, SkillShot = true, MenuCode = "Shaco Q", SpriteName = Resources.ShacoQ, Color = Color.Red },
                #endregion

                #region Ward
                new Spell { SpellType = SpellType.Ward, GameObject = true, Name = "TrinketTotemLvl1", ObjectName = "YellowTrinket", EndTime = 60000, MenuCode = "Warding Totem", SpriteName = Resources.Warding_Totem, Color = Color.Yellow },
                new Spell { SpellType = SpellType.Ward, GameObject = true, Name = "ItemGhostWard", ObjectName = "SightWard", EndTime = 150000, MenuCode = "Sightstone", SpriteName = Resources.Sightstone, Color = Color.LawnGreen },
                new Spell { SpellType = SpellType.Ward, GameObject = true, Name = "TrinketOrbLvl3", ObjectName = "BlueTrinket", EndTime = 77777777, MenuCode = "Farsight Alteration", SpriteName = Resources.Farsight_Totem, Color = Color.SkyBlue }, // Global_Trinket_ItemClairvoyance.troy
                new Spell { SpellType = SpellType.Ward, GameObject = true, Name = "VisionWard", ObjectName = "VisionWard", EndTime = 77777777, MenuCode = "VisionWard", SpriteName = Resources.Vision_Ward, Color = Color.HotPink },
                #endregion

                #region Trap
                new Spell { SpellType = SpellType.Trap, Slot = SpellSlot.R, GameObject = true, ChampionName = "Teemo", Name = "Noxious Trap", ObjectName = "TeemoMushroom", EndTime = 600000, MenuCode = "Teemo Trap", SpriteName = Resources.TeemoR },
                new Spell { SpellType = SpellType.Trap, Slot = SpellSlot.W, GameObject = true, ChampionName = "Nidalee", Name = "Noxious Trap", ObjectName = "NidaleeSpear", EndTime = 120000, MenuCode = "Nidalee Trap", SpriteName = Resources.NidaleeHumanW },
                new Spell { SpellType = SpellType.Trap, Slot = SpellSlot.W, GameObject = true, ChampionName = "Caitlyn", Name = "Cupcake Trap", EndTime = 90000, MenuCode = "Caitlyn Trap", SpriteName = Resources.CaitlynW },
                new Spell { SpellType = SpellType.Trap, Slot = SpellSlot.E, GameObject = true, ChampionName = "Jinx", Name = "JinxEMine", ObjectName = "Jinx Mine", EndTime = 5000, MenuCode = "Jinx Mine", SpriteName = Resources.JinxE },
                new Spell { SpellType = SpellType.Trap, Slot = SpellSlot.W, GameObject = true, ChampionName = "Shaco", Name = "Jack In The Box", ObjectName = "ShacoBox", EndTime = 60000, MenuCode = "Shaco Box", SpriteName = Resources.ShacoW },
                #endregion
                
                //SRU_RiftHerald_Relic_PickUp.troy   Add     Type: Obj_AI_Minion | Name: Rift Herald Relic | NetID: 1073745760 | objectName: SRU_RiftHerald_Relic
                //new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Shen", Name = "shen_Teleport_target_v2.troy", EndTime = 3000, MenuCode = "Shen R", SpriteName = Resources. },
                //new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Shen", Name = "ShenTeleport_v2.troy", EndTime = 3000, MenuCode = "Shen R", SpriteName = Resources. },
            };
            
            #region Spell
            if (Hero.Contains("Aatrox"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Aatrox", Name = "AatroxEConeMissile", MenuCode = "Aatrox E", SpriteName = Resources.AatroxE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Aatrox", Name = "AatroxR", MenuCode = "Aatrox R", SpriteName = Resources.AatroxR, Importance = Importance.High });
            };

            if (Hero.Contains("Ahri"))
            {                
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Ahri", Name = "", MenuCode = "Ahri W", SpriteName = Resources.AhriW });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Ahri", Name = "", MenuCode = "Ahri R", SpriteName = Resources.AhriR, Importance = Importance.High });
            };

            if (Hero.Contains("Akali"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Akali", Name = "AkaliMota", MenuCode = "Akali Q", SpriteName = Resources.AkaliQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Akali", Name = "Akali_Base_smoke_bomb_tar_team_", EndTime = 8000, MenuCode = "Akali W", SpriteName = Resources.AkaliW });
            };

            if (Hero.Contains("Alistar"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Alistar", Name = "Trample Buff", MenuCode = "Alistar Passive", SpriteName = Resources.AlistarPassive });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Alistar", EndTime = 7000, MenuCode = "Alistar R", SpriteName = Resources.AlistarR, Importance = Importance.High });
            };

            if (Hero.Contains("Amumu"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Amumu", Name = "CursedTouch", MenuCode = "Amumu Passive", SpriteName = Resources.AmumuPassive, OnlyMe = true, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Amumu", Name = "CurseoftheSadMummy", MenuCode = "Amumu R", SpriteName = Resources.AmumuR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Anivia"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Unknown, GameObject = true, ChampionName = "Anivia", Name = "EggTimer", EndTime = 6000, MenuCode = "Anivia Egg", SpriteName = Resources.AniviaPassive, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Anivia", EndTime = 5000, SkillShot = true, MenuCode = "Anivia W", SpriteName = Resources.AniviaW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Anivia", Name = "Chilled", MenuCode = "Anivia R", SpriteName = Resources.AniviaR });
            };

            if (Hero.Contains("Annie"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, ChampionName = "Annie", EndTime = 5000, MenuCode = "Annie E", SpriteName = Resources.AnnieE });
            };

            if (Hero.Contains("Ashe"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, ChampionName = "Ashe", EndTime = 4000, MenuCode = "Ashe Q", SpriteName = Resources.AsheQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Ashe", Name = "AsheQ", MenuCode = "Ashe Q Stack", SpriteName = Resources.AsheQ, OnlyMe = true });
            };

            if (Hero.Contains("Azir"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "Azir", Name = "Azir_Base_W_SoldierCape", EndTime = 9000, MenuCode = "Azir E", SpriteName = Resources.AzirE });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Azir", Name = "Azir_Base_R_SoldierCape", EndTime = 5000, MenuCode = "Azir R", SpriteName = Resources.AzirR });
            };

            if (Hero.Contains("Bard"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Bard", Name = "BardPChimePickupSpeedBoost", MenuCode = "Bard P", SpriteName = Resources.BardPassive, OnlyMe = true });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Bard", Name = "BardQShackleDebuff", MenuCode = "Bard Q", SpriteName = Resources.BardQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Bard", Name = "Bard_Base_W_pack_base.troy", EndTime = 10000, MenuCode = "Bard W", SpriteName = Resources.BardW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "Bard", Name = "Bard_Base_E_Corridor.troy", EndTime = 10000, MenuCode = "Bard E", SpriteName = Resources.BardE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Bard", Name = "BardRStasis", MenuCode = "Bard R", SpriteName = Resources.BardR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Blitzcrank"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Blitzcrank", Name = "ManaBarrier", MenuCode = "Blitzcrank Passive", SpriteName = Resources.BlitzcrankPassive, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Blitzcrank", Name = "Overdrive", MenuCode = "Blitzcrank W", SpriteName = Resources.BlitzcrankW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Blitzcrank", Name = "PowerFist", MenuCode = "Blitzcrank E", SpriteName = Resources.BlitzcrankE });
            };

            if (Hero.Contains("Brand"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Brand", Name = "BrandAblaze", MenuCode = "Brand Passive", SpriteName = Resources.BrandPassive });
            };

            if (Hero.Contains("Braum"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Braum", Name = "", MenuCode = "Braum Passive", SpriteName = Resources.BraumPassive, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Braum", Name = "", MenuCode = "Braum Q", SpriteName = Resources.BraumQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Braum", Name = "", MenuCode = "Braum W", SpriteName = Resources.BraumW });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Braum", Name = "", MenuCode = "Braum E", SpriteName = Resources.BraumE, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Braum", Name = "", EndTime = 4000, MenuCode = "Braum R", SpriteName = Resources.BraumR });
            };

            if (Hero.Contains("Caitlyn"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, ChampionName = "Caitlyn", EndTime = 500, MenuCode = "Caitlyn Q", SpriteName = Resources.CaitlynQ, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Caitlyn", EndTime = 1000, MenuCode = "Caitlyn R", SpriteName = Resources.CaitlynR, Importance = Importance.High });
            };

            if (Hero.Contains("Cassiopeia"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Cassiopeia", Name = "CassiopeiaNoxiousBlast", MenuCode = "Cassiopeia Q", SpriteName = Resources.CassiopeiaQ, OnlyMe = true, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Cassiopeia", EndTime = 7000, SkillShot = true, MenuCode = "Cassiopeia W", SpriteName = Resources.CassiopeiaW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Cassiopeia", Name = "CassiopeiaMiasma", MenuCode = "Cassiopeia W", SpriteName = Resources.CassiopeiaW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Cassiopeia", Name = "CassiopeiaTwinFangDebuff", MenuCode = "Cassiopeia E", SpriteName = Resources.CassiopeiaE, Importance = Importance.Low });
            };

            if (Hero.Contains("Corki"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Corki", Name = "CorkiLoaded", MenuCode = "Corki Package", SpriteName = Resources.CorkiBoom });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Corki", Name = "PhosphorusBomb", MenuCode = "Corki Q", SpriteName = Resources.CorkiQ, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Corki", Name = "Corki_Base_W_tar.troy", EndTime = 2000, MenuCode = "Corki W", SpriteName = Resources.CorkiW, Importance = Importance.Low, DrawType = DrawType.Number });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Corki", Name = "Corki_Base_W_AoE_ground.troy", EndTime = 5000, MenuCode = "Corki W", SpriteName = Resources.CorkiW, Importance = Importance.Low, DrawType = DrawType.Number });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, ChampionName = "Corki", EndTime = 4000, MenuCode = "Corki E", SpriteName = Resources.CorkiE });
            };

            if (Hero.Contains("Darius"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Darius", Name = "DariusHemo", MenuCode = "Darius Passive", SpriteName = Resources.DariusPassive });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Darius", Name = "DariusHemoMax", MenuCode = "Darius Passive", SpriteName = Resources.DariusPassive, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Darius", Name = "DariusExecuteMulticast", MenuCode = "Darius R", SpriteName = Resources.DariusR, Importance = Importance.High });
            };

            if (Hero.Contains("Diana"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Diana", Name = "DianaPassiveMarker", MenuCode = "Diana Passive", SpriteName = Resources.DianaP, OnlyMe = true });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Diana", Name = "DianaMoonlight", MenuCode = "Diana Q", SpriteName = Resources.DianaQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Diana", Name = "DianaOrbs", MenuCode = "Diana W", SpriteName = Resources.DianaW });
            };

            if (Hero.Contains("DrMundo"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "DrMundo", Name = "InfectedCleaverDebuff", MenuCode = "DrMundo Q", SpriteName = Resources.DrMundoQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "DrMundo", Name = "Masochism", MenuCode = "DrMundo E", SpriteName = Resources.DrMundoE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "DrMundo", Name = "Sadism", MenuCode = "DrMundo R", SpriteName = Resources.DrMundoR, Importance = Importance.High });
            };

            if (Hero.Contains("Draven"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Draven", Name = "", MenuCode = "Draven Q", SpriteName = Resources.DravenQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Draven", Name = "", MenuCode = "Draven W", SpriteName = Resources.DravenW, OnlyMe = true });
            };

            if (Hero.Contains("Ekko"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Ekko", Name = "", MenuCode = "Ekko Passive", SpriteName = Resources.EkkoPassive });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Ekko", EndTime = 3000, SkillShot = true, MenuCode = "Ekko W", SpriteName = Resources.EkkoW, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Ekko", Name = "", MenuCode = "Ekko W", SpriteName = Resources.EkkoW });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Ekko", Name = "", MenuCode = "Ekko E", SpriteName = Resources.EkkoE });
            };

            if (Hero.Contains("Elise"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Elise", Name = "VolatileSpiderling", EndTime = 3000, MenuCode = "Elise Human W", SpriteName = Resources.EliseHumanW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Elise", Name = "EliseSpiderW", MenuCode = "Elise Spider W", SpriteName = Resources.EliseSpiderW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Elise", Name = "BuffEliseCocoon", MenuCode = "Elise Human E", SpriteName = Resources.EliseHumanE, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "Elise", Name = "EliseSpider_Base_E_indicator_", EndTime = 2500, MenuCode = "Elise Spider E", SpriteName = Resources.EliseSpiderE, Importance = Importance.High });
            };

            if (Hero.Contains("Evelynn"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Evelynn", Name = "", MenuCode = "Evelynn Passive", SpriteName = Resources.EvelynnPassive });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Evelynn", EndTime = 3000, MenuCode = "Evelynn W", SpriteName = Resources.EvelynnW });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Evelynn", Name = "", MenuCode = "Evelynn E", SpriteName = Resources.EvelynnE });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Evelynn", Name = "", MenuCode = "Evelynn R", SpriteName = Resources.EvelynnR });
            };

            if (Hero.Contains("Ezreal"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Ezreal", Name = "EzrealRisingSpellForce", MenuCode = "Ezreal Passive", SpriteName = Resources.EzrealPassive, OnlyMe = true, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Ezreal", Name = "EzrealEssenceFluxBuff", MenuCode = "Ezreal W", SpriteName = Resources.EzrealW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Ezreal", EndTime = 1000, MenuCode = "Ezreal R", SpriteName = Resources.EzrealR });
            };

            if (Hero.Contains("FiddleSticks"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "FiddleSticks", Name = "", MenuCode = "Fiddlesticks Passive", SpriteName = Resources.FiddlesticksPassive });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "FiddleSticks", Name = "Drain", MenuCode = "FiddleSticks W", SpriteName = Resources.FiddlesticksW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "FiddleSticks", EndTime = 1500 + 250, MenuCode = "FiddleSticks R", SpriteName = Resources.FiddlesticksR, Importance = Importance.VeryHigh, DrawType = DrawType.NumberLine });
            };

            if (Hero.Contains("Fiora"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Fiora", Name = "FioraQ", MenuCode = "Fiora Q", SpriteName = Resources.FioraQ, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Fiora", Name = "FioraW", MenuCode = "Fiora W", SpriteName = Resources.FioraW, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Fiora", Name = "FioraE", MenuCode = "Fiora E", SpriteName = Resources.FioraE, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Fiora", Name = "FioraE2", MenuCode = "Fiora E", SpriteName = Resources.FioraE, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Fiora", Name = "FioraRMark", MenuCode = "Fiora R", SpriteName = Resources.FioraR, Importance = Importance.High });
            };

            if (Hero.Contains("Fizz"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Fizz", Name = "FizzSeastoneActive", MenuCode = "Fizz W", SpriteName = Resources.FizzW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Fizz", Name = "FizzMalison", MenuCode = "Fizz W", SpriteName = Resources.FizzW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Fizz", Name = "Fizz_Ring_", EndTime = 1500, MenuCode = "Fizz R", SpriteName = Resources.FizzR, Importance = Importance.High });
            };

            if (Hero.Contains("Galio"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Galio", Name = "GalioBulwark", MenuCode = "Galio W", SpriteName = Resources.GalioW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Galio", Name = "Galio_BeguilingStatue_Taunt_Indicator_Team_", EndTime = 6000, MenuCode = "Galio R", SpriteName = Resources.GalioR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Gangplank"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "Gangplank", Name = "GangplankE", EndTime = 60000 });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Gangplank", Name = "", MenuCode = "Gangplank R Buff", SpriteName = Resources.GangplankBuff });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Gangplank", Name = "", MenuCode = "Gangplank R Buff", SpriteName = Resources.GangplankDeBuff });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Gangplank", EndTime = 2000, SkillShot = true, MenuCode = "Gangplank R", SpriteName = Resources.GangplankR, Importance = Importance.High, DrawType = DrawType.NumberLine });
            };

            if (Hero.Contains("Garen"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Garen", Name = "GarenQBuff", MenuCode = "Garen Q", SpriteName = Resources.GarenQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Garen", EndTime = 2000, MenuCode = "Garen W", SpriteName = Resources.GarenW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Garen", Name = "GarenE", MenuCode = "Garen E", SpriteName = Resources.GarenE, Importance = Importance.High });
            };

            if (Hero.Contains("Gnar"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Gnar", Name = "gnartransformsoon", MenuCode = "Gnar Transform", SpriteName = Resources.GnarTransformSoon, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Gnar", Name = "GnarWBuff", MenuCode = "Gnar W", SpriteName = Resources.GnarWBuff });
            };

            if (Hero.Contains("Gragas"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, GameObject = true, ChampionName = "Gragas", Name = "Gragas_Base_Q_Ally", EndTime = 4000, MenuCode = "Gragas Q", SpriteName = Resources.GragasQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, GameObject = true, ChampionName = "Gragas", Name = "Gragas_Base_Q_Enemy", EndTime = 4000, MenuCode = "Gragas Q", SpriteName = Resources.GragasQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Gragas", Name = "GragasWAttackBuff", MenuCode = "Gragas W", SpriteName = Resources.GragasW });
            };

            if (Hero.Contains("Graves"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Graves", Name = "Graves_SmokeGrenade_Cloud_Team_", EndTime = 4000, MenuCode = "Graves W", SpriteName = Resources.GravesW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Graves", Name = "GravesEGrit", MenuCode = "Graves E", SpriteName = Resources.GravesE, OnlyMe = true });
            };

            if (Hero.Contains("Hecarim"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Hecarim", Name = "HecarimRapidSlash2", MenuCode = "Hecarim Q", SpriteName = Resources.HecarimQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Hecarim", Name = "HecarimW", MenuCode = "Hecarim W", SpriteName = Resources.HecarimW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Hecarim", Name = "HecarimRamp", MenuCode = "Hecarim E", SpriteName = Resources.HecarimE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Hecarim", Name = "hecarimultmissilegrab", MenuCode = "Hecarim R", SpriteName = Resources.HecarimR, Importance = Importance.Low });
            };

            if (Hero.Contains("Heimerdinger"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, GameObject = true, ChampionName = "Heimerdinger", Name = "", EndTime = 8000, MenuCode = "Heimerdinger Q", SpriteName = Resources.HeimerdingerQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, GameObject = true, ChampionName = "Heimerdinger", Name = "", EndTime = 8000, MenuCode = "Heimerdinger Q", SpriteName = Resources.HeimerdingerQ });
            };

            if (Hero.Contains("Illaoi"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Unknown, GameObject = true, ChampionName = "Illaoi", Name = "Illaoi", EndTime = 60000 });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Illaoi", EndTime = 8000, MenuCode = "Illaoi R", SpriteName = Resources.IllaoiR, Importance = Importance.High });
            };

            if (Hero.Contains("Irelia"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Irelia", EndTime = 6000, MenuCode = "Irelia W", SpriteName = Resources.IreliaW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Irelia", Name = "IreliaTranscendentBlades", MenuCode = "Irelia R", SpriteName = Resources.IreliaR });
            };

            if (Hero.Contains("Janna"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, GameObject = true, ChampionName = "Janna", Name = "HowlingGale_cas.troy", EndTime = 3000, MenuCode = "Janna Q", SpriteName = Resources.JannaQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Janna", Name = "Eye Of The Storm", MenuCode = "Janna E", SpriteName = Resources.JannaE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Janna", Name = "Reap The Whirlwind", MenuCode = "Janna R", SpriteName = Resources.JannaR, Importance = Importance.High, DrawType = DrawType.NumberLine });
            };

            if (Hero.Contains("JarvanIV"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "JarvanIV", Name = "JarvanIVDragonStrikeDebuff", MenuCode = "JarvanIV Q", SpriteName = Resources.JarvanQ, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "JarvanIV", Name = "JarvanIVGoldenAegis", MenuCode = "JarvanIV W", SpriteName = Resources.JarvanW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "JarvanIV", Name = "Beacon", ObjectName = "JarvanIVStandard", EndTime = 8000, MenuCode = "JarvanIV E", SpriteName = Resources.JarvanE });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "JarvanIV", EndTime = 3500 + 250, SkillShot = true, MenuCode = "JarvanIV R", SpriteName = Resources.JarvanR, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "JarvanIV", Name = "", EndTime = 3500, MenuCode = "JarvanIV R", SpriteName = Resources.JarvanR });
            };

            if (Hero.Contains("Jax"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Jax", Name = "JaxRelentlessAssaultAS", MenuCode = "Jax Passive", SpriteName = Resources.JaxPassive, OnlyMe = true });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Jax", Name = "EmpowerTwo", MenuCode = "Jax W", SpriteName = Resources.JaxW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Jax", Name = "JaxEvasion", MenuCode = "Jax E", SpriteName = Resources.JaxE, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Jax", EndTime = 8000, MenuCode = "Jax R", SpriteName = Resources.JaxR, Importance = Importance.High });
            };

            if (Hero.Contains("Jayce"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Jayce", Name = "JayceStaticField", MenuCode = "Jayce W Hammer", SpriteName = Resources.JayceHammerW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Jayce", Name = "ApolloWRbuff", MenuCode = "Jayce W Cannon", SpriteName = Resources.JayceCannonW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Jayce", Name = "JayceAccelerationGate", MenuCode = "Jayce E Buff", SpriteName = Resources.JayceCannonE, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "Jayce", Name = "Jayce_Base_accel_gate_start.troy", EndTime = 4000, MenuCode = "Jayce E Gate", SpriteName = Resources.JayceCannonE });
            };

            if (Hero.Contains("Jinx"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Jinx", Name = "", MenuCode = "Jinx Passive", SpriteName = Resources.JinxPassive });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Jinx", Name = "", MenuCode = "Jinx Q", SpriteName = Resources.JinxQ });
            };

            if (Hero.Contains("Kalista"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kalista", Name = "kalistacoopstrikemarkself", MenuCode = "Kalista W", SpriteName = Resources.KalistaW, OnlyMe = true, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kalista", Name = "kalistacoopstrikemarkally", MenuCode = "Kalista W", SpriteName = Resources.KalistaW, OnlyMe = true, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kalista", Name = "kalistacoopstrikemarkenemy", MenuCode = "Kalista W", SpriteName = Resources.KalistaW, OnlyMe = true, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kalista", Name = "KalistaExpungeMarker", MenuCode = "Kalista E", SpriteName = Resources.KalistaE, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kalista", Name = "KalistaRx", MenuCode = "Kalista R", SpriteName = Resources.KalistaR, Importance = Importance.High });
            };

            if (Hero.Contains("Karma"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, GameObject = true, ChampionName = "Karma", Name = "", EndTime = 1500, MenuCode = "Karma Q", SpriteName = Resources.KarmaQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Karma", Name = "", MenuCode = "Karma W", SpriteName = Resources.KarmaW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, ChampionName = "Karma", EndTime = 1500, MenuCode = "Karma E", SpriteName = Resources.KarmaE });
            };

            if (Hero.Contains("Karthus"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Karthus", Name = "Death Defied Buff", MenuCode = "Karthus Passive", SpriteName = Resources.KarthusP, Importance = Importance.VeryHigh, DrawType = DrawType.NumberLine });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Karthus", Name = "Wall of Pain", MenuCode = "Karthus W Debuff", SpriteName = Resources.KarthusW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Karthus", EndTime = 5000, SkillShot = true, MenuCode = "Karthus W Wall", SpriteName = Resources.KarthusW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Karthus", EndTime = 3000, MenuCode = "Karthus R", SpriteName = Resources.KarthusR, Importance = Importance.VeryHigh, DrawType = DrawType.NumberLine });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Karthus", Name = "Karthus_Base_R_Target_", EndTime = 3000, MenuCode = "Karthus R", SpriteName = Resources.KarthusR, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Karthus", Name = "KarthusFallenOne", MenuCode = "Karthus R", SpriteName = Resources.KarthusR, Importance = Importance.High, DrawType = DrawType.NumberLine }); 
            };

            if (Hero.Contains("Kassadin"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kassadin", Name = "NullLance", MenuCode = "Kassadin Q", SpriteName = Resources.KassadinQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kassadin", Name = "NetherBladeArmorPen", MenuCode = "Kassadin W", SpriteName = Resources.KassadinW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kassadin", Name = "RiftWalk", MenuCode = "Kassadin R", SpriteName = Resources.KassadinR, Importance = Importance.High });
            };

            if (Hero.Contains("Katarina"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Katarina", Name = "", MenuCode = "Katarina Passive", SpriteName = Resources.KatarinaP });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Katarina", Name = "", MenuCode = "Katarina Q", SpriteName = Resources.KatarinaQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Katarina", Name = "", MenuCode = "Katarina W", SpriteName = Resources.KatarinaW });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Katarina", Name = "", MenuCode = "Katarina E", SpriteName = Resources.KatarinaE });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Katarina", Name = "", MenuCode = "Katarina R", SpriteName = Resources.KatarinaR });
            };

            if (Hero.Contains("Kayle"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kayle", Name = "JudicatorHolyFervorDebuff", MenuCode = "Kayle Passive", SpriteName = Resources.KayleP, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kayle", Name = "JudicatorReckoning", MenuCode = "Kayle Q", SpriteName = Resources.KayleQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kayle", Name = "JudicatorDivineBlessing", MenuCode = "Kayle W", SpriteName = Resources.KayleW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kayle", Name = "JudicatorRighteousFury", MenuCode = "Kayle E", SpriteName = Resources.KayleE, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kayle", Name = "JudicatorIntervention", MenuCode = "Kayle R", SpriteName = Resources.KayleR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Kennen"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kennen", Name = "KennenMarkOfStorm", MenuCode = "Kennen Passive", SpriteName = Resources.KennenP });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kennen", Name = "KennenLightningRush", MenuCode = "Kennen E", SpriteName = Resources.KennenE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Kennen", Name = "KennenShurikenStorm", MenuCode = "Kennen R", SpriteName = Resources.KennenR, Importance = Importance.High });
            };

            if (Hero.Contains("Khazix"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Khazix", Name = "", MenuCode = "Khazix R", SpriteName = Resources.KhazixR });
            };

            if (Hero.Contains("Kindred"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Kindred", EndTime = 4000, SkillShot = true, MenuCode = "Kindred R", SpriteName = Resources.KindredR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("KogMaw"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "KogMaw", Name = "kogmawicathiansurprise", MenuCode = "KogMaw Passive", SpriteName = Resources.KogMawP, Importance = Importance.High, DrawType = DrawType.NumberLine });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "KogMaw", Name = "KogMawCausticSpittleCharged", MenuCode = "KogMaw Q", SpriteName = Resources.KogMawQ, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "KogMaw", Name = "KogMawBioArcaneBarrage", MenuCode = "KogMaw W", SpriteName = Resources.KogMawW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "KogMaw", Name = "KogMawVoidOoze", MenuCode = "KogMaw E", SpriteName = Resources.KogMawE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "KogMaw", Name = "KogMawLivingArtillery", MenuCode = "KogMaw R", SpriteName = Resources.KogMawR, OnlyMe = true, Importance = Importance.High });
            };

            if (Hero.Contains("Leblanc"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Leblanc", Name = "LeblancMarkOfSlience", MenuCode = "Leblanc Q", SpriteName = Resources.LeBlancQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Leblanc", Name = "LeblancMarkOfSlienceM", MenuCode = "Leblanc Q", SpriteName = Resources.LeBlancQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Leblanc", Name = "LeblancDisplacement", MenuCode = "Leblanc W", SpriteName = Resources.LeBlancW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Leblanc", Name = "LeblancDisplacementM", MenuCode = "Leblanc W", SpriteName = Resources.LeBlancW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Leblanc", Name = "LeBlanc_Base_W_return_indicator.troy", EndTime = 4000, MenuCode = "Leblanc W", SpriteName = Resources.LeBlancW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Leblanc", Name = "LeBlanc_Base_RW_return_indicator.troy", EndTime = 4000, MenuCode = "Leblanc W", SpriteName = Resources.LeBlancW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Leblanc", Name = "LeblancShackleBeam", MenuCode = "Leblanc E", SpriteName = Resources.LeBlancE, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Leblanc", Name = "LeblancShackleBeamM", MenuCode = "Leblanc E", SpriteName = Resources.LeBlancE, Importance = Importance.High });
            };

            if (Hero.Contains("LeeSin"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "LeeSin", Name = "BlindMonkSonicWave", MenuCode = "LeeSin Q", SpriteName = Resources.LeeSinQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "LeeSin", Name = "BlindMonkSafeguard", MenuCode = "LeeSin W", SpriteName = Resources.LeeSinW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "LeeSin", Name = "BlindMonkIronWill", MenuCode = "LeeSin W2", SpriteName = Resources.LeeSinWW, OnlyMe = true });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "LeeSin", Name = "BlindMonkTempest", MenuCode = "LeeSin E", SpriteName = Resources.LeeSinE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "LeeSin", Name = "BlindMonkCripple", MenuCode = "LeeSin E2", SpriteName = Resources.LeeSinEE, Importance = Importance.High });
            };

            if (Hero.Contains("Leona"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Leona", Name = "LeonaShieldOfDaybreak", MenuCode = "Leona Q", SpriteName = Resources.LeonaQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Leona", EndTime = 3000, MenuCode = "Leona W", SpriteName = Resources.LeonaW, Importance = Importance.High });
            };

            if (Hero.Contains("Lissandra"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Lissandra", Name = "Lissandra_Base_R_ring_", EndTime = 1500, MenuCode = "Lissandra R", SpriteName = Resources.LissandraR, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Lissandra", Name = "Lissandra_Base_R_iceblock", EndTime = 2500, MenuCode = "Lissandra R", SpriteName = Resources.LissandraR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Lucian"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Lucian", Name = "LucianPassiveBuff", MenuCode = "Lucian Passive", SpriteName = Resources.LucianP });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Lucian", Name = "LucianWBuff", MenuCode = "Lucian W", SpriteName = Resources.LucianW, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Lucian", Name = "lucianwdebuff", MenuCode = "Lucian W", SpriteName = Resources.LucianW, Importance = Importance.Low });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Lucian", Name = "", MenuCode = "Lucian R", SpriteName = Resources.LucianR, Importance = Importance.High });
            };

            if (Hero.Contains("Lulu"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Lulu", Name = "", MenuCode = "Lulu W", SpriteName = Resources.LuluW, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Lulu", Name = "", MenuCode = "Lulu E", SpriteName = Resources.LuluE, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Lulu", EndTime = 7000, Targeting = true, MenuCode = "Lulu R", SpriteName = Resources.LuluR, Importance = Importance.High });
            };

            if (Hero.Contains("Lux"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Lux", Name = "LuxIlluminatingFraulein", MenuCode = "Lux Passive", SpriteName = Resources.LuxP, OnlyMe = true, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Lux", Name = "LuxShield", MenuCode = "Lux W", SpriteName = Resources.LuxW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "Lux", Name = "Lux_Base_E_tar_aoe_", EndTime = 5000, MenuCode = "Lux E", SpriteName = Resources.LuxE });
            };

            if (Hero.Contains("Malphite"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Malphite", Name = "SeismicShard", MenuCode = "Malphite Q", SpriteName = Resources.MalphiteQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Malphite", Name = "MalphiteCleave", MenuCode = "Malphite W", SpriteName = Resources.MalphiteW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Malphite", Name = "LandslideDebuff", MenuCode = "Malphite E", SpriteName = Resources.MalphiteE, Importance = Importance.Low });
            };

            if (Hero.Contains("Malzahar"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Malzahar", EndTime = 4000, SkillShot = true, MenuCode = "Malzahar W", SpriteName = Resources.MalzaharW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Malzahar", Name = "AlZaharMaleficVisions", MenuCode = "Malzahar E", SpriteName = Resources.MalzaharE, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Malzahar", Name = "", EndTime = 2500, MenuCode = "Malzahar R", SpriteName = Resources.MalzaharR });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Malzahar", Name = "", MenuCode = "Malzahar R", SpriteName = Resources.MalzaharR });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Malzahar", Name = "AlZaharNetherGrasp", MenuCode = "Malzahar R", SpriteName = Resources.MalzaharR, Importance = Importance.High });
            };

            if (Hero.Contains("MasterYi"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "MasterYi", Name = "Meditate", MenuCode = "MasterYi W", SpriteName = Resources.MasterYiW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, ChampionName = "MasterYi", EndTime = 5000, MenuCode = "MasterYi E", SpriteName = Resources.MasterYiE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "MasterYi", Name = "Highlander", MenuCode = "MasterYi R", SpriteName = Resources.MasterYiR, Importance = Importance.High });
            };

            if (Hero.Contains("MissFortune"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "MissFortune", Name = "MissFortuneViciousStrikes", MenuCode = "MissFortune W", SpriteName = Resources.MissFortuneW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "MissFortune", Name = "MissFortune_Base_E_Unit_Tar_green.troy", EndTime = 2500, MenuCode = "MissFortune E", SpriteName = Resources.MissFortuneE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "MissFortune", Name = "MissFortune_Base_E_Unit_Tar_red.troy", EndTime = 2500, MenuCode = "MissFortune E", SpriteName = Resources.MissFortuneE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "MissFortune", Name = "MissFortuneBulletSound", MenuCode = "MissFortune R", SpriteName = Resources.MissFortuneR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Mordekaiser"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Mordekaiser", Name = "mordekaisermaceofspades1", MenuCode = "Mordekaiser Q", SpriteName = Resources.MordekaiserQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Mordekaiser", Name = "mordekaisermaceofspades15", MenuCode = "Mordekaiser Q", SpriteName = Resources.MordekaiserQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Mordekaiser", Name = "mordekaisermaceofspades2", MenuCode = "Mordekaiser Q", SpriteName = Resources.MordekaiserQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Mordekaiser", Name = "mordekaisercreepingdeath2", MenuCode = "Mordekaiser W", SpriteName = Resources.MordekaiserW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Mordekaiser", Name = "MordekaiserCOTGDot", MenuCode = "Mordekaiser R", SpriteName = Resources.MordekaiserR, Importance = Importance.High });
            };

            if (Hero.Contains("Morgana"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Morgana", Name = "Dark Binding", MenuCode = "Morgana Q", SpriteName = Resources.MorganaQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Morgana", EndTime = 5000, SkillShot = true, MenuCode = "Morgana W", SpriteName = Resources.MorganaW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Morgana", Name = "Black Shield", MenuCode = "Morgana E", SpriteName = Resources.MorganaE, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Morgana", EndTime = 3000, MenuCode = "Morgana R", SpriteName = Resources.MorganaR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Nami"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Nami", Name = "", MenuCode = "Nami Passive", SpriteName = Resources.NamiPassive, Importance = Importance.Low });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Nami", Name = "", MenuCode = "Nami E", SpriteName = Resources.NamiE });
            };

            if (Hero.Contains("Nasus"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Nasus", Name = "NasusW", MenuCode = "Nasus W", SpriteName = Resources.NasusW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "Nasus", Name = "Nasus_Base_E_SpiritFire.troy", EndTime = 4500, MenuCode = "Nasus E", SpriteName = Resources.NasusE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Nasus", EndTime = 15000, MenuCode = "Nasus R", SpriteName = Resources.NasusR, Importance = Importance.High });
            };

            if (Hero.Contains("Nautilus"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Nautilus", Name = "", MenuCode = "Nautilus W", SpriteName = Resources.NautilusW });
            };

            if (Hero.Contains("Nidalee"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Nidalee", Name = "NidaleePassiveHunted", MenuCode = "Nidalee Passive", SpriteName = Resources.NidaleeP, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Nidalee", Name = "PrimalSurge", MenuCode = "Nidalee E", SpriteName = Resources.NidaleeHumanE });
            };

            if (Hero.Contains("Nocturne"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Nocturne", Name = "", MenuCode = "Nocturne Q", SpriteName = Resources.NocturneQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Nocturne", Name = "", MenuCode = "Nocturne W", SpriteName = Resources.NocturneW, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Nocturne", Name = "", MenuCode = "Nocturne E", SpriteName = Resources.NocturneE, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Nocturne", EndTime = 4000, MenuCode = "Nocturne R", SpriteName = Resources.NocturneR, Importance = Importance.High });
            };

            if (Hero.Contains("Nunu"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Nunu", Name = "", MenuCode = "Nunu Q", SpriteName = Resources.NunuQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Nunu", Name = "Blood Boil", MenuCode = "Nunu W", SpriteName = Resources.NunuW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Nunu", Name = "IceBlast", MenuCode = "Nunu E", SpriteName = Resources.NunuE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Nunu", Name = "Nunu_Base_R_indicator_", EndTime = 3000, MenuCode = "Nunu R", SpriteName = Resources.NunuR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Olaf"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Olaf", Name = "OlafFrenziedStrikes", MenuCode = "Olaf W", SpriteName = Resources.OlafW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Olaf", Name = "OlafRagnarok", MenuCode = "Olaf R", SpriteName = Resources.OlafR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Orianna"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Orianna", Name = "OrianaDissonanceAlly", MenuCode = "Orianna W", SpriteName = Resources.OriannaW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Orianna", Name = "OrianaDissonanceEnemy", MenuCode = "Orianna W", SpriteName = Resources.OriannaW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Orianna", Name = "OrianaRedactShield", MenuCode = "Orianna E", SpriteName = Resources.OriannaE });
            };

            if (Hero.Contains("Pantheon"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Pantheon", Name = "PantheonESound", MenuCode = "Pantheon E", SpriteName = Resources.PantheonE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Pantheon", Name = "Pantheon_Base_R_cas", EndTime = 2000, MenuCode = "Pantheon R", SpriteName = Resources.PantheonR, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Pantheon", Name = "Pantheon_Base_R_indicator_", EndTime = 4500, MenuCode = "Pantheon R", SpriteName = Resources.PantheonR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Poppy"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Poppy", Name = "poppypassivecooldown", MenuCode = "Poppy Passive Cooldown", SpriteName = Resources.PoppyP, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Poppy", Name = "PoppyPassiveShield", MenuCode = "Poppy Passive Shield", SpriteName = Resources.PoppyP });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Poppy", Name = "PoppyWZone", MenuCode = "Poppy W", SpriteName = Resources.PoppyW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Poppy", Name = "PoppyR", MenuCode = "Poppy R", SpriteName = Resources.PoppyR, Importance = Importance.High });
            };

            if (Hero.Contains("Quinn"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Quinn", Name = "QuinnW", MenuCode = "Quinn Passive", SpriteName = Resources.QuinnP, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Quinn", Name = "QuinnQSightReduction", MenuCode = "Quinn Q", SpriteName = Resources.QuinnQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Quinn", EndTime = 2000, MenuCode = "Quinn W", SpriteName = Resources.QuinnW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Quinn", EndTime = 2000, MenuCode = "Quinn R", SpriteName = Resources.QuinnR, Importance = Importance.High });
            };

            if (Hero.Contains("Rammus"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Rammus", Name = "PowerBall", MenuCode = "Rammus Q", SpriteName = Resources.RammusQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Rammus", EndTime = 6000, MenuCode = "Rammus W", SpriteName = Resources.RammusW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Rammus", EndTime = 8000, MenuCode = "Rammus R", SpriteName = Resources.RammusR, Importance = Importance.High });
            };

            if (Hero.Contains("RekSai"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "RekSai", Name = "RekSaiQ", MenuCode = "RekSai Q", SpriteName = Resources.RekSaiQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "RekSai", Name = "RekSaiQBurrowedSlow", MenuCode = "RekSai Q Slow", SpriteName = Resources.RekSaiQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "RekSai", Name = "RekSaiKnockupImmune", MenuCode = "RekSai E", SpriteName = Resources.RekSaiE });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "RekSai", Name = "", EndTime = 600000, MenuCode = "RekSai E", SpriteName = Resources.RekSaiE });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "RekSai", Name = "", EndTime = 12000, MenuCode = "RekSai E", SpriteName = Resources.RekSaiE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "RekSai", Name = "RekSaiR", MenuCode = "RekSai R", SpriteName = Resources.RekSaiR, Importance = Importance.High });
            };

            if (Hero.Contains("Renekton"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Renekton", Name = "RenektonExecuteReady", MenuCode = "Renekton W", SpriteName = Resources.RenektonW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Renekton", Name = "RenekthonSliceAndDiceDelay", MenuCode = "Renekton E", SpriteName = Resources.RenektonE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Renekton", Name = "RenekthonTyrantForm", MenuCode = "Renekton R", SpriteName = Resources.RenektonR, Importance = Importance.High });
            };

            if (Hero.Contains("Rengar"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Rengar", Name = "RengarQBuff", MenuCode = "Rengar Q", SpriteName = Resources.RengarQ, OnlyMe = true });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Rengar", Name = "RengarQBuffMAX", MenuCode = "Rengar Q", SpriteName = Resources.RengarQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Rengar", Name = "RengarWBuff", MenuCode = "Rengar W", SpriteName = Resources.RengarW, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Rengar", Name = "RengarRSpeed", MenuCode = "Rengar R", SpriteName = Resources.RengarR, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Rengar", Name = "RengarRBuff", MenuCode = "Rengar R", SpriteName = Resources.RengarR });
            };

            if (Hero.Contains("Riven"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Riven", Name = "", MenuCode = "Riven Passive", SpriteName = Resources.RivenP, OnlyMe = true, Importance = Importance.Low });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Riven", Name = "", MenuCode = "Riven Q", SpriteName = Resources.RivenQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Riven", Name = "", MenuCode = "Riven E", SpriteName = Resources.RivenE });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Riven", Name = "", MenuCode = "Riven R", SpriteName = Resources.RivenR, Importance = Importance.High });
            };

            if (Hero.Contains("Rumble"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, ChampionName = "Rumble", EndTime = 3000, MenuCode = "Rumble Q", SpriteName = Resources.RumbleQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Rumble", EndTime = 2000, MenuCode = "Rumble W", SpriteName = Resources.RumbleW });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Rumble", Name = "", EndTime = 5000, MenuCode = "Rumble R", SpriteName = Resources.RumbleR });
            };

            if (Hero.Contains("Ryze"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Ryze", Name = "RyzePassiveStack", MenuCode = "Ryze Passive Stack", SpriteName = Resources.RyzeP, OnlyMe = true, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Ryze", Name = "RyzePassiveCharged", MenuCode = "Ryze Passive Charge", SpriteName = Resources.RyzeP });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Ryze", Name = "RyzeR", MenuCode = "Ryze R", SpriteName = Resources.RyzeR, Importance = Importance.High });
            };

            if (Hero.Contains("Sejuani"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sejuani", Name = "SejuaniPassiveDisplay", MenuCode = "Sejuani Passive", SpriteName = Resources.SejuaniP, OnlyMe = true, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sejuani", Name = "sejuaniarcticassaultchampion", MenuCode = "Sejuani Q", SpriteName = Resources.SejuaniQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sejuani", Name = "SejuaniNorthernWindsEnrage", MenuCode = "Sejuani W", SpriteName = Resources.SejuaniW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sejuani", Name = "SejuaniNorthernWinds", MenuCode = "Sejuani W", SpriteName = Resources.SejuaniW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sejuani", Name = "SejuaniFrost", MenuCode = "Sejuani E", SpriteName = Resources.SejuaniE, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sejuani", Name = "SejuaniFrostArctic", MenuCode = "Sejuani E Debuff", SpriteName = Resources.SejuaniE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sejuani", Name = "SejuaniGlacialPrison", MenuCode = "Sejuani R", SpriteName = Resources.SejuaniR, Importance = Importance.High });
            };

            if (Hero.Contains("Shaco"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Shaco", Name = "Deceive", MenuCode = "Shaco Q", SpriteName = Resources.ShacoQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Shaco", Name = "Two Shiv Poison", MenuCode = "Shaco E", SpriteName = Resources.ShacoE, Importance = Importance.Low });
            };

            if (Hero.Contains("Shen"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Shen", Name = "ShenPassiveShield", MenuCode = "Shen Passive", SpriteName = Resources.ShenP });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Shen", Name = "ShenQBuffStrong", MenuCode = "Shen Q", SpriteName = Resources.ShenQ, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Shen", Name = "ShenQBuffWeak", MenuCode = "Shen Q", SpriteName = Resources.ShenQ, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Shen", Name = "ShenQSlow", MenuCode = "Shen Q", SpriteName = Resources.ShenQ, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Shen", Name = "ShenWBuff", MenuCode = "Shen W", SpriteName = Resources.ShenW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Shen", Name = "Shen_Base_W_Buf.troy", EndTime = 1750, MenuCode = "Shen W", SpriteName = Resources.ShenW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Shen", Name = "ShenRTargetTracker", MenuCode = "Shen R", SpriteName = Resources.ShenR, DrawType = DrawType.NumberLine, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Shen", Name = "shenrchannelbuffbar", MenuCode = "Shen R", SpriteName = Resources.ShenR, DrawType = DrawType.NumberLine, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Shen", Name = "ShenRShield", MenuCode = "Shen R Shield", SpriteName = Resources.ShenR, Importance = Importance.Low });
            };

            if (Hero.Contains("Shyvana"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Shyvana", Name = "ShyvanaScorchedEarth", MenuCode = "Shyvana W", SpriteName = Resources.ShyvanaW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Shyvana", Name = "ShyvanaFlameBreathDebuff", MenuCode = "Shyvana E", SpriteName = Resources.ShyvanaE });
            };

            if (Hero.Contains("Singed"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Singed", Name = "", MenuCode = "Singed Q", SpriteName = Resources.SingedQ, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Singed", EndTime = 5000, SkillShot = true, MenuCode = "Singed W", SpriteName = Resources.SingedW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Singed", Name = "Insanity Potion", MenuCode = "Singed R", SpriteName = Resources.SingedR });
            };

            if (Hero.Contains("Sion"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sion", Name = "SionPassiveSpeed", MenuCode = "Sion Passive", SpriteName = Resources.SionP, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sion", Name = "SionQ", MenuCode = "Sion Q", SpriteName = Resources.SionQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sion", Name = "SionWShieldStacks", MenuCode = "Sion W", SpriteName = Resources.SionW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sion", Name = "SionR", MenuCode = "Sion R", SpriteName = Resources.SionR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Sivir"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sivir", Name = "SivirPassiveSpeed", MenuCode = "Sivir Passive", SpriteName = Resources.SivirP, OnlyMe = true, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sivir", Name = "SivirWMarker", MenuCode = "Sivir W", SpriteName = Resources.SivirW, OnlyMe = true });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sivir", Name = "Spell Shield", MenuCode = "Sivir E", SpriteName = Resources.SivirE, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sivir", Name = "On The Hunt", MenuCode = "Sivir R", SpriteName = Resources.SivirR, Importance = Importance.High });
            };

            if (Hero.Contains("Skarner"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Unknown, GameObject = true, ChampionName = "Skarner", Name = "", EndTime = 2500, MenuCode = "Skarner Passive", SpriteName = Resources.SkarnerP });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Skarner", Name = "", MenuCode = "Skarner Q", SpriteName = Resources.SkarnerQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Skarner", Name = "", MenuCode = "Skarner W", SpriteName = Resources.SkarnerW });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Skarner", Name = "", MenuCode = "Skarner E", SpriteName = Resources.SkarnerE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Skarner", Name = "Skarner_Base_R_beam", EndTime = 2000, MenuCode = "Skarner R", SpriteName = Resources.SkarnerR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Sona"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sona", Name = "SonaQProcAttacker", MenuCode = "Sona Q", SpriteName = Resources.SonaQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sona", Name = "SonaWShield", MenuCode = "Sona W", SpriteName = Resources.SonaW, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sona", Name = "SonaEZone", MenuCode = "Sona E", SpriteName = Resources.SonaE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Sona", Name = "SonaR", MenuCode = "Sona R", SpriteName = Resources.SonaR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Soraka"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, ChampionName = "Soraka", EndTime = 1500 + 250, SkillShot = true, MenuCode = "Soraka E", SpriteName = Resources.SorakaE, Importance = Importance.High });
            };

            if (Hero.Contains("Swain"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Swain", Name = "SwainBeamDamage", MenuCode = "Swain Q", SpriteName = Resources.SwainQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Swain", Name = "SwainTormentDoT", MenuCode = "Swain E", SpriteName = Resources.SwainE, Importance = Importance.High });
            };

            if (Hero.Contains("TahmKench"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "TahmKench", Name = "", MenuCode = "TahmKench Passive", SpriteName = Resources.TahmKenchP });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "TahmKench", Name = "", MenuCode = "TahmKench W", SpriteName = Resources.TahmKenchW });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "TahmKench", Name = "", MenuCode = "TahmKench E", SpriteName = Resources.TahmKenchE });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "TahmKench", Name = "", MenuCode = "TahmKench R", SpriteName = Resources.TahmKenchR });
            };

            if (Hero.Contains("Talon"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Talon", Name = "", MenuCode = "Talon Q", SpriteName = Resources.TalonQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Talon", Name = "", MenuCode = "Talon Q", SpriteName = Resources.TalonQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, ChampionName = "Talon", EndTime = 3000, MenuCode = "Talon E", SpriteName = Resources.TalonE });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Talon", Name = "", MenuCode = "Talon R", SpriteName = Resources.TalonR });
            };

            if (Hero.Contains("Taric"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Taric", Name = "TaricGemcraftBuff", MenuCode = "Taric Passive", SpriteName = Resources.TaricP, OnlyMe = true, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Taric", Name = "Shatter", MenuCode = "Taric W", SpriteName = Resources.TaricW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Taric", EndTime = 10000, MenuCode = "Taric R", SpriteName = Resources.TaricR, Importance = Importance.High });
            };

            if (Hero.Contains("Teemo"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Teemo", Name = "CamoflagueBuff", MenuCode = "Teemo Passive", SpriteName = Resources.TeemoP, OnlyMe = true, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Teemo", EndTime = 3000, MenuCode = "Teemo W", SpriteName = Resources.TeemoW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Teemo", Name = "Toxic Shot", MenuCode = "Teemo E", SpriteName = Resources.TeemoE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Teemo", Name = "Noxious Trap Target", MenuCode = "Teemo R", SpriteName = Resources.TeemoR });
            };

            if (Hero.Contains("Thresh"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Thresh", Name = "ThreshQ", MenuCode = "Thresh Q", SpriteName = Resources.ThreshQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Thresh", Name = "Thresh_Base_W_RingLarge.troy", EndTime = 6000, MenuCode = "Thresh W", SpriteName = Resources.ThreshW, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Thresh", Name = "Thresh_Base_R_wall.troy", EndTime = 4000, MenuCode = "Thresh R", SpriteName = Resources.ThreshR }); 수정 필요
            };

            if (Hero.Contains("Tristana"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, ChampionName = "Tristana", EndTime = 5000, MenuCode = "Tristana Q", SpriteName = Resources.TristanaQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Tristana", Name = "TristanaEChargeSound", MenuCode = "Tristana E", SpriteName = Resources.TristanaE, Importance = Importance.High });
            };

            if (Hero.Contains("Trundle"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Trundle", Name = "TrundleQDebuff", MenuCode = "Trundle Q", SpriteName = Resources.TrundleQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Trundle", EndTime = 8000, SkillShot = true, MenuCode = "Trundle W", SpriteName = Resources.TrundleW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, ChampionName = "Trundle", EndTime = 6000, SkillShot = true, MenuCode = "Trundle E", SpriteName = Resources.TrundleE, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Trundle", Name = "TrundleCircleSlow", MenuCode = "Trundle E", SpriteName = Resources.TrundleE, Importance = Importance.Low });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Trundle", Name = "", MenuCode = "Trundle R", SpriteName = Resources.TrundleR });
            };

            if (Hero.Contains("Tryndamere"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Tryndamere", Name = "Mocking Shout", MenuCode = "Tryndamere W", SpriteName = Resources.TryndamereW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Tryndamere", Name = "Undying Rage", MenuCode = "Tryndamere R", SpriteName = Resources.TryndamereR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("TwistedFate"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "TwistedFate", Name = "Pick A Card", MenuCode = "TwistedFate W", SpriteName = Resources.TwistedFateW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "TwistedFate", Name = "Destiny Marker", MenuCode = "TwistedFate R", SpriteName = Resources.TwistedFateR, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "TwistedFate", Name = "Gate", MenuCode = "TwistedFate R", SpriteName = Resources.TwistedFateR, DrawType = DrawType.NumberLine, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "TwistedFate", Name = "GateMarker_green.troy", EndTime = 1500, MenuCode = "TwistedFate R", SpriteName = Resources.TwistedFateR, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "TwistedFate", Name = "GateMarker_red.troy", EndTime = 1500, MenuCode = "TwistedFate R", SpriteName = Resources.TwistedFateR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Twitch"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Twitch", Name = "TwitchDeadlyVenom", MenuCode = "Twitch Passive", SpriteName = Resources.TwitchP });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, ChampionName = "Twitch", EndTime = 1500, MenuCode = "Twitch Q", SpriteName = Resources.TwitchQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Twitch", Name = "TwitchHideInShadows", MenuCode = "Twitch Q", SpriteName = Resources.TwitchQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Twitch", Name = "TwitchUlt", MenuCode = "Twitch R", SpriteName = Resources.TwitchR, Importance = Importance.High });
            };

            if (Hero.Contains("Udyr"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Udyr", Name = "UdyrMonkeyAgilityBuff", MenuCode = "Udyr Passive", SpriteName = Resources.UdyrP });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, ChampionName = "Udyr", EndTime = 5000, MenuCode = "Udyr Q", SpriteName = Resources.UdyrQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Udyr", EndTime = 5000, MenuCode = "Udyr W", SpriteName = Resources.UdyrW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Udyr", Name = "UdyrBearActivation", MenuCode = "Udyr E", SpriteName = Resources.UdyrE, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Udyr", EndTime = 5000, MenuCode = "Udyr R", SpriteName = Resources.UdyrR });
            };

            if (Hero.Contains("Urgot"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Urgot", Name = "UrgotEntropyDebuff", MenuCode = "Urgot Passive", SpriteName = Resources.UrgotP, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Urgot", Name = "UrgotTerrorCapacitor", MenuCode = "Urgot W", SpriteName = Resources.UrgotW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Urgot", Name = "UrgotCorrosiveDamage", MenuCode = "Urgot E", SpriteName = Resources.UrgotE, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Urgot", Name = "UrgotSwapTarget", MenuCode = "Urgot R", SpriteName = Resources.UrgotR, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Urgot", EndTime = 1000, MenuCode = "Urgot R", SpriteName = Resources.UrgotR, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Urgot", Name = "UrgotSwapDef", MenuCode = "Urgot R Debuff", SpriteName = Resources.UrgotR });
            };

            if (Hero.Contains("Varus"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Varus", Name = "", MenuCode = "Varus Passive", SpriteName = Resources.VarusP, Importance = Importance.Low });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Varus", Name = "", MenuCode = "Varus Q", SpriteName = Resources.VarusQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Varus", Name = "", MenuCode = "Varus W", SpriteName = Resources.VarusW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, ChampionName = "Varus", EndTime = 4000 + 250, MenuCode = "Varus E", SpriteName = Resources.VarusE });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Varus", Name = "", MenuCode = "Varus R", SpriteName = Resources.VarusR, Importance = Importance.High });
            };

            if (Hero.Contains("Vayne"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Vayne", Name = "VayneTumble", MenuCode = "Vayne Q", SpriteName = Resources.VayneQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Vayne", Name = "VayneSilverDebuff", MenuCode = "Vayne W", SpriteName = Resources.VayneW });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Vayne", Name = "", MenuCode = "Vayne E", SpriteName = Resources.VayneE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Vayne", Name = "VayneInquisition", MenuCode = "Vayne R", SpriteName = Resources.VayneR, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Vayne", Name = "VayneInquisitionStealth", MenuCode = "Vayne R", SpriteName = Resources.VayneR });
            };

            if (Hero.Contains("Veigar"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Veigar", EndTime = 1200, SkillShot = true, MenuCode = "Veigar W", SpriteName = Resources.VeigarW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "Veigar", Name = "Veigar_Base_E_cage_", EndTime = 3000, MenuCode = "Veigar E", SpriteName = Resources.VeigarE, Importance = Importance.High });
            };

            if (Hero.Contains("Velkoz"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Velkoz", Name = "", MenuCode = "Velkoz Passive", SpriteName = Resources.VelkozP });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Velkoz", Name = "", MenuCode = "Velkoz R", SpriteName = Resources.VelkozR, Importance = Importance.High });
            };

            if (Hero.Contains("Vi"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Vi", Name = "", MenuCode = "Vi Passive", SpriteName = Resources.ViP });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Vi", Name = "", MenuCode = "Vi Q", SpriteName = Resources.ViQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Vi", Name = "", MenuCode = "Vi W", SpriteName = Resources.ViW });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Vi", Name = "", MenuCode = "Vi R", SpriteName = Resources.ViR, Importance = Importance.High });
            };

            if (Hero.Contains("Viktor"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Viktor", Name = "ViktorPowerTransferReturn", MenuCode = "Viktor Q", SpriteName = Resources.ViktorQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Viktor",Name = "", MenuCode = "Viktor Q Evolution", SpriteName = Resources.ViktorQQ });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Viktor", Name = "Viktor_Catalyst_", EndTime = 4000, MenuCode = "Viktor W", SpriteName = Resources.ViktorW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Viktor", Name = "Viktor_ChaosStorm_", EndTime = 7000, MenuCode = "Viktor R", SpriteName = Resources.ViktorR, Importance = Importance.High });
            };

            if (Hero.Contains("Vladimir"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "Vladimir", EndTime = 2000, MenuCode = "Vladimir W", SpriteName = Resources.VladimirW, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Vladimir", Name = "VladimirTidesofBloodCost", MenuCode = "Vladimir E", SpriteName = Resources.VladimirE, OnlyMe = true });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Vladimir", Name = "VladimirHemoplagueDebuff", MenuCode = "Vladimir R", SpriteName = Resources.VladimirR, Importance = Importance.High });
            };

            if (Hero.Contains("Volibear"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Volibear", Name = "", MenuCode = "Volibear Passive", SpriteName = Resources.VolibearP, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Volibear", Name = "", MenuCode = "Volibear Q", SpriteName = Resources.VolibearQ, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Volibear", Name = "", MenuCode = "Volibear W", SpriteName = Resources.VolibearW, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Volibear", Name = "", MenuCode = "Volibear E", SpriteName = Resources.VolibearE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Volibear", EndTime = 12000, MenuCode = "Volibear R", SpriteName = Resources.VolibearR, Importance = Importance.High });
            };

            if (Hero.Contains("Warwick"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Warwick", Name = "EternalThirstBuff", MenuCode = "Warwick Passive", SpriteName = Resources.WarwickP, OnlyMe = true });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Warwick", Name = "Hunter's Call", MenuCode = "Warwick W", SpriteName = Resources.WarwickW });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Warwick", Name = "", MenuCode = "Warwick R", SpriteName = Resources.WarwickR });   Supression
            };

            if (Hero.Contains("MonkeyKing"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "MonkeyKing", EndTime = 1500, MenuCode = "Wukong W", SpriteName = Resources.WukongW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, ChampionName = "MonkeyKing", EndTime = 4000, MenuCode = "Wukong E", SpriteName = Resources.WukongE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, ChampionName = "MonkeyKing", EndTime = 4000, MenuCode = "Wukong R", SpriteName = Resources.WukongR, Importance = Importance.High });
            };

            if (Hero.Contains("Xerath"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Xerath", Name = "XerathR", MenuCode = "Xerath R", SpriteName = Resources.XerathR, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("XinZhao"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "XinZhao", Name = "XenZhaoComboTarget", MenuCode = "XinZhao Q", SpriteName = Resources.XinZhaoQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, ChampionName = "XinZhao", EndTime = 5000, MenuCode = "XinZhao W", SpriteName = Resources.XinZhaoW });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "XinZhao", Name = "XenZhaoParry", MenuCode = "XinZhao R", SpriteName = Resources.XinZhaoR, Importance = Importance.Low });
            };

            if (Hero.Contains("Yasuo"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Yasuo", Name = "YasuoPassiveMSShieldOn", MenuCode = "Yasuo Passive", SpriteName = Resources.YasuoP, Importance = Importance.Low });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Yasuo", Name = "YasuoQ", MenuCode = "Yasuo Q", SpriteName = Resources.YasuoQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Yasuo", Name = "YasuoQ3W", MenuCode = "Yasuo Q", SpriteName = Resources.YasuoQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Yasuo", Name = "Yasuo_Base_W_windwall1.troy", EndTime = 4000, MenuCode = "Yasuo W", SpriteName = Resources.YasuoW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Yasuo", Name = "Yasuo_Base_W_windwall2.troy", EndTime = 4000, MenuCode = "Yasuo W", SpriteName = Resources.YasuoW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Yasuo", Name = "Yasuo_Base_W_windwall3.troy", EndTime = 4000, MenuCode = "Yasuo W", SpriteName = Resources.YasuoW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Yasuo", Name = "Yasuo_Base_W_windwall4.troy", EndTime = 4000, MenuCode = "Yasuo W", SpriteName = Resources.YasuoW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Yasuo", Name = "Yasuo_Base_W_windwall5.troy", EndTime = 4000, MenuCode = "Yasuo W", SpriteName = Resources.YasuoW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Yasuo", EndTime = 15000, MenuCode = "Yasuo R", SpriteName = Resources.YasuoR, Importance = Importance.Low });
            };

            if (Hero.Contains("Zac"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Unknown, GameObject = true, ChampionName = "Zac", Name = "", EndTime = 8000, MenuCode = "Zac Passive", SpriteName = Resources.ZacP });
            };

            if (Hero.Contains("Zed"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Zed", Name = "Zed_Base_W_cloneswap_buf", EndTime = 4000, MenuCode = "Zed W", SpriteName = Resources.ZedW, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Zed", Name = "Zed_Base_R_cloneswap_buf", EndTime = 6000, MenuCode = "Zed R", SpriteName = Resources.ZedR, Importance = Importance.High });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Zed", Name = "", MenuCode = "Zed R", SpriteName = Resources.ZedR });
            };

            if (Hero.Contains("Ziggs"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Ziggs", Name = "Ziggs_Base_W_aoe_", EndTime = 4000, MenuCode = "Ziggs W", SpriteName = Resources.ZiggsW, Importance = Importance.High });
            };

            if (Hero.Contains("Zilean"))
            {
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, GameObject = true, ChampionName = "Zilean", Name = "Zilean_Base_Q_Indicator_", EndTime = 3000, MenuCode = "Zilean Q", SpriteName = Resources.ZileanQ, Importance = Importance.High });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Zilean", Name = "TimeWarp", MenuCode = "Zilean E", SpriteName = Resources.ZileanE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Buff = true, ChampionName = "Zilean", Name = "ChronoShift", MenuCode = "Zilean R", SpriteName = Resources.ZileanR, DrawType = DrawType.NumberLine, Importance = Importance.VeryHigh });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, GameObject = true, ChampionName = "Zilean", Name = "ZileanBase_R_Buf.troy", EndTime = 3000, MenuCode = "Zilean R", SpriteName = Resources.ZileanR, DrawType = DrawType.NumberLine, Importance = Importance.VeryHigh });
            };

            if (Hero.Contains("Zyra"))
            {
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Unknown, GameObject = true, ChampionName = "Zyra", Name = "", EndTime = 2000, MenuCode = "Zyra Passive", SpriteName = Resources.ZyraP });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.Q, GameObject = true, ChampionName = "Zyra", Name = "", EndTime = 10000, MenuCode = "Zyra Q", SpriteName = Resources.ZyraQ });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.W, GameObject = true, ChampionName = "Zyra", Name = "", EndTime = 30000, MenuCode = "Zyra W", SpriteName = Resources.ZyraW });
                //Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.E, GameObject = true, ChampionName = "Zyra", Name = "", EndTime = 10000, MenuCode = "Zyra E", SpriteName = Resources.ZyraE });
                Database.Add(new Spell { SpellType = SpellType.Spell, Slot = SpellSlot.R, ChampionName = "Zyra", EndTime = 2000, SkillShot = true, MenuCode = "Zyra R", SpriteName = Resources.ZyraR, Importance = Importance.High });
            };
            #endregion
        }
    }
}