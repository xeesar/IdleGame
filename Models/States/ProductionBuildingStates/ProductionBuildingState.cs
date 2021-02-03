using Models.ProductionBuildings;

namespace Models.States.ProductionBuildingStates
{
    public abstract class ProductionBuildingState
    {
        #region Fields

        protected ProductionBuilding m_targetBuilding;

        #endregion



        #region Properties

        public abstract string Status { get; }

        #endregion
        
        
        
        #region Public Methods

        public virtual void OnStateEnter(ProductionBuilding productionBuilding)
        {
            m_targetBuilding = productionBuilding;
        }


        public virtual void OnStateExit()
        {
            
        }


        public abstract ProductionBuildingState HandleState(float deltaTime);

        #endregion
    }
}
