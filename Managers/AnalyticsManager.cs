using System.Collections.Generic;
using Data;
using Extensions;
using Facebook.Unity;
using Interfaces;
using Models.Analytics;
using Services;
using UnityEngine;

namespace Managers
{
    public class AnalyticsManager : Singleton<AnalyticsManager>, IManager
    {
        #region Fields

        [Header("Options")]
        [SerializeField] private List<BaseAnalytics> m_analytics;

        #endregion
        
        
        
        #region Unity Lifecycle
        
        private void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus)
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                }
                else
                {
                    FB.Init(OnInited);
                }
            }
        }

        #endregion
        
        
        
        #region Public Methods

        public void Initialize()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                FB.Init(OnInited);
            }
        }


        public void SendCustomEvent(AnalyticEvent analyticEvent)
        {
            AddDefaultParameters(analyticEvent);
            
            for (int i = 0; i < m_analytics.Count; i++)
            {
                m_analytics[i].CustomEvent(analyticEvent);
            }
        }
        
        #endregion



        #region Private Methods

        private void OnInited()
        {
            FB.ActivateApp();
        }


        private void AddDefaultParameters(AnalyticEvent analyticEvent)
        {
            string cityName = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCity;
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.City, cityName.ToLower());
        }

        #endregion
    }
}
