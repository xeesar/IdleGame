using System;
using Data;
using Data.Configs;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Models.ProductionBuildings;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.UI;

namespace Models.Popups
{
    public class ProductionBuildingMenu : ProductionBuildingPopup
    {
        #region Fields

        public event Action EventAutoCollectUnlocked;
        public event Action EventIncomeUpgraded;
        public event Action EventProductionSpeedUpgraded;
        
        [Header("Auto Collect Components")] 
        [SerializeField] private Button m_autoCollectBuyButton;
        [SerializeField] private TextMeshProUGUI m_priceAutoCollectText;
        [SerializeField] private Image m_autoCollectWalletImage;

        [Header("Income Components")] 
        [SerializeField] private DynamicBuildingButton m_incomeDynamicButton;

        [Header("Production Speed Components")]
        [SerializeField] private DynamicBuildingButton m_productionDynamicButton;

        [Header("Icons")] 
        [SerializeField] private Sprite m_respectSprite;
        [SerializeField] private Sprite m_dollarsSprite;

        #endregion
        
        
        #region Properties

        protected override SceneType SceneType => SceneType.CoreView;

        #endregion



        #region Public Methods

        public override void Show()
        {
            base.Show();
            
            ServiceLocator.Instance.Get<IUserProfileModel>().GetCurrency(m_targetBuilding.CurrencyType).EventCurrencyValueChanged += DisplayButtonsInfo;

            Sprite walletSprite = m_targetBuilding.CurrencyType == CurrencyType.Respect
                ? m_respectSprite
                : m_dollarsSprite;
            string incomeFormat = m_targetBuilding.CurrencyType == CurrencyType.Respect
                ? StringConstants.Formats.IncomeRespectFormat
                : StringConstants.Formats.IncomeDollarsFormat;
            
            m_autoCollectBuyButton.onClick.AddListener(OnAutoCollectionUnlocked);

            m_incomeDynamicButton.Initialize(m_targetBuilding.CurrencyType, m_targetBuilding.Income, walletSprite, incomeFormat);
            m_productionDynamicButton.Initialize(m_targetBuilding.CurrencyType, m_targetBuilding.ProductionSpeed, walletSprite, StringConstants.Formats.ProductionSpeedFormat);

            m_incomeDynamicButton.EventUpgraded += OnIncomeUpgraded;
            m_productionDynamicButton.EventUpgraded += OnProductionSpeedUpgraded;
            
            DisplayButtonsInfo(ServiceLocator.Instance.Get<IUserProfileModel>().GetCurrency(m_targetBuilding.CurrencyType).Value);

        }


        public override void Hide()
        {
            base.Hide();

            ServiceLocator.Instance.Get<IUserProfileModel>().GetCurrency(m_targetBuilding.CurrencyType).EventCurrencyValueChanged -= DisplayButtonsInfo;
            m_autoCollectBuyButton.onClick.RemoveListener(OnAutoCollectionUnlocked);
            m_incomeDynamicButton.EventUpgraded -= OnIncomeUpgraded;
            m_productionDynamicButton.EventUpgraded -= OnProductionSpeedUpgraded;
            
            EventAutoCollectUnlocked = null;
            EventIncomeUpgraded = null;
            EventProductionSpeedUpgraded = null;
        }

        #endregion
        
        
        
        #region Private Methods
        
        private void DisplayButtonsInfo(float currency)
        {
            DisplayAutoCollectInfo(currency);
            m_incomeDynamicButton.DisplayInfo(currency);
            m_productionDynamicButton.DisplayInfo(currency);
        }

        
        private void DisplayAutoCollectInfo(float currency)
        {
            float price = m_targetBuilding.AutoCollectPrice;
            bool isMoneyEnough = currency >= price;
            bool isAutoUnlockBought = m_targetBuilding.IsAutoCollectingEnabled;
            
            m_priceAutoCollectText.text = isAutoUnlockBought ? string.Empty : price.AbbreviateNumber();
            m_autoCollectBuyButton.interactable = !isAutoUnlockBought && isMoneyEnough;
            m_autoCollectWalletImage.sprite = m_targetBuilding.CurrencyType == CurrencyType.Dollar
                ? m_dollarsSprite
                : m_respectSprite;
        }
        
        
        private void DisplayIncomeInfo(float currency)
        {
//            float price = m_targetBuilding.Income.Price;
//
//            bool isMoneyEnough = currency >= price;
//            bool isMaxLevel = m_targetBuilding.Income.IsMaxLevel;
//
//            m_priceIncomeText.text = isMaxLevel ? StringConstants.UIText.MaxLevel : price.AbbreviateNumber();
//            m_incomeBuyButton.interactable = isMoneyEnough && !isMaxLevel;
//            
//            if (m_targetBuilding is RespectProductionBuilding)
//            {
//                m_valueIncomeText.text = string.Format(StringConstants.Formats.IncomeRespectFormat, m_targetBuilding.Income.Value.AbbreviateNumber());
//                m_incomeWalletImage.sprite = m_respectSprite;
//            }
//            else if (m_targetBuilding is DollarsProductionBuilding)
//            {
//                m_valueIncomeText.text = string.Format(StringConstants.Formats.IncomeDollarsFormat, m_targetBuilding.Income.Value.AbbreviateNumber());
//                m_incomeWalletImage.sprite = m_dollarsSprite;
//            }
//            
//            m_incomeWalletImage.gameObject.SetActive(!isMaxLevel);
        }
        
        
        private void DisplayProductionSpeedInfo(float currency)
        {
//            float price = m_targetBuilding.ProductionSpeed.Price;
//            
//            bool isMoneyEnough = currency >= price;
//            bool isMaxLevel = m_targetBuilding.ProductionSpeed.IsMaxLevel;
//            
//            m_priceProductionSpeedText.text = isMaxLevel ? StringConstants.UIText.MaxLevel : price.AbbreviateNumber();
//            m_productionSpeedBuyButton.interactable = isMoneyEnough && !isMaxLevel;
//
//            m_valueProductionSpeedText.text = string.Format(StringConstants.Formats.ProductionSpeedFormat, m_targetBuilding.ProductionSpeed.Value.AbbreviateNumber());
//            m_productionSpeedWalletImage.sprite = m_targetBuilding.CurrencyType == CurrencyType.Dollar
//                ? m_dollarsSprite
//                : m_respectSprite;
//            
//            m_productionSpeedWalletImage.gameObject.SetActive(!isMaxLevel);
        }


        private void OnAutoCollectionUnlocked()
        {
            PlayUpgradeAudio();
            EventAutoCollectUnlocked?.Invoke();
        }
        

        private void OnIncomeUpgraded()
        {
            PlayUpgradeAudio();
            EventIncomeUpgraded?.Invoke();
        }


        private void OnProductionSpeedUpgraded()
        {
            PlayUpgradeAudio();
            EventProductionSpeedUpgraded?.Invoke();
        }


        private void PlayUpgradeAudio()
        {
            AudioClipData audioClipData = ConfigManager.Instance.Get<AudioConfig>().UpgradeParameterAudioClipData;
            AudioManager.Instance.PlaySoundFx(audioClipData);
        }
 
        #endregion
    }
}
