using System.Collections;
using System.Collections.Generic;
using NTPackage.UI;
using Rubik.UI;
using UnityEngine;

namespace Rubik.UI
{
    public class HUDCanvas : MonoBehaviour
    {
        public static HUDCanvas Instance; 
        [SerializeField] LoadingPanel loadingPanel;
        [SerializeField] MessagePanel messagePanel;
        public int LanguageIndex = 0;
       

        // Start is called before the first frame update
        void Awake()
        {
            if(Instance == null)
                Instance = this;
        }
        public void ShowLoadingPanel()
        {
            loadingPanel.ShowLoading();
            loadingPanel.transform.SetAsLastSibling();
        }
        public void HideLoadingPanel()
        {
            loadingPanel.HideLoading();
        }
        public void ShowNotification(string content, string title = "Notification", string buttonConfirm = "Confirm")
        {
            PopupManager.Instance.OnUI(PopupCode.MessagePanel, null, (popupUI) => {
                MessagePanel messagePanel = popupUI as MessagePanel;
                messagePanel.SetData(title, content);
            });
        }
    }
}