using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Rubik.DataCenter
{
    public enum DataName
    {
        DataVersion,
        VersionGame,
        DataScoopTiny,
    }

    [System.Serializable]
    public class DataVersion
    {
        public int VersionGame = 0;
        public int DataScoopTiny = 0;
    }
}