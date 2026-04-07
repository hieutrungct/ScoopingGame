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
        public const string API_DataCenter_CheckVersion = "/api/multiplayer/scoop_tiny/check_version";
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
                    DataName.DataScoopTiny,
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
                if (jdata["Data"]["DataScoopTiny"] != null)
                {
                    this.SetData(DataName.DataScoopTiny, jdata["Data"]["DataScoopTiny"].ToString());
                    this.DataVersion.DataScoopTiny = dataVersion.DataScoopTiny;
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

