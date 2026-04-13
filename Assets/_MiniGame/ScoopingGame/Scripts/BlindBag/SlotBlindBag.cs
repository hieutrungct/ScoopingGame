using System;
using DG.Tweening;
using NTPackage.Functions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Rubik.ScoopingGame
{
    public class SlotBlindBag : MonoBehaviour
    {
        [SerializeField] private Animator Animator;
        [SerializeField] private Button button;
        [SerializeField] private int slotIndex;
        private Vector3 initialPosition;

        void Start()
        {
            initialPosition = transform.position;
        }

        public void Init(int index)
        {
            Animator.Play("BlindBagSwaying", 0, UnityEngine.Random.value);
            var ub = GameController.instance.unboxingController;
            slotIndex = index;
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => 
                {
                    if (!ub.isOpened)
                    {
                        ub.isOpened = true;
                        GameController.instance.unboxingController.Open(slotIndex);
                        transform.DOMoveY(transform.position.y + 10, 0.5f)
                            .SetEase(Ease.OutCubic)
                            .OnComplete(() => 
                            {
                                transform.DOMove(initialPosition, 0.5f).SetEase(Ease.OutCubic);
                                gameObject.SetActive(false);
                            });

                        NTLog.LogMessage("SlotBlindBag clicked: " + slotIndex);
                    }
                });
            }
        }

        // public void OnPointerClick(PointerEventData eventData)
        // {
        //     onSelected?.Invoke(slotIndex);
        // }
    }
}