using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NTPackage.EventDispatcher;
using NTPackage.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.Loading
{
    public class LoadingData
    {
        public float Process;
        public string Detail;

        public LoadingData(float process, string detail)
        {
            this.Process = process;
            this.Detail = detail;
        }
    }

    public class LoadingUI : PopupUI
    {
        public Text TextDetail;

        public Image Progress;
        public Text TextProgress;

        public string OriginText = "Loading";

        public override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadSlider();
            this.LoadTextDetail();
            this.LoadTextProgress();
        }

        protected void LoadSlider()
        {
            if (Progress != null) return;
            this.Progress = this.FindByPath("Panel/Bar/Progress").GetComponent<Image>();
        }

        protected void LoadTextDetail()
        {
            if (TextDetail != null) return;
            this.TextDetail = this.FindByPath("Panel/Bar/TextDetail(Legacy)").GetComponent<Text>();
        }

        protected void LoadTextProgress()
        {
            if (TextProgress != null) return;
            this.TextProgress = this.FindByPath("Panel/Bar/TextProgess(Legacy)").GetComponent<Text>();
        }

        protected override void Start()
        {
            base.Start();
            EventListenerManager.instance.Register(EventCode.BattleDeck_DoneLoad, "LoadingUI", (data) =>
            {
                this.UpdateData((LoadingData)data);
            });

        }

        public void UpdateData(LoadingData loadingData)
        {
            if (!this.IsShow())
            {
                this.OnUI();
                if (loadingData.Process < this.Progress.fillAmount)
                    this.Progress.fillAmount = 0;
            }
            // this.OriginText = loadingData.Detail + " " + (Mathf.Floor(loadingData.Process * 10000)) / 100 + "%";
            this.OriginText = loadingData.Detail;
            this.TextDetail.text = this.OriginText;
            this.TextProgress.text = (Mathf.Floor(loadingData.Process*10000))/100 + "%";
            this.Progress.DOComplete();
            this.Progress.DOFillAmount(loadingData.Process, 0.5f);
            if (CoroutineUpdateTextDetail != null) StopCoroutine(CoroutineUpdateTextDetail);
            CoroutineUpdateTextDetail = StartCoroutine(UpdateTextDetail());
        }

        public override void OffUI()
        {
            if (CoroutineUpdateTextDetail != null) StopCoroutine(CoroutineUpdateTextDetail);
            base.OffUI();
        }

        public Coroutine CoroutineUpdateTextDetail;
        public int time = 0;
        public string[] text = { "    ", ".   ", "..  ", "... ", "...." };
        public IEnumerator UpdateTextDetail()
        {
            while (true)
            {
                time++;
                yield return new WaitForSeconds(0.5f);
                this.TextDetail.text = this.OriginText + text[time % text.Length];
            }
        }
    }
}

