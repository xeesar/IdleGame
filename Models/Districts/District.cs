using System;
using System.Collections.Generic;
using UnityEngine;
using View.UI;

namespace Models.Districts
{
    public class District : MonoBehaviour
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private DistrictView m_districtView;
        [SerializeField] private List<DistrictElement> m_elements;

        [Header("Options")] 
        [SerializeField] private int m_unlockCost;
        
        #endregion



        #region Properties

        public bool IsUnlocked { get; private set; }
        
        public int DrawnBuildingsToUnlock => m_unlockCost;

        #endregion

        

        #region Unity Lifecycle

        private void Start()
        {
            DistrictsManager.Instance.AddDistrict(this);
        }

        #endregion
        
        
        
        #region Public Methods

        public void SetDistrictActive(bool isUnlocked)
        {
            IsUnlocked = isUnlocked;
            
            for (int i = 0; i < m_elements.Count; i++)
            {
                m_elements[i].SetElementActive(isUnlocked);
            }
            
            m_districtView.SetDistrictStatus(isUnlocked, m_unlockCost);
        }

        #endregion
    }
}

