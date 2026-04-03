using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class BlindBagClassification : MonoBehaviour
    {
        public BlindBagClassificationUI blindBagClassificationUI;
        
        public void ClassifyItemsText(ItemData items)
        {
            foreach (var classifiedItem in blindBagClassificationUI.classifiedItems)
            {
                if (classifiedItem.rarity == items.rarity)
                {
                    classifiedItem.SetUp();
                    return;
                }
            }
        }
        public void ClassifyItems()
        {
            var bf = GameController.instance.blindBagFlyEffect;
            foreach (var classifiedItem in blindBagClassificationUI.classifiedItems)
            {
                if (classifiedItem.number > 0)
                {
                    
                }
            }
        }
    }
}