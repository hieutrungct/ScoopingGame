using System.Collections.Generic;
using UnityEngine;
// namespace Rubik.ScoopingGame
// {
    public class CatchZone : MonoBehaviour
    {
        public List<BlindBag> caughtItems = new List<BlindBag>();
        
        public void ActiveCatchZone()
        {
            GetComponent<Collider2D>().enabled = true;
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("BlindBag"))
            {
                var blindBag = collision.GetComponent<BlindBag>();
                if (blindBag != null)
                {
                    HandleCatch(blindBag);
                }
            }
            else
            {
                // Debug.LogWarning("Collided with object that is not a BlindBag");
            }
            
        }
        private void HandleCatch(BlindBag blindBag)
        {
            // Add vào inventory
            // GameManager.Instance.AddItem(item.Data);
            // Attach vào spoon
            Spoon spoon = ScoopingGameController.instance.spoon;
            if (spoon != null)
            {
                spoon.AttachItems(caughtItems);
            }
            caughtItems.Add(blindBag);
            blindBag.isGrabbed = true;
            Debug.Log("Caught: " + blindBag.id);
            // Xóa object trong scene
            // Destroy(item.gameObject);
        }
    }
// }
