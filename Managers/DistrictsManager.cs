using System.Collections.Generic;
using Extensions;
using Interfaces;
using Managers;
using Models.Districts;
using Models.Popups;
using Services;

public class DistrictsManager : Singleton<DistrictsManager>, IManager
{
    #region Fields

    private List<District> m_districts;
    
    #endregion

    

    #region Unity Lifecycle

    private void OnDisable()
    {
        m_districts.Clear();
    }

    #endregion



    #region Public Methods

    public void Initialize()
    {
        m_districts = new List<District>();
    }


    public void AddDistrict(District district)
    {
        int unlockedBuildings = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress.DrawnBuildings;
        bool isUnlocked = unlockedBuildings >= district.DrawnBuildingsToUnlock;
        
        district.SetDistrictActive(isUnlocked);
        
        m_districts.Add(district);
    }

    
    public void OnBuildingDrawn()
    {
        int drawnBuildings = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress.DrawnBuildings;

        for (int i = 0; i < m_districts.Count; i++)
        {
            if(m_districts[i].IsUnlocked || drawnBuildings < m_districts[i].DrawnBuildingsToUnlock) continue;

            m_districts[i].SetDistrictActive(true);
            NewDistrictPopup popup = PopupManager.Instance.GetPopup<NewDistrictPopup>();
            popup.TargetDistrictPosition = m_districts[i].transform.position;
            popup.Show();

            return;
        }
    }

    #endregion
}
