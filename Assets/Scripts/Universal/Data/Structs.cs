using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Universal.Data
{
    [Serializable]
    public struct TileData
    {
        public float PositionX;
        public float PositionY;
        public TileType Type;

        public TileData(Vector3 position, TileType type)
        {
            PositionX = position.x;
            PositionY = position.y;
            Type = type;
        }
    }
}