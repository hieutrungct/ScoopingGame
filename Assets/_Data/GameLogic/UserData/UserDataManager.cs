using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTPackage;
using NTPackage.EventDispatcher;
using NTPackage.Functions;
using Rubik.Account;
using Rubik.DataCenter;
using Rubik.Manager;
using Rubik.Config;
using SimpleJSON;
using UnityEngine;

namespace Rubik.UserData
{
    public class UserDataConfig
    {
        public const string API_UserData_LightLogin = "/api/multiplayer/user_data/light_login";
        public const string API_UserData_Login = "/api/multiplayer/user_data/login";
    }

    public class UserDataManager : NTBehaviour
    {
        #region Player Data
        public UserData UserData;
        #endregion

        #region Game Data
        public NTDictionary<int, ExpData> ExpData;
        #endregion

        #region Server Data
        #endregion

        public static UserDataManager Instance;
        protected override void Awake()
        {
            base.Awake();
            UserDataManager.Instance = this;
        }

        public override void LoadComponents()
        {
            base.LoadComponents();
        }

        public IEnumerator LoadData()
        {
            yield return null;
        }

        public void Init()
        {

        }

        public void Logout()
        {
            this.UserData = new UserData();
        }

        public void UpdateUserData(UserDataResponse userDataResponse)
        {

        }

        public void UpdateName(DisplayNameData nameData)
        {

        }

        public string GetUserID()
        {
            return this.UserData._id;
        }

        private int GetPropValue(string name)
        {
            return (int)this.UserData.GetType().GetField(name).GetValue(this.UserData);
        }

        private void SetPropValue(string name, int amount)
        {
            this.UserData.GetType().GetField(name).SetValue(this.UserData, amount);
        }

        private void AddPropValue(string name, int amount)
        {
            this.SetPropValue(name, this.GetPropValue(name) + amount);
        }

        #region API
        public IEnumerator Login(Action<bool> done = null)
        {
            if (AccountManager.Instance.Account._id.Length > 0)
            {
                JSONNode jdata = new JSONObject();
                jdata["accountID"] = AccountManager.Instance.Account._id;
                yield return Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), URL_Config.BASE_API_URL + UserDataConfig.API_UserData_Login, (data) =>
                {
                    ServerManager.instance.APIResponse(data.downloadHandler.text);
                    EventListenerManager.instance.PostEvent(EventCode.BattleDeck_TorchUpdate, this.UserData);
                    done?.Invoke(true);
                    done = null;
                });
                done?.Invoke(false);
            }
        }

        public IEnumerator LightLogin(Action<bool> done = null)
        {
            if (AccountManager.Instance.Account._id.Length > 0)
            {
                JSONNode jdata = new JSONObject();
                jdata["accountID"] = AccountManager.Instance.Account._id;
                jdata["server"] = 0;
                yield return Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), URL_Config.BASE_API_URL + UserDataConfig.API_UserData_LightLogin, (data) =>
                {
                    ServerManager.instance.APIResponse(data.downloadHandler.text);
                    done?.Invoke(true);
                    done = null;
                });
                done?.Invoke(false);
            }
        }

        public IEnumerator GetUserShortData(string userID, Action<UserDataShort> done = null)
        {
            // TODO: Get User Info
            done?.Invoke(null);
            yield return null;
        }
        #endregion

        // level, exp, max ex

        #region Get
        public int GetServerPlay()
        {
            if (this.UserData == null) return 0;
            return this.UserData.Server;
        }

        public string GetDisplayName()
        {
            return this.UserData.DisplayName;
        }

        public string GetUserId()
        {
            return this.UserData._id;
        }

        public (long exp, long expNext, int level) GetPlayerLevel()
        {
            ExpData expData = this.ExpData.Get(this.UserData.Level);
            if (expData == null) return (0, 0, this.UserData.Level);
            long exp = this.UserData.Exp;
            long expNext = expData.Exp;
            return (exp, expNext, expData.Level);
        }

        #endregion

        #region Set


        #endregion
    }
}
