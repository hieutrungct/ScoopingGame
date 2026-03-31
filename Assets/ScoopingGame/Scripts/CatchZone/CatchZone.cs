using UnityEngine;
// namespace Rubik.ScoopingGame
// {
    public class CatchZone : MonoBehaviour
    {
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
            blindBag.isGrabbed = true;
            Debug.Log("Caught: " + blindBag.id);
            // Xóa object trong scene
            // Destroy(item.gameObject);
        }
    }
// }
