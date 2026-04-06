using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ScoopingGame
{
    public class SpoonController : MonoBehaviour
    {
        public SpoonUI spoonUI;
        [SerializeField] private CatchZone catchZone;


        public List<BlindBag> lsBlindBag = new List<BlindBag>();

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
                    lsBlindBag = catchZone.caughtItems;
                    GameController.instance.OnScoopingDone(lsBlindBag);
                }
            );
        }
    }
}