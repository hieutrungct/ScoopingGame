using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Rubik.DataCenter
{
    public enum DataName
    {
        DataVersion,
        VersionGame,
        AvatarData,
        AvatarBorderData,
        CostChangeName,
        SkinData,
        RestaurentShopData,
        ClothShopData,
        ItemDataInfo,
        DailyRewardData,
        CharacterClothData,
        CharacterPlayerData,
        ServerGameData,
        MercaShopData,
        MercaItemData,
        MercaStreetData,
        MercaShopRarityData,
        MercaShopLevelData,
        MiniGameFishingData,
        MiniGameFishingBagData,
        MiniGameFishingLevelData,
        MiniGameFishingDataPlayer,
        MiniGameFishingRobData,
        MiniGameFishingBaitData,
        LimitLandPotRentData,
        ExpPlayerData,
        EnglishLanguageData,
        MiniGameForestGameData,
        PlayerChestDataHolder,
    }

    [System.Serializable]
    public class DataVersion
    {
        public int VersionGame = 0;
        public int AvatarData = 0;
        public int AvatarBorderData = 0;
        public int CostChangeName = 0;
        public int SkinData = 0;
        public int RestaurentShopData = 0;
        public int ClothShopData = 0;
        public int ItemDataInfo = 0;
        public int DailyRewardData = 0;
        public int CharacterClothData = 0;
        public int CharacterPlayerData = 0;
        public int ServerGameData = 0;
        public int MercaShopData = 0;
        public int MercaItemData = 0;
        public int MercaStreetData = 0;
        public int MercaShopRarityData = 0;
        public int MercaShopLevelData = 0;
        public int MiniGameFishingData = 0;
        public int MiniGameFishingBagData = 0;
        public int MiniGameFishingLevelData = 0;
        public int MiniGameFishingDataPlayer = 0;
        public int MiniGameFishingRobData = 0;
        public int MiniGameFishingBaitData = 0;
        public int LimitLandPotRentData = 0;
        public int ExpPlayerData = 0;
        public int EnglishLanguageData = 0;
        public int MiniGameForestGameData = 0;
        public int PlayerChestDataHolder = 0;
    }
}