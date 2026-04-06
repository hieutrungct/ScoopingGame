using UnityEngine;
using UnityEngine.UI;
namespace Rubik.ScoopingGame
{
    public class RewardItem : MonoBehaviour
    {
        public Image icon;
        public Rarity rarity;
        public void SetUp(ItemData itemData)
        {
            transform.localScale = Vector3.one;
            this.rarity = itemData.rarity;
            icon.sprite = DataAssets.instance.loadImage.IconItems[(int)rarity];
        }
        
    }
}