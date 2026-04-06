using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class BlindBagClassificationUI : MonoBehaviour
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
    }
}