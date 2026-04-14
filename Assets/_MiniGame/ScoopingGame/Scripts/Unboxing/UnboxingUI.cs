using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace Rubik.ScoopingGame
{
    public class UnboxingUI : MonoBehaviour
    {
        [SerializeField] private RectTransform fullBag;
        [SerializeField] private RectTransform topPart;
        [SerializeField] private RectTransform bottomPart;
        [SerializeField] private GameObject vfxPrefab;
        [SerializeField] private Image flash;
        [SerializeField] private RewardItem reward;
        [SerializeField] private List<SlotBlindBag> slotBlindBag;

        private Vector3 initialTopPartPosition;
        private Vector3 initialBottomPartPosition;

        void Start()
        {
            initialTopPartPosition = topPart.position;
            initialBottomPartPosition = bottomPart.position;
        }
        public void Init()
        {
            ResetUI();
            foreach (var slot in slotBlindBag)
            {
                slot.gameObject.SetActive(false);
            }
        }

        public void ResetUI()
        {
            vfxPrefab.SetActive(false);
            fullBag.gameObject.SetActive(false);
            topPart.gameObject.SetActive(false);
            bottomPart.gameObject.SetActive(false);
            flash.gameObject.SetActive(false);
            reward.gameObject.SetActive(false);

            topPart.position = initialTopPartPosition;
            bottomPart.position = initialBottomPartPosition;

            // fullBag.DOMoveY(0, 0.5f)
            //     .From(new Vector3(0, 500, 0))
            //     .SetEase(Ease.OutCubic)
            //     .OnComplete(() => vfxPrefab.SetActive(true));
        }

        public void ShowBlindBagSelection(Transform startPos, List<TinyType> rewards)
        {
            
            for (int i = 0; i < slotBlindBag.Count; i++)
            {
                if (i < rewards.Count)
                {
                    slotBlindBag[i].Init(i);
                    Sprite itemIcon = DataAssets.instance.loadImage.IconBlindBags[0]; // Giả sử tất cả blind bag đều có cùng 1 icon, có thể thay đổi sau
                    GameController.instance.blindBagFlyEffect.BlindBagFlyEffects(startPos, slotBlindBag[i].transform, itemIcon);
                }
            }
        }

        public void HideSlot(int index)
        {
            if (index >= 0 && index < slotBlindBag.Count)
            {
                slotBlindBag[index].gameObject.SetActive(false);
            }
        }

        public void PlayOpen(TinyType item, Action onOpened)
        {
            ResetUI();

            transform.DOKill();
            Sequence seq = DOTween.Sequence();
            fullBag.gameObject.SetActive(true);
            seq.Append(fullBag.DOMoveY(0, 0.5f)
                .From(new Vector3(0, 500, 0))
                .SetEase(Ease.OutCubic)
                .OnComplete(() => vfxPrefab.SetActive(true)));

            // Shake
            seq.Append(fullBag.DOShakeScale(0.5f, 0.1f));

            // Tear
            seq.AppendCallback(() =>
            {
                fullBag.gameObject.SetActive(false);
                topPart.gameObject.SetActive(true);
                bottomPart.gameObject.SetActive(true);
            });

            seq.Append(topPart.DOAnchorPosY(300, 0.4f));
            seq.Join(bottomPart.DOAnchorPosY(-300, 0.4f));

            // Flash
            seq.AppendCallback(() =>
            {
                flash.gameObject.SetActive(true);
                flash.color = new Color(1,1,1,1);
            });

            seq.Append(flash.DOFade(0, 0.3f));

            // Show reward
            seq.AppendCallback(() =>
            {
                reward.gameObject.SetActive(true);
                reward.SetUp(item);
            });

            seq.Append(reward.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack));
            seq.Append(reward.transform.DOScale(1f, 0.2f));

            seq.OnComplete(() =>
            {
                onOpened?.Invoke();
            });
        }

        public void HideReward(Action onDone)
        {
            transform.DOKill();

            reward.transform.DOScale(0f, 0.3f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    reward.gameObject.SetActive(false);
                    onDone?.Invoke();
                });
        }

        public void HideAll()
        {
            transform.DOKill();
            gameObject.SetActive(false);
        }
        public void SkipToReward(TinyType tiny, Action onOpened)
        {
            transform.DOKill();

            fullBag.gameObject.SetActive(false);
            topPart.gameObject.SetActive(true);
            bottomPart.gameObject.SetActive(true);
            flash.gameObject.SetActive(true);
            flash.color = new Color(1,1,1,0);
            reward.gameObject.SetActive(true);
            reward.SetUp(tiny);

            Sequence seq = DOTween.Sequence();
            seq.Append(flash.DOFade(0.5f, 0.2f));
            seq.Append(flash.DOFade(0, 0.2f));
            seq.Append(reward.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack));

            seq.OnComplete(() =>
            {
                onOpened?.Invoke();
            });
        }
    }
}