using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class SpoonUI : MonoBehaviour
    {
        [SerializeField] private Transform point_1_7;
        [SerializeField] private Transform point_2_6;
        [SerializeField] private Transform point_3;
        [SerializeField] private Transform point_4;
        [SerializeField] private Transform point_5;

        public void PlayScoop(Action onReachCatchZone, Action onComplete)
        {
            transform.DOKill();

            Sequence seq = DOTween.Sequence();
            Vector3 startPos = transform.position;

            // Path đi ra
            Vector3[] path1 = new Vector3[]
            {
                startPos,
                point_1_7.position,
                point_2_6.position,
            };

            seq.Append(transform.DOPath(path1, 1f, PathType.CatmullRom).SetEase(Ease.Linear));

            // xuống
            seq.Append(transform.DOMove(point_3.position, 1f).SetEase(Ease.Linear));
            seq.Join(transform.DOLocalRotate(new Vector3(0,0,90), 1f).SetEase(Ease.Linear));

            // lên
            seq.Append(transform.DOMove(point_4.position, 0.5f).SetEase(Ease.Linear));
            seq.Join(transform.DOLocalRotate(Vector3.zero, 1f).SetEase(Ease.Linear));

            seq.Append(transform.DOMove(point_5.position, 1f).SetEase(Ease.Linear));

            // quay về
            seq.Append(transform.DOMove(point_2_6.position, 0.7f).SetEase(Ease.Linear));
            seq.AppendCallback(() =>
            {
                onReachCatchZone?.Invoke();
            });

            Vector3[] path2 = new Vector3[]
            {
                point_2_6.position,
                point_1_7.position,
                startPos
            };

            seq.Append(transform.DOPath(path2, 2f, PathType.CatmullRom).SetEase(Ease.Linear));
            seq.OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
        
    }
}
