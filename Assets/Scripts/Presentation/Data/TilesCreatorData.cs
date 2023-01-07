using Presentation.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Presentation.Data
{
    [CreateAssetMenu(fileName = "LevelCreatorData", menuName = "Data/LevelCreatorData")]
    class TilesCreatorData : ScriptableObject
    {
        [SerializeField]
        public AbstractTileView[] TilePrefabs;

        [SerializeField]
        [Range(0f, 1f)]
        public float MultipleHitTilePercent;

        [SerializeField]
        [Range(0f, 1f)]
        public float MovingtTilePercent;

        [SerializeField]
        public int HeightInTiles;

        [SerializeField]
        public int WidthInTiles;

        [SerializeField]
        public float TileHeight;

        [SerializeField]
        public float TileWidth;
    }
}