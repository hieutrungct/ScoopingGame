using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace Rubik.ScoopingGame
{
    public class Unboxing : MonoBehaviour
    {
        [SerializeField] private RectTransform fullBag;
        [SerializeField] private RectTransform topPart;
        [SerializeField] private RectTransform bottomPart;
        [SerializeField] private GameObject vfxPrefab;
        [SerializeField] private Image flash;
        public RewardItem reward;
        private bool isOpened;
        private Vector3 initialTopPartPosition;
        private Vector3 initialBottomPartPosition;
        private int rewardIndex;

        void Start()
        {
            SetUp();
            initialTopPartPosition = topPart.position;
            initialBottomPartPosition = bottomPart.position;
        }
        public void SetUp()
        {
            transform.DOKill();
            Sequence seq = DOTween.Sequence();
            seq.Append(fullBag.DOMoveY(0,0.5f).From(new Vector3(0,500,0)).SetEase(Ease.OutCubic));
            seq.OnComplete(() =>
            {
                vfxPrefab.SetActive(true);
            });
            vfxPrefab.SetActive(false);
            fullBag.gameObject.SetActive(true);
            topPart.gameObject.SetActive(false);
            bottomPart.gameObject.SetActive(false);
            flash.gameObject.SetActive(false);
            reward.gameObject.SetActive(false);
            topPart.position = initialTopPartPosition;
            bottomPart.position = initialBottomPartPosition;

        }
        public void OpenBlindBag()
        {
            var s = ScoopingGameController.instance;

            transform.DOKill();
            Sequence seq = DOTween.Sequence();

            // 🔹 Phase 1: Shake
            seq.Append(fullBag.DOShakeScale(0.5f, 0.1f));

            // 🔹 Phase 2: Tear
            seq.AppendCallback(() =>
            {
                fullBag.gameObject.SetActive(false);
                topPart.gameObject.SetActive(true);
                bottomPart.gameObject.SetActive(true);
            });

            seq.Append(topPart.DOAnchorPosY(300, 0.4f).SetEase(Ease.OutCubic));
            seq.Join(bottomPart.DOAnchorPosY(-300, 0.4f).SetEase(Ease.OutCubic));

            // 🔹 Phase 3: Flash
            seq.AppendCallback(() =>
            {
                flash.gameObject.SetActive(true);
                flash.color = new Color(1,1,1,1);
            });

            seq.Append(flash.DOFade(0, 0.3f));

            // 🔹 Phase 4: Reward
            seq.AppendCallback(() =>
            {
                reward.gameObject.SetActive(true);
                reward.SetUp(s.caughtItems[rewardIndex]);
                
            });

            seq.Append(reward.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack));
            seq.Append(reward.transform.DOScale(1f, 0.2f));
            
            isOpened = true;
            
        }
        public void CollectReward()
        {
            var s = ScoopingGameController.instance;
            if (isOpened && rewardIndex < ScoopingGameController.instance.catchZone.caughtItems.Count)
            {
                Sequence seq = DOTween.Sequence();
                seq.Append(reward.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack));
                seq.OnComplete(() =>
                {
                    reward.gameObject.SetActive(false);
                    isOpened = false;
                    
                    s.blindBagClassification.ClassifyItemsText(s.caughtItems[rewardIndex]);
                    rewardIndex++;
                    SetUp();
                });
            }
            else if (rewardIndex >= ScoopingGameController.instance.catchZone.caughtItems.Count)
            {
                gameObject.SetActive(false);
                SetUp();
                
            }
            else
            {
                
            }
        }
    }
}