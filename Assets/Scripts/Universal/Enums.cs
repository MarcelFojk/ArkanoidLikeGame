using System;

namespace Universal
{
    public enum GameState
    {
        Initialize,
        CoreScene,
        UIScene,
        GameScene,
        MainMenu,
        LoadingScene,
    }

    public enum LevelShape
    {
        Square,
        Triangle
    }

    [Serializable]
    public enum TileType
    {
        NormalTile,
        MultipleHitTile,
        MovingTile,
    }

    public enum BoostType
    {
        CountBoost,
        StrengthBoost
    }
}