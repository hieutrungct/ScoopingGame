using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class SlotBlindBag : MonoBehaviour
    {
        [SerializeField] private Animator Animator;
        void Start()
        {
            Animator.Play("BlindBagSwaying", 0, Random.value);
        }
    }
}