using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using Rubik.Config;
using SimpleJSON;
using UnityEngine;

namespace Rubik.SystemData
{
    [System.Serializable]
    public class SystemData
    {
        public string[] Versions;
        public string API_Url = "http://15.235.180.137:4080";
        public string Socket_Url = "ws://15.235.180.137:4000";
        public string Colyseus_Url = "http://15.235.180.137:4000";
        public string AppStore_Url = "";
        public string GooglePlay_Url = "";

    }

    public class SystemConfig
    {
        public const string URL_HOST = "http://15.235.180.137:4100";
        public const string URL_HOST_NT = "http://ntdream.click:4100";
        public const string API_GET_SYSTEM = "/api/system/get_system_data";
    }
    public class SystemManager : NTBehaviour
    {
        public SystemData SystemData = new SystemData();

        public static SystemManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (SystemManager.Instance != null)
            {
                NTLog.LogError("Only 1 Instance allow");
                return;
            }
            SystemManager.Instance = this;
        }

        public IEnumerator IEGetSystemData()
        {
            JSONNode jdata = new JSONObject();
            jdata["version"] = Application.version;
            yield return Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), SystemConfig.URL_HOST_NT + SystemConfig.API_GET_SYSTEM, (data) =>
            {
                jdata = JSONNode.Parse(data.downloadHandler.text);
                this.SystemData = JsonUtility.FromJson<SystemData>(jdata["Data"].ToString());
            });
        }
    }
}