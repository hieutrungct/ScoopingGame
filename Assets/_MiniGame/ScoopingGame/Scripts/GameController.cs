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
        public SpoonController spoonController;
        public UnboxingController unboxingController;
        public BlindBagClassification blindBagClassification;
        public BlindBagFlyEffect blindBagFlyEffect;
        public CollectedItemsController collectedItemsController;
        public CreatBindBag creatBindBag;
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
        
        
        public void OnScoopingDone(int caughtItemCount)
        {

            // giả lập reward 
            List<ItemData> rewards = SimulateRewards(caughtItemCount);
 
            unboxingController.Init(rewards); 
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
