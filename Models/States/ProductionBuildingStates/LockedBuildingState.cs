using Extensions;
using Interfaces;
using Models.Currencies;
using Models.ProductionBuildings;
using Services;

namespace Models.States.ProductionBuildingStates
{
    public class LockedBuildingState : ProductionBuildingState
    {
        #region Properties

        public override string Status =>  StringConstants.AnalyticsEventsValues.Locked;

        private Currency m_currency;

        #endregion
        
        
        
        #region Public Methods

        public override void OnStateEnter(ProductionBuilding productionBuilding)
        {
            base.OnStateEnter(productionBuilding);

            m_currency = ServiceLocator.Instance.Get<IUserProfileModel>().GetCurrency(m_targetBuilding.CurrencyType);

            m_targetBuilding.ProductionBuildingView.SetUnlockButtonStatus(m_targetBuilding.UnlockPrice <= m_currency.Value);
            m_targetBuilding.ProductionBuildingView.DisplayUnlockPrice(m_targetBuilding.UnlockPrice, m_targetBuilding.CurrencyType);
        }
        

        public override ProductionBuildingState HandleState(float deltaTime)
        {
            m_targetBuilding.ProductionBuildingView.SetUnlockButtonStatus(m_targetBuilding.UnlockPrice <= m_currency.Value);
            
            return null;
        }


        public override void OnStateExit()
        {
            base.OnStateExit();
            
            m_targetBuilding.ProductionBuildingView.SetLockedUIActive(false);
        }

        #endregion
    }
}