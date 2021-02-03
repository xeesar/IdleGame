using System.Linq;
using Data;
using Enums;
using Interfaces;
using Models.Currencies;
using UnityEngine;

namespace Models.UserProfile
{
    public class UserProfileModel : IUserProfileModel
    {
        #region Properties

        public IUserProfile UserProfile { get; set; }
        
        public IStorage Storage { get; set; }

        public string CurrentCity { get; set; }

        public CityProgressData CurrentCityProgress => UserProfile.UserInventory.CityProgressData.Find(city => city.cityName == CurrentCity);

        public int OpenedBuildingId { get; set; }

        public int OpenedBuildingArtists { get; set; }

        public CurrencyType OpenedBuildingCurrencyType { get; set; }

        public int TutorialStage
        {
            get => UserProfile.UserStats.TutorialStage;
            set => UserProfile.UserStats.TutorialStage = value;
        }
        public bool IsFirstSession
        {
            get => UserProfile.UserStats.IsFirstSession;
            set => UserProfile.UserStats.IsFirstSession = value;
        }

        public bool IsSoundOn
        {
            get => UserProfile.GameOptions.IsSoundOn;
            set => UserProfile.GameOptions.IsSoundOn = value;
        }

        
        public bool IsMusicOn
        {
            get => UserProfile.GameOptions.IsMusicOn;
            set => UserProfile.GameOptions.IsMusicOn = value;
        }


        public Vector3 LastCameraPos { get; set; }

        #endregion



        #region Public Methods

        public Currency GetCurrency(CurrencyType currencyType)
        {
            var currencies = UserProfile.UserInventory.Currencies;

            return currencies.FirstOrDefault(t => t.CurrencyType == currencyType);
        }


        public void UpdateCurrency(CurrencyType currencyType, float value)
        {
            Currency currency = GetCurrency(currencyType);
            currency.Value += value;
        }


        public void SaveUserProfile()
        {
            Storage.Save(this);
        }

        
        public void LoadUserProfile()
        {
            Storage.Load(this);
        }
        
        #endregion
    }
}

