using System.Collections.Generic;
using Data;
using Data.Configs;
using Enums;
using Interfaces;
using Managers;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Models.Screens
{
    public class GODScreen : BaseScreen
    {
        #region Fields

        [Header("Components")]
        [SerializeField] private TMP_Dropdown m_dropDown = null;
        [SerializeField] private Button m_addArtistBtn = null;
        [SerializeField] private Button m_addRespectBtn = null;
        [SerializeField] private Button m_addDollarsBtn = null;
        [SerializeField] private Button m_closeButton = null;

        [SerializeField] private Button m_speedButton = null;
        [SerializeField] private TMP_InputField m_speedField = null;
        
        [SerializeField] private Button m_drawButton = null;
        [SerializeField] private TMP_InputField m_drawField = null;
        
        [SerializeField] private Button m_capacityButton = null;
        [SerializeField] private TMP_InputField m_capacityField = null;

        [Header("Options")] 
        [SerializeField] private int m_walleteToAdd = 100000;
        
        #endregion



        #region Unity Lifecycle

        private void Start()
        {
            Initialize();

            m_dropDown.onValueChanged.AddListener(OnDropDownValueChanged);
            m_addArtistBtn.onClick.AddListener(OnUpgradeArtists);
            m_addRespectBtn.onClick.AddListener(OnAddRespect);
            m_addDollarsBtn.onClick.AddListener(OnAddDollars);
            m_closeButton.onClick.AddListener(() =>
            {
                ScreensManager.Instance.ShowScreen<GameScreen>();
            });
            
            m_speedButton.onClick.AddListener(OnSetSpeed);
            m_drawButton.onClick.AddListener(OnSetDraw);
            m_capacityButton.onClick.AddListener(OnSetCapacity);
        }

        #endregion

        

        #region Private Methods

        private void Initialize()
        {
            InitializeDropDown();
        }
        
        
        private void InitializeDropDown()
        {
            m_dropDown.ClearOptions();

            var cityConfig = ConfigManager.Instance.Get<CityConfig>();
            int levels = cityConfig.Cities[0].LevelsCount;

            List<string> options = new List<string>();
            
            for (int i = 0; i < levels; i++)
            {
                LevelData levelData = cityConfig.Cities[0].GetLevelData(i);
                options.Add(levelData.name);
            }
            
            m_dropDown.AddOptions(options);
        }


        private void OnDropDownValueChanged(int levelID)
        {
//            Hide();
//            
//            LoadingScreen loadingScreen = ScreensManager.Instance.GetScreen<LoadingScreen>();
//            loadingScreen.Initialize(SceneType.Core);
//            loadingScreen.Show();
        }


        private void OnUpgradeArtists()
        {
            DynamicParametersManager.Instance.Get(DynamicParameterType.ArtistsCount).Upgrade();
        }


        private void OnAddRespect()
        {
            ServiceLocator.Instance.Get<IUserProfileModel>().UpdateCurrency(CurrencyType.Respect, m_walleteToAdd);
        }

        
        private void OnAddDollars()
        {
            ServiceLocator.Instance.Get<IUserProfileModel>().UpdateCurrency(CurrencyType.Dollar, m_walleteToAdd);
        }
        

        private void OnSetSpeed()
        {
            int level = int.Parse(m_speedField.text);
            DynamicParametersManager.Instance.Get(DynamicParameterType.RunningSpeed).SetLevel(level);
        }
        
        
        private void OnSetDraw()
        {
            int level = int.Parse(m_drawField.text);
            DynamicParametersManager.Instance.Get(DynamicParameterType.DrawingSpeed).SetLevel(level);
        }
        
        
        private void OnSetCapacity()
        {
            int level = int.Parse(m_capacityField.text);
            DynamicParametersManager.Instance.Get(DynamicParameterType.SprayBottleCapacity).SetLevel(level);
        }

        #endregion
    }
}

