using Data;
using Data.Configs;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Models.Buildings;
using Models.DynamicParameters;
using Models.ParametersFactory;
using Models.Popups;
using Models.States.ProductionBuildingStates;
using Services;
using UnityEngine;
using View;

namespace Models.ProductionBuildings
{
    public abstract class ProductionBuilding : MonoBehaviour
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] protected ProductionBuildingView m_productionBuildingView;
        
        [Header("Options")] 
        [SerializeField] protected ProductionBuildingConfig m_buildingConfig = null;
        
        protected DynamicParameter m_income;
        protected DynamicParameter m_productionSpeed;

        private ProductionBuildingData m_buildingData;

        private ProductionBuildingState m_currentState;

        private Building m_building;
        
        #endregion



        #region Properties

        public int Id => m_buildingConfig.Id;
        
        public float ProductionProgress
        {
            get => m_buildingData.productionProgress;
            set => m_buildingData.productionProgress = value;
        }

        public float DrawProgress
        {
            get => m_buildingData.drawProgress;
            set => m_buildingData.drawProgress = value;
        }
        
        public bool IsAutoCollectingEnabled
        {
            get => m_buildingData.isAutoCollectingEnabled;
            private set => m_buildingData.isAutoCollectingEnabled = value;
        }

        public bool IsDrawn
        {
            get => m_buildingData.isDrawn;
            set => m_buildingData.isDrawn = value;
        }

        public bool IsUnlocked => m_buildingData.isUnlocked;

        public bool IsReadyToCollect => ProductionProgress >= ProductionSpeed.Value;

        public DynamicParameter ProductionSpeed => m_productionSpeed;

        public DynamicParameter Income => m_income;

        public float UnlockPrice => m_buildingConfig.UnlockPrice;
        
        public float AutoCollectPrice => m_buildingConfig.AutoCollectingPrice;

        public ProductionBuildingView ProductionBuildingView => m_productionBuildingView;

        public Sprite CompletedSprite => m_building.CompletedTileSprite;

        public int ArtistsCount
        {
            get => m_buildingData.artistsCount;
            set => m_buildingData.artistsCount = value;
        }

        public string Status => m_currentState.Status;
        
        public abstract CurrencyType CurrencyType { get; }
        
        #endregion



        #region Unity Lifecycle

        private void OnEnable()
        {
            UpdateManager.EventOnUpdate += OnUpdate;
            
            m_productionBuildingView.EventOnUnlockClick += UnlockBuilding;
            m_productionBuildingView.EventOnCollectClick += OnIncomeCollected;
        }


        private void OnDisable()
        {
            UpdateManager.EventOnUpdate -= OnUpdate;

            m_productionBuildingView.EventOnUnlockClick -= UnlockBuilding;
            m_productionBuildingView.EventOnCollectClick -= OnIncomeCollected;
        }

        private void Awake()
        {
            Initialize();
        }


        private void Start()
        {
            InitializeBuildingState();
        }

        #endregion



        #region Public Methods

        public void OnSelected()
        {
            SendClickEvent();
            
            if (m_currentState is LockedBuildingState)
            {
                m_productionBuildingView.SetLockedUIActive(true);
                return;
            }
            
            if (m_currentState is IncomeState && !IsAutoCollectingEnabled && IsReadyToCollect)
            {
                OnIncomeCollected();
                SendClaimIncomeEvent();
                
                return;
            }
            
            if (m_currentState is ProductionState || m_currentState is IncomeState)
            {
                var popup = PopupManager.Instance.GetPopup<ProductionBuildingMenu>();
                popup.EventAutoCollectUnlocked += UnlockAutoCollecting;
                popup.EventIncomeUpgraded += UpgradeIncome;
                popup.EventProductionSpeedUpgraded += UpgradeProductionSpeed;
                
                PopupManager.Instance.ShowPopup<ProductionBuildingMenu>();
            }


            if (m_currentState is DrawingBuildingState)
            {
                PopupManager.Instance.ShowPopup<ControlArtistPopup>();
            }
        }


        public void OnUnSelected()
        {
            if (m_currentState is LockedBuildingState)
            {
                m_productionBuildingView.SetLockedUIActive(false);
            }
            
            if (m_currentState is ProductionState || m_currentState is IncomeState)
            {
                var popup = PopupManager.Instance.GetPopup<ProductionBuildingMenu>();
                popup.EventAutoCollectUnlocked -= UnlockAutoCollecting;
                popup.EventIncomeUpgraded -= UpgradeIncome;
                popup.EventProductionSpeedUpgraded -= UpgradeProductionSpeed;
            }
        }


        public virtual void CollectIncome()
        {
            ProductionProgress = 0;

            m_productionBuildingView.DisplayCollectAnimation(() =>
            {
                ChangeState(new ProductionState());
            });
        }
        
        #endregion



        #region Private Methods
        
        protected virtual void UnlockBuilding()
        {
            SendUnlockEvent();

            AudioClipData audioClipData = ConfigManager.Instance.Get<AudioConfig>().ButtonClickAudioClipData;
            AudioManager.Instance.PlaySoundFx(audioClipData);
            
            PopupManager.Instance.ShowPopup<ControlArtistPopup>();
            
            ChangeState(new DrawingBuildingState());
            m_buildingData.isUnlocked = true;
        }
        
        
        protected virtual void UnlockAutoCollecting()
        {
            IsAutoCollectingEnabled = true;
            SendUnlockAutoCollectEvent();
        }

        protected virtual void UpgradeIncome()
        {
            m_income.Upgrade();
            m_buildingData.incomeParameterLevel = m_income.Level;
            m_buildingData.incomeValue = m_income.Value;
            SendUpgradeProductionBuildingEvent(DynamicParameterType.BuildingIncome.ToFriendlyName());
        }


        protected virtual void UpgradeProductionSpeed()
        {
            m_productionSpeed.Upgrade();
            m_buildingData.productionSpeedParameterLevel = m_productionSpeed.Level;
            m_buildingData.productionSpeedValue = m_productionSpeed.Value;
            SendUpgradeProductionBuildingEvent(DynamicParameterType.ProductionSpeed.ToFriendlyName());
        }


        private void OnUpdate(float deltaTime)
        {
            ProductionBuildingState newState = m_currentState?.HandleState(deltaTime);

            if (newState != null)
            {
                ChangeState(newState);
            }
        }

        private void Initialize()
        {
            CityProgressData cityProgressData = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress;
            m_buildingData = cityProgressData.GetProductionBuildingData(m_buildingConfig.Id);
            m_buildingData.currency = (int)CurrencyType;
            
            ParameterFactory parameterFactory = DynamicParametersManager.Instance.ParameterFactory;
            m_income = parameterFactory.Get(DynamicParameterType.BuildingIncome, m_buildingConfig.IncomeData, m_buildingData.incomeParameterLevel);
            m_productionSpeed = parameterFactory.Get(DynamicParameterType.ProductionSpeed, m_buildingConfig.ProductionSpeedData, m_buildingData.productionSpeedParameterLevel);
            m_buildingData.incomeValue = m_income.Value;
            m_buildingData.productionSpeedValue = m_productionSpeed.Value;

            string cityNumber = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCity;
            CityData cityData = ConfigManager.Instance.Get<CityConfig>().GetCityData(cityNumber);
            LevelData levelData = cityData.GetLevelData(Id);
            m_building = levelData.Building;

            Sprite buildingSprite = m_buildingData.isDrawn ? m_building.CompletedTileSprite : m_building.DefaultTileSprite;
            m_productionBuildingView.InitBuilding(buildingSprite);
        }


        private void InitializeBuildingState()
        {
            if (!m_buildingData.isUnlocked)
            {
                ChangeState(new LockedBuildingState());
            }
            else if (!IsDrawn)
            {
                ChangeState(new DrawingBuildingState());
            }
            else if(!IsReadyToCollect)
            {
                TeamManager.Instance.BusyArtistsCount -= ArtistsCount;
                ArtistsCount = 0;

                ProductionProgress += TimeManager.Instance.SecondsOffline;
                ChangeState(new ProductionState());
            }
            else
            {
                ChangeState(new IncomeState());
            }
        }


        private void ChangeState(ProductionBuildingState newState)
        {
            m_currentState?.OnStateExit();
            m_currentState = newState;
            m_currentState?.OnStateEnter(this);
        }


        private void OnIncomeCollected()
        {
            AudioClipData audioClipData = ConfigManager.Instance.Get<AudioConfig>().CollectIncomeAudioClipData;
            AudioManager.Instance.PlaySoundFx(audioClipData);
            
            CollectIncome();
        }


        private void SendClickEvent()
        {
            AnalyticEvent analyticEvent = new AnalyticEvent(StringConstants.AnalyticsEvents.HouseTapped);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.BuildingId, m_buildingConfig.Id);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.HouseType, m_currentState.Status);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.CurrencyType, CurrencyType.ToString().ToLower());
            
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
        }
        
        
        private void SendUnlockEvent()
        {
            AnalyticEvent analyticEvent = new AnalyticEvent(StringConstants.AnalyticsEvents.HouseUnlocked);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.BuildingId, m_buildingConfig.Id);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.CurrencyType, CurrencyType.ToString().ToLower());
            
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
        }


        private void SendClaimIncomeEvent()
        {
            AnalyticEvent analyticEvent = new AnalyticEvent(StringConstants.AnalyticsEvents.IncomeCollected);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.BuildingId, m_buildingConfig.Id);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.CurrencyType, CurrencyType.ToString().ToLower());

            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
        }
        
        
        private void SendUnlockAutoCollectEvent()
        {
            AnalyticEvent analyticEvent = new AnalyticEvent(StringConstants.AnalyticsEvents.UnlockAutoCollect);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.BuildingId, m_buildingConfig.Id);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.CurrencyType, CurrencyType.ToString().ToLower());

            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
        }


        private void SendUpgradeProductionBuildingEvent(string parameterType)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent(StringConstants.AnalyticsEvents.UpgradeProductionBuilding);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.BuildingId, m_buildingConfig.Id);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.CurrencyType, CurrencyType.ToString().ToLower());
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.ParameterType, parameterType);

            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
        }
        
        #endregion
    }
}

