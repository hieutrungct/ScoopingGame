using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class BlindBagClassification : MonoBehaviour
    {
        [SerializeField] private BlindBagClassificationUI blindBagClassificationUI;
        public Transform inventoryItemTarget;
        [ContextMenu("ClassifyItemsIntoCollectedItems")]
        public void ClassifyItemsIntoCollectedItems()
        {
            var bf = GameController.instance.blindBagFlyEffect;
            foreach (var classifiedItem in blindBagClassificationUI.classifiedItems)
            {
                if (classifiedItem.number > 0)
                {
                    Sprite itemIcon = DataAssets.instance.loadImage.IconItems[(int)classifiedItem.rarity];
                    inventoryItemTarget = GameController.instance.collectedItemsController.GetInventoryItemTarget(classifiedItem.rarity);
                    bf.FlyEffect(classifiedItem.transform, classifiedItem.number, inventoryItemTarget, itemIcon);
                }
            }
            StartCoroutine(HideClassificationUI());
        }
        public void ClassifyItems(ItemData items)
        {
            blindBagClassificationUI.ClassifyItemsText(items);
        }
        public void ShowClassificationUI()
        {
            blindBagClassificationUI.gameObject.SetActive(true);
        }
        IEnumerator HideClassificationUI()
        {
            yield return new WaitForSeconds(1f);
            blindBagClassificationUI.gameObject.SetActive(false);
        }

    }
}