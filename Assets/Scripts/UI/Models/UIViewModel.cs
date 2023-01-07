using UI.Controllers;
using Universal.CustomAttributes;
using Zenject;
using UnityEngine;
using Presentation.Models;
using Universal.SaveSystem;
using Universal;

namespace UI.Models
{
    [BindAsSingle]
    public class UIViewModel : IInitializable
    {
        static UIViewModel _instance;

        [Inject]
        readonly InputController _inputController;

        [Inject]
        readonly UIMainController _uiMainController;

        [Inject]
        readonly ScoreController _scoreController;

        static bool _gameSceneLoaded;

        public void Initialize() 
        {
            _instance = this;
            _instance._inputController.Initialize();
            _gameSceneLoaded = false;

            _uiMainController.Initialize();
        }

        public static void CustomUpdate()
        {
            if (!_gameSceneLoaded)
                return;

            _instance._uiMainController.CustomUpdate();
            _instance._inputController.CustomUpdate();
        }

        public static void GameSceneOnEntry()
        {   
            _gameSceneLoaded = true;

            _instance._uiMainController.GameSceneOnEnter();
            if (!PlayerSystem.Initialized)
                PlayerSystem.Initialize();
        }

        public static void GameSceneOnExit()
        {   
            _gameSceneLoaded = false;

            if (UISceneReferences.GameMenuViewRef.GameMenuOpen)
                OpenCloseGameMenu();

            _instance._uiMainController.GameSceneOnExit();
            if (PlayerSystem.Initialized)
                PlayerSystem.Deinitialize();
        }

        public static void MainMenuOnEntry()
        {
            _instance._uiMainController.MainMenuOnEntry();
        }

        public static void OpenCloseGameMenu()
        {
            UISceneReferences.GameMenuViewRef.GameMenuOpen = !UISceneReferences.GameMenuViewRef.GameMenuOpen;

            if (UISceneReferences.GameMenuViewRef.GameMenuOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                PresentationViewModel.PauseGame();
                PlayerSystem.DeinitializeForGameMenu();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
                PresentationViewModel.ResumeGame();
                PlayerSystem.InitializeForGameMenu();
            }

        }

        public static void SaveLevel()
        {
            SaveAndLoadSystem.SaveHighScores(_instance._scoreController.HighScores);
            Signals.OnUISave?.Invoke(_instance._scoreController.Score, _instance._scoreController.Time, UniversalData.Health,
                _instance._scoreController.TileScore, _instance._scoreController.BoostScore);
            PresentationViewModel.SaveLevel();
        }
    }
}