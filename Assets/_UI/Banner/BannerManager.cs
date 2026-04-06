using NTPackage.Functions;
using NTPackage.UI;
using TMPro;
using UnityEngine;

namespace Rubik.Banner
{
    public class BannerManager : NTBehaviour
    {
        public BannerTopUI BannerTopUI;
        
        public static BannerManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (BannerManager.Instance != null){
               NTLog.LogError("Only 1 Instance allow");
               return;
            }
            BannerManager.Instance = this;
        }

        public void AddTopBanner(string id, string message, BannerTopType type){
            if(this.BannerTopUI == null){
                this.BannerTopUI = PopupManager.Instance.GetPopupUIByCode(PopupCode.BannerTopUI) as BannerTopUI;
            }
            if(!this.BannerTopUI.IsShow())
            {
                this.BannerTopUI.OnUI();
            }
            this.BannerTopUI.AddBanner(id, message, type);
        }

        public void RemoveTopBanner(string id){
            // Banner top will remove every 3 seconds
            // this.BannerTopUI.RemoveBanner(id);
        }
    }
}