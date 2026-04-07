using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using Rubik.DataCenter;
using UnityEngine;

namespace Rubik.ScoopingGame
{
    public class ScoopTinyManager : NTBehaviour
    {
        public ScoopTinyData ScoopTinyData;
        public UserScoopTiny UserScoopTiny;

        public static ScoopTinyManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (ScoopTinyManager.Instance != null)
            {
                NTLog.LogError("Only 1 Instance allow");
                return;
            }
            ScoopTinyManager.Instance = this;
        }

        #region Function
        public void Logout(){
            this.UserScoopTiny = new UserScoopTiny();
        }

        public IEnumerator LoadData(){
            this.ScoopTinyData = JsonUtility.FromJson<ScoopTinyData>(DataCenterManager.Instance.GetData(DataName.DataScoopTiny));
            yield return null;
        }

        public void UpdateUserScoopTiny(UserScoopTiny userScoopTiny){
            if(userScoopTiny == null || userScoopTiny.Normal == null 
            || userScoopTiny.Normal.PoolTiny == null || userScoopTiny.Normal.HoldTiny == null
            || userScoopTiny.Normal.PoolTiny.Count == 0 || userScoopTiny.Normal.HoldTiny.Count == 0
            ){
                return;
            }
            this.UserScoopTiny = userScoopTiny;
        }
        #endregion
    }
}