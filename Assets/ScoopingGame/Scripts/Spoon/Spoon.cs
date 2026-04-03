using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
namespace Rubik.ScoopingGame
{
    public enum SpoonState
    {
        Idle,
        Moving,
        ScoopingDown,
        ScoopingUp,
        ScoopingReturn,
        Collecting
    }
    public class Spoon : MonoBehaviour
    {
        public Transform holdPoint;
        [SerializeField] private Transform point_1_7;
        [SerializeField] private Transform point_2_6;
        [SerializeField] private Transform point_3;
        [SerializeField] private Transform point_4;
        [SerializeField] private Transform point_5;
        [SerializeField] private Animator animator;
        public SpoonState currentState = SpoonState.Idle;
        private bool isScooping;
        private Unboxing ub;
        private BlindBagClassification bc;
        private void Start()
        {
            ub = GameController.instance.unboxing;
            bc = GameController.instance.blindBagClassification;
        }
        public void Scoop()
        {
            transform.DOKill();
            // animator.enabled = true;
            Sequence seq = DOTween.Sequence();
            Vector3 startPos = transform.position;
            Vector3[] path1 = new Vector3[]
            {
                startPos,
                point_1_7.position,
                point_2_6.position,
            };
            seq.Append(transform.DOPath(path1, 1f, PathType.CatmullRom)
                .SetEase(Ease.Linear));
            seq.AppendCallback(() =>
            {            
                currentState = SpoonState.ScoopingDown;
            });
            seq.Append(transform.DOMove(point_3.position, 1f).SetEase(Ease.Linear));
            seq.Join(transform.DOLocalRotate(new Vector3(0,0,90), 1f).SetEase(Ease.Linear));
            seq.AppendCallback(() =>
            {
                currentState = SpoonState.ScoopingUp;
            });
            seq.Append(transform.DOMove(point_4.position, 0.5f).SetEase(Ease.Linear));
            // seq.Join(transform.DOLocalRotate(new Vector3(0,0,45), 0.6f).SetEase(Ease.InQuad));
            seq.Join(transform.DOLocalRotate(Vector3.zero, 1f).SetEase(Ease.Linear));
            seq.Append(transform.DOMove(point_5.position, 0.5f).SetEase(Ease.Linear));
            
            seq.AppendCallback(() =>
            {
            
            });
            seq.Append(transform.DOMove(point_2_6.position, 1f).SetEase(Ease.Linear));
            seq.AppendCallback(() =>
            {
                currentState = SpoonState.ScoopingReturn;
                GameController.instance.catchZone.ActiveCatchZone();
            });
            Vector3[] path2 = new Vector3[]
            {
                point_2_6.position,
                point_1_7.position,
                startPos
            };
            seq.Append(transform.DOPath(path2, 2f, PathType.CatmullRom).SetEase(Ease.Linear));
            seq.AppendCallback(() =>
            {
                currentState = SpoonState.Collecting;
                GameController.instance.catchZone.DeactiveCatchZone();
                if (GameController.instance.catchZone.caughtItems.Count > 0)
                {
                    GameController.instance.SimulateCatchItems();
                    
                    ub.gameObject.SetActive(true);
                    bc.gameObject.SetActive(true);
                }
                
                // Debug.Log("Show unboxing and classification");
            });

        }
        
    }
}
