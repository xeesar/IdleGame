using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Enums;
using Extensions;
using Interfaces;
using Models.DynamicParameters;
using Services;
using IUserProfile = UnityEngine.SocialPlatforms.IUserProfile;

namespace Managers
{
    public class TeamManager : Singleton<TeamManager>, IManager
    {
        #region Fields

        public event Action EventOnArtistsUpdated;
        
        private DynamicParameter m_artistsParameter;

        private int m_busyArtists = 0;

        private int m_selectedBuildingArtistsCount = 0;

        #endregion



        #region Properties

        public int ArtistsCount => (int) m_artistsParameter.Value;

        public int BusyArtistsCount
        {
            get => m_busyArtists;
            set
            {
                m_busyArtists = value;
                EventOnArtistsUpdated?.Invoke();
            }
        }
        
        #endregion



        #region Public Methods

        public void Initialize()
        {
            m_artistsParameter = DynamicParametersManager.Instance.Get(DynamicParameterType.ArtistsCount);

            CalculateBusyArtists();
        }

        #endregion



        #region Private Methods

        private void CalculateBusyArtists()
        {
            Dictionary<int, ProductionBuildingData> buildings = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress.productionBuildings;
            
            foreach (var building in buildings)
            {
                BusyArtistsCount += building.Value.artistsCount;
            }
        }

        #endregion
    }
}
