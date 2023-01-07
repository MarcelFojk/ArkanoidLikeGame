using UnityEngine;

namespace Presentation.Views
{
    class MultipleHitTileView : AbstractTileView
    {
        [SerializeField]
        private int _health;

        internal override void OnGetDamage(BallView view)
        {
            _health -= view.Damage;

            if (_health <= 0)
                DestroyTile();
        } 
    }
}