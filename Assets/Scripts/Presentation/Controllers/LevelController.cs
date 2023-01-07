using Universal.Data;
using Universal.CustomAttributes;
using Zenject;
using Universal.SaveSystem;
using Universal;

namespace Presentation.Controllers
{
    [BindAsSingle]
    class LevelController
    {
        [Inject]
        readonly BallsController _ballsController;

        [Inject]
        readonly TilesController _tilesController;

        [Inject]
        readonly BoostController _boostController;

        private float _time;
        private int _score;
        private int _health;
        private int _tileScore;
        private int _boostScore;

        internal void Initialize()
        {
            Signals.OnUISave += SaveUI;
            Signals.ChangeLevel += NextLevel;
            Signals.QuitAfterLevelCompleted += QuitAndSave;
        }

        internal void CreateLevel()
        {
            var result = GameSceneReferences.TilesCreatorViewRef.CreateLevel();
            _tilesController.TileViews = result.Item1;
            _tilesController.TilesData = result.Item2;
        }

        internal void SaveLevel()
        {
            SaveAndLoadSystem.SaveLevel
            (_tilesController.TilesData, _ballsController.BallsPosition, _ballsController.BallsVelocity,
            UniversalData.CurrentLevel, _ballsController.StrengthRunning, _ballsController.StrengthTime, _boostController.BoostData, _score, _time, _health, _tileScore, _boostScore);
        }

        internal void LoadData()
        {
            LevelData levelData = SaveAndLoadSystem.LoadData();

            UniversalData.CurrentLevel = levelData.CurrentLevel;
            _tilesController.TilesData = levelData.TilesData;
            _ballsController.StrengthRunning = levelData.StrengthRunning;
            _ballsController.StrengthTime = levelData.StrengthTime;
            _ballsController.InitializedDamageChange = false;
            _boostController.LoadBoosts(levelData.BoostData);

            Signals.OnUILoad?.Invoke(levelData.Score, levelData.Time, levelData.Health, levelData.TileScore, levelData.BoostScore);
            _tilesController.TileViews = GameSceneReferences.TilesCreatorViewRef.LoadLevel(_tilesController.TilesData);
            _ballsController.LoadBalls(levelData.BallsPosition, levelData.BallsVelocity);

            if (UniversalData.BallPushPosible && _ballsController.BallsList.Count > 0)
                _ballsController.BallsList[0].ResetPosition();
            else
                for (int i = 0; i < _ballsController.BallsList.Count; i++)
                    _ballsController.BallsList[i].transform.parent = null;

            if (_ballsController.BallsList.Count == 0)
            {
                _ballsController.InitializeFirstBall();
                UniversalData.BallPushPosible = true;
            }
        }
        
        internal void NextLevel()
        {
            UniversalData.CurrentLevel++;
            Signals.OnNextLevel?.Invoke();
            CreateLevel();

            _ballsController.DestoryAllBalls();
            _ballsController.ClearPostionAndVelocity();
            _boostController.DestroyAllBoosts();
            _ballsController.ResetUpdate();

            _ballsController.InitializeFirstBall();

            UniversalData.BallPushPosible = true;
        }

        private void QuitAndSave()
        {
            NextLevel();
            SaveLevel();
        }

        private void SaveUI(int score, float time, int health, int tileScore, int boostScore)
        {
            _score = score;
            _time = time;
            _health = health;
            _tileScore = tileScore;
            _boostScore = boostScore;
        }
    }
}