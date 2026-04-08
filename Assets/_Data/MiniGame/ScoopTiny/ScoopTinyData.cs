using System.Collections.Generic;

namespace Rubik.ScoopingGame
{
    public enum TinyType
    {
        Tiny_0 = 0,
        Tiny_1 = 1,
        Tiny_2 = 2,
        Tiny_3 = 3,
        Tiny_4 = 4,
    }

    public enum RewardType
    {
        Reward_0 = 0,
        Reward_1 = 1,
        Reward_2 = 2,
        Reward_3 = 3,
    }

    [System.Serializable]
    public class TinyRateData
    {
        public TinyType TinyType;
        public float Rate;
    }

    [System.Serializable]
    public class TinyRewardData
    {
        public TinyRequiredRewardData[] TinyRequiredRewardData;
        public RewardType RewardType;
    }

    [System.Serializable]
    public class TinyRequiredRewardData
    {
        public TinyType TinyType;
        public int RequiredRewardAmount;
    }

    [System.Serializable]
    public class ScoopTinyData
    {
        public int Version;
        public List<TinyRateData> TinyRateData;
        public List<TinyRewardData> TinyRewardData;
    }

    [System.Serializable]
    public class UserScoopTiny
    {
        public UserScoopTinySession Normal;
    }

    [System.Serializable]
    public class UserScoopTinySession
    {
        public string _id;
        public List<TinyRateData> PoolTiny;
        public List<TinyType> HoldTiny;
    }
}