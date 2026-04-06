using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.Banner
{
    public class BannerItem : MonoBehaviour
    {

        public List<Image> Backgrounds;
        public TextMeshProUGUI Message;
        public BannerTopData Data;

        public void SetData(BannerTopData data){
            this.Data = data;
            this.Message.text = data.Message;
            for (int i = 0; i < this.Backgrounds.Count; i++)
            {
                this.Backgrounds[i].gameObject.SetActive(false);
            }
            this.Backgrounds[(int)(this.Data.Type-1)%this.Backgrounds.Count].gameObject.SetActive(true);
        }
    }
}