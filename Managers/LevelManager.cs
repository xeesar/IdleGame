using System.Collections.Generic;
using Controllers;
using Data;
using Data.Configs;
using Extensions;
using Interfaces;
using Models.Buildings;
using Models.Levels;
using Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>, IManager
    {
        #region Fields

        [Header("Options")] 
        [SerializeField] private Level m_levelPrefab = null;

        private Level m_level;

        private CityData m_cityData;
        
        #endregion



        #region Properties

        public Building Building => m_level.Building;

        public GraffitiData Graffiti
        {
            get
            {
                int buildingId = ServiceLocator.Instance.Get<IUserProfileModel>().OpenedBuildingId;

                return m_cityData.GetLevelData(buildingId).GraffitiData;
            }
        }

        #endregion



        #region Private Methods

        public void Initialize()
        {
            InitializeLevel();
            CreateLevel();

            FindObjectOfType<CameraController>().Target = Building.Holst.Transform;
        }
        

        private void InitializeLevel()
        {
            string cityNumber = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCity;
            m_cityData =  ConfigManager.Instance.Get<CityConfig>().GetCityData(cityNumber);
        }
        
        
        private void CreateLevel()
        {
            SpawnLevel();
        }


        private void SpawnLevel()
        {
            m_level = Instantiate(m_levelPrefab, transform);
            
            SpawnEnvironmentBuildings(m_cityData.EnvironmenBuildings);
            SpawnMainBuilding(m_cityData);
        }


        private void SpawnEnvironmentBuildings(List<GameObject> environmentBuildings)
        {
            List<GameObject> spawnedBuildings = new List<GameObject>();
            
            int levelEnvironmentsCount = m_level.EnvironmentBuildingsCount;

            for (int i = 0; i < levelEnvironmentsCount; i++)
            {
                GameObject building = GetRandomBuilding(environmentBuildings, spawnedBuildings);
                spawnedBuildings.Add(building);
                
                m_level.SpawnEnvironmentBuilding(i, building);
            }
        }


        private GameObject GetRandomBuilding(List<GameObject> buildingsPrefabs, List<GameObject> spawnedBuildings)
        {
            GameObject building = null;
            
            while (true)
            {
                var randomNumber = Random.Range(0, buildingsPrefabs.Count);
                building = buildingsPrefabs[randomNumber];
                
                if(spawnedBuildings.Contains(building)) continue;
                
                break;
            }

            return building;
        }


        private void SpawnMainBuilding(CityData cityData)
        {
            int levelNumber = ServiceLocator.Instance.Get<IUserProfileModel>().OpenedBuildingId;
            LevelData levelData = cityData.GetLevelData(levelNumber);
            
            m_level.SpawnMainBuilding(levelData.Building.gameObject);
        }

        #endregion
    }
}
