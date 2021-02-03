using System.Collections.Generic;
using Data;
using Interfaces;
using Managers;
using Models.Popups;
using Services;

namespace Models.GraffitiDrawer
{
    public class OfflineGraffitiDrawer : BaseGraffitiDrawer
    {
        #region Properties

        private float OfflineProgress
        {
            get => _offlineProgress;
            set => _offlineProgress = value > 0 ? value : 0;
        }

        #endregion
        
        
        
        #region Fields

        private float _offlineProgress = 0;

        #endregion
        
        
        
        #region Public Methods

        public OfflineGraffitiDrawer(GraffitiData graffitiData, IGraffitiHolst holst) : base(graffitiData, holst)
        {
            
        }
        
        
        public override void Initialize()
        {
            OfflineProgress = GetOfflineProgress();

            if (OfflineProgress > 0)
            {
                DrawOfflineProgress();
            }
        }

        #endregion



        #region Private Methods

        private float GetOfflineProgress()
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();

            float lastProgress = userProfileModel.CurrentCityProgress.GetProductionBuildingData(userProfileModel.OpenedBuildingId).drawProgress;
            float drawProgress = lastProgress * DrawSpeed;
            
            return drawProgress;
        }


        private void DrawOfflineProgress()
        {
            List<GraffitiBlockData> blocks = GraffitiData.BlocksData;
            
            for (int i = 0; i < blocks.Count && OfflineProgress > 0; i++)
            {
                Draw(blocks[i]);
            }
        }
        
        
        protected override float GetDrawTime(float currentDrawTime)
        {
            float drawTime = 0;
            
            if (OfflineProgress > 0)
            {
                float drawTimeOfOnePixel = DrawSpeed;
                
                drawTime = OfflineProgress >= drawTimeOfOnePixel ? drawTimeOfOnePixel : OfflineProgress;
                OfflineProgress -= drawTime;
            }
            
            return drawTime;
        }

        #endregion
    }
}

