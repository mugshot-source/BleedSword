using HarmonyLib;
using JotunnLib.Entities;
using UnityEngine;
using ValheimLib.Util;
using ValheimLib.ODB;
using ValheimLib;
using ValheimLib.Spawn;
using System.Collections.Generic;

namespace BleedSword
{
    public static class ItemData
    {
        internal static void Init()
        {
            AddCustomItems();
        }

        public static void AddCustomItems()
        {
            // Configure item drop
            // ItemDrop is a component on GameObjects which determines info about the item when it's picked up in the inventory
            var mock = Mock<ItemDrop>.Create("SwordSilver");
            var cloned = Prefab.GetRealPrefabFromMock<ItemDrop>(mock).gameObject.InstantiateClone($"{LanguageData.TokenValue}", true);
            cloned.name = LanguageData.TokenValue;

            var newItemPrefab = cloned;
            var sword = new CustomItem(cloned, fixReference: true);
            var item = sword.ItemDrop;
            item.m_itemData.m_dropPrefab = newItemPrefab;
            item.m_itemData.m_shared.m_name = LanguageData.TokenName;
            item.m_itemData.m_shared.m_description = LanguageData.TokenDescriptionName;
            item.m_itemData.m_shared.m_setName = string.Empty;

            //            item.m_itemData.m_shared.m_setSize = 3;
            item.m_itemData.m_shared.m_weight = 2;
            item.m_itemData.m_shared.m_setStatusEffect = null;
            item.m_itemData.m_shared.m_itemType = ItemDrop.ItemData.ItemType.OneHandedWeapon;
            item.m_itemData.m_shared.m_attackStatusEffect = ValheimLib.Prefab.Cache.GetPrefab<SE_Bleeding>("BleedSword");
            item.m_itemData.m_shared.m_equipStatusEffect = ValheimLib.Prefab.Cache.GetPrefab<SE_Bled>("Bleeding");

            ////////secondary
            item.m_itemData.m_shared.m_secondaryAttack.m_attackChainLevels = 0;
            item.m_itemData.m_shared.m_secondaryAttack.m_attackStamina = 20f;
            item.m_itemData.m_shared.m_secondaryAttack.m_damageMultiplier = 2.5f;
            item.m_itemData.m_shared.m_secondaryAttack.m_forceMultiplier = 30f;
            item.m_itemData.m_shared.m_secondaryAttack.m_staggerMultiplier = 6f;
            ///
            item.m_itemData.m_shared.m_equipDuration = 0.2f;
            item.m_itemData.m_shared.m_maxQuality = 1;
            item.m_itemData.m_shared.m_backstabBonus = 3f;
            item.m_itemData.m_shared.m_aiAttackInterval = 0.2f;
            item.m_itemData.m_shared.m_attackForce = 15f;
            item.m_itemData.m_shared.m_attack.m_attackStamina = 5f;
            item.m_itemData.m_shared.m_skillType = Skills.SkillType.Swords;
            item.m_itemData.m_shared.m_damages.m_slash = 60f;
            item.m_itemData.m_shared.m_damages.m_spirit = 0f;

            item.m_itemData.m_shared.m_secondaryAttack.m_attackAnimation = "knife_secondary";

            var meshRenderer = newItemPrefab.transform.GetComponentInChildren<MeshRenderer>();
            var colorTarget = new Color(0.78431f,0.25490f,0.25490f, 1f);
            meshRenderer.material.color = colorTarget;

             //Add our recipe to the object db so our item can be crafted
             var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = "Recipe_BleedSword";
            recipe.m_item = sword.ItemDrop;
            recipe.m_enabled = true;
            recipe.m_minStationLevel = 4;
            recipe.m_craftingStation = Mock<CraftingStation>.Create("forge");
            var neededResources = new List<Piece.Requirement>
            {
                MockRequirement.Create("TrophyFenring", BleedSword.TrophyFenring.Value),
                MockRequirement.Create("Iron", BleedSword.IronRequired.Value),
                MockRequirement.Create("WolfFang", BleedSword.WolfFang.Value),
                MockRequirement.Create("Bloodbag", BleedSword.Bloodbag.Value)
            };
            recipe.m_resources = neededResources.ToArray();
            var swordRecipe = new CustomRecipe(recipe, true, true);

            ObjectDBHelper.Add(sword);
            ObjectDBHelper.Add(swordRecipe);
        }
    }
}
