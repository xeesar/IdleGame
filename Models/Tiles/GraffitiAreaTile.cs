using Enums;
using UnityEngine;

namespace Models.Tiles
{
    public class GraffitiAreaTile : Tile
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private Transform m_artistSpawnTransform = null;
        [SerializeField] private Transform m_sprayCanBoxTransform = null;
        [SerializeField] private Transform m_confetiSpawnPos = null;
        
        #endregion
        
        
        
        #region Properties

        public override TileType TileType => TileType.GraffitiArea;

        public Transform ArtistSpawn => m_artistSpawnTransform;

        public Vector3 SprayCanBoxPos => m_sprayCanBoxTransform.position;

        public Transform ConfetiSpawnPos => m_confetiSpawnPos;

        #endregion
    }
}

