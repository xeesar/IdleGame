using Data;
using Enums;
using Models.Currencies;
using UnityEngine;

namespace Interfaces
{
    public interface IUserProfileModel
    {
        #region Properties

        IUserProfile UserProfile { get; set; }
        
        IStorage Storage { get; set; }
        
        string CurrentCity { get; set; }
        
        CityProgressData CurrentCityProgress { get; }
        
        int OpenedBuildingId { get; set; }
        
        int OpenedBuildingArtists { get; set; }
        
        CurrencyType OpenedBuildingCurrencyType { get; set; }

        bool IsFirstSession { get; set; }
        
        int TutorialStage { get; set; }
        
        bool IsSoundOn { get; set; }
        
        bool IsMusicOn { get; set; }

        Vector3 LastCameraPos { get; set; }
        
        #endregion



        #region Public Methods

        Currency GetCurrency(CurrencyType currencyType);
        void UpdateCurrency(CurrencyType currencyType, float value);
        void SaveUserProfile();
        void LoadUserProfile();

        #endregion
    }
}

