namespace BleedSword
{
    class LanguageData
    {
        public const string TokenName = "$custom_item_bleedsword";
        public const string TokenValue = "Haemophilia";

        public const string TokenDescriptionName = "$custom_item_bleedsword_description";
        public const string TokenDescriptionValue = "A master artist takes his time when he's painting. Unfortunately, my kind doesn't often have that luxury.";

        public const string EffectName = "$se_bleeding_name";
        public const string EffectValue = "BleedSword";


        public const string EffectName2 = "$se_bled_name";
        public const string EffectValue2 = "Bleeding";

        public const string BleedSwordTooltipName = "$se_bleeding_name";
        public const string BleedSwordTooltipValue = "A master artist takes his time when he's painting. Unfortunately, my kind doesn't often have that luxury. Enemies that die by bleeding explode for 30% of their maximum health and deal that damage to nearby enemies.";

        public static void Init()
        {
            ValheimLib.Language.AddToken(TokenName, TokenValue, true);
            ValheimLib.Language.AddToken(TokenDescriptionName, TokenDescriptionValue, true);
            ValheimLib.Language.AddToken(EffectName, EffectValue, true);
            ValheimLib.Language.AddToken(EffectName2, EffectValue2, true);
            ValheimLib.Language.AddToken(BleedSwordTooltipName, BleedSwordTooltipValue, true);
        }
    }
}
