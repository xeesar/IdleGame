using System.Collections.Generic;
using Data;
using Data.Configs;
using Enums;
using Interfaces;
using Managers;
using Models.DynamicParameters;
using Models.ProgressionCalculators;
using Services;

namespace Models.ParametersFactory
{
    public class ParameterFactory
    {
        #region Fields

        private CityData m_cityData;
        private CityProgressData m_cityProgressData;
        private Dictionary<int, int> m_dynamicParametersLevels;

        #endregion
        
        
        
        #region Public Methods

        public ParameterFactory(string currentCity)
        {
            m_cityData = ConfigManager.Instance.Get<CityConfig>().GetCityData(currentCity);
            m_cityProgressData = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress;
            m_dynamicParametersLevels = m_cityProgressData.dynamicParametersLevels;
        }
        
        
        public DynamicParameter Get(DynamicParameterType parameterType)
        {
            int level = m_dynamicParametersLevels[(int) parameterType];
            DynamicParameterData data = m_cityData.GetDynamicParameterData(parameterType);

            DynamicParameter parameter = CreateParameter(parameterType, level, data);
            
            parameter.SetCalculators(GetProgressionCalculator(data.Parameter.progressionType), GetProgressionCalculator(data.Price.progressionType));

            return parameter;
        }
        
        
        public DynamicParameter Get(DynamicParameterType parameterType, DynamicParameterData data, int level)
        {
            DynamicParameter parameter = CreateParameter(parameterType, level, data);
            
            parameter.SetCalculators(GetProgressionCalculator(data.Parameter.progressionType), GetProgressionCalculator(data.Price.progressionType));

            return parameter;
        }

        #endregion



        #region Private Methods

        private DynamicParameter CreateParameter(DynamicParameterType dynamicParameterType, int level, DynamicParameterData data)
        {
            switch (dynamicParameterType)
            {
                case DynamicParameterType.ArtistsCount:
                    return new ArtistsCount(level, data);
                case DynamicParameterType.DrawingSpeed:
                    return new DrawingSpeed(level, data);
                case DynamicParameterType.RunningSpeed:
                    return new RunSpeed(level, data);
                case DynamicParameterType.SprayBottleCapacity:
                    return new SprayCanCapacity(level, data);
                case DynamicParameterType.RespectIncomePerBlock:
                    return new RespectIncomePerBlock(level, data);
                case DynamicParameterType.BuildingIncome:
                    return new BuildingIncome(level, data);
                case DynamicParameterType.ProductionSpeed:
                    return new ProductionSpeed(level, data);
                default:
                    return null;
            }
        }


        private ProgressionCalculator GetProgressionCalculator(ProgressionType progressionType)
        {
            switch (progressionType)
            {
                case ProgressionType.Arithmetic:
                    return new ArithmeticCalculator();
                case ProgressionType.Geometric:
                    return new GeometricCalculator();
                default:
                    return null;
            }
        }

        #endregion
    }
}
