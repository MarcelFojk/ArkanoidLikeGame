using Universal;
using Presentation.Models;
using UnityEngine;

namespace Presentation.Views
{
    public abstract class AbstractTileView : MonoBehaviour
    {
        [SerializeField]
        internal TileType Type;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Collider2D collider = collision.collider;
            if (collider.CompareTag("Ball"))
                OnGetDamage(collider.GetComponent<BallView>());
        }

        internal abstract void OnGetDamage(BallView view);

        internal protected void DestroyTile() => PresentationViewModel.DestroyTile(this);
    }
}