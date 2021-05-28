using BepInEx;
using JotunnLib.Entities;
using JotunnLib.Managers;
using System;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Configuration;
using ValheimLib.ODB;
using UnityEngine;

namespace BleedSword
{
    [BepInPlugin(ModName, Name, Version)]
    [BepInProcess("valheim.exe")]
    [BepInDependency("ValheimModdingTeam.ValheimLib", BepInDependency.DependencyFlags.HardDependency)]
    public class BleedSword : BaseUnityPlugin
    {
        public const string Version = "1.0.0";
        public const string Name = "BleedSword";
        public const string ModName = "mugshot.BleedSword";

            public const string ModGuid = ModName;
            public static ConfigEntry<int> TrophyFenring; //10
            public static ConfigEntry<int> IronRequired; //30
            public static ConfigEntry<int> Bloodbag; //20
            public static ConfigEntry<int> WolfFang; //20
            private static ConfigEntry<int> nexusID;
            internal static BleedSword Instance { get; private set; }
            private void Awake()
            {
                Instance = this;
                LanguageData.Init();
                InitConfigData();
                ObjectDBHelper.OnBeforeCustomItemsAdded += InitStatusEffects;
                ObjectDBHelper.OnBeforeCustomItemsAdded += ItemData.Init;
            }
            private void InitConfigData()
            {
                nexusID = Config.Bind<int>("General", "NexusID", 960, "Nexus mod ID for updates");
                IronRequired = Config.Bind("Crafting", "IronRequired", 30, "Iron required");
            WolfFang = Config.Bind("Crafting", "WolfFang", 30, "Wolf fangs required");
            Bloodbag = Config.Bind("Crafting", "Bloodbag", 20, "Blood Bag requirement");
            TrophyFenring = Config.Bind("Crafting", "TrophyFenring", 4, "Fenring Trophy required");
            }

            private void InitStatusEffects()
            {
                Debug.Log("Initializing status effects");
                var effect = ScriptableObject.CreateInstance<SE_Bleeding>();
                effect.m_name = LanguageData.EffectValue;
                effect.name = LanguageData.EffectValue;
                effect.m_tooltip = LanguageData.BleedSwordTooltipName;

            var effect2 = ScriptableObject.CreateInstance<SE_Bled>();
            effect2.m_name = LanguageData.EffectValue2;
            effect2.name = LanguageData.EffectValue2;

            ObjectDBHelper.Add(new CustomStatusEffect(effect2, true));
            ObjectDBHelper.Add(new CustomStatusEffect(effect, true));
            }
    }
}
