using System.Collections.Generic;
using Models.Buildings;
using UnityEngine;

namespace Models.Levels
{
    public class Level : MonoBehaviour
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private List<Transform> m_environmentBuildingsSpawnPoses = new List<Transform>();
        [SerializeField] private Transform m_mainBuildingSpawnPos = null;

        private Building m_mainBuilding;
        
        #endregion



        #region Properties

        public Building Building => m_mainBuilding;
        public int EnvironmentBuildingsCount => m_environmentBuildingsSpawnPoses.Count;

        
        #endregion



        #region Public Methods

        public void SpawnEnvironmentBuilding(int index, GameObject building)
        {
            if (index >= EnvironmentBuildingsCount) return;

            Transform spawnPos = m_environmentBuildingsSpawnPoses[index];

            SpawnBuilding(building, spawnPos);
        }


        public void SpawnMainBuilding(GameObject building)
        {
            SpawnBuilding(building, m_mainBuildingSpawnPos);

            m_mainBuilding = m_mainBuildingSpawnPos.GetChild(0).GetComponent<Building>();
        }

        #endregion



        #region Private Methods

        private void SpawnBuilding(GameObject building, Transform spawnPos)
        {
            GameObject buildingObject = Instantiate(building, spawnPos);
            buildingObject.transform.localPosition = Vector3.zero;
            buildingObject.transform.localRotation = Quaternion.identity;
        }

        #endregion
    }
}

