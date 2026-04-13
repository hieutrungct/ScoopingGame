using System;
using System.Collections.Generic;
using NTPackage.Functions;
using UnityEngine;

namespace Rubik.ScoopingGame
{
    public class SpoonController : MonoBehaviour
    {
        public SpoonUI spoonUI;
        public CatchZone catchZone;


        public List<TinyType> lsBlindBag = new List<TinyType>();

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
                    int caughtItemCount = catchZone.caughtItems.Count;
                    // GameController.instance.OnScoopingDone(lsBlindBag);
                    // sau khi có dữ liệu rồi thì sẽ sửa lại chỗ này, hiện tại tạm thời sẽ giả lập bằng cách truyền dữ liệu từ catchzone sang
                    ScoopTinyManager.Instance.StartCoroutine(ScoopTinyManager.Instance.ScoopTiny(caughtItemCount, (result) =>
                    {
                        NTLog.LogMessage("ScoopTiny done:" + JsonUtility.ToJson(result));
                        GameController.instance.OnScoopingDone(caughtItemCount);
                        catchZone.caughtItems.Clear();
                        catchZone.ClearScoop();
                    }));

                }
            );
        }
    }
}