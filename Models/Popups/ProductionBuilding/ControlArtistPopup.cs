using Data;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Models.Popups
{
    public class ControlArtistPopup : ProductionBuildingPopup
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private Slider m_slider;

        [SerializeField] private TextMeshProUGUI m_addArtistText;

        private TeamManager m_teamManager;
        
        #endregion



        #region Properties

        protected override SceneType SceneType => SceneType.Core;

        #endregion



        #region Unity Lifecycle

        private void OnEnable()
        {
            DynamicParametersManager.Instance.Get(DynamicParameterType.ArtistsCount).EventOnUpgraded += DisplayInfo;
            
            m_slider.onValueChanged.AddListener(OnSliderValueChanged);
        }


        private void OnDisable()
        {
            DynamicParametersManager.Instance.Get(DynamicParameterType.ArtistsCount).EventOnUpgraded -= DisplayInfo;
            
            m_slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        #endregion



        #region Private Methods

        protected override void Initialize()
        {
            base.Initialize();
            
            m_teamManager = TeamManager.Instance;
            
            DisplayInfo();

            if (ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress.DrawnBuildings == 0)
            {
                TryToShowAddArtistTutorial();
            }
        }
        

        private void DisplayInfo()
        {
            int maxArtists = m_teamManager.ArtistsCount - m_teamManager.BusyArtistsCount + m_targetBuilding.ArtistsCount;
            int currentArtists = m_targetBuilding.ArtistsCount;
            
            m_addArtistText.text = string.Format(StringConstants.Formats.AddArtistFormat, currentArtists, maxArtists);

            m_slider.minValue = 0;
            m_slider.maxValue = maxArtists;
            m_slider.value = currentArtists;
        }

        
        private void OnSliderValueChanged(float currentValue)
        {
            int maxArtists = (int)m_slider.maxValue;
            int currentArtists = (int)currentValue;
            int artistsDifference = currentArtists - m_targetBuilding.ArtistsCount;
                
            m_addArtistText.text = string.Format(StringConstants.Formats.AddArtistFormat, currentArtists, maxArtists);
            m_slider.value = currentArtists;

            m_targetBuilding.ArtistsCount = currentArtists;
            m_teamManager.BusyArtistsCount += artistsDifference;
        }


        private void TryToShowAddArtistTutorial()
        {
            if (m_targetBuilding.ArtistsCount > 0)
            {
                TryToShowWatchGraffitiTutorial();
                return;
            }

            TutorialPopup tutorialPopup = PopupManager.Instance.GetPopup<TutorialPopup>();
            tutorialPopup.InitializeTutorial(TutorialType.AddArtist);
            tutorialPopup.Show();
            
            m_slider.onValueChanged.AddListener(OnTutorialValueChanged);
        }


        private void TryToShowWatchGraffitiTutorial()
        {
            if(ServiceLocator.Instance.Get<IUserProfileModel>().TutorialStage > (int)TutorialType.WatchGraffiti) return;
            
            TutorialPopup tutorialPopup = PopupManager.Instance.GetPopup<TutorialPopup>();
            tutorialPopup.InitializeTutorial(TutorialType.WatchGraffiti);
            tutorialPopup.Show();
        }


        private void OnTutorialValueChanged(float value)
        {
            m_slider.onValueChanged.RemoveListener(OnTutorialValueChanged);

            ServiceLocator.Instance.Get<IUserProfileModel>().TutorialStage += 1;
            TryToShowWatchGraffitiTutorial();
        }


        protected override void OnClaimButtonClick()
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();
            
            if(userProfileModel.TutorialStage == (int)TutorialType.AddArtist) return;
            
            userProfileModel.OpenedBuildingArtists = m_targetBuilding.ArtistsCount;
            
            if (userProfileModel.TutorialStage == (int)TutorialType.WatchGraffiti)
            {
                userProfileModel.TutorialStage += 1;
                TutorialPopup tutorialPopup = PopupManager.Instance.GetPopup<TutorialPopup>();
                tutorialPopup.Hide();
            }

            base.OnClaimButtonClick();
        }


        protected override void OnCloseButtonClick()
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();
            
            if(userProfileModel.TutorialStage <= (int)TutorialType.WatchGraffiti) return;
            
            base.OnCloseButtonClick();
        }

        #endregion
    }
}

