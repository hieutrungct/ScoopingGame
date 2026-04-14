using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class BlindBagClassificationUI : MonoBehaviour
    {
        public List<ClassifiedItems> classifiedItems;
        public void ClassifyItemsText(TinyType tiny)
        {
            foreach (var classifiedItem in classifiedItems)
            {
                if (classifiedItem.tinyType == tiny)
                {
                    classifiedItem.SetUp();
                    return;
                }
            }
        }
        public void ResetClassification()
        {
            foreach (var classifiedItem in classifiedItems)
            {
                classifiedItem.number = 0;
                classifiedItem.numberText.text = "0";
            }
        }
    }
}