using System.Collections.Generic;
using UnityEngine;
namespace Rubik
{
    public class LoadImage : MonoBehaviour
    {
        public List<Sprite> IconItems;
        public List<Sprite> IconBlindBags;
        public void LoadAllSprites()
        {
            IconItems = new List<Sprite>(Resources.LoadAll<Sprite>("IconItems"));
        }
    }
}
