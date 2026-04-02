using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
namespace Rubik.ScoopingGame
{
    public class BlindBagFlyEffect : MonoBehaviour
    {
        public ItemFly itemPrefab;
        public Canvas canvas;
        public float duration = 0.8f;

        public void Play(Vector3 worldPos, int coinCount, RectTransform targetUI)
        {
            // transform.DOKill();
            Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

            for (int i = 0; i < coinCount; i++)
            {
                ItemFly coin = Instantiate(itemPrefab, canvas.transform);
                coin.transform.position = screenPos;

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

                coin.transform.localScale = Vector3.zero;

                Sequence seq = DOTween.Sequence();

                seq.AppendInterval(delay);

                // scale pop
                seq.Append(coin.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));

                // bay theo path
                seq.Join(coin.transform.DOPath(path, duration, PathType.CatmullRom)
                    .SetEase(Ease.InOutQuad));

                // nhỏ lại khi gần tới
                seq.Join(coin.transform.DOScale(0.3f, duration));

                seq.OnComplete(() =>
                {
                    Destroy(coin.gameObject);

                });
            }
        }
    }
}
