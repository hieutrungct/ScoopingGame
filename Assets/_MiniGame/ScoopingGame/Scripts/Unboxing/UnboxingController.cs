using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class UnboxingController : MonoBehaviour
    {
        [SerializeField] private UnboxingUI unboxingUI;
        
        private List<ItemData> items;
        private int currentIndex;
        private bool isOpened;

        public void Init(List<ItemData> rewards)
        {
            items = rewards;
            currentIndex = 0;
            isOpened = false;

            unboxingUI.gameObject.SetActive(true);
            unboxingUI.Init();
        }

        public void Open()
        {
            if (items == null || currentIndex >= items.Count) return;

            unboxingUI.PlayOpen(items[currentIndex], () =>
            {
                isOpened = true;
            });
        }
        public void ShowUnboxingUI()
        {
            unboxingUI.gameObject.SetActive(true);
        }

        public void Collect()
        {
            if (!isOpened) return;

            unboxingUI.HideReward(() =>
            {
                GameController.instance.blindBagClassification.ClassifyItems(items[currentIndex]);

                currentIndex++;
                isOpened = false;

                if (currentIndex < items.Count)
                {
                    unboxingUI.ResetUI();
                }
                else
                {
                    unboxingUI.HideAll();
                    GameController.instance.blindBagClassification.ClassifyItemsIntoCollectedItems();
                }
            });
        }
        public void SkipUnboxing()
        {
            if (items == null || currentIndex >= items.Count) return;
            for(int i = currentIndex; i < items.Count; i++)
            {
                GameController.instance.blindBagClassification.ClassifyItems(items[i]);
            }
            // nếu có phần thưởng nào chưa mở thì sẽ bỏ qua phần mở hộp và phân loại luôn vào kho đồ
            
            
            unboxingUI.HideAll();
            GameController.instance.blindBagClassification.ClassifyItemsIntoCollectedItems();
            
        }
        // sau này sẽ thêm hiệu ứng đặc biệt khi mở được item hiếm khi người chơi skip
        public void ShowEffectOpenItemRare()
        {
            
        }
    }
}
