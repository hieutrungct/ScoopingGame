using System;
using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using Rubik.UI;
using SimpleJSON;
using UnityEngine;

namespace Rubik.UserProfile
{
    using NTPackage.EventDispatcher;
    using Rubik.DataCenter;
    using Rubik.Manager;
    using Rubik.Config;
    using Rubik.UserData;
    using UnityEngine.U2D;
    using NTPackage;
    using NTPackage.UI;

    public class UserProfileConfig
    {
        public const string API_UserData_ChangeDisplayName = "/api/multiplayer/user_data/change_display_name";
        public const string API_UserData_ChangeAvatar = "/api/multiplayer/user_data/change_avatar";
        public const string API_UserData_ChangeAvatarBorder = "/api/multiplayer/user_data/change_avatar_border";
        public const string API_UserData_ChangeSkin = "/api/multiplayer/user_data/change_skin";
        public const string API_UserData_GetUserDataShort = "/api/multiplayer/user_data/get_user_data_short";
    }

    public class UserProfileManager : NTBehaviour
    {

        public List<AvatarBorderData> AvatarBorderDatas;
        public List<AvatarData> AvatarDatas;
        public List<SkinData> SkinDatas;

        public AvatarPlayer AvatarPlayer = new AvatarPlayer();
        public AvatarBorderPlayer AvatarBorderPlayer = new AvatarBorderPlayer();
        public SkinPlayer SkinPlayer = new SkinPlayer();
        public EmojiPlayer EmojiPlayer = new EmojiPlayer();

        public Sprite AvatarSpriteDefault;
        public List<Sprite> AvatarSprites;
        public Sprite AvatarBorderSpriteDefault;
        public List<Sprite> AvatarBorderSprites;
        public List<GameObject> SkinUIObjects;
        public List<GameObject> EmojiObjects;
        public List<Sprite> EmojiSprites;

        public int SkinSelectedIndex;

        // Cache
        public NTDictionary<string, UserDataShort> UserDataShortCache;

        public static UserProfileManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (UserProfileManager.Instance != null)
            {
                NTLog.LogWarning("Only 1 Instance allow");
                return;
            }
            UserProfileManager.Instance = this;
        }

        #region Function

        public IEnumerator LoadData()
        {
            NTLog.LogMessage("UserProfileManager LoadData", gameObject);
            JSONNode jdata = JSONNode.Parse(DataCenterManager.Instance.GetData(DataName.AvatarData));
            this.AvatarDatas = new List<AvatarData>();
            foreach (JSONNode item in jdata)
            {
                this.AvatarDatas.Add(JsonUtility.FromJson<AvatarData>(item.ToString()));
            }

            jdata = JSONNode.Parse(DataCenterManager.Instance.GetData(DataName.AvatarBorderData));
            this.AvatarBorderDatas = new List<AvatarBorderData>();
            foreach (JSONNode item in jdata)
            {
                this.AvatarBorderDatas.Add(JsonUtility.FromJson<AvatarBorderData>(item.ToString()));
            }

            jdata = JSONNode.Parse(DataCenterManager.Instance.GetData(DataName.SkinData));
            this.SkinDatas = new List<SkinData>();
            foreach (JSONNode item in jdata)
            {
                this.SkinDatas.Add(JsonUtility.FromJson<SkinData>(item.ToString()));
            }

            yield return null;
        }

        public void Logout()
        {
            this.AvatarPlayer = new AvatarPlayer();
            this.AvatarBorderPlayer = new AvatarBorderPlayer();
            this.SkinPlayer = new SkinPlayer();
            this.EmojiPlayer = new EmojiPlayer();
        }

        public void Update_AvatarPlayer(AvatarPlayer avatarPlayer)
        {
            if (avatarPlayer.Current < -1)
            {
                return;
            }
            this.AvatarPlayer = avatarPlayer;
        }

        public void Update_AvatarBorderPlayer(AvatarBorderPlayer avatarBorderPlayer)
        {
            if (avatarBorderPlayer.Current < 0)
            {
                return;
            }
            this.AvatarBorderPlayer = avatarBorderPlayer;
        }

        public void Update_SkinPlayer(SkinPlayer skinPlayer)
        {
            if (skinPlayer.Current < 0)
            {
                return;
            }
            this.SkinPlayer = skinPlayer;
            this.SkinSelectedIndex = skinPlayer.Current;
        }

        public void Update_UserProfile(UserDataShort[] userDataShorts){
            foreach (UserDataShort userDataShort in userDataShorts)
            {
                this.UserDataShortCache.Add(userDataShort.UserID, userDataShort);
            }
        }

        public void ChangeDisplayName(string newDisplayName, System.Action callback = null)
        {
            StartCoroutine(IEChangeDisplayName(newDisplayName, callback));
        }

        public void ChangeAvatar(int index, System.Action callback = null)
        {
            StartCoroutine(IEChangeAvatar(index, callback));
        }

        public void TempChangeSkin(int index)
        {
            this.SkinPlayer.Current = index;
            EventListenerManager.instance.PostEvent(EventCode.ChangeSkin);
        }

        public void ChangeSkin(System.Action callback = null)
        {
            StartCoroutine(IEChangeSkin(callback));
        }

        public void ShowUserDataShortUI(string userID)
        {
            PopupManager.Instance.OnUI(PopupCode.UserDataShortUI, (object) userID);
        }

        #endregion

        #region API

        public IEnumerator IEChangeDisplayName(string newDisplayName, System.Action callback = null)
        {
            JSONNode jdata = new JSONObject();
            jdata["userID"] = UserDataManager.Instance.GetUserID();
            jdata["display_name"] = newDisplayName;

            yield return Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), URL_Config.BASE_API_URL + UserProfileConfig.API_UserData_ChangeDisplayName, (data) =>
            {
                ServerManager.instance.APIResponse(data.downloadHandler.text);

                if (callback != null)
                {
                    callback();
                }
                EventListenerManager.instance.PostEvent(EventCode.ChangeDisplayName, (object)newDisplayName);
            });
        }

        public IEnumerator IEChangeAvatar(int index, System.Action callback = null)
        {
            JSONNode jdata = new JSONObject();
            jdata["userID"] = UserDataManager.Instance.GetUserID();
            jdata["index"] = index;
            yield return Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), URL_Config.BASE_API_URL + UserProfileConfig.API_UserData_ChangeAvatar, (data) =>
            {
                ServerManager.instance.APIResponse(data.downloadHandler.text);
                callback?.Invoke();
                EventListenerManager.instance.PostEvent(EventCode.ChangeAvatar);
            });
        }

        public IEnumerator IEChangeAvatarBorder(int index, System.Action callback = null)
        {
            JSONNode jdata = new JSONObject();
            jdata["userID"] = UserDataManager.Instance.GetUserID();
            jdata["index"] = index;

            yield return Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), URL_Config.BASE_API_URL + UserProfileConfig.API_UserData_ChangeAvatarBorder, (data) =>
            {
                ServerManager.instance.APIResponse(data.downloadHandler.text);
                callback?.Invoke();
                EventListenerManager.instance.PostEvent(EventCode.ChangeAvatar, (object)null);
            });
        }

        public IEnumerator IEChangeSkin(System.Action callback = null)
        {
            JSONNode jdata = new JSONObject();
            jdata["userID"] = UserDataManager.Instance.GetUserID();
            jdata["index"] = this.SkinPlayer.Current;
            if (this.SkinSelectedIndex == this.SkinPlayer.Current)
            {
                callback?.Invoke();
                yield break;
            }

            yield return Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), URL_Config.BASE_API_URL + UserProfileConfig.API_UserData_ChangeSkin, (data) =>
            {
                ServerManager.instance.APIResponse(data.downloadHandler.text);
                callback?.Invoke();
                EventListenerManager.instance.PostEvent(EventCode.ChangeSkin);
            });
        }

        public IEnumerator IEGetUserDataShort(string[] userIDs, System.Action callback = null)
        {
            List<string> userIDsList = new List<string>();
            foreach (string userID in userIDs)
            {
                UserDataShort userDataShort = this.GetUserDataShort(userID);
                if (userDataShort != null)
                {
                    // 5 minutes
                    if(userDataShort.LastTimeGet < ServerManager.instance.TimeServer - 5 * 60){
                        userIDsList.Add(userID);
                    }
                }
                else{
                    userIDsList.Add(userID);
                }
            }

            JSONNode jdata = new JSONObject();
            jdata["userIDs"] = userIDsList.ToArray();
            yield return Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), URL_Config.BASE_API_URL + UserProfileConfig.API_UserData_GetUserDataShort, (data) =>
            {
                ServerManager.instance.APIResponse(data.downloadHandler.text);
                callback?.Invoke();
            });
        }
        #endregion

        #region Getter

        public Sprite GetAvatarSprite(int index)
        {
            try
            {
                return this.AvatarSprites[index];
            }
            catch (System.Exception e)
            {
                return this.AvatarSpriteDefault;
            }
            ;
        }

        public Sprite GetAvatarBorderSprite(int index)
        {
            try
            {
                return this.AvatarBorderSprites[index];
            }
            catch (System.Exception e)
            {
                return this.AvatarBorderSpriteDefault;
            }
            ;
        }

        public GameObject GetSkinUIObject(int index)
        {
            return this.SkinUIObjects[index];
        }

        public int GetAvatarUsedIndex()
        {
            return this.AvatarPlayer.Current;
        }

        public int GetAvatarBorderUsedIndex()
        {
            return this.AvatarBorderPlayer.Current;
        }

        public GameObject GetEmojiPrefabByIndex(int index)
        {
            return this.EmojiObjects[index];
        }

        public Sprite GetEmojiSpriteByIndex(int index)
        {
            return this.EmojiSprites[index];
        }

        // Emoji
        public List<int> GetListEmojiAvailable()
        {
            return new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        }

        public UserDataShort GetUserDataShort(string userID)
        {
            return this.UserDataShortCache.Get(userID);
        }
        #endregion
    }
}
