using Data;
using Data.Configs;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Models.Popups;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Models.Screens
{
    public class GameScreen : BaseScreen
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private Button m_backButton;
        [SerializeField] private Button m_settingsOfArtistButton;
        
        #endregion



        #region Public Methods

        public override void Show()
        {
            base.Show();
            m_backButton.onClick.AddListener(OnBackButtonClick);
            m_settingsOfArtistButton.onClick.AddListener(ArtistSettingsOnButtonClick);
        }

        
        public override void Hide()
        {
            base.Hide();
            
            m_backButton.onClick.RemoveListener(OnBackButtonClick);
            m_settingsOfArtistButton.onClick.RemoveListener(ArtistSettingsOnButtonClick);
        }


        public void SetBackButtonActive(bool isActive)
        {
            m_backButton.gameObject.SetActive(isActive);
        }


        public void TryToShowBackButtonTutorial()
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();
            
            if(userProfileModel.CurrentCityProgress.UnlockedBuildings > 1 || userProfileModel.TutorialStage != (int)TutorialType.ReturnToCity) return;
            
            TutorialPopup tutorialPopup = PopupManager.Instance.GetPopup<TutorialPopup>();
            tutorialPopup.InitializeTutorial(TutorialType.ReturnToCity);
            tutorialPopup.Show();
        }
        
        #endregion



        #region Private Methods

        private void OnBackButtonClick()
        {
            if(DynamicParametersManager.Instance.Get(DynamicParameterType.ArtistsCount).Level <= 1) return;
            
            SetBackButtonActive(false);
            
            AudioClipData audioClipData = ConfigManager.Instance.Get<AudioConfig>().OpenPopupAudioClipData;
            AudioManager.Instance.PlaySoundFx(audioClipData);
            
            LoadingScreen loadingScreen = ScreensManager.Instance.GetScreen<LoadingScreen>();
            loadingScreen.Initialize(SceneType.Main);
            loadingScreen.Show();

            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();

            if (userProfileModel.TutorialStage == (int) TutorialType.ReturnToCity)
            {
                TutorialPopup tutorialPopup = PopupManager.Instance.GetPopup<TutorialPopup>();
                tutorialPopup.Hide();

                userProfileModel.TutorialStage++;   
            }
        }
        
        
        private void ArtistSettingsOnButtonClick()
        {
            PopupManager.Instance.ShowPopup<ArtistSettingPopup>();
        }

        #endregion
    }
}
