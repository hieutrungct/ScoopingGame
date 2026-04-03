// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ScoopingGame
{
public class ScoopingGameController : MonoBehaviour
    {
        #region Singleton
        public static ScoopingGameController instance { get; private set; }
        // public Claw claw;
        public ArcadeLeverRotate joystick;
        public BlindBag blindBagPrefab;
        // public Transform blindBagSpawnPoint;
        public CatchZone catchZone;
        public Spoon spoon;
        public Unboxing unboxing;
        public BlindBagClassification blindBagClassification;
        public BlindBagFlyEffect blindBagFlyEffect;
        public CollectedItems collectedItems;
        #endregion
        
        public float clawMoveSpeed;
        public float powerTime;
        public float gameTime;
        public float mulTime;
        #region Data
        public List<ItemData> caughtItems = new List<ItemData>();
        #endregion
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                // DontDestroyOnLoad(gameObject);
            }
        }
        
        // public void StartScooping()
        // {
        //     claw.StartScooping();
        // }
        
        
        // hiện tại chưa có dữ liệu từ client nên tạm thời sẽ giả lập bằng cách random dữ liệu blindbag, sau này có dữ liệu rồi thì sẽ sửa lại
        public void SimulateCatchItems()
        {
            caughtItems.Clear();
            for (int i = 0; i < catchZone.caughtItems.Count; i++)
            {
                ItemData b = new ItemData();
                b.id = System.Guid.NewGuid().GetHashCode().ToString();
                b.rarity = (Rarity)Random.Range(1, 4);
                caughtItems.Add(b);
            }
            
        }
    }
}
