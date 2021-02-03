using Data;
using Data.Configs;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Models.ProductionBuildings;
using Services;

namespace Models.States.ProductionBuildingStates
{
    public class DrawingBuildingState : ProductionBuildingState
    {
        #region Fields

        private int m_graffitiBlocksCount = 0;
        
        #endregion
        
        
        
        #region Properties
        
        public override string Status =>  StringConstants.AnalyticsEventsValues.InProgress;
        
        #endregion
        
        
        
        #region Public Methods

        public override void OnStateEnter(ProductionBuilding productionBuilding)
        {
            base.OnStateEnter(productionBuilding);

            m_targetBuilding.ProductionBuildingView.SetDrawingUIActive(true);
            
            string cityNumber = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCity;
            CityData cityData = ConfigManager.Instance.Get<CityConfig>().GetCityData(cityNumber);
            GraffitiData graffitiData = cityData.GetLevelData(m_targetBuilding.Id).GraffitiData;
            
            m_graffitiBlocksCount = graffitiData.blocksCount;
        }

        
        public override ProductionBuildingState HandleState(float deltaTime)
        {
            float drawTimePerDelta = deltaTime / TimeManager.Instance.TimeOfDrawingOnePixel * m_targetBuilding.ArtistsCount;
            m_targetBuilding.DrawProgress += drawTimePerDelta;
            m_targetBuilding.ProductionBuildingView.DisplayDrawingProgress(m_targetBuilding.DrawProgress / m_graffitiBlocksCount);
            
            if (m_targetBuilding.DrawProgress >= m_graffitiBlocksCount)
            {
                SendDrawnEvent();
                TeamManager.Instance.BusyArtistsCount -= m_targetBuilding.ArtistsCount;
                m_targetBuilding.ArtistsCount = 0;
                m_targetBuilding.ProductionBuildingView.SetBuildingSprite(m_targetBuilding.CompletedSprite);
                m_targetBuilding.ProductionBuildingView.DisplayUnlockParticle();
                m_targetBuilding.IsDrawn = true;
                DistrictsManager.Instance.OnBuildingDrawn();
                
                return new ProductionState();
            }

            return null;
        }


        public override void OnStateExit()
        {
            base.OnStateExit();
            
            m_targetBuilding.ProductionBuildingView.SetDrawingUIActive(false);
        }

        #endregion



        #region Private Methods

        private void SendDrawnEvent()
        {
            DynamicParametersManager dynamicParametersManager = DynamicParametersManager.Instance;
            
            AnalyticEvent analyticEvent = new AnalyticEvent(StringConstants.AnalyticsEvents.DrawnGraffiti);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.BuildingId, m_targetBuilding.Id);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.CountOfArtists, dynamicParametersManager.Get(DynamicParameterType.ArtistsCount).Value);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.MorePerBlock, dynamicParametersManager.Get(DynamicParameterType.RespectIncomePerBlock).Level);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.RunningSpeed, dynamicParametersManager.Get(DynamicParameterType.RunningSpeed).Level);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.DrawingSpeed, dynamicParametersManager.Get(DynamicParameterType.DrawingSpeed).Level);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.CapacityOfCans, dynamicParametersManager.Get(DynamicParameterType.SprayBottleCapacity).Level);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.CurrencyType, m_targetBuilding.CurrencyType.ToString().ToLower());
        }

        #endregion
    }
}
