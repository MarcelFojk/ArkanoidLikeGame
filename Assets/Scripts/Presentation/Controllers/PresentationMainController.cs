using Presentation.Views;
using System.Collections.Generic;
using UnityEngine;
using Universal;
using Universal.Interfaces;
using Universal.CustomAttributes;
using Zenject;

namespace Presentation.Controllers
{
    [BindAsSingle]
    class PresentationMainController : ICustomUpdate
    {
        [Inject]
        readonly BallsController _ballsController;

        [Inject]
        readonly LevelController _levelController;

        [Inject]
        readonly TilesController _tilesController;

        [Inject]
        readonly BoostController _boostController;

        internal void Initialize()
        {
            _levelController.Initialize();
        }

        public void CustomUpdate()
        {
            if (UniversalData.GamePaused)
                return;

            List<BallView> balls = _ballsController.BallsList;
            for (int i = 0; i < balls.Count; i++)
            {
                if (Mathf.Abs(balls[i].Rigidbody2D.velocity.y) <= 0.2f && !UniversalData.BallPushPosible)
                    balls[i].Rigidbody2D.AddForce(Vector2.up * 10f);
            }

            _ballsController.CustomUpdate();
        }

        internal void GameSceneOnEntry()
        {
            UniversalData.GamePaused = false;

            if (UniversalData.LoadedFromSave)
            {
                _levelController.LoadData();
                return;
            }

            UniversalData.CurrentLevel = 0;
            _levelController.CreateLevel();
            _ballsController.InitializeFirstBall();
        }

        internal void GameSceneOnExit()
        {
            _ballsController.DestoryAllBalls();
            _ballsController.ClearPostionAndVelocity();
            _ballsController.ResetUpdate();
            _boostController.DestroyAllBoosts();
            UniversalData.BallPushPosible = true;
        }

        internal void UpdateMousePostion(Vector2 mousePosition)
        {
            if (UniversalData.GamePaused)
                return;

            GameSceneReferences.PlayerViewRef.Move(mousePosition);
        }

        internal void PushBall() => _ballsController.PushBall();

        internal void PauseGame() 
        {
            _ballsController.PauseBall();
            _boostController.PauseBoosts();
            UniversalData.GamePaused = true;
        }

        internal void ResumeGame() 
        {
            _ballsController.ResumeBall();
            _boostController.ResumeBoosts();
            UniversalData.GamePaused = false;
        }

        internal void SaveLevel() => _levelController.SaveLevel();

        internal void DestroyTile(AbstractTileView tileView) => _tilesController.DestroyTile(tileView);

        internal void OnGather(AbstractBoostView boostView) => _boostController.OnGather(boostView);

        internal bool OnBallMiss(BallView view) => _ballsController.OnBallMiss(view);
    }
}