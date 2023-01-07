using Universal.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Universal.Data
{
    [Serializable]
    public class LevelData
    {
        public List<TileData> TilesData;

        public List<(float, float)> BallsVelocity;
        public List<(float, float)> BallsPosition;

        public List<(float, float, BoostType)> BoostData;

        public int CurrentLevel;

        public bool StrengthRunning;
        public float StrengthTime;

        public int Score;
        public float Time;
        public int Health;
        public int TileScore;
        public int BoostScore;

        public bool BallPushPossible;

        public LevelData(List<TileData> tilesData, List<(float, float)> ballsPosition, List<(float, float)> ballsVelocity,
            int currentLevel, bool strengthRunning, float strengthTime, List<(float, float, BoostType)> boostData, int score, float time, int health, int tileScore, int boostScore)
        {
            TilesData = tilesData;
            BallsPosition = ballsPosition;
            BallsVelocity = ballsVelocity;
            CurrentLevel = currentLevel;
            StrengthRunning = strengthRunning;
            StrengthTime = strengthTime;
            BoostData = boostData;
            Score = score;
            Time = time;
            Health = health;
            TileScore = tileScore;
            BoostScore = boostScore;
            BallPushPossible = UniversalData.BallPushPosible;
        }
    }
}