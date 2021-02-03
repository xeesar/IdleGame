using DG.Tweening;
using Enums;
using Interfaces;
using Managers;
using Models.DynamicParameters;
using Models.Spray;

namespace Models.States
{
    public class GetSprayState : BaseArtistState
    {
        #region Public Methods

        public override IArtistState HandleState()
        {
            if (Artist.PaintSpray?.Capacity > 0)
            {
                return new IdleState();
            }

            return null;
        }


        public override void OnStateEnter(IArtist artist)
        {
            base.OnStateEnter(artist);
            
            Artist.ArtistView.PlayAnimation(AnimationType.TakeSprayCan);

            DOTween.Sequence().SetDelay(Artist.ArtistView.GetClipDuration(AnimationType.TakeSprayCan)).OnComplete(() =>
            {
                IPaintSpray paintSpray = new PaintSpray();

                paintSpray.Capacity = DynamicParametersManager.Instance.Get(DynamicParameterType.SprayBottleCapacity).Value;

                Artist.PaintSpray = paintSpray;
            });
        }

        #endregion
    }
}

