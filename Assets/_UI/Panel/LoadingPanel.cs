using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
namespace Rubik.UI
{
    public class LoadingPanel : MonoBehaviour
    {
        public Transform IconLoading;

        public int ShowCount = 0;
        public double LastTimeShow = 0;

        private void Update()
        {
            if(Time.time - this.LastTimeShow > 5)
            {
                this.ShowCount = 0;
                this.HideLoading();
            }
        }

        public void ShowLoading()
        {
            gameObject.SetActive(true);
            IconLoading.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
            this.ShowCount++;
            this.LastTimeShow = Time.time;
        }


        public void HideLoading()
        {
            IconLoading.DOKill();
            this.ShowCount--;
            if(this.ShowCount <= 0)
            {
                this.ShowCount = 0;
                this.LastTimeShow = 0;
                gameObject.SetActive(false);
            }
        }
    }
}