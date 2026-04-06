using System.Collections.Generic;

namespace Rubik.UserProfile
{
    using Rubik.UserData;

    public enum UnlockType
    {
        None = 0,
        Level = 1,
        Purchase = 2,
        Event = 3,
    }

    [System.Serializable]
    public class UnlockData
    {
        public UnlockType UnlockType;
        public int LevelUnlock;
    }

    [System.Serializable]
    public class AvatarData
    {
        public int Index;
        public bool Lock;
        public UnlockData UnlockData;
    }

    [System.Serializable]
    public class AvatarPlayer
    {
        public List<int> Own;
        public int Current = -2;
    }

    [System.Serializable]
    public class AvatarBorderData
    {
        public int Index;
        public bool Lock;
        public UnlockData UnlockData;
    }

    [System.Serializable]
    public class AvatarBorderPlayer
    {
        public List<int> Own;
        public int Current = -1;
    }

    [System.Serializable]
    public class SkinData
    {
        public int Index;
        public bool Lock;
        public UnlockData UnlockData;
    }

    [System.Serializable]
    public class SkinPlayer
    {
        public List<int> Own;
        public int Current = -1;
    }

    [System.Serializable]
    public class EmojiData
    {
        public int Index;
        public bool Lock;
        public UnlockData UnlockData;
    }

    [System.Serializable]
    public class EmojiPlayer
    {
        public List<int> Own;
    }
}