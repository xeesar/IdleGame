using System;
using System.Linq;
using Controllers;
using Data;
using Data.Configs;
using Enums;
using Extensions;
using Interfaces;
using Models.Income;
using Models.Popups;
using Models.ProductionBuildings;
using Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class CityManager : Singleton<CityManager>, IManager
    {
        #region Fields

        [Header("Options")] 
        [SerializeField] private LayerMask m_clickableLayer;
        [SerializeField] private LayerMask m_productionBuildingLayer = 0;

        private InputService m_inputService;
        private ProductionBuilding m_selectedProductionBuilding;

        private Camera m_mainCamera;
        
        #endregion



        #region Properties

        public ProductionBuilding SelectedProductionBuilding => m_selectedProductionBuilding;

        #endregion



        #region Unity Lifecycle

        private void OnEnable()
        {
            m_inputService = ServiceLocator.Instance.Get<InputService>();
            m_inputService.eventOnPointerUp += OnPointerUp;
        }


        private void OnDisable()
        {
            m_inputService.eventOnPointerUp -= OnPointerUp;
        }

        
        private void Start()
        {
            m_mainCamera = FindObjectOfType<CityCameraController>().Camera;
            TryShowOfflineIncome();
            TryShowTutorialOpenBuildingTutorial();
        }
        
        #endregion
        
        
        
        #region Public Methods

        public void Initialize()
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();

            string cityNumber = userProfileModel.CurrentCity;
            AudioClipData audioClipData = ConfigManager.Instance.Get<CityConfig>().GetCityData(cityNumber).AudioClipData;
            
            AudioManager.Instance.PlayBackgroundAudio(audioClipData);
            
            userProfileModel.CurrentCityProgress.enterTime = DateTime.Now;
        }

        #endregion



        #region Private Methods

        private void OnPointerUp()
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();
            
            Vector2 mousePosition = Input.mousePosition;

            if(m_inputService.IsDragged || EventSystem.current.IsPointerUnderUI(mousePosition, m_clickableLayer)) return;

            Ray ray = m_mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, m_productionBuildingLayer);
            
            if (hit)
            {
                ProductionBuilding productionBuilding = hit.collider.gameObject.GetComponentInParent<ProductionBuilding>();
                
                if(!IsCanChooseBuilding(productionBuilding)) return;
                    
                m_selectedProductionBuilding?.OnUnSelected();
                m_selectedProductionBuilding = productionBuilding;
                m_selectedProductionBuilding.OnSelected();
                userProfileModel.OpenedBuildingId = m_selectedProductionBuilding.Id;
                userProfileModel.OpenedBuildingCurrencyType = m_selectedProductionBuilding.CurrencyType;

                if (userProfileModel.TutorialStage == (int) TutorialType.OpenHouse)
                {
                    userProfileModel.TutorialStage += 1;
                }
                
                TryToShowBuyTutorial(productionBuilding);
                return;
            }
            
            m_selectedProductionBuilding?.OnUnSelected();
        }


        private bool IsCanChooseBuilding(ProductionBuilding building)
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();

            bool isTutorialCompleted = userProfileModel.TutorialStage > (int) TutorialType.BuyHouse;
            bool isItCheapestBuilding = building.Equals(GetCheapestBuilding());
            bool hasFewUnlockedBuildings = userProfileModel.CurrentCityProgress.UnlockedBuildings > 1;

            return isTutorialCompleted || isItCheapestBuilding || hasFewUnlockedBuildings;
        }

        private void TryShowOfflineIncome()
        {
            bool isPlayerWasOfflineEnough = TimeManager.Instance.MinutesOffline >= ConfigManager.Instance.Get<GameConfig>().MinMinutesForGetOfflineIncome;
            OfflineIncome offlineIncome = (OfflineIncome) ServiceLocator.Instance.Get<IncomeService>().GetIncome(IncomeType.Offline);
            
            if (!ServiceLocator.Instance.Get<IUserProfileModel>().IsFirstSession && isPlayerWasOfflineEnough && offlineIncome.HasIncome)
            {
                PopupManager.Instance.ShowPopup<OfflineEarningPopup>();
            }
        }


        private void TryShowTutorialOpenBuildingTutorial()
        {
            if(ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress.UnlockedBuildings > 0) return;
            
            TutorialPopup tutorialPopup = PopupManager.Instance.GetPopup<TutorialPopup>();
            tutorialPopup.InitializeTutorial(TutorialType.OpenHouse);
            tutorialPopup.Show();
        }


        private void TryToShowBuyTutorial(ProductionBuilding productionBuilding)
        {
            if(ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress.UnlockedBuildings > 0) return;
            
            TutorialPopup tutorialPopup = PopupManager.Instance.GetPopup<TutorialPopup>();
            tutorialPopup.InitializeTutorial(TutorialType.BuyHouse);
            tutorialPopup.Show();
        }


        private ProductionBuilding GetCheapestBuilding()
        {
            var buildings = FindObjectsOfType<ProductionBuilding>();
            ProductionBuilding cheapestBuilding = buildings
                .Where(building => building.CurrencyType == CurrencyType.Respect)
                .Aggregate((i1, i2) => i1.UnlockPrice < i2.UnlockPrice ? i1 : i2);

            return cheapestBuilding;
        }

        #endregion
    }
}
