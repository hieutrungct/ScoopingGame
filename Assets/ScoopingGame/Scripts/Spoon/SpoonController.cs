using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ScoopingGame
{
    public class SpoonController : MonoBehaviour
    {
        [SerializeField] private SpoonUI spoonUI;
        [SerializeField] private CatchZone catchZone;

        public event Action<List<ItemData>> OnScoopingDone;

        public void StartScooping()
        {
            spoonUI.PlayScoop(
                onReachCatchZone: () =>
                {
                    catchZone.ActiveCatchZone();
                },
                onComplete: () =>
                {
                    catchZone.DeactiveCatchZone();

                    var items = catchZone.caughtItems;
                    OnScoopingDone?.Invoke(items);
                }
            );
        }
    }
}