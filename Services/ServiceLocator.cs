using System;
using System.Collections.Generic;
using Data.Configs;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Models.Converters;
using Models.UserProfile;

namespace Services
{
    public class ServiceLocator : Singleton<ServiceLocator>, IManager
    {
        #region Fields

        private Dictionary<Type, object> _services = new Dictionary<Type, object>();

        #endregion



        #region Unity Lifecycle

#if UNITY_EDITOR
        private void OnApplicationQuit()
        {
            EndGame();
        }
#else
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                EndGame();
            }
        }
#endif

        #endregion


        
        #region Public Methods

        public void Initialize()
        {
            InitializeServices();
        }
        

        public T Get<T>()
        {
            T service = default;
            
            Type serviceType = typeof(T);
            
            if (_services.ContainsKey(typeof(T)))
            {
                return (T) _services[serviceType];
            }

            return service;
        }

        #endregion



        #region Private Methods

        private void EndGame()
        {
            IUserProfileModel userProfileModel = Get<IUserProfileModel>();
            
            userProfileModel.CurrentCityProgress.exitTime = DateTime.Now;
            userProfileModel.IsFirstSession = false;
            userProfileModel.SaveUserProfile();
        }
        
        
        private void InitializeServices()
        {
            _services.Add(typeof(IUserProfileModel), GetUserProfileModel());
            _services.Add(typeof(InputService), GetInputService());
            _services.Add(typeof(IncomeService), GetIncomeService());

        }


        private IUserProfileModel GetUserProfileModel()
        {
            IUserProfileModel userProfileModel = new UserProfileModel();
            IStorage storage = new PlayerPrefsStorage();
            storage.Converter = new JSONConverter();
            userProfileModel.Storage = storage;
            userProfileModel.LoadUserProfile();
            userProfileModel.CurrentCity = "Minsk";

            return userProfileModel;
        }


        private InputService GetInputService()
        {
            InputService inputService = new InputService();

            return inputService;
        }


        private IncomeService GetIncomeService()
        {
            return new IncomeService();
        }

        #endregion
    }
}

