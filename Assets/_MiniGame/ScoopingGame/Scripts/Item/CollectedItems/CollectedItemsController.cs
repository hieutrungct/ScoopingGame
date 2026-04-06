using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class CollectedItemsController : MonoBehaviour
    {
        public CollectedItemsUI collectedItemsUI;
        public void UpdateInventoryItems(ItemData items)
        {
            collectedItemsUI.SetUp(items);
        }
        public Transform GetInventoryItemTarget(Rarity rarity)
        {
            foreach (var inventory in collectedItemsUI.lsItemInventory)
            {
                if (inventory.rarity == rarity)
                {
                    return inventory.itemCountText.transform;
                }
            }
            return null;
        }

    }
}