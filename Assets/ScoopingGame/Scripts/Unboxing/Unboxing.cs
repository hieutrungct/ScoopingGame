using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Unboxing : MonoBehaviour
{
    [SerializeField] private RectTransform fullBag;
    [SerializeField] private RectTransform topPart;
    [SerializeField] private RectTransform bottomPart;
    [SerializeField] private GameObject vfxPrefab;
    [SerializeField] private Image flash;
    [SerializeField] private RectTransform reward;
    private bool isOpened;
    private Vector3 initialTopPartPosition;
    private Vector3 initialBottomPartPosition;
    private int rewardAmount;

    void Start()
    {
        SetUp();
        initialTopPartPosition = topPart.position;
        initialBottomPartPosition = bottomPart.position;
        rewardAmount = ScoopingGameController.instance.catchZone.caughtItems.Count;
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
            reward.localScale = Vector3.zero;
        });

        seq.Append(reward.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack));
        seq.Append(reward.DOScale(1f, 0.2f));
        // Camera.main.transform.DOShakePosition(0.2f, 5f);
        isOpened = true;
        
    }
    public void CollectReward()
    {
        if (isOpened && rewardAmount > 0)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(reward.DOScale(0f, 0.3f).SetEase(Ease.InBack));
            seq.OnComplete(() =>
            {
                reward.gameObject.SetActive(false);
                isOpened = false;
                reward.localScale = Vector3.one;
                rewardAmount -= 1;
                SetUp();
            });
        }
        else if (rewardAmount <= 0)
        {
            gameObject.SetActive(false);
            SetUp();
        }
        else
        {
            
        }
    }
}
