
namespace Presentation.Views
{
    class MovingTileView : AbstractTileView
    {
        internal override void OnGetDamage(BallView view) => DestroyTile();
    }
}