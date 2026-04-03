using System.Collections.Generic;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class CatchZone : MonoBehaviour
    {
        public List<BlindBag> caughtItems = new List<BlindBag>();
        
        public void ActiveCatchZone()
        {
            GetComponent<Collider2D>().enabled = true;
        }
        public void DeactiveCatchZone()
        {
            GetComponent<Collider2D>().enabled = false;
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
            
            
            caughtItems.Add(blindBag);
            blindBag.isGrabbed = true;
            Debug.Log("Caught: " + blindBag.id);
            AttachItems(caughtItems);
            // Xóa object trong scene
            // Destroy(item.gameObject);
        }
        void AttachItems(List<BlindBag> items)
        {
            // var holdPoint = GameController.instance.spoon.holdPoint;
            foreach (var item in items)
            {
                // item.AttachToSpoon(holdPoint);
            }
        }
    }
}
