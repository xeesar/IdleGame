using System;
using System.Collections;
using System.Linq;
using Data;
using Enums;
using Extensions;
using Interfaces;
using Models.Popups;
using Models.Tiles;
using Services;
using UnityEngine;
using View.UI;

namespace Managers
{
    public class ArtistsManager : Singleton<ArtistsManager>, IManager
    {
        #region Fields

        [Header("Options")]
        [SerializeField] private GameObject m_artistPrefab = null;
        [SerializeField] private float m_spawnInterval = 1f;
        
        private GraffitiAreaTile m_graffitiAreaTile;

        private int m_tutorialTapsCount = 0;
        
        #endregion



        #region Unity Lifecycle

        private void OnEnable()
        {
            ServiceLocator.Instance.Get<InputService>().eventOnPointerDown += OnPointerDown;

            DynamicParametersManager.Instance.Get(DynamicParameterType.ArtistsCount).EventOnUpgraded += OnArtistAdded;
        }


        private void OnDisable()
        {
            ServiceLocator.Instance.Get<InputService>().eventOnPointerDown -= OnPointerDown;
            
            DynamicParametersManager.Instance.Get(DynamicParameterType.ArtistsCount).EventOnUpgraded -= OnArtistAdded;
        }

        #endregion


        
        #region Public Methods

        public void Initialize()
        {
            m_graffitiAreaTile = LevelManager.Instance.Building.GraffitiAreaTile;
            SpawnArtists();
        }
        

        #endregion



        #region Private Methods

        private void OnArtistAdded()
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();

            if (userProfileModel.TutorialStage == (int)TutorialType.BuyArtists)
            {
                userProfileModel.TutorialStage++;
                TutorialPopup tutorialPopup = PopupManager.Instance.GetPopup<TutorialPopup>();
                tutorialPopup.Hide();
            }

            TeamManager.Instance.BusyArtistsCount += 1;

            var buildings =userProfileModel.CurrentCityProgress.productionBuildings;
            buildings[userProfileModel.OpenedBuildingId].artistsCount = userProfileModel.OpenedBuildingArtists += 1;
            
            SpawnArtist();
        }
        
        
        private void SpawnArtists()
        {
            int artistsCount = ServiceLocator.Instance.Get<IUserProfileModel>().OpenedBuildingArtists;
            StartCoroutine(SpawnArtistsWithInterval(artistsCount));
        }
        
        
        private IEnumerator SpawnArtistsWithInterval(int artistsCount)
        {
            for (int i = 0; i < artistsCount; i++)
            {
                SpawnArtist();
                yield return new WaitForSeconds(m_spawnInterval);
            }

            TryToShowSpeedUpTutorial();
        }
        
        
        private void SpawnArtist()
        {
            var artist = Instantiate(m_artistPrefab, m_graffitiAreaTile.ArtistSpawn);
            artist.transform.localPosition = Vector3.zero;
        }


        private void TryToShowSpeedUpTutorial()
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();
            
            if (userProfileModel.CurrentCityProgress.UnlockedBuildings > 1 || userProfileModel.TutorialStage > (int)TutorialType.SpeedUpArtists)
            {
                TryToShowBuyArtistTutorial();
                return;
            }

            TutorialPopup tutorialPopup = PopupManager.Instance.GetPopup<TutorialPopup>();
            tutorialPopup.InitializeTutorial(TutorialType.SpeedUpArtists);
            tutorialPopup.Show();
            
            ServiceLocator.Instance.Get<InputService>().eventOnPointerDown += OnTutorialPointerDown;
        }


        private void OnTutorialPointerDown()
        {
            m_tutorialTapsCount++;
            
            if(m_tutorialTapsCount < 3) return;
            
            ServiceLocator.Instance.Get<InputService>().eventOnPointerDown -= OnTutorialPointerDown;
            ServiceLocator.Instance.Get<IUserProfileModel>().TutorialStage += 1;
            TryToShowBuyArtistTutorial();
        }
        

        private void OnPointerDown()
        {
            BonusManager.Instance.ActivateBonus(BonusType.MovingSpeed);
            BonusManager.Instance.ActivateBonus(BonusType.DrawingSpeed);
        }


        private void TryToShowBuyArtistTutorial()
        {
            if(DynamicParametersManager.Instance.Get(DynamicParameterType.ArtistsCount).Level > 1) return;
            
            TutorialPopup tutorialPopup = PopupManager.Instance.GetPopup<TutorialPopup>();
            tutorialPopup.InitializeTutorial(TutorialType.BuyArtists);
            tutorialPopup.Show();
        }

        #endregion
    }
}
