using Presentation.Views;
using UnityEngine;

namespace Presentation
{
    class GameSceneReferences : MonoBehaviour
    {
        internal static PlayerView PlayerViewRef;

        internal static Transform BallSpawn;

        internal static BallsCreatorView BallsCreatorViewRef;

        internal static TilesCreatorView TilesCreatorViewRef;

        internal static BoostCreatorView BoostCreatorViewRef;

        internal static AbstractTileView[] TilePrefabs;

        internal static AbstractBoostView[] BoostPrefabs;

        internal static Transform TilesContainer;

        internal static Transform BallsContainer;

        internal static Transform BoostContainer;

        [SerializeField]
        PlayerView _playerView;

        [SerializeField]
        Transform _ballSpawn;

        [SerializeField]
        BallsCreatorView _ballsCreatorView;

        [SerializeField]
        TilesCreatorView _tilesCreatorView;

        [SerializeField]
        BoostCreatorView _boostCreatoView;

        [SerializeField]
        AbstractTileView[] _tilePrefabs;

        [SerializeField]
        AbstractBoostView[] _boostPrefabs;

        [SerializeField]
        Transform _tileContainer;

        [SerializeField]
        Transform _ballsContainer;

        [SerializeField]
        Transform _boostContainer;

        private void OnEnable()
        {
            PlayerViewRef = _playerView;
            BallSpawn = _ballSpawn;
            BallsCreatorViewRef = _ballsCreatorView;
            TilesCreatorViewRef = _tilesCreatorView;
            TilePrefabs = _tilePrefabs;
            TilesContainer = _tileContainer;
            BallsContainer = _ballsContainer;
            BoostPrefabs = _boostPrefabs;
            BoostCreatorViewRef = _boostCreatoView;
            BoostContainer = _boostContainer;
        }
    }
}