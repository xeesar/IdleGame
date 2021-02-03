using System;
using System.Collections.Generic;
using Data;
using Data.Configs;
using Enums;
using Extensions;
using Interfaces;
using Models.DynamicParameters;
using Models.ParametersFactory;
using Services;

namespace Managers
{
    public class DynamicParametersManager : Singleton<DynamicParametersManager>, IManager
    {
        #region Fields
        
        private List<DynamicParameter> m_dynamicParameters = new List<DynamicParameter>();

        private ParameterFactory m_parameterFactory;

        #endregion



        #region Properties

        public ParameterFactory ParameterFactory => m_parameterFactory;

        #endregion


        
        #region Public Methods

        public void Initialize()
        {
            m_parameterFactory = new ParameterFactory(ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCity);
            
            InitializeDynamicParameters();
        }
        

        public DynamicParameter Get(DynamicParameterType type)
        {
            DynamicParameter parameter = default;

            for (int i = 0; i < m_dynamicParameters.Count; i++)
            {
                if (m_dynamicParameters[i].ParameterType == type)
                {
                    return m_dynamicParameters[i];
                }
            }

            return parameter;
        }


        public void DynamicParameterUpdated(DynamicParameterType type, int level)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent(StringConstants.AnalyticsEvents.UpgradeDynamicParameter);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.ParameterType, type.ToFriendlyName());
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);

            CityProgressData cityProgressData = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress;
            Dictionary<int, int> dynamicParametersLevels = cityProgressData.dynamicParametersLevels;

            dynamicParametersLevels[(int) type] = level;
        }

        #endregion



        #region Private Methods

        private void InitializeDynamicParameters()
        {
            //Переменные влияющие на геймплей
            m_dynamicParameters.Add(m_parameterFactory.Get(DynamicParameterType.ArtistsCount));
            m_dynamicParameters.Add(m_parameterFactory.Get(DynamicParameterType.SprayBottleCapacity));
            m_dynamicParameters.Add(m_parameterFactory.Get(DynamicParameterType.DrawingSpeed));
            m_dynamicParameters.Add(m_parameterFactory.Get(DynamicParameterType.RunningSpeed));

            //Переменные влияющие на доход
            m_dynamicParameters.Add(m_parameterFactory.Get(DynamicParameterType.RespectIncomePerBlock));
        }

        #endregion
    }
}
