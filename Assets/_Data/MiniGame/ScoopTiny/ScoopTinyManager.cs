using System;
using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using Rubik.Config;
using Rubik.DataCenter;
using Rubik.Manager;
using Rubik.UserData;
using SimpleJSON;
using UnityEngine;

namespace Rubik.ScoopingGame
{
    public class ScoopTinyConfig
    {
        public const string API_ScoopTiny_OpenScoopTiny = "/api/multiplayer/scoop_tiny/open_scoop_tiny";
        public const string API_ScoopTiny_ScoopTiny = "/api/multiplayer/scoop_tiny/scoop_tiny";
        public const string API_ScoopTiny_InviteScoopTinyFriend = "/api/multiplayer/scoop_tiny/invite_scoop_tiny_friend";
    }

    public class ScoopTinyManager : NTBehaviour
    {
        [SerializeField] private ScoopTinyData ScoopTinyData;
        [SerializeField]private UserScoopTiny UserScoopTiny;

        public static ScoopTinyManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (ScoopTinyManager.Instance != null)
            {
                NTLog.LogError("Only 1 Instance allow");
                return;
            }
            ScoopTinyManager.Instance = this;
        }

        #region Function
        public void Logout(){
            this.UserScoopTiny = new UserScoopTiny();
        }

        public IEnumerator LoadData(){
            this.ScoopTinyData = JsonUtility.FromJson<ScoopTinyData>(DataCenterManager.Instance.GetData(DataName.DataScoopTiny));
            yield return null;
        }

        public void UpdateUserScoopTiny(UserScoopTiny userScoopTiny){
            if(userScoopTiny == null || userScoopTiny.Normal == null 
            || userScoopTiny.Normal.PoolTiny == null || userScoopTiny.Normal.HoldTiny == null
            || userScoopTiny.Normal.PoolTiny.Count == 0 || userScoopTiny.Normal.HoldTiny.Count == 0
            ){
                return;
            }
            this.UserScoopTiny = userScoopTiny;
        }
        #endregion

        #region API
        public IEnumerator OpenScoopTiny(){
            JSONNode jdata = new JSONObject();
            jdata["userID"] = UserDataManager.Instance.GetUserID();
            yield return Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), URL_Config.BASE_API_URL + ScoopTinyConfig.API_ScoopTiny_OpenScoopTiny, (data) =>
            {
                ServerManager.instance.APIResponse(data.downloadHandler.text);
            });
        }

        public IEnumerator ScoopTiny(int number, Action<ScoopTinyResult> done = null){
            JSONNode jdata = new JSONObject();
            jdata["userID"] = UserDataManager.Instance.GetUserID();
            jdata["number"] = number;
            yield return Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), URL_Config.BASE_API_URL + ScoopTinyConfig.API_ScoopTiny_InviteScoopTinyFriend, (data) =>
            {
                APIResponseData apiResponseData = ServerManager.instance.APIResponse(data.downloadHandler.text);
                if(apiResponseData.Status == 1){
                    done?.Invoke(apiResponseData.ScoopTinyResult);
                }
            });
        }

        public IEnumerator InviteScoopTinyFriend(long friendID){
            JSONNode jdata = new JSONObject();
            jdata["userID"] = UserDataManager.Instance.GetUserID();
            jdata["friendID"] = friendID;
            yield return Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), URL_Config.BASE_API_URL + ScoopTinyConfig.API_ScoopTiny_InviteScoopTinyFriend, (data) =>
            {
                ServerManager.instance.APIResponse(data.downloadHandler.text);
            });
        }
        #endregion

        #region Getter
        public ScoopTinyData GetScoopTinyData(){
            return this.ScoopTinyData;
        }

        
        #endregion
    }
}