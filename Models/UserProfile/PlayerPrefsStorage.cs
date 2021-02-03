using System.Collections.Generic;
using Data;
using Data.Configs;
using Extensions;
using Interfaces;
using Managers;
using Models.Currencies;
using UnityEngine;

namespace Models.UserProfile
{
    public class PlayerPrefsStorage : IStorage
    {
        #region Properties

        public IConverter Converter { get; set; }

        #endregion
        
        
        
        #region Public Methods

        public void Save(IUserProfileModel userProfileModel)
        {
            SaveInventory(userProfileModel.UserProfile.UserInventory);
            SaveUserStats(userProfileModel.UserProfile.UserStats);
            SaveGameOptions(userProfileModel.UserProfile.GameOptions);
        }

        public void Load(IUserProfileModel userProfileModel)
        {
            IUserInventory inventory = LoadInventory();
            IUserStats userStats = LoadUserStats();
            IGameOptions gameOptions = LoadGameOptions();
            
            userProfileModel.UserProfile = new UserProfile(inventory, userStats, gameOptions);
        }


        public void Reset()
        {
            PlayerPrefs.DeleteAll();
        }

        #endregion



        #region Private Methods

        private void SaveInventory(IUserInventory inventory)
        {
            Converter.Serialize(inventory.CityProgressData, StringConstants.PlayerPrefsKeys.CitiesData);

            SaveCurrencies(inventory.Currencies);
        }
        
        
        
        private IUserInventory LoadInventory()
        {
            IUserInventory inventory = new UserInventory();

            inventory.CityProgressData = LoadCityProgress();
            inventory.Currencies = LoadCurrencies();
            
            return inventory;
        }


        private List<CityProgressData> LoadCityProgress()
        {
            var data = Converter.Deserialize<List<CityProgressData>>(StringConstants.PlayerPrefsKeys.CitiesData);

            if (data != null)
            {
                return data;
            }

            List<CityData> cities = ConfigManager.Instance.Get<CityConfig>().Cities;
            List<CityProgressData> cityProgress = new List<CityProgressData>();

            for (int i = 0; i < cities.Count; i++)
            {
                var city = cities[i];
                
                cityProgress.Add(new CityProgressData());
                
                cityProgress[i].dynamicParametersLevels = new Dictionary<int, int>();
                cityProgress[i].productionBuildings = new Dictionary<int, ProductionBuildingData>();

                cityProgress[i].cityName = city.cityName;
                
                for (int j = 0; j < city.DynamicParametersCount; j++)
                {
                    cityProgress[i].dynamicParametersLevels.Add(j, 1);
                }
            }

            return cityProgress;
        }


        private List<Currency> LoadCurrencies()
        {
            GameConfig gameConfig = ConfigManager.Instance.Get<GameConfig>();

            List<Currency> currencies = new List<Currency>();
            
            currencies.Add(new Dollar(CustomPlayerPrefs.GetFloat(StringConstants.PlayerPrefsKeys.Dollar,gameConfig.StartDollars)));
            currencies.Add(new Respect(CustomPlayerPrefs.GetFloat(StringConstants.PlayerPrefsKeys.Respect,gameConfig.StartRespect)));

            return currencies;
        }


        private void SaveCurrencies(List<Currency> currencies)
        {
            for (int i = 0; i < currencies.Count; i++)
            {
                CustomPlayerPrefs.SetFloat(currencies[i].Name, currencies[i].Value);
            }
        }


        private IUserStats LoadUserStats()
        {
            IUserStats inventory = new UserStats();

            inventory.IsFirstSession = CustomPlayerPrefs.GetBool(StringConstants.PlayerPrefsKeys.FirstSession, true);
            inventory.TutorialStage =  CustomPlayerPrefs.GetInt(StringConstants.PlayerPrefsKeys.TutorialStage, 0);

            return inventory;
        }


        private void SaveUserStats(IUserStats userStats)
        {
            CustomPlayerPrefs.SetBool(StringConstants.PlayerPrefsKeys.FirstSession, userStats.IsFirstSession);
            CustomPlayerPrefs.SetInt(StringConstants.PlayerPrefsKeys.TutorialStage, userStats.TutorialStage);
        }

        
        private IGameOptions LoadGameOptions()
        {
            IGameOptions gameOptions = new GameOptions();

            gameOptions.IsSoundOn = CustomPlayerPrefs.GetBool(StringConstants.PlayerPrefsKeys.IsSoundOn, true);
            gameOptions.IsMusicOn = CustomPlayerPrefs.GetBool(StringConstants.PlayerPrefsKeys.IsMusicOn, true);

            return gameOptions;
        }


        private void SaveGameOptions(IGameOptions gameOptions)
        {
            CustomPlayerPrefs.SetBool(StringConstants.PlayerPrefsKeys.IsSoundOn, gameOptions.IsSoundOn);
            CustomPlayerPrefs.SetBool(StringConstants.PlayerPrefsKeys.IsMusicOn, gameOptions.IsMusicOn);
        }
        
        #endregion
    }
}
