// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ScoopingGame
{
    public class GameController : MonoBehaviour
    {
        #region Singleton
        public static GameController instance { get; private set; }
        // public Claw claw;
        // public ArcadeLeverRotate joystick;
        public BlindBag blindBagPrefab;
        // public Transform blindBagSpawnPoint;
        // public CatchZone catchZone;
        public SpoonController spooningController;
        public UnboxingController unboxingController;
        public BlindBagClassification blindBagClassification;
        public BlindBagFlyEffect blindBagFlyEffect;
        public CollectedItems collectedItems;
        #endregion
        

        #region DataTest
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
        
        
        private void OnScoopingDone(List<ItemData> items)
        {
            if (items == null || items.Count == 0) return;

            // giả lập reward (tạm giữ logic cũ)
            var rewards = SimulateRewards(items.Count);

            unboxingController.unboxingUI.gameObject.SetActive(true);
            // classification.gameObject.SetActive(true);

            unboxingController.Init(rewards); // 👉 bạn cần thêm hàm này
        }
        
        // hiện tại chưa có dữ liệu từ client nên tạm thời sẽ giả lập bằng cách random dữ liệu blindbag, sau này có dữ liệu rồi thì sẽ sửa lại
        private List<ItemData> SimulateRewards(int count)
        {
            List<ItemData> list = new List<ItemData>();

            for (int i = 0; i < count; i++)
            {
                ItemData b = new ItemData();
                b.id = System.Guid.NewGuid().ToString();
                b.rarity = (Rarity)Random.Range(1, 4);
                list.Add(b);
            }

            return list;
        }
    }
}
