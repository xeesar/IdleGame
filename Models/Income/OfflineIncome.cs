using System.Collections.Generic;
using Data;
using Data.Configs;
using Enums;
using Interfaces;
using Managers;
using Services;
using UnityEngine;

namespace Models.Income
{
    public class OfflineIncome : Income
    {
        #region Fields

        private float m_respectOfflineIncome = 0;
        private float m_dollarsOfflineIncome = 0;

        private Dictionary<int, ProductionBuildingData> m_buildingsData;
        
        #endregion
        
        
        
        #region Properties

        public float RespectIncome => m_respectOfflineIncome;

        public float DollarsIncome => m_dollarsOfflineIncome;

        public bool HasIncome => RespectIncome > 0 || DollarsIncome > 0;
        
        #endregion
        
        
        
        #region Public Methods

        public OfflineIncome()
        {
            m_buildingsData = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress.productionBuildings;
                
            m_respectOfflineIncome = GetIncome(CurrencyType.Respect);
            m_dollarsOfflineIncome = GetIncome(CurrencyType.Dollar);
        }
        

        public override void Give(float multiplier)
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();
            userProfileModel.UpdateCurrency(CurrencyType.Respect, RespectIncome * multiplier);
            userProfileModel.UpdateCurrency(CurrencyType.Dollar, DollarsIncome * multiplier);
        }

        #endregion



        #region Private Methods

        private float GetIncome(CurrencyType currencyType)
        {
            float maxOfflineIncomeTime = ConfigManager.Instance.Get<GameConfig>().MaxOfflineIncomeMinutes * 60;
            float offlineTime = Mathf.Min(TimeManager.Instance.SecondsOffline, maxOfflineIncomeTime);
            float buildingsIncomeForOneSecond = GetBuildingsIncomeForOneSecond((int)currencyType);
            
            return buildingsIncomeForOneSecond * offlineTime;
        }

        
        private float GetBuildingsIncomeForOneSecond(int currencyType)
        {
            float totalIncome = 0;

            foreach (var buildingData in m_buildingsData)
            {
                var data = buildingData.Value;
                
                if(!data.isAutoCollectingEnabled || data.currency != currencyType) continue;

                float income = data.incomeValue;
                float productionTime = data.productionSpeedValue;

                totalIncome += income / productionTime;
            }

            return totalIncome;
        }
        
        #endregion
    }
}

