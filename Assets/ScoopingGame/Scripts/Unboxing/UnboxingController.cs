using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class UnboxingController : MonoBehaviour
    {
        public UnboxingUI unboxingUI;
        [SerializeField] private BlindBagClassification classification;

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

        public void Collect()
        {
            if (!isOpened) return;

            unboxingUI.HideReward(() =>
            {
                classification.ClassifyItemsText(items[currentIndex]);

                currentIndex++;
                isOpened = false;

                if (currentIndex < items.Count)
                {
                    unboxingUI.ResetUI();
                }
                else
                {
                    unboxingUI.HideAll();
                }
            });
        }
    }
}
