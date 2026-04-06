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
                    
                    bf.FlyEffect(classifiedItem.transform, classifiedItem.number, inventoryItemTarget);
                }
            }
        }
        public void ClassifyItems(ItemData items)
        {
            blindBagClassificationUI.ClassifyItemsText(items);
        }

    }
}