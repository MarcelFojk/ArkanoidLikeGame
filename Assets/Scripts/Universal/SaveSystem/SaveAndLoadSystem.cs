using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Universal.Data;

namespace Universal.SaveSystem
{
    public static  class SaveAndLoadSystem
    {
        public static void SaveLevel(List<TileData> tilesData, List<(float, float)> ballPosition,
            List<(float, float)> ballVelocity, int currentLevel, bool strengthRunning, float strengthTime, List<(float, float, BoostType)> boostData, 
            int score, float time, int health, int tileScore, int boostScore)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "Level.data";
            FileStream fileStream = new FileStream(path, FileMode.Create);

            LevelData levelData = new LevelData(tilesData, ballPosition, ballVelocity, currentLevel, strengthRunning,
                strengthTime, boostData, score, time, health, tileScore, boostScore);

            binaryFormatter.Serialize(fileStream, levelData);
            fileStream.Close();
        }

        public static LevelData LoadData()
        {   
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "Level.data";
            FileStream fileStream = new FileStream(path, FileMode.Open);

            LevelData levelData = (LevelData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            UniversalData.BallPushPosible = levelData.BallPushPossible;

            return levelData;
        }

        public static void SaveHighScores(List<int> highScores)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "HighScores.data";
            FileStream fileStream = new FileStream(path, FileMode.Create);

            binaryFormatter.Serialize(fileStream, highScores);
            fileStream.Close();
        }

        public static List<int> LoadHighScores()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "HighScores.data";
            FileStream fileStream = new FileStream(path, FileMode.Open);

            List<int> highScores = (List<int>)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            return highScores;
        }

        public static bool PathExists()
        {
            string path = Application.persistentDataPath + "Level.data";
            return File.Exists(path);
        }

        public static bool HighScoresPathExists()
        {
            string path = Application.persistentDataPath + "HighScores.data";
            return File.Exists(path);
        }
    }
}