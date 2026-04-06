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

    }
}