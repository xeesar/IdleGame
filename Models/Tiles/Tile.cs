using Enums;
using UnityEngine;

namespace Models.Tiles
{
    public abstract class Tile : MonoBehaviour
    {
        #region Properties

        public abstract TileType TileType { get; }

        #endregion
    }
}

