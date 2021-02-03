using Models.Buildings;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Level Data", fileName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        #region Fields

        [Header("Options")] 
        [SerializeField] private GraffitiData m_graffitiData = null;
        [SerializeField] private Building m_building = null;

        #endregion



        #region Properties
        
        public GraffitiData GraffitiData => m_graffitiData;

        public Building Building => m_building;

        #endregion
    }
}

