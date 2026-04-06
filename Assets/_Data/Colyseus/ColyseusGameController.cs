using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using NTPackage.UI;
using Rubik.Config;
using Rubik.MsgDelivery;
using Rubik.UI;
using UnityEngine;

namespace Rubik.ColyseusGame
{

    public class ColyseusGameController : NTBehaviour
    {

        public static ColyseusGameController Instance;
        protected override void Awake()
        {
            base.Awake();
            if (ColyseusGameController.Instance != null)
            {
                NTLog.LogError("Only 1 Instance allow");
                return;
            }
            ColyseusGameController.Instance = this;
        }
    }
}