using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.UserData
{
    using System.Linq;

    [System.Serializable]
    public class DisplayNameData
    {
        public string DisplayeName = "";
        public bool FirstChange = false;
    }

    [System.Serializable]
    public class UserData
    {
        public string _id;
        public long PlayerID;
        public string AccountID;

        public string DisplayName;
        public bool FirstChangeName;
        public int Server;
        public long LastLogin;
        public int Level;
        public long Exp;
    }

    [System.Serializable]
    public class UserDataResponse
    {
        public string _id;
        public string AccountID;
        public int Server;
        public long PlayerID;
        public int Level;
        public long Exp;
        public long LastLogin;
    }


    [System.Serializable]
    public class ExpData
    {
        public int Level;
        public long Exp;
        public bool Max;
    }
}