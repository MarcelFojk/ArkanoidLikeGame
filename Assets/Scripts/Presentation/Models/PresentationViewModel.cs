using Presentation.Controllers;
using Presentation.Views;
using UnityEngine;
using Universal.CustomAttributes;
using Zenject;

namespace Presentation.Models
{
    [BindAsSingle]
    public class PresentationViewModel : IInitializable
    {
        [Inject]
        readonly PresentationMainController _presentationMainController;

        static PresentationViewModel _instance;

        static bool _gameSceneLoaded;

        public void Initialize()
        {
            _instance = this;
            _presentationMainController.Initialize();
        }

        public static void CustomUpdate()
        {
            if (!_gameSceneLoaded)
                return;

            _instance._presentationMainController.CustomUpdate();
        }
        
        public static void GameSceneOnEntry()
        {   
            _gameSceneLoaded = true;

            _instance._presentationMainController.GameSceneOnEntry();
        }

        public static void GameSceneOnExit()
        {
            _gameSceneLoaded = false;
            
            _instance._presentationMainController.GameSceneOnExit();
        }

        public static void UpdateMousePosition(Vector2 mousePosition) =>_instance._presentationMainController.UpdateMousePostion(mousePosition);

        public static void PushBall() => _instance._presentationMainController.PushBall();

        public static void PauseGame() => _instance._presentationMainController.PauseGame();

        public static void ResumeGame() => _instance._presentationMainController.ResumeGame();

        public static void SaveLevel() => _instance._presentationMainController.SaveLevel();

        public static void DestroyTile(AbstractTileView tileView) => _instance._presentationMainController.DestroyTile(tileView);

        public static void OnGather(AbstractBoostView boostView) => _instance._presentationMainController.OnGather(boostView);

        public static bool OnBallMiss(BallView view) => _instance._presentationMainController.OnBallMiss(view);
    }
}