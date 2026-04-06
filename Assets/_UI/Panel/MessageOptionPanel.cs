using System;
using System.Collections;
using System.Collections.Generic;
using NTPackage.UI;
using UnityEngine;
using TMPro;

namespace Rubik.UI
{
    using UnityEngine.UI;

    public class MessageOptionPanel : PopupUI
    {
        public TextMeshProUGUI TextContent;
        public TextMeshProUGUI TextTitle;

        public Action ActionReject;
        public Action ActionConfirm;

        public NTButtonEffect ButtonReject;
        public NTButtonEffect ButtonConfirm;

        public TextMeshProUGUI TextReject;
        public TextMeshProUGUI TextConfirm;

        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            this.ButtonReject.SetActive(false);
            this.ButtonConfirm.SetActive(false);
        }

        public void SetData(string title, string content){
            this.TextTitle.text = title;
            this.TextContent.text = content;
        }

        public void SetActionReject(Action action, string text){
            this.ButtonReject.SetActive(true);
            this.ButtonReject.Onclick.RemoveAllListeners();
            this.ButtonReject.Onclick.AddListener(() => {
                this.ActionReject?.Invoke();
            });
            this.TextReject.text = text;
            this.ActionReject = action;
        }

        public void SetActionConfirm(Action action, string text){
            this.ButtonConfirm.SetActive(true);
            this.ButtonConfirm.Onclick.RemoveAllListeners();
            this.ButtonConfirm.Onclick.AddListener(() => {
                this.ActionConfirm?.Invoke();
            });
            this.TextConfirm.text = text;
            this.ActionConfirm = action;
        }
    }
}
