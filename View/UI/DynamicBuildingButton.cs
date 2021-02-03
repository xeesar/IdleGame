using System;
using Data.Configs;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Models.DynamicParameters;
using Models.UnlockService;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class DynamicBuildingButton : MonoBehaviour
    {
        public event Action EventUpgraded;

        [Header("Components")] 
        [SerializeField] private Button m_buyButton = null;
        [SerializeField] private TextMeshProUGUI m_priceText = null;
        [SerializeField] private TextMeshProUGUI m_valueText = null;
        [SerializeField] private Image m_walletImage = null;
        [SerializeField] private GameObject m_walletPrice = null;
        [SerializeField] private GameObject m_adPrice = null;

        private CurrencyType m_currencyType = CurrencyType.None;
        private DynamicParameter m_dynamicParameter = null;
        private string m_valueFormat = "";
        
        private int m_upgradesCount = 0;
        
        
        private bool IsAdUpgrade
        {
            get
            {
                bool isUpgradesEnough = m_upgradesCount >= ConfigManager.Instance.Get<GameConfig>().UpgradesCountForAdsUpgrade;
                bool hasMoney = ServiceLocator.Instance.Get<IUserProfileModel>().GetCurrency(m_currencyType).Value >= m_dynamicParameter.Price;

                return isUpgradesEnough && !hasMoney && !m_dynamicParameter.IsMaxLevel;
            }
        }

        
        private void OnEnable()
        {
            m_buyButton.onClick.AddListener(OnBuyButtonClick);
        }


        private void OnDisable()
        {
            m_buyButton.onClick.RemoveListener(OnBuyButtonClick);
        }


        public void Initialize(CurrencyType currencyType, DynamicParameter dynamicParameter, Sprite walletSprite, string format)
        {
            m_upgradesCount = 0;

            m_currencyType = currencyType;
            m_dynamicParameter = dynamicParameter;

            m_walletImage.sprite = walletSprite;

            m_valueFormat = format;
        }


        public void DisplayInfo(float currency)
        {
            float price = m_dynamicParameter.Price;

            bool isMoneyEnough = currency >= price;
            bool isMaxLevel = m_dynamicParameter.IsMaxLevel;

            m_priceText.text = isMaxLevel ? StringConstants.UIText.MaxLevel : price.AbbreviateNumber();
            m_buyButton.interactable = (isMoneyEnough && !isMaxLevel) || IsAdUpgrade;
            
            m_valueText.text = string.Format(m_valueFormat, m_dynamicParameter.Value.AbbreviateNumber());
            
            m_walletImage.gameObject.SetActive(!isMaxLevel);
            
            m_adPrice.SetActive(IsAdUpgrade);
            m_walletPrice.SetActive(!IsAdUpgrade);
        }


        private void OnBuyButtonClick()
        {
            if (IsAdUpgrade)
            {
                string parameterName = m_dynamicParameter.ParameterType.ToFriendlyName();
                
                BuyService buyService = new AdBuyService(string.Format(StringConstants.Formats.AdUpgradeParameterFormat, parameterName));
                buyService.TryToBuy(0, () =>
                {
                    m_upgradesCount = 0;
                    DisplayInfo(ServiceLocator.Instance.Get<IUserProfileModel>().GetCurrency(m_currencyType).Value);
                    EventUpgraded?.Invoke();
                });
            }
            else
            {

                BuyService buyService;

                if (m_currencyType == CurrencyType.Dollar)
                {
                    buyService = new DollarsBuyService();
                }
                else
                {
                    buyService = new RespectBuyService();
                }
                
                buyService.TryToBuy(m_dynamicParameter.Price, () =>
                {
                    m_upgradesCount++;
                    DisplayInfo(ServiceLocator.Instance.Get<IUserProfileModel>().GetCurrency(m_currencyType).Value);
                    EventUpgraded?.Invoke();
                });
            }
        }
    }
}
