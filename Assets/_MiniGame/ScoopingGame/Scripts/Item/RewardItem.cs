using UnityEngine;
using UnityEngine.UI;
namespace Rubik.ScoopingGame
{
    public class RewardItem : MonoBehaviour
    {
        public Image icon;
        public TinyType tinyType;
        public void SetUp(TinyType tiny)
        {
            transform.localScale = Vector3.one;
            icon.sprite = DataAssets.instance.loadImage.IconItems[(int)tiny];
        }
        
    }
}