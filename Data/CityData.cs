using System.Collections.Generic;
using System.Linq;
using Enums;
using Models.DynamicParameters;
using Models.ProgressionCalculators;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/City Data", fileName = "CityData")]
    public class CityData : ScriptableObject
    {
        #region Fields

        public string cityName;
        
        [SerializeField] private List<DynamicParameterData> m_dynamicParameters = new List<DynamicParameterData>();
        [SerializeField] private List<LevelData> m_levels = new List<LevelData>();
        [SerializeField] private List<GameObject> m_environmentBuildings = new List<GameObject>();
        [SerializeField] private AudioClipData m_audioClipData;
        
        #endregion



        #region Properties

        public int DynamicParametersCount => m_dynamicParameters.Count;

        public int LevelsCount => m_levels.Count;

        public List<GameObject> EnvironmenBuildings => m_environmentBuildings;

        public AudioClipData AudioClipData => m_audioClipData;
        
        #endregion
        
        

        #region Public Methods

        public DynamicParameterData GetDynamicParameterData(DynamicParameterType parameterType)
        {
            DynamicParameterData dynamicParameterData = default;
            
            for (int i = 0; i < m_dynamicParameters.Count; i++)
            {
                if (m_dynamicParameters[i].type != parameterType) continue;
                
                dynamicParameterData = m_dynamicParameters[i];
                break;
            }

            return dynamicParameterData;
        }

        
        public LevelData GetLevelData(int index)
        {
            return m_levels[index];
        }


        [ContextMenu("Debug DynamicParameters")]
        public void Debug()
        {
            for (int i = 0; i < m_dynamicParameters.Count; i++)
            {
                DynamicParameter dynamicParameter =
                    GetDynamicParameter(m_dynamicParameters[i].type, m_dynamicParameters[i]);

                for (int level = 1; level < 20; level++)
                {
                    dynamicParameter.SetLevel(level);
                    dynamicParameter.SetCalculators(GetProgressionCalculator(m_dynamicParameters[i].Parameter.progressionType), GetProgressionCalculator(m_dynamicParameters[i].Price.progressionType));
                    UnityEngine.Debug.Log($"{dynamicParameter.ParameterType} - Level: {level}, Value: {dynamicParameter.Value}, Price: {dynamicParameter.Price}");
                }
            }
        }

        #endregion



        #region Private Methods

        private DynamicParameter GetDynamicParameter(DynamicParameterType type, DynamicParameterData data)
        {
            switch (type)
            {
                case DynamicParameterType.ArtistsCount:
                    return new ArtistsCount(1, data);
                case DynamicParameterType.DrawingSpeed:
                    return new DrawingSpeed(1, data);
                case DynamicParameterType.RunningSpeed:
                    return new RunSpeed(1, data);
                case DynamicParameterType.SprayBottleCapacity:
                    return new SprayCanCapacity(1, data);
                case DynamicParameterType.RespectIncomePerBlock:
                    return new RespectIncomePerBlock(1, data);
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

