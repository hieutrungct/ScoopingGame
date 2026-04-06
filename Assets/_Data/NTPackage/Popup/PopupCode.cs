using System;
using UnityEngine;

namespace NTPackage.UI
{
    public class PopupCodeParser
    {
        public static PopupCode FromString(string name)
        {
            //name = name.ToLower();W
            return (PopupCode)Enum.Parse(typeof(PopupCode), name);
        }
    }

    [System.Serializable]
    public enum PopupCode
    {
        Unknown = 0,
        LoadingUI,
        NameChangeUI,
        AvatarChangeUI,
        ChangeSkinPlayerUI,
        ChatUI,
        Emoji_Popup,
        FriendUI,
        RestaurantSellFoodUI,
        MessageOptionPanel,
        GiftCodeUI,
        RewardDataUI,
        ItemDataDetailUI,
        UserDataShortUI,
        InventoryUI,
        MessagePanel,
        DailyRewardPopupUI,
        LanguagePopup,
        SettingPopup,
        PlayerMailUI,
        PlayerMailInfoUI,
        ChangeBaseCharacterClothUI,
        ChoseCharacterUI,
        InviteGameRoomUI,
        MercaLandPotListPopupUI,
        MercaShopOrderPopupUI,
        MercaLandPotBuildShopPopupUI,
        LoginPanel,
        RegisterPanel,
        BannerTopUI,
        PlayerChestUI,
        PlayerChestSelectUI,
        LeaderboardUIController,
    }
}
