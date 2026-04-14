using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
namespace Rubik.ScoopingGame
{
    public class BlindBagFlyEffect : MonoBehaviour
    {
        public ItemFly itemPrefab;
        public Canvas canvas;
        public float duration = 1f;
        
        public void ItemFlyEffect(Transform startPos, int coinCount, Transform targetUI, Sprite itemIcon, Action onComplete)
        {
            
            transform.DOKill();

            for (int i = 0; i < coinCount; i++)
            {
                ItemFly coin = Instantiate(itemPrefab, canvas.transform);
                coin.transform.position = startPos.position;
                coin.SetUpImage(itemIcon);
                coin.transform.localScale = Vector3.zero;

                float delay = UnityEngine.Random.Range(0f, 0.7f);
                
                Vector3 burstPos = startPos.position + (Vector3)UnityEngine.Random.insideUnitCircle * 1f; 
                
                Vector3 midPoint = Vector3.Lerp(burstPos, targetUI.position, 0.5f) + new Vector3(
                    UnityEngine.Random.Range(-0.3f, 0.3f), 
                    UnityEngine.Random.Range(0.3f, 0.6f), 
                    0
                );

                Vector3[] path = new Vector3[] { burstPos, midPoint, targetUI.position };

                Sequence seq = DOTween.Sequence();

                seq.AppendInterval(delay);

                seq.AppendCallback(() => coin.PlayTrail()); // Start trail effect at the burst position

                seq.Append(coin.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack));
                seq.Join(coin.transform.DOMove(burstPos, 0.4f).SetEase(Ease.OutQuad));

                seq.Append(coin.transform.DOPath(path, duration, PathType.CatmullRom)
                    .SetEase(Ease.InBack)); 
                
                seq.Join(coin.transform.DOScale(0.4f, duration));

                seq.OnComplete(() =>
                {
                    targetUI.DOKill(true);
                    targetUI.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.1f);
                    
                    coin.StopTrailAndDisconnect(); // Stop the trail effect and disconnect it from the coin
                    onComplete?.Invoke();

                    Destroy(coin.gameObject);
                });
            }
        }
        public void BlindBagFlyEffects(Transform startPos, Transform targetUI, Sprite itemIcon)
        {
            ItemFly blindBag = Instantiate(itemPrefab, canvas.transform);
            blindBag.transform.position = startPos.position;
            blindBag.SetUpImage(itemIcon);
            blindBag.transform.localScale = Vector3.zero;

            float delay = UnityEngine.Random.Range(0f, 0.2f);

            Vector3 burstPos = startPos.position + (Vector3)UnityEngine.Random.insideUnitCircle * 1f; 
            Vector3 midPoint = Vector3.Lerp(burstPos, targetUI.position, 0.5f) + new Vector3(
                    UnityEngine.Random.Range(-0.1f, 0.1f), 
                    UnityEngine.Random.Range(0.1f, 0.2f), 
                    0
                );

            Vector3[] path = new Vector3[] { burstPos, midPoint, targetUI.position };

            Sequence seq = DOTween.Sequence();

            seq.AppendInterval(delay);

            seq.AppendCallback(() => blindBag.PlayTrail());

            // scale + xuất hiện
            seq.Append(blindBag.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack));

            // bay theo arc
            seq.Append(blindBag.transform.DOPath(path, duration/2, PathType.CatmullRom)
                .SetEase(Ease.InCubic));

            // scale to lên khi hút vào
            seq.Join(blindBag.transform.DOScale(1.2f, duration/2).SetEase(Ease.InQuad));

            // rotate nhẹ cho đẹp
            seq.Join(blindBag.transform.DORotate(new Vector3(0, 0, UnityEngine.Random.Range(-45, 45)), duration/2));

            seq.OnComplete(() =>
            {
                targetUI.DOKill(true);
                targetUI.DOPunchScale(Vector3.one * 0.15f, 0.1f);
                targetUI.gameObject.SetActive(true); 
                blindBag.StopTrailAndDisconnect();
                Destroy(blindBag.gameObject);
            });
        }
        
        
        
    }
}
