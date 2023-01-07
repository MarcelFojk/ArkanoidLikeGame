using Presentation.Models;
using UI.Models;
using UnityEngine;
using Universal;
using Universal.CustomAttributes;
using Universal.Interfaces;
using Universal.SaveSystem;
using Zenject;

namespace UI.Controllers
{
    [BindAsSingle]
    class UIMainController : ICustomUpdate
    {
        [Inject]
        readonly ScoreController _scoreController;

        [Inject]
        readonly GameOverController _gameOverController;

        [Inject]
        readonly NextLevelUIController _nextLevelUIController;

        internal void Initialize()
        {
            _scoreController.Initalize();
            _nextLevelUIController.Initialize();
            Signals.BallMissed += OnBallMissed;
            Signals.OnNextLevel += ResetHealth;
            Signals.OnUILoad += LoadUI;

            if (SaveAndLoadSystem.HighScoresPathExists())
                _scoreController.HighScores = SaveAndLoadSystem.LoadHighScores();
        }

        public void CustomUpdate()
        {   
            if (UniversalData.GamePaused)
                return;

            _scoreController.UpdateTime(Time.deltaTime);
        }

        internal void GameSceneOnEnter()
        {   
            if (!UniversalData.LoadedFromSave)
                ResetHealth();

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            UISceneReferences.ProgressUI.SetActive(true);
            _scoreController.UpdateScore();
        }

        internal void GameSceneOnExit()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UISceneReferences.ProgressUI.SetActive(false);
            _scoreController.ResetScore();
            _scoreController.ResetTime();
            _gameOverController.Reset();
        }

        internal void MainMenuOnEntry()
        {
            if (SaveAndLoadSystem.PathExists())
                MainMenuSceneReferences.ContinueButton.SetActive(true);
            else
                MainMenuSceneReferences.ContinueButton.SetActive(false);
        }

        private void LoadUI(int score, float time, int health, int tileScore, int boostScore)
        {   
            ResetHealth();
            UniversalData.Health = health;
            MinusHealth();

            _scoreController.Score = score;
            _scoreController.UpdateScore();

            _scoreController.Time = time;
            _scoreController.TileScore = tileScore;
            _scoreController.BoostScore = boostScore;
        }

        private void OnBallMissed()
        {
            UniversalData.Health -= 1;
            MinusHealth();

            if (UniversalData.Health == 0)
            {
                PresentationViewModel.PauseGame();
                PlayerSystem.Deinitialize();

                if (UISceneReferences.GameMenuViewRef.GameMenuOpen)
                    UIViewModel.OpenCloseGameMenu();

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                _scoreController.SetHealthScore();

                _gameOverController.GameOver();
                return;
            }

            UniversalData.BallPushPosible = true;
        }

        private void MinusHealth()
        {
            for (int i = 2; i > UniversalData.Health - 1; i--)
                UISceneReferences.Health.GetChild(i).gameObject.SetActive(false);
        }

        private void ResetHealth()
        {
            UniversalData.Health = 3;
            for (int i = 0; i < UniversalData.Health; i++)
                UISceneReferences.Health.GetChild(i).gameObject.SetActive(true);
        }
    }
}