using Enums;
using UnityEngine;

namespace Data.Configs
{
    [CreateAssetMenu(menuName = "Configs/Production Building Config", fileName = "ProductionBuilding")]
    public class ProductionBuildingConfig : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private int m_id = 0;

        [SerializeField] private float m_unlockPrice = 0;
        [SerializeField] private float m_autoCollectingPrice = 0;

        [SerializeField] private DynamicParameterData m_incomeDynamicParameterData = default;
        [SerializeField] private DynamicParameterData m_productionSpeedDynamicParameterData = default;

        #endregion



        #region Properties

        public int Id => m_id;

        public float UnlockPrice
        {
            get => m_unlockPrice;
            set => m_unlockPrice = value;

        }

        public float AutoCollectingPrice
        {
            get => m_autoCollectingPrice;
            set => m_autoCollectingPrice = value;
        }

        public DynamicParameterData IncomeData => m_incomeDynamicParameterData;

        public DynamicParameterData ProductionSpeedData => m_productionSpeedDynamicParameterData;

        #endregion
    }
}
