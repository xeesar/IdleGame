using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    [System.Serializable]
    public class CityProgressData
    {
        #region Fields

        public string cityName;

        public DateTime exitTime;
        public DateTime enterTime;

        public Dictionary<int, int> dynamicParametersLevels;

        public Dictionary<int, ProductionBuildingData> productionBuildings;

        #endregion



        #region Properties

        public int DrawnBuildings => productionBuildings.Count(building => building.Value.isDrawn);

        public int UnlockedBuildings => productionBuildings.Count(building => building.Value.isUnlocked);

        #endregion



        #region Public Methods

        public ProductionBuildingData GetProductionBuildingData(int id)
        {
            if (productionBuildings.ContainsKey(id))
            {
                return productionBuildings[id];
            }

            ProductionBuildingData productionBuildingData = new ProductionBuildingData
            {
                isUnlocked = false,
                isAutoCollectingEnabled = false,

                incomeParameterLevel = 1,
                productionSpeedParameterLevel = 1,
                
                productionProgress = 0,
                drawProgress = 0
            };
            
            productionBuildings.Add(id, productionBuildingData);

            return productionBuildingData;
        }

        #endregion
    }
}
