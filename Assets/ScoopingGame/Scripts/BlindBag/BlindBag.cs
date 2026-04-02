using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class BlindBag : MonoBehaviour
    {
        public string id;
        public SpriteRenderer icon;
        public bool isGrabbed;
        public void SetUp()
        {
            isGrabbed = false;
        }
        private Rigidbody2D rb;
        private Collider2D col;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
        }

        public void AttachToSpoon(Transform parent)
        {
            // FIX BUG: xuyên collider + rung
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0;

            rb.bodyType = RigidbodyType2D.Kinematic;
            col.enabled = false;

            transform.SetParent(parent);
        }

        public void Detach()
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            col.enabled = true;
            transform.SetParent(null);
        }
        
    }
}
