using Data;
using Enums;
using Interfaces;
using Managers;
using Models.Currencies;
using Models.DynamicParameters;
using Services;
using UnityEngine;

namespace Models.States
{
    public class DrawState : BaseArtistState
    {
        #region Public Methods

        public override IArtistState HandleState()
        {
            if (IsCanDraw())
            {
                Artist.PaintSpray.Sprinkle();
                GraffitiManager.Instance.DrawBlock(Artist.GraffitiBlockData);
                return null;
            }

            if (Artist.PaintSpray.Capacity <= 0 || Artist.GraffitiBlockData == null || Artist.GraffitiBlockData.IsComplete)
            {
                return new IdleState();
            }

            return null;
        }
        
        
        public override void OnStateEnter(IArtist artist)
        {
            base.OnStateEnter(artist);

            artist.ArtistView.PlayAnimation(AnimationType.Paint);
        }


        public override void OnStateExit()
        {
            base.OnStateExit();
            
            Artist.ArtistView.StopAnimation(AnimationType.Paint);
        }

        #endregion



        #region Private Methods
        
        private bool IsCanDraw()
        {
            bool isCurrentBlockNotCompleted = Artist.GraffitiBlockData != null && !Artist.GraffitiBlockData.IsComplete;
            bool hasSpray = Artist.PaintSpray.Capacity > 0;

            return hasSpray && isCurrentBlockNotCompleted;
        }

        #endregion
    }
}
