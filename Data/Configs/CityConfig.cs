using System.Collections.Generic;
using UnityEngine;

namespace Data.Configs
{
    [CreateAssetMenu(menuName = "Configs/City Config", fileName = "CityConfig")]
    public class CityConfig : BaseConfig
    {
        #region Fields

        [SerializeField] private List<CityData> m_cities = new List<CityData>();

        #endregion



        #region Properties

        public List<CityData> Cities => m_cities;

        #endregion
        
        

        #region Public Methods

        public CityData GetCityData(string cityName)
        {
            for (int i = 0; i < m_cities.Count; i++)
            {
                if (m_cities[i].cityName == cityName) return m_cities[i];
            }

            return null;
        }

        #endregion
    }
}

