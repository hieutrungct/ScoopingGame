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
                    Sprite itemIcon = DataAssets.instance.loadImage.IconItems[(int)classifiedItem.tinyType];
                    inventoryItemTarget = GameController.instance.collectedItemsController.GetInventoryItemTarget(classifiedItem.tinyType);
                    bf.ItemFlyEffect(classifiedItem.transform, classifiedItem.number, inventoryItemTarget, itemIcon, () =>
                    {
                        GameController.instance.collectedItemsController.UpdateInventoryItems(classifiedItem.tinyType);
                    });
                }
            }
            StartCoroutine(HideClassificationUI());
        }
        public void ClassifyItems(TinyType tiny)
        {
            blindBagClassificationUI.ClassifyItemsText(tiny);
        }
        public void ShowClassificationUI()
        {
            blindBagClassificationUI.gameObject.SetActive(true);
        }
        IEnumerator HideClassificationUI()
        {
            yield return new WaitForSeconds(1f);
            blindBagClassificationUI.gameObject.SetActive(false);
            blindBagClassificationUI.ResetClassification();
        }


    }
}