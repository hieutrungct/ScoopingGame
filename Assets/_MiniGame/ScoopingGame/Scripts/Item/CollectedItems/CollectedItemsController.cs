using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class CollectedItemsController : MonoBehaviour
    {
        [SerializeField] private CollectedItemsUI collectedItemsUI;
        public void UpdateInventoryItems(TinyType tinyType)
        {
            collectedItemsUI.SetUp(tinyType);
        }
        public Transform GetInventoryItemTarget(TinyType tinyType)
        {
            foreach (var inventory in collectedItemsUI.lsItemInventory)
            {
                if (inventory.tinyType == tinyType)
                {
                    return inventory.itemCountText.transform;
                }
            }
            return null;
        }

    }
}