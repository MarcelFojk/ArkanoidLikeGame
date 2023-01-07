using UnityEngine;
using Universal;

namespace Presentation.Views
{
    class PlayerView : MonoBehaviour
    {

        [SerializeField]
        private float _movementModifier;

        internal void Move(Vector2 mousePosition)
        {
            float position = Mathf.Clamp(transform.position.x + mousePosition.x / _movementModifier, -5.83f, 5.83f);
            transform.position = new Vector3(position, transform.position.y, 0f);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Collider2D collider = collision.collider;

            if (collider.CompareTag("Ball") && !UniversalData.BallPushPosible)
            {
                BallView ball = collider.GetComponent<BallView>();

                Vector2 contactPoint = collision.contacts[0].point;

                ball.transform.position = new Vector2(contactPoint.x, contactPoint.y + 0.2f);
                ball.Rigidbody2D.velocity = Vector2.zero;

                Vector2 direction = (ball.transform.position - transform.position).normalized;

                ball.Rigidbody2D.AddForce(direction * 500f);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Boost"))
            {
                AbstractBoostView view = collider.GetComponent<AbstractBoostView>();

                switch (view.Type)
                {
                    case BoostType.CountBoost:
                        view.OnGather();
                        return;

                    case BoostType.StrengthBoost:
                        view.OnGather();
                        return;
                }

            }
        }
    }
}