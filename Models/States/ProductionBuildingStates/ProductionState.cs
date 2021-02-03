using Extensions;
using Managers;
using Models.ProductionBuildings;

namespace Models.States.ProductionBuildingStates
{
    public class ProductionState : ProductionBuildingState
    {
        #region Properties

        public override string Status =>  StringConstants.AnalyticsEventsValues.Done;

        #endregion
        
        
        
        #region Public Methods

        public override void OnStateEnter(ProductionBuilding productionBuilding)
        {
            base.OnStateEnter(productionBuilding);
            
            m_targetBuilding.ProductionBuildingView.SetProductionUIActive(true);
        }
        

        public override ProductionBuildingState HandleState(float deltaTime)
        {
            if (m_targetBuilding.IsReadyToCollect)
            {
                return new IncomeState();
            }
            
            m_targetBuilding.ProductionProgress += deltaTime;
            m_targetBuilding.ProductionBuildingView.DisplayProductionProgress(m_targetBuilding.ProductionProgress / m_targetBuilding.ProductionSpeed.Value);
            
            return null;
        }


        public override void OnStateExit()
        {
            base.OnStateExit();
            
            m_targetBuilding.ProductionBuildingView.SetProductionUIActive(false);
        }

        #endregion
    }
}
