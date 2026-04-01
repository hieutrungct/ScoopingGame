using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BlindBagFlyEffect : MonoBehaviour
{
    public RectTransform coinPrefab;
    public RectTransform targetUI;
    public Canvas canvas;

    public int coinCount = 10;
    public float duration = 0.8f;

    public void Play(Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        for (int i = 0; i < coinCount; i++)
        {
            RectTransform coin = Instantiate(coinPrefab, canvas.transform);
            coin.position = screenPos;

            float delay = Random.Range(0f, 0.2f);

            // random điểm cong
            Vector3 midPoint = screenPos + new Vector2(
                Random.Range(-100f, 100f),
                Random.Range(100f, 200f)
            );

            Vector3[] path = new Vector3[]
            {
                screenPos,
                midPoint,
                targetUI.position
            };

            coin.localScale = Vector3.zero;

            Sequence seq = DOTween.Sequence();

            seq.AppendInterval(delay);

            // scale pop
            seq.Append(coin.DOScale(1f, 0.2f).SetEase(Ease.OutBack));

            // bay theo path
            seq.Join(coin.DOPath(path, duration, PathType.CatmullRom)
                .SetEase(Ease.InOutQuad));

            // nhỏ lại khi gần tới
            seq.Join(coin.DOScale(0.3f, duration));

            seq.OnComplete(() =>
            {
                Destroy(coin.gameObject);

                // cộng tiền ở đây
                // CurrencyManager.Add(1);
            });
        }
    }
}
