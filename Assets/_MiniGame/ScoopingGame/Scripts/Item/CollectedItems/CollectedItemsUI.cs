using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class CollectedItemsUI : MonoBehaviour
    {
        public List<ItemInventory> lsItemInventory;
        public void SetUp(TinyType tinyType)
        {
            lsItemInventory.ForEach(inventory =>
            {
                if (inventory.tinyType == tinyType)
                {
                    inventory.SetUp();
                }
            });
        }
        
    }
}
