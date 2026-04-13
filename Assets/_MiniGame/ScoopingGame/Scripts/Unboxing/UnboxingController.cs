using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class UnboxingController : MonoBehaviour
    {
        [SerializeField] private UnboxingUI unboxingUI;
        
        private List<ItemData> items;
        [SerializeField] private bool[] opened;
        private int selectedIndex;
        public bool isOpened;

        public void Init(List<ItemData> rewards)
        {
            items = rewards;
            opened = new bool[rewards.Count];
            selectedIndex = -1;
            isOpened = false;

            unboxingUI.gameObject.SetActive(true);
            unboxingUI.Init();
            unboxingUI.ShowBlindBagSelection(GameController.instance.spoonController.catchZone.transform, items);
        }

        private void OnBlindBagSelected(int index)
        {
            if (items == null || index < 0 || index >= items.Count || opened[index]) return;

            selectedIndex = index;
            unboxingUI.PlayOpen(items[index], () =>
            {
                isOpened = true;
            });
        }

        public void Open(int index)
        {
            if (items == null || index < 0 || index >= items.Count || opened[index]) return;
            selectedIndex = index;
            unboxingUI.PlayOpen(items[selectedIndex], () =>
            {
                isOpened = false;
            });
        }
        

        public void Collect()
        {
            if (!isOpened || selectedIndex < 0 || selectedIndex >= items.Count) return;

            unboxingUI.HideReward(() =>
            {
                GameController.instance.blindBagClassification.ClassifyItems(items[selectedIndex]);
                opened[selectedIndex] = true;
                
                selectedIndex = -1;
                isOpened = false;

                if (AllOpened())
                {
                    unboxingUI.HideAll();
                    GameController.instance.blindBagClassification.ClassifyItemsIntoCollectedItems();
                }
            });
        }

        private bool AllOpened()
        {
            if (opened == null || opened.Length == 0) return true;
            foreach (var itemOpened in opened)
            {
                if (!itemOpened) return false;
            }
            return true;
        }
        public void SkipUnboxing()
        {
            if (items == null) return;

            for (int i = 0; i < items.Count; i++)
            {
                if (!opened[i])
                {
                    GameController.instance.blindBagClassification.ClassifyItems(items[i]);
                    opened[i] = true;
                }
            }

            unboxingUI.HideAll();
            GameController.instance.blindBagClassification.ClassifyItemsIntoCollectedItems();
        }
        // sau này sẽ thêm hiệu ứng đặc biệt khi mở được item hiếm khi người chơi skip
        public void ShowEffectOpenItemRare()
        {
            
        }
    }
}
