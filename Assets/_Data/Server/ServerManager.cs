using System.Collections;
using UnityEngine;
using SimpleJSON;


namespace Rubik.Manager
{
    using UserData;
    using Rubik.Account;
    using NTPackage.Functions;
    using NTPackage.EventDispatcher;
    using NTPackage.UI;
    using UnityEngine.SceneManagement;
    using System;
    using System.Collections.Generic;
    using Rubik.DataCenter;
    using Rubik.Loading;
    using Rubik.Config;
    using Rubik.UserProfile;
    using Rubik.MsgDelivery;
    using System.Threading.Tasks;
    using Colyseus;
    using NTPackage;
    using Rubik.SystemData;
    using Rubik.UI;
    using Rubik.NotificationMsg;
    using Rubik.ScoopingGame;

    public class APIResponse
    {
        public APIResponseData Data;
    }

    [System.Serializable]
    public class APIResponseData
    {
        public string Url_API;
        public int Status;
        public string Error;
        
        public UserDataResponse UserDataResponse;
        public DisplayNameData Update_DisplayName;
        public UserScoopTiny ScoopTiny;
        public ScoopTinyResult ScoopTinyResult;

        public long TimeServer;
    }

    public class ServerManager : NTBehaviour
    {
        public const string KeyToken = "BattleDeck:KeyToken";
        public const string KeyLastServer = "BattleDeck:LastServer";

        #region Loading
        public const float TimeWait = 0.4f;
        public const float CheckVersion = 1;
        public const float AssetsLoading = 2;
        public const float LoadingGameData = 3;
        public const float LoginDeviceID = 4;
        public const float LoginToken = 5;
        public const float JoinSever = 6;
        public const float JoinGame = 7;
        public const float DoneLoad = 7.999f;
        public const float MaxLoad = 8;
        #endregion

        public bool IsLoad = false;

        public bool LoadAddressableDone = false;

        public long TimeServer = 0;
        public Coroutine CorTimeServer;

        public static ServerManager instance;
        protected override void Awake()
        {
            base.Awake();
            if (ServerManager.instance != null)
            {
                NTLog.LogWarning("Only 1 instance allow");
                return;
            }
            ServerManager.instance = this;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            if (IsLoad) return;
            StartCoroutine(LoadData());
            if (this.CorTimeServer != null) StopCoroutine(this.CorTimeServer);
            this.CorTimeServer = StartCoroutine(this.CountTimeServer());
        }

        public long GetTimeServer()
        {
            return this.TimeServer;
        }

        private IEnumerator CountTimeServer()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                this.TimeServer++;
            }
        }

        #region Join Server
        public IEnumerator Play()
        {
            NTLog.LogMessage("Play");
            if (AccountManager.Instance.Account == null || AccountManager.Instance.Account._id.Length == 0)
            {
                bl_SceneLoaderManager.LoadScene(SceneConfig.Login_Screen);
            }
            else
            {
                PopupManager.Instance.OffUI(PopupCode.Popup_Login);
                // Join Server
                EventListenerManager.instance.PostEvent(EventCode.BattleDeck_DoneLoad, new LoadingData(JoinSever / MaxLoad, Lean.Localization.LeanLocalization.GetTranslationText("loading_join_server", "Join Sever")));
                int login_result = -1;
                yield return UserDataManager.Instance.Login((suc) =>
                {
                    if (suc) login_result = 1;
                    else login_result = 0;
                });
                yield return new WaitUntil(() => login_result != -1);
                if (login_result == 0)
                {
                    // Back to Login Screen if Join Server Failed
                    bl_SceneLoaderManager.LoadScene(SceneConfig.Login_Screen);
                    yield break;
                }
                yield return JoinSocket();
                yield return new WaitForSeconds(TimeWait);
                NotificationMsgManager.Instance.Join();

                EventListenerManager.instance.PostEvent(EventCode.BattleDeck_DoneLoad, new LoadingData(JoinGame / MaxLoad, Lean.Localization.LeanLocalization.GetTranslationText("loading_join_game", "Join Game")));

                // if (!CharacterPlayerManager.Instance.IsInitialCloth())
                // {
                //     yield return SceneController.Instance.LoadScreenProgress(SceneConfig.ChangeCharacter);
                // }
                // else
                // {
                    // Change to Home Screen
                    // yield return SceneController.Instance.LoadScreenProgress(SceneConfig.MiniGameScooping_Screen);
                // }
                // Login Success
                yield return SceneController.Instance.LoadScreenProgress(SceneConfig.Login_Screen);


                EventListenerManager.instance.PostEvent(EventCode.BattleDeck_DoneLoad, new LoadingData(DoneLoad / MaxLoad, Lean.Localization.LeanLocalization.GetTranslationText("loading_done", "Loading")));

                yield return new WaitForSeconds(TimeWait);
                PopupManager.Instance.OffUI(PopupCode.LoadingUI);
            }
        }
        #endregion

        public IEnumerator JoinSocket()
        {
            ColyseusRoomManager.Instance.Init();
            MsgDeliveryRoom.Instance.Init();
            yield return null;
        }

        #region LoadData
        public IEnumerator LoadData()
        {
            // wait for logo screen
            yield return new WaitForSeconds(3);
            NTLog.LogMessage("LoadData");
            this.IsLoad = true;
            this.LoadAddressableDone = false;

            EventListenerManager.instance.PostEvent(EventCode.BattleDeck_DoneLoad, new LoadingData(CheckVersion / MaxLoad, Lean.Localization.LeanLocalization.GetTranslationText("loading_checkversion", "Check Version")));
            yield return SystemManager.Instance.IEGetSystemData();
            yield return DataCenterManager.Instance.ProcessCheckVersion();
            EventListenerManager.instance.PostEvent(EventCode.BattleDeck_DoneLoad, new LoadingData(AssetsLoading / MaxLoad, Lean.Localization.LeanLocalization.GetTranslationText("loading_assets", "Assets Loading")));
            yield return LoadAddressable();
            EventListenerManager.instance.PostEvent(EventCode.BattleDeck_DoneLoad, new LoadingData(LoadingGameData / MaxLoad, Lean.Localization.LeanLocalization.GetTranslationText("loading_gamedata", "Game Data Loading")));
            yield return UserDataManager.Instance.LoadData();
            yield return ScoopTinyManager.Instance.LoadData();
            yield return new WaitForSeconds(TimeWait);
            this.AutoLogin();
        }
        #endregion

        public IEnumerator LoadAddressable()
        {
            // Load Addressable
            yield return null;
        }

        public void AutoLogin()
        {
            NTLog.LogMessage("AutoLogin");
            if (AccountManager.Instance.IsAutoLogin() && AccountManager.Instance.GetToken() != "")
            {
                this.LoginByToken();
            }
            else
            {
                PopupManager.Instance.OnUI(PopupCode.Popup_Login);
                PopupManager.Instance.OffUI(PopupCode.LoadingUI);
            }
        }
        public void TapToStart()
        {
            if (AccountManager.Instance.Account != null && AccountManager.Instance.Account._id != null && AccountManager.Instance.Account._id.Length > 0)
            {
                bl_SceneLoaderManager.LoadScene(SceneConfig.MiniGameScooping_Screen);
            }
        }
        public void GameStart()
        {
            NTLog.LogMessage("GameStart");
            if (AccountManager.Instance.Account != null && AccountManager.Instance.Account._id != null && AccountManager.Instance.Account._id.Length > 0)
            {
                StartCoroutine(this.Play());
            }
            else
            {
                this.LoginByDeviceID(true);
            }
        }

        public void LoginByDeviceID(bool isPlay = false)
        {
            EventListenerManager.instance.PostEvent(EventCode.LoadingUI_DoneLoad, new LoadingData(LoginDeviceID / MaxLoad, Lean.Localization.LeanLocalization.GetTranslationText("loading_login_deviceid", "Login DeviceID")));
            StartCoroutine(AccountManager.Instance.LoginByDeviceID(UnityEngine.SystemInfo.deviceUniqueIdentifier, (authenResponse) =>
            {
                if (authenResponse.Status == 1 && isPlay)
                    StartCoroutine(Play());
                else
                {
                    PopupManager.Instance.OnUI(PopupCode.Popup_Login);
                    PopupManager.Instance.OffUI(PopupCode.LoadingUI);
                }
            }));
        }

        public void LoginByGooglePlay()
        {
            EventListenerManager.instance.PostEvent(EventCode.LoadingUI_DoneLoad, new LoadingData(LoginDeviceID / MaxLoad, Lean.Localization.LeanLocalization.GetTranslationText("loading_login_googleplay", "Login GooglePlay")));
            AccountManager.Instance.LoginGooglePlay((authenResponse) =>
            {
                if (authenResponse.Status == 1)
                {
                    StartCoroutine(Play());
                }
                else
                {
                    PopupManager.Instance.OnUI(PopupCode.Popup_Login);
                    PopupManager.Instance.OffUI(PopupCode.LoadingUI);
                }
            });
        }

        public void LoginByToken()
        {
            EventListenerManager.instance.PostEvent(EventCode.LoadingUI_DoneLoad, new LoadingData(LoginToken / MaxLoad, Lean.Localization.LeanLocalization.GetTranslationText("loading_login_token", "Login Token")));
            StartCoroutine(AccountManager.Instance.LoginByToken(AccountManager.Instance.GetToken(), (authenResponse) =>
            {
                if (authenResponse.Status == 1)
                {
                    StartCoroutine(Play());
                }
                else
                {
                    PopupManager.Instance.OnUI(PopupCode.Popup_Login);
                    PopupManager.Instance.OffUI(PopupCode.LoadingUI);
                }
            }));
        }


        public void LoginByUsernamePassword(string username, string password, Action<AuthenResponse> done = null)
        {
            EventListenerManager.instance.PostEvent(EventCode.LoadingUI_DoneLoad, new LoadingData(LoginToken / MaxLoad, Lean.Localization.LeanLocalization.GetTranslationText("loading_login_account", "Login Account")));
            StartCoroutine(AccountManager.Instance.IELogin(username, password, (authenResponse) =>
            {
                if (authenResponse.Status == 1)
                {
                    StartCoroutine(Play());
                    done?.Invoke(authenResponse);
                }
                else
                {
                    PopupManager.Instance.OnUI(PopupCode.Popup_Login);
                    PopupManager.Instance.OffUI(PopupCode.LoadingUI);
                    done?.Invoke(authenResponse);
                }
            }));
        }

        public void LogOut()
        {
            PlayerPrefs.DeleteKey(KeyToken);
            PlayerPrefs.DeleteKey(KeyLastServer);
            AccountManager.Instance.Logout();
            UserDataManager.Instance.Logout();
            NotificationMsgManager.Instance.Logout();
            StartCoroutine(MsgDeliveryRoom.Instance.UnInit());
            bl_SceneLoaderManager.LoadScene(SceneConfig.Login_Screen);
        }

        public List<APIResponseData> LastAPIResponseData = new List<APIResponseData>();

        public APIResponseData APIResponse(string data)
        {
            APIResponse apiResponse = JsonUtility.FromJson<APIResponse>(data);
            APIResponseData apiResponseData = apiResponse.Data;
#if UNITY_EDITOR
            this.LastAPIResponseData.Add(apiResponseData);
#endif
            if (apiResponseData.Status == 0)
            {
                HUDCanvas.Instance.ShowNotification(apiResponseData.Error);
            }

            #region Stream Update
            // Update User Data Response
            UserDataManager.Instance.UpdateUserData(apiResponseData.UserDataResponse);
            UserDataManager.Instance.UpdateName(apiResponseData.Update_DisplayName);
            #endregion
            #region Normal Update
            // Update Scoop Tiny
            ScoopTinyManager.Instance.UpdateUserScoopTiny(apiResponseData.ScoopTiny);
            

            this.TimeServer = apiResponse.Data.TimeServer;
            #endregion
            return apiResponseData;
        }

        public void SetToken(string token)
        {
            PlayerPrefs.SetString(KeyToken, token);
        }

        // Function to wait for scene to load successfully
        public IEnumerator WaitForSceneLoad(string sceneName, Action done = null)
        {
            yield return new WaitUntil(() => SceneManager.GetSceneByName(sceneName).isLoaded);
            done?.Invoke();
        }

        public void SortChildElementByName()
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform child in transform)
            {
                children.Add(child);
            }
            children.Sort((x, y) => string.Compare(x.name, y.name));
            foreach (Transform child in children)
            {
                child.SetSiblingIndex(children.IndexOf(child));

                List<Transform> children2 = new List<Transform>();
                foreach (Transform child2 in child)
                {
                    children2.Add(child2);
                }
                children2.Sort((x, y) => string.Compare(x.name, y.name));
                foreach (Transform child2 in children2)
                {
                    child2.SetSiblingIndex(children2.IndexOf(child2));
                }
            }
        }
    }
}
