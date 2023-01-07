using System;
using System.Collections.Generic;
using Universal;
using Universal.CustomAttributes;

namespace UI.Controllers
{
    [BindAsSingle]
    class ScoreController
    {   
        internal List<int> HighScores = new();

        internal int Score;

        internal float Time;

        internal int TileScore;

        internal int BoostScore;

        internal int TimeScore;

        internal int HealthScore;

        internal void Initalize()
        {
            Signals.OnTileDestroy += OnTileDestroy;
            Signals.OnNextLevel += NextLevelReset;
            Signals.OnBoostGather += OnBoostGather;
        }

        internal void OnTileDestroy(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.NormalTile:
                    Score += 10;
                    TileScore += 10;
                    break;
                case TileType.MultipleHitTile:
                    Score += 30;
                    TileScore += 30;
                    break;
                case TileType.MovingTile:
                    Score += 20;
                    TileScore += 20;
                    break;
            }

            UpdateScore();
        }

        internal void OnBoostGather(BoostType boostType)
        {
            switch (boostType)
            {
                case BoostType.CountBoost:
                    Score += 200;
                    BoostScore += 200;
                    return;
                case BoostType.StrengthBoost:
                    Score += 100;
                    BoostScore += 100;
                    return;
            }

            UpdateScore();
        }

        internal void UpdateScore()
        {
            if (HighScores.Count == UniversalData.CurrentLevel)
                HighScores.Add(Score);

            if (Score > HighScores[UniversalData.CurrentLevel])
                HighScores[UniversalData.CurrentLevel] = Score;

            UISceneReferences.Score.SetText(Score.ToString());
            UISceneReferences.HighScore.SetText(HighScores[UniversalData.CurrentLevel].ToString());
        }

        internal void ResetScore()
        {
            Score = 0;
            TileScore = 0;
            BoostScore = 0;
            TimeScore = 0;
            HealthScore = 0;
        }

        internal void ResetTime() => Time = 0;

        internal void UpdateTime(float deltaTime)
        {
            Time += deltaTime;

            TimeSpan timeSpan = TimeSpan.FromSeconds(Time);
            string changedTime = timeSpan.ToString(@"mm\:ss");
            UISceneReferences.Time.SetText(changedTime);
        }

        internal void SetHealthScore()
        {
            HealthScore = UniversalData.Health * 500;
            Score += HealthScore;
        }

        internal void LevelCompleted()
        {
            float score = 180f - Time;
            if (score >= 0)
                TimeScore = (int)(score * 20);
            else
                TimeScore = 0;

            Score += TimeScore;
            SetHealthScore();
            UpdateScore();
        }

        private void NextLevelReset()
        {
            ResetScore();
            ResetTime();
            UpdateScore();
            UpdateTime(0);
        }
    }
}