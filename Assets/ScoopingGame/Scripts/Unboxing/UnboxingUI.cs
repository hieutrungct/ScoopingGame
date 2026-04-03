using System;
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

        private Vector3 initialTopPartPosition;
        private Vector3 initialBottomPartPosition;

        public void Init()
        {
            initialTopPartPosition = topPart.position;
            initialBottomPartPosition = bottomPart.position;
            ResetUI();
        }

        public void ResetUI()
        {
            transform.DOKill();

            vfxPrefab.SetActive(false);
            fullBag.gameObject.SetActive(true);
            topPart.gameObject.SetActive(false);
            bottomPart.gameObject.SetActive(false);
            flash.gameObject.SetActive(false);
            reward.gameObject.SetActive(false);

            topPart.position = initialTopPartPosition;
            bottomPart.position = initialBottomPartPosition;

            fullBag.DOMoveY(0, 0.5f)
                .From(new Vector3(0, 500, 0))
                .SetEase(Ease.OutCubic)
                .OnComplete(() => vfxPrefab.SetActive(true));
        }

        public void PlayOpen(ItemData item, Action onOpened)
        {
            transform.DOKill();

            Sequence seq = DOTween.Sequence();

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
            gameObject.SetActive(false);
        }
    }
}