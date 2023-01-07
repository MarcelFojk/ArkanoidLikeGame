using System;

namespace Universal
{
    public static class Signals
    {
        public static Action<TileType> OnTileDestroy;
        public static Action OnNextLevel;
        public static Action BallMissed;
        public static Action GameOver;
        public static Action<BoostType> OnBoostGather;
        public static Action<int, float, int, int, int> OnUISave;
        public static Action<int, float, int, int, int> OnUILoad;
        public static Action NextLevelUI;
        public static Action ChangeLevel;
        public static Action QuitAfterLevelCompleted;
    }
}