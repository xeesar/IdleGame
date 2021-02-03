using Data;
using Enums;
using Interfaces;
using Managers;
using Models.Tiles;
using UnityEngine;

namespace Models.States
{
    public class IdleState : BaseArtistState
    {
        #region Public Methods

        public override IArtistState HandleState()
        {
            bool isOnIdle = Artist.ArtistView.IsOnState(AnimationType.Idle);
            bool hasArtistBlock = Artist.GraffitiBlockData != null && !Artist.GraffitiBlockData.IsComplete;
            bool hasEmptyBlocks = GraffitiManager.Instance.HasEmptyBlock();
            bool isGraffitiCompleted = !hasArtistBlock && !hasEmptyBlocks;
            
            if((isGraffitiCompleted && Artist.IsFinished) || !isOnIdle)
            {
                return null;
            }
            
            if (isGraffitiCompleted && !Artist.IsFinished)
            {
                Artist.IsFinished = true;
                return new MovingState(Object.FindObjectOfType<GraffitiAreaTile>().ArtistSpawn.position);
            }

            if (Artist.PaintSpray == null || Artist.PaintSpray.Capacity <= 0)
            {
                return new MovingState(Object.FindObjectOfType<GraffitiAreaTile>().SprayCanBoxPos);
            }

            if (Artist.GraffitiBlockData == null || Artist.GraffitiBlockData.IsComplete)
            {
                Artist.GraffitiBlockData = GraffitiManager.Instance.GetEmptyBlock();
            }
            
            Vector3 blockPos = GraffitiManager.Instance.GetBlockPosition(Artist.GraffitiBlockData);
            
            return new MovingState(blockPos);
        }

        #endregion
    }
}

