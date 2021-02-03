using System;
using Data;
using Data.Configs;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Models.ProductionBuildings;
using Models.Screens;
using Services;

namespace Models.Popups
{
    public abstract class ProductionBuildingPopup : BasePopup
    {
        #region Fields

        protected ProductionBuilding m_targetBuilding;

        #endregion



        #region Properties

        protected abstract SceneType SceneType { get; }

        #endregion
                
                
                
        #region Private Methods

        protected override void Initialize()
        {
            m_targetBuilding = CityManager.Instance.SelectedProductionBuilding;
        }

        
        protected override void OnClaimButtonClick()
        {
            base.OnClaimButtonClick();
            
            SendOpenEvent();
            
            LoadingScreen loadingScreen = ScreensManager.Instance.GetScreen<LoadingScreen>();
            loadingScreen.Initialize(SceneType);
            loadingScreen.Show();
            
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();
            userProfileModel.CurrentCityProgress.exitTime = DateTime.Now;
        }
        

        private void SendOpenEvent()
        {
            AnalyticEvent analyticEvent = new AnalyticEvent(StringConstants.AnalyticsEvents.HouseOpened);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.BuildingId, m_targetBuilding.Id);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.HouseType, m_targetBuilding.Status);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.CurrencyType, m_targetBuilding.CurrencyType.ToString().ToLower());
            
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
        }

        #endregion
    }
}