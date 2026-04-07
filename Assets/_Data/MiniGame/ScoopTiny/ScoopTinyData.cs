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
        Tiny_5 = 5,
        Tiny_6 = 6,
        Tiny_7 = 7,
        Tiny_8 = 8,
        Tiny_9 = 9,
        Tiny_10 = 10,
    }

    public enum RewardType
    {
        Reward_0 = 0,
        Reward_1 = 1,
        Reward_2 = 2,
        Reward_3 = 3,
        Reward_4 = 4,
        Reward_5 = 5,
        Reward_6 = 6,
        Reward_7 = 7,
        Reward_8 = 8,
        Reward_9 = 9,
        Reward_10 = 10,
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
        public TinyType TinyType;
        public int RequiredTinyAmount;
        public RewardType RewardType;
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
        public List<TinyRateData> PoolTiny;
        public List<TinyType> HoldTiny;
    }
}