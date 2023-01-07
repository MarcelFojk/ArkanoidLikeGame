using Presentation.Views;
using System.Collections.Generic;
using Universal;
using Universal.CustomAttributes;
using Universal.Data;
using Zenject;

namespace Presentation.Controllers
{
    [BindAsSingle]
    class TilesController
    {
        [Inject]
        readonly BoostController _boostController;

        internal List<TileData> TilesData = new();
        internal List<AbstractTileView> TileViews = new();

        internal void DestroyTile(AbstractTileView tileView)
        {
            if (!TileViews.Contains(tileView))
                return;

            int index = TileViews.IndexOf(tileView);
            TilesData.RemoveAt(index);
            TileViews.RemoveAt(index);

            GameSceneReferences.TilesCreatorViewRef.DestroyTile(tileView);

            Signals.OnTileDestroy?.Invoke(tileView.Type);
            _boostController.SpawnBoost(tileView.transform.position);

            if (TileViews.Count <= 0)
                Signals.NextLevelUI?.Invoke();

        }
    }
}