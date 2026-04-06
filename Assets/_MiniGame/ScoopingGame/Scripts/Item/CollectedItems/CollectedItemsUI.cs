using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class CollectedItemsUI : MonoBehaviour
    {
        public List<ItemInventory> lsItemInventory;
        public void SetUp(ItemData items)
        {
            lsItemInventory.ForEach(inventory =>
            {
                if (inventory.rarity == items.rarity)
                {
                    inventory.SetUp(items);
                }
            });
        }
        
    }
}
