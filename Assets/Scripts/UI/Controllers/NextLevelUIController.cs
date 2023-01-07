using Presentation.Models;
using UI.Models;
using UnityEngine;
using Universal;
using Universal.CustomAttributes;
using Zenject;

namespace UI.Controllers
{
    [BindAsSingle]
    class NextLevelUIController
    {
        [Inject]
        readonly ScoreController _scoreController;

        internal void Initialize()
        {
            Signals.NextLevelUI += LevelCompleted;
            Signals.OnNextLevel += Reset;
        }

        internal void LevelCompleted()
        {
            if (UISceneReferences.GameMenuViewRef.GameMenuOpen)
               UIViewModel.OpenCloseGameMenu();

            PresentationViewModel.PauseGame();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerSystem.Deinitialize();
            _scoreController.LevelCompleted();


            UISceneReferences.CompletedUI.gameObject.SetActive(true);
            UISceneReferences.CompletedScore.SetText(_scoreController.Score.ToString());
            UISceneReferences.CompletedHighScore.SetText(_scoreController.HighScores[UniversalData.CurrentLevel].ToString());
            UISceneReferences.CompletedTileScore.SetText(_scoreController.TileScore.ToString());
            UISceneReferences.CompletedBoostScore.SetText(_scoreController.BoostScore.ToString());
            UISceneReferences.CompletedTimeScore.SetText(_scoreController.TimeScore.ToString());
            UISceneReferences.CompletedHealthScore.SetText(_scoreController.HealthScore.ToString());
        }

        internal void Reset()
        {
            UISceneReferences.CompletedUI.gameObject.SetActive(false);
            UniversalData.GamePaused = false;

            if (!PlayerSystem.Initialized)
                PlayerSystem.Initialize();

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }
}