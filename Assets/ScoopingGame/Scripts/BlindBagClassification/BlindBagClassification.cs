using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class BlindBagClassification : MonoBehaviour
    {
        public List<ClassifiedItems> classifiedItems;
        public void ClassifyItemsText(ItemData items)
        {
            foreach (var classifiedItem in classifiedItems)
            {
                if (classifiedItem.rarity == items.rarity)
                {
                    classifiedItem.SetUp();
                    return;
                }
            }
        }
        public void ClassifyItems(ItemData items)
        {
            foreach (var classifiedItem in classifiedItems)
            {
                if (classifiedItem.rarity == items.rarity && classifiedItem.number > 0)
                {
                    
                }
            }
        }
    }
}