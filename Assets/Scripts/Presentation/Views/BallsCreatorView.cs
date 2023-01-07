using System.Collections.Generic;
using UnityEngine;

namespace Presentation.Views
{
    class BallsCreatorView : MonoBehaviour
    {
        [SerializeField]
        BallView _ballPrefab;

        internal BallView SpawnFirstBall()
        {
            BallView ballView = Instantiate(_ballPrefab);
            ballView.ResetPosition();
            return ballView;
        }

        internal List<BallView> LoadBalls(List<(float, float)> ballsPosition, List<(float, float)> ballsVelocity)
        {
            List<BallView> ballsViews = new();

            for (int i = 0; i < ballsPosition.Count; i++)
            {
                BallView view = Instantiate(_ballPrefab);
                view.transform.position = new Vector3(ballsPosition[i].Item1, ballsPosition[i].Item2, 0f);
                view.Rigidbody2D.velocity = new Vector3(ballsVelocity[i].Item1, ballsVelocity[i].Item2, 0f);
                view.transform.parent = GameSceneReferences.BallsContainer;

                ballsViews.Add(view);
            }

            return ballsViews;
        }

        internal List<BallView> SpawnBalls(BallView ballView, int ballsCount)
        {
            float angleValue = 90f;
            float angleStart = -45f;

            float angleChange = angleValue / (ballsCount - 1);

            List<BallView> ballsViews = new();
            for (int i = 0; i < ballsCount; i++)
            {
                BallView view = Instantiate(_ballPrefab);
                view.transform.position = ballView.transform.position;
                view.transform.parent = GameSceneReferences.BallsContainer;

                float angle = (angleStart + angleChange * i) * Mathf.Deg2Rad;

                float x = Mathf.Cos(angle) * ballView.Rigidbody2D.velocity.x - Mathf.Sin(angle) * ballView.Rigidbody2D.velocity.y;
                float y = Mathf.Sin(angle) * ballView.Rigidbody2D.velocity.x + Mathf.Cos(angle) * ballView.Rigidbody2D.velocity.y;

                view.Rigidbody2D.AddForce(new Vector2(x, y).normalized * 500f);

                ballsViews.Add(view);
            }
            return ballsViews;
        }

        internal void DestroyBall(BallView ballView) => Destroy(ballView.gameObject);

    }
}