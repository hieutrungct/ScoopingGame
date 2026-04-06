using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using SimpleJSON;
using UnityEngine;
using System.IO;
using System;
using Rubik.Manager;
using Rubik.Config;
using Rubik.UI;

namespace Rubik.DataCenter
{
    public class DataCenterConfig
    {
        public const string API_DataCenter_CheckVersion = "/api/multiplayer/data_center/check_version";
    }

    public class DataCenterManager : NTBehaviour
    {
        public DataVersion DataVersion;

        public TextAsset DataVersionText;

        public static DataCenterManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (DataCenterManager.Instance != null)
            {
                NTLog.LogWarning("Only 1 instance allow");
                return;
            }
            DataCenterManager.Instance = this;
        }

        public override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadDataVersionData();
        }

        protected void LoadDataVersionData()
        {
            try
            {
                string data = this.GetData(DataName.DataVersion);
                if (data == null) this.DataVersion = JsonUtility.FromJson<DataVersion>(this.DataVersionText.text);
                else this.DataVersion = JsonUtility.FromJson<DataVersion>(data);
            }
            catch (System.Exception e)
            {
                NTLog.LogError(e.ToString(), gameObject);
                this.DataVersion = new DataVersion();
            }

        }

        public bool CheckVersionDone = false;
        public IEnumerator ProcessCheckVersion(Action done = null)
        {
            this.CheckVersionDone = false;

            while (!this.CheckVersionDone)
            {

                yield return this.IECheckVersion();

                List<DataName> dataNames = new List<DataName>()
                {
                    DataName.DataVersion,
                    DataName.VersionGame,
                    DataName.AvatarData,
                    DataName.AvatarBorderData,
                    DataName.CostChangeName,
                    DataName.SkinData,
                    DataName.RestaurentShopData,
                    DataName.ClothShopData,
                    DataName.ItemDataInfo,
                    DataName.DailyRewardData,
                    DataName.CharacterClothData,
                    DataName.CharacterPlayerData,
                    DataName.ServerGameData,
                    DataName.MercaShopData,
                    DataName.MercaItemData,
                    DataName.MercaStreetData,
                    DataName.MercaShopRarityData,
                    DataName.MercaShopLevelData,
                };
                bool isAllDataDone = true;
                foreach (DataName dataName in dataNames)
                {
                    if (this.GetData(dataName) == null)
                    {
                        isAllDataDone = false;
                        NTLog.LogError("ProcessCheckVersion: Data not found: " + dataName.ToString());
                    }
                }
                if (isAllDataDone)
                {
                    this.CheckVersionDone = true;
                    break;
                }
                yield return new WaitForSeconds(1f);
            }
        }

        public IEnumerator IECheckVersion(Action done = null)
        {
            this.LoadDataVersionData();
            JSONNode jdata = new JSONObject();
            jdata["dataVersion"] = JSONNode.Parse(JsonUtility.ToJson(this.DataVersion));
            yield return Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), URL_Config.BASE_API_URL + DataCenterConfig.API_DataCenter_CheckVersion, (data) =>
            {
                jdata = JSONNode.Parse(data.downloadHandler.text);
                DataVersion dataVersion = JsonUtility.FromJson<DataVersion>(jdata["Data"]["DataVersion"].ToString());
                if (jdata["Data"]["VersionGame"] != null)
                {
                    this.SetData(DataName.VersionGame, jdata["Data"]["VersionGame"].ToString());
                    this.DataVersion.VersionGame = dataVersion.VersionGame;
                }
                if (jdata["Data"]["AvatarData"] != null)
                {
                    this.SetData(DataName.AvatarData, jdata["Data"]["AvatarData"].ToString());
                    this.DataVersion.AvatarData = dataVersion.AvatarData;
                }
                if (jdata["Data"]["AvatarBorderData"] != null)
                {
                    this.SetData(DataName.AvatarBorderData, jdata["Data"]["AvatarBorderData"].ToString());
                    this.DataVersion.AvatarBorderData = dataVersion.AvatarBorderData;
                }
                if (jdata["Data"]["CostChangeName"] != null)
                {
                    this.SetData(DataName.CostChangeName, jdata["Data"]["CostChangeName"].ToString());
                    this.DataVersion.CostChangeName = dataVersion.CostChangeName;
                }
                if (jdata["Data"]["SkinData"] != null)
                {
                    this.SetData(DataName.SkinData, jdata["Data"]["SkinData"].ToString());
                    this.DataVersion.SkinData = dataVersion.SkinData;
                }
                if (jdata["Data"]["RestaurentShopData"] != null)
                {
                    this.SetData(DataName.RestaurentShopData, jdata["Data"]["RestaurentShopData"].ToString());
                    this.DataVersion.RestaurentShopData = dataVersion.RestaurentShopData;
                }
                if (jdata["Data"]["ClothShopData"] != null)
                {
                    this.SetData(DataName.ClothShopData, jdata["Data"]["ClothShopData"].ToString());
                    this.DataVersion.ClothShopData = dataVersion.ClothShopData;
                }
                if (jdata["Data"]["ItemDataInfo"] != null)
                {
                    this.SetData(DataName.ItemDataInfo, jdata["Data"]["ItemDataInfo"].ToString());
                    this.DataVersion.ItemDataInfo = dataVersion.ItemDataInfo;
                }
                if (jdata["Data"]["DailyRewardData"] != null)
                {
                    this.SetData(DataName.DailyRewardData, jdata["Data"]["DailyRewardData"].ToString());
                    this.DataVersion.DailyRewardData = dataVersion.DailyRewardData;
                }
                if (jdata["Data"]["CharacterClothData"] != null)
                {
                    this.SetData(DataName.CharacterClothData, jdata["Data"]["CharacterClothData"].ToString());
                    this.DataVersion.CharacterClothData = dataVersion.CharacterClothData;
                }
                if (jdata["Data"]["CharacterPlayerData"] != null)
                {
                    this.SetData(DataName.CharacterPlayerData, jdata["Data"]["CharacterPlayerData"].ToString());
                    this.DataVersion.CharacterPlayerData = dataVersion.CharacterPlayerData;
                }
                if (jdata["Data"]["ServerGameData"] != null)
                {
                    this.SetData(DataName.ServerGameData, jdata["Data"]["ServerGameData"].ToString());
                    this.DataVersion.ServerGameData = dataVersion.ServerGameData;
                }
                if (jdata["Data"]["MercaShopData"] != null)
                {
                    this.SetData(DataName.MercaShopData, jdata["Data"]["MercaShopData"].ToString());
                    this.DataVersion.MercaShopData = dataVersion.MercaShopData;
                }
                if (jdata["Data"]["MercaItemData"] != null)
                {
                    this.SetData(DataName.MercaItemData, jdata["Data"]["MercaItemData"].ToString());
                    this.DataVersion.MercaItemData = dataVersion.MercaItemData;
                }
                if (jdata["Data"]["MercaStreetData"] != null)
                {
                    this.SetData(DataName.MercaStreetData, jdata["Data"]["MercaStreetData"].ToString());
                    this.DataVersion.MercaStreetData = dataVersion.MercaStreetData;
                }
                if (jdata["Data"]["MercaShopRarityData"] != null)
                {
                    this.SetData(DataName.MercaShopRarityData, jdata["Data"]["MercaShopRarityData"].ToString());
                    this.DataVersion.MercaShopRarityData = dataVersion.MercaShopRarityData;
                }
                if (jdata["Data"]["MercaShopLevelData"] != null)
                {
                    this.SetData(DataName.MercaShopLevelData, jdata["Data"]["MercaShopLevelData"].ToString());
                    this.DataVersion.MercaShopLevelData = dataVersion.MercaShopLevelData;
                }
                if (jdata["Data"]["LimitLandPotRentData"] != null)
                {
                    this.SetData(DataName.LimitLandPotRentData, jdata["Data"]["LimitLandPotRentData"].ToString());
                    this.DataVersion.LimitLandPotRentData = dataVersion.LimitLandPotRentData;
                }
                if (jdata["Data"]["ExpPlayerData"] != null)
                {
                    this.SetData(DataName.ExpPlayerData, jdata["Data"]["ExpPlayerData"].ToString());
                    this.DataVersion.ExpPlayerData = dataVersion.ExpPlayerData;
                }
                if (jdata["Data"]["MiniGameFishingData"] != null)
                {
                    this.SetData(DataName.MiniGameFishingData, jdata["Data"]["MiniGameFishingData"].ToString());
                    this.DataVersion.MiniGameFishingData = dataVersion.MiniGameFishingData;
                }
                if (jdata["Data"]["MiniGameFishingBagData"] != null)
                {
                    this.SetData(DataName.MiniGameFishingBagData, jdata["Data"]["MiniGameFishingBagData"].ToString());
                    this.DataVersion.MiniGameFishingBagData = dataVersion.MiniGameFishingBagData;
                }
                if (jdata["Data"]["MiniGameFishingLevelData"] != null)
                {
                    this.SetData(DataName.MiniGameFishingLevelData, jdata["Data"]["MiniGameFishingLevelData"].ToString());
                    this.DataVersion.MiniGameFishingLevelData = dataVersion.MiniGameFishingLevelData;
                }
                if (jdata["Data"]["MiniGameFishingDataPlayer"] != null)
                {
                    this.SetData(DataName.MiniGameFishingDataPlayer, jdata["Data"]["MiniGameFishingDataPlayer"].ToString());
                    this.DataVersion.MiniGameFishingDataPlayer = dataVersion.MiniGameFishingDataPlayer;
                }
                if (jdata["Data"]["MiniGameFishingRobData"] != null)
                {
                    this.SetData(DataName.MiniGameFishingRobData, jdata["Data"]["MiniGameFishingRobData"].ToString());
                    this.DataVersion.MiniGameFishingRobData = dataVersion.MiniGameFishingRobData;
                }
                if (jdata["Data"]["MiniGameFishingBaitData"] != null)
                {
                    this.SetData(DataName.MiniGameFishingBaitData, jdata["Data"]["MiniGameFishingBaitData"].ToString());
                    this.DataVersion.MiniGameFishingBaitData = dataVersion.MiniGameFishingBaitData;
                }
                if (jdata["Data"]["EnglishLanguageData"] != null)
                {
                    this.SetData(DataName.EnglishLanguageData, jdata["Data"]["EnglishLanguageData"].ToString());
                    this.DataVersion.EnglishLanguageData = dataVersion.EnglishLanguageData;
                }
                if (jdata["Data"]["MiniGameForestGameData"] != null)
                {
                    this.SetData(DataName.MiniGameForestGameData, jdata["Data"]["MiniGameForestGameData"].ToString());
                    this.DataVersion.MiniGameForestGameData = dataVersion.MiniGameForestGameData;
                }
                if (jdata["Data"]["PlayerChestDataHolder"] != null)
                {
                    this.SetData(DataName.PlayerChestDataHolder, jdata["Data"]["PlayerChestDataHolder"].ToString());
                    this.DataVersion.PlayerChestDataHolder = dataVersion.PlayerChestDataHolder;
                }
                this.SetData(DataName.DataVersion, JsonUtility.ToJson(this.DataVersion));
                this.CheckVersionDone = true;
                done?.Invoke();
            });
        }

        public void SetData(DataName dataName, string data)
        {
            PlayerPrefs.SetString("Multiplayer" + dataName, data);
        }

        public string GetData(DataName dataName)
        {
            string data = PlayerPrefs.GetString("Multiplayer" + dataName);
            if (data == null || data.Length == 0)
            {
                NTLog.LogError("Not found: " + dataName.ToString(), gameObject);
                return null;
            }
            return data;
        }

        public DataName ShowDataName;
        [NTButton]
        public void ShowData()
        {
            NTLog.LogMessage(this.GetData(this.ShowDataName));
        }
    }
}

