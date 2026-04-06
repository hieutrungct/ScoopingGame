using System.Collections;
using NTPackage.Functions;
using Rubik.Banner;
using Rubik.Manager;
using Rubik.MsgDelivery;
using Rubik.UI;
using Rubik.UserData;
using UnityEngine;

namespace Rubik.NotificationMsg
{
    public class NotificationMsgManager : NTBehaviour
    {
        public Coroutine CheckNotificationMsgCoroutine;

        public static NotificationMsgManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (NotificationMsgManager.Instance != null){
               NTLog.LogError("Only 1 Instance allow");
               return;
             }
            NotificationMsgManager.Instance = this;
        }

        #region Function

        public void Logout(){
            if(this.CheckNotificationMsgCoroutine != null){
                this.StopCoroutine(this.CheckNotificationMsgCoroutine);
            }
        }

        public void Join(){
            this.CheckNotificationMsgCoroutine = this.StartCoroutine(this.CheckNotificationMsg());
        }

        public IEnumerator CheckNotificationMsg(){
            while(true){
                this.SendNotificationMsg();
                yield return new WaitForSeconds(60f);
            }
        }

        #endregion

        #region Send Msg
        public void SendNotificationMsg(){
            MsgDeliveryRoom.Instance.SendMessage(MsgDeliveryKey.NotificationMsg, UserDataManager.Instance.GetUserID());
        }
        #endregion

        #region Event

        public void OnNotificationMsg(string data){
            NTLog.LogMessage("OnNotificationMsg: " + data);
            NotificationMsg notificationMsg = JsonUtility.FromJson<NotificationMsg>(data);
            switch(notificationMsg.Type){
                case NotificationMsgType.MessageBanner:
                    break;
                case NotificationMsgType.WarningBanner:
                    break;
                case NotificationMsgType.ErrorBanner:
                    break;
                case NotificationMsgType.MessagePopup:
                    break;
                default:
                    break;
            }
        }

        public void OnMessageBanner(NotificationMsg notificationMsg){
            NotificationMessage notificationMessage = JsonUtility.FromJson<NotificationMessage>(notificationMsg.Data);
            string message = Lean.Localization.LeanLocalization.GetTranslationText(notificationMessage.Message, notificationMessage.Message);
            message = string.Format(message, notificationMessage.Data);
            BannerManager.Instance.AddTopBanner(notificationMsg._id, message, BannerTopType.Message);
        }

        public void OnWarningBanner(NotificationMsg notificationMsg){
            NotificationMessage notificationMessage = JsonUtility.FromJson<NotificationMessage>(notificationMsg.Data);
            string message = Lean.Localization.LeanLocalization.GetTranslationText(notificationMessage.Message, notificationMessage.Message);
            message = string.Format(message, notificationMessage.Data);
            BannerManager.Instance.AddTopBanner(notificationMsg._id, message, BannerTopType.Warning);
        }

        public void OnErrorBanner(NotificationMsg notificationMsg){
            NotificationMessage notificationMessage = JsonUtility.FromJson<NotificationMessage>(notificationMsg.Data);
            string message = Lean.Localization.LeanLocalization.GetTranslationText(notificationMessage.Message, notificationMessage.Message);
            message = string.Format(message, notificationMessage.Data);
            BannerManager.Instance.AddTopBanner(notificationMsg._id, message, BannerTopType.Error);
        }

        public void OnMessagePopup(NotificationMsg notificationMsg){
            NotificationPopup notificationPopup = JsonUtility.FromJson<NotificationPopup>(notificationMsg.Data);
            if(notificationPopup.ForceLogout){
                ServerManager.instance.LogOut();
                string title = Lean.Localization.LeanLocalization.GetTranslationText(notificationPopup.Title, notificationPopup.Title);
                string message = Lean.Localization.LeanLocalization.GetTranslationText(notificationPopup.Message, notificationPopup.Message);
                message = string.Format(message, notificationPopup.Data);
                HUDCanvas.Instance.ShowNotification(message, title);
            }
        }

        #endregion
    }
}