using Presentation.Views;
using System.Collections.Generic;
using UnityEngine;
using Universal.CustomAttributes;
using Universal;
using Zenject;

namespace Presentation.Controllers
{
    [BindAsSingle]
    class BallsController
    {
        [Inject]
        readonly BoostController _boostController;

        internal List<BallView> BallsList = new();

        internal List<(float, float)> BallsVelocity = new();
        internal List<(float, float)> BallsPosition = new();

        internal bool StrengthRunning;

        internal float StrengthTime;

        internal bool InitializedDamageChange;

        private int _ballCount;

        private int _previousDamage = 1;

        private int _boostDamage;

        internal void CustomUpdate()
        {
            if (StrengthRunning)
            {
                if (!InitializedDamageChange || _ballCount != BallsList.Count)
                    ChangeDamage();

                StrengthTime -= Time.deltaTime;

                if (StrengthTime >= 0)
                    return;

                ResetDamage();
                StrengthRunning = false;
                InitializedDamageChange = false;
            }
        }

        internal void InitializeFirstBall() => BallsList.Add(GameSceneReferences.BallsCreatorViewRef.SpawnFirstBall());

        internal void LoadBalls(List<(float, float)> ballsPosition, List<(float, float)> ballsVelocity) 
            => BallsList = GameSceneReferences.BallsCreatorViewRef.LoadBalls(ballsPosition, ballsVelocity);

        internal void SpawnBalls(int ballsCount)
        {   
            BallsList.AddRange(GameSceneReferences.BallsCreatorViewRef.SpawnBalls(BallsList[0], ballsCount));
        }

        internal void ChangeBallsDamage(int damageBoost)
        {
            StrengthTime = 10f;
            _boostDamage = damageBoost;
            StrengthRunning = true;
        }

        internal void PushBall()
        {
            if (!UniversalData.BallPushPosible)
                return;

            BallsList[0].transform.parent = GameSceneReferences.BallsContainer;
            BallsList[0].Rigidbody2D.AddForce(new Vector2(1f, 1f).normalized * 500f);
            UniversalData.BallPushPosible = false;
        }

        internal void PauseBall()
        {
            for (int i = 0; i < BallsList.Count; i++)
            {
                BallsPosition.Add((BallsList[i].transform.position.x, BallsList[i].transform.position.y));
                BallsVelocity.Add((BallsList[i].Rigidbody2D.velocity.x, BallsList[i].Rigidbody2D.velocity.y));
                BallsList[i].Rigidbody2D.Sleep();
            }
        }

        internal void ResumeBall()
        {
            for (int i = 0; i < BallsList.Count; i++)
            {
                BallsList[i].Rigidbody2D.WakeUp();
                BallsList[i].Rigidbody2D.velocity = new Vector3(BallsVelocity[i].Item1, BallsVelocity[i].Item2, 0f);
            }

            ClearPostionAndVelocity();
        }

        internal void DestoryAllBalls()
        {
            int counter = BallsList.Count;
            for (int i = counter - 1; i >= 0; i--)
            {
                BallView view = BallsList[i];
                BallsList.Remove(view);

                GameSceneReferences.BallsCreatorViewRef.DestroyBall(view);
            }
        }

        internal void ResetUpdate()
        {
            StrengthRunning = false;
            StrengthTime = 10f;
            InitializedDamageChange = false;
            _ballCount = 0;
        }

        internal void ClearPostionAndVelocity()
        {
            BallsVelocity.Clear();
            BallsPosition.Clear();
        }

        internal bool OnBallMiss(BallView view)
        {
            if (BallsList.Count == 1)
            {
                BallsList[0].ResetPosition();
                UniversalData.BallPushPosible = true;
                _boostController.DestroyAllBoosts();
                return true;
            }

            GameSceneReferences.BallsCreatorViewRef.DestroyBall(view);
            BallsList.Remove(view);
            return false;
        }

        private void ChangeDamage()
        {
            _ballCount = BallsList.Count;
            for (int i = 0; i < BallsList.Count; i++)
            {
                BallsList[i].Damage = _boostDamage;
                BallsList[i].GetComponent<SpriteRenderer>().color = new Color(1, 0.2311f, 0.2311f, 1);
            }

            InitializedDamageChange = true;
        }

        private void ResetDamage()
        {
            for (int i = 0; i < BallsList.Count; i++)
            {
                BallsList[i].Damage = _previousDamage;
                BallsList[i].GetComponent<SpriteRenderer>().color = new Color(0.132f, 0.132f, 0.132f, 1);
            }
        }
    }
}