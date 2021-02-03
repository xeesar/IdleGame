using Extensions;
using Models.ProductionBuildings;
using UnityEngine;

namespace Models.States.ProductionBuildingStates
{
    public class IncomeState : ProductionBuildingState
    {
        #region Properties

        public override string Status =>  StringConstants.AnalyticsEventsValues.Done;

        #endregion
        
        
        
        #region Public Methods

        public override void OnStateEnter(ProductionBuilding productionBuilding)
        {
            base.OnStateEnter(productionBuilding);
            
            m_targetBuilding.ProductionBuildingView.SetIncomeUIActive(true);
            m_targetBuilding.ProductionBuildingView.DisplayIncome(m_targetBuilding.Income.Value, m_targetBuilding.CurrencyType);
        }
        
        
        public override ProductionBuildingState HandleState(float deltaTime)
        {
            if (m_targetBuilding.IsReadyToCollect && m_targetBuilding.IsAutoCollectingEnabled)
            {
                m_targetBuilding.CollectIncome();
            }
            
            return null;
        }
        

        public override void OnStateExit()
        {
            base.OnStateExit();
            
            m_targetBuilding.ProductionBuildingView.SetIncomeUIActive(false);
        }

        #endregion
    }
}

