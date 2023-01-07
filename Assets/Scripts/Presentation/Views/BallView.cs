using UnityEngine;

namespace Presentation.Views
{
    public class BallView : MonoBehaviour
    {
        [SerializeField]
        internal Rigidbody2D Rigidbody2D;

        internal int Damage = 1;

        internal void ResetPosition()
        {
            Rigidbody2D.velocity = Vector3.zero;
            transform.parent = GameSceneReferences.PlayerViewRef.transform;
            transform.localPosition = GameSceneReferences.BallSpawn.localPosition;
        }
    }
}