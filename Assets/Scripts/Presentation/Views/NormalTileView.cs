
namespace Presentation.Views
{
    class NormalTileView : AbstractTileView
    {
        internal override void OnGetDamage(BallView view) => DestroyTile();
    }
}