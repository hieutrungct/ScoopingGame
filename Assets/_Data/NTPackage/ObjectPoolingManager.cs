using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NTPackage.Functions
{
    public class ObjectPoolingConfig
    {
        public const string RestaurantSellFoodItem = "RestaurantSellFoodItem";
        public const string BoxChatItemUI = "BoxChatItemUI";
        public const string EmojiPrefab = "EmojiPrefab";
        public const string BtnEmoji = "BtnEmoji";
        public const string FriendItemUI = "FriendItemUI";
        public const string RequestItemUI = "RequestItemUI";
        public const string CharacterInUI = "CharacterInUI";
        public const string AvatarChangeItemUI = "AvatarChangeItemUI";
        public const string ChangeSkinPlayerItemUI = "ChangeSkinPlayerItemUI";
        public const string RewardDataItemUI = "RewardDataItemUI";
        public const string ItemInventory = "ItemInventory";
        public const string PlayerMailAttachItemUI = "PlayerMailAttachItemUI";
        public const string PlayerMailItemUI = "PlayerMailItemUI";
        public const string CharacterClothSkeletonAnimation = "CharacterClothSkeletonAnimation";
        public const string CharacterClothSkeletonGraphic = "CharacterClothSkeletonGraphic";
        public const string ChangeBaseCharacterClothItemUI = "ChangeBaseCharacterClothItemUI";
        public const string ChoseCharacterItemUI = "ChoseCharacterItemUI";
        public const string InviteGameRoomItemUI = "InviteGameRoomItemUI";
        public const string MercaLandPotListItemUI = "MercaLandPotListItemUI";
        public const string ItemDataBarUI = "ItemDataBarUI";
        public const string MercaShopOrderItemUI = "MercaShopOrderItemUI";
        public const string MercaLandPotBuildShopItemUI = "MercaLandPotBuildShopItemUI";
        public const string BannerItem = "BannerItem";
        public const string GestureOrb = "GestureOrb";
        public const string GestureCloud = "GestureCloud";
        public const string GestureSymbol = "GestureSymbol";
    }

    public class ObjectPoolingManager : NTBehaviour
    {
        public NTDictionary<string, List<Transform>> ObjectNTDictionary = new NTDictionary<string, List<Transform>>();
        public Transform Holder;

        public static ObjectPoolingManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (ObjectPoolingManager.Instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            ObjectPoolingManager.Instance = this;
        }

        public override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadHolder();
        }

        protected void LoadHolder()
        {
            if (Holder != null) return;
            this.Holder = transform.Find("Holder");
        }

        public virtual void PushChildObjectIntoPooling(Transform trans)
        {
            List<Transform> listTrans = new List<Transform>();
            foreach (Transform item in trans)
            {
                listTrans.Add(item);
            }
            for (int i = listTrans.Count - 1; i >= 0; i--)
            {
                this.PushObjectIntoPooling(listTrans[i]);
            }
        }

        public void PushObjectIntoPooling(Transform trans)
        {
            trans.SetParent(this.Holder);
            trans.gameObject.SetActive(false);
            try
            {
                this.ObjectNTDictionary.Get(trans.name).Add(trans);
            }
            catch (System.Exception)
            {
                List<Transform> lsTrans = new List<Transform>();
                lsTrans.Add(trans);
                this.ObjectNTDictionary.Add(trans.name, lsTrans);
            }
        }

        public Transform PullObjectFromPooling(string nameOb)
        {
            try
            {
                Transform trans = this.ObjectNTDictionary.Get(nameOb)[0];
                this.ObjectNTDictionary.Get(nameOb).RemoveAt(0);
                trans.gameObject.SetActive(true);
                return trans;
            }
            catch (System.Exception)
            {
                try
                {
                    Debug.LogWarning
                    (this.ObjectNTDictionary.Get(nameOb).Count);
                }
                catch (System.Exception)
                { }
                return null;
            }
        }
        public T PullObjectFromPooling<T>(string nameOb)
        {
            Transform trans;
            try
            {
                trans = this.ObjectNTDictionary.Get(nameOb)[0];
                this.ObjectNTDictionary.Get(nameOb).RemoveAt(0);
                trans.gameObject.SetActive(true);
                return trans.GetComponent<T>();
            }
            catch (System.Exception)
            {
                return default(T);
            }
        }

        public T InstantiateObject<T>(string nameOb, Transform prefab)
        {
            Transform trans;
            try
            {
                trans = this.ObjectNTDictionary.Get(nameOb)[0];
                this.ObjectNTDictionary.Get(nameOb).RemoveAt(0);
                trans.name = nameOb;
                trans.gameObject.SetActive(true);
                return trans.GetComponent<T>();
            }
            catch (System.Exception)
            {
                trans = Instantiate(prefab.gameObject).transform;
                trans.gameObject.SetActive(true);
                trans.name = nameOb;
                return trans.GetComponent<T>();
            }
        }
    }
}
