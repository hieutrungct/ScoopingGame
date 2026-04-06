using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Rubik.ScoopingGame
{
    public class ItemInventory : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemCountText;
        [SerializeField] private Button redeemReward_btn;
        [SerializeField] private int itemCount;
        public Image itemIcon;
        public Rarity rarity;
        public void SetUp(ItemData items)
        {
            itemCount++;
            itemCountText.text = itemCount.ToString();
            if (itemCount > 0)
            {
                redeemReward_btn.interactable = true;
            }
        }
        
        
    }
}