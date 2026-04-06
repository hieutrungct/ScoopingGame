using System.Collections;
using System.Collections.Generic;
using Colyseus;
using Rubik.Config;
using NTPackage.Functions;
using UnityEngine;

namespace Rubik.Colyseus
{
    public class ColyseusRoomName
    {
        public const string MsgDelivery = "MsgDelivery";
    }
    public class ColyseusRoomManager : NTBehaviour
    {
        public static ColyseusRoomManager Instance;
        public ColyseusClient Client;

        public string RoomId = "";

        protected override void Awake()
        {
            base.Awake();
            if (ColyseusRoomManager.Instance != null)
            {
                NTLog.LogWarning("Only 1 Instance allow");
                return;
            }
            ColyseusRoomManager.Instance = this;
        }

        public void Init()
        {
            this.Client = new ColyseusClient(URL_Config.Socket_URL);
        }
    }
}

