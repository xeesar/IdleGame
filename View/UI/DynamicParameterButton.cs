using System;
using System.Collections.Generic;
using Data;
using Data.Configs;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Models.Currencies;
using Models.DynamicParameters;
using Models.UnlockService;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.UI.UIInteractable;

namespace View.UI
{
    public class DynamicParameterButton : MonoBehaviour
    {
        #region Fields

        [Header("Components")]
        [SerializeField] private Button m_button = null;
        [SerializeField] private TextMeshProUGUI m_valueText = null;
        [SerializeField] private TextMeshProUGUI m_priceText = null;
        [SerializeField] private GameObject m_defaultPrice = null;
        [SerializeField] private GameObject m_adsPrice = null;
        
        [SerializeField] private List<UIInteractableView> m_uiInteractableElements = new List<UIInteractableView>();
        
        [Header("Options")]
        [SerializeField] private DynamicParameterType m_dynamicParameterType = DynamicParameterType.None;
        
        private DynamicParameter m_dynamicParameter = null;

        private int m_upgradesCount = 0;

        #endregion


        #region Properties

        private bool IsAdUpgrade
        {
            get
            {
                bool isUpgradesEnough = m_upgradesCount >= ConfigManager.Instance.Get<GameConfig>().UpgradesCountForAdsUpgrade;
                bool hasMoney = ServiceLocator.Instance.Get<IUserProfileModel>().GetCurrency(CurrencyType.Respect).Value >= m_dynamicParameter.Price;

                return isUpgradesEnough && !hasMoney && !m_dynamicParameter.IsMaxLevel;
            }
        }

        #endregion


        
        #region Unity Lifecycle

        private void Awake()
        {
            m_dynamicParameter = DynamicParametersManager.Instance.Get(m_dynamicParameterType);
        }
        

        private void Start()
        {
            ServiceLocator.Instance.Get<IUserProfileModel>().GetCurrency(CurrencyType.Respect).EventCurrencyValueChanged += OnCurrencyValueChanged;
            m_button.onClick.AddListener(UpgradeButtonClick);
            m_dynamicParameter.EventOnUpgraded += DisplayButtonInfo;
            
            DisplayButtonInfo();
        }

        #endregion
        
        

        #region Private Methods

        private void OnCurrencyValueChanged(float respect)
        {
            DisplayButtonPrice();
            DisplayButtonActivity(respect);
        }
        
        
        private void UpgradeButtonClick()
        {
            if(m_dynamicParameter.IsMaxLevel) return;
            
            if (IsAdUpgrade)
            {
                string parameterName = m_dynamicParameter.ParameterType.ToFriendlyName();
                
                BuyService buyService = new AdBuyService(string.Format(StringConstants.Formats.AdUpgradeParameterFormat, parameterName));
                buyService.TryToBuy(0, () =>
                {
                    m_upgradesCount = 0;
                    m_dynamicParameter.Upgrade();
                });
            }
            else
            {
                BuyService buyService = new RespectBuyService();
                buyService.TryToBuy((int)m_dynamicParameter.Price, () =>
                {
                    m_upgradesCount++;
                    m_dynamicParameter.Upgrade();
                });
            }
            
            AudioClipData audioClipData = ConfigManager.Instance.Get<AudioConfig>().UpgradeParameterAudioClipData;
            AudioManager.Instance.PlaySoundFx(audioClipData);
        }


        private void DisplayButtonInfo()
        {
            DisplayButtonValue();
            DisplayButtonPrice();
            DisplayButtonActivity(ServiceLocator.Instance.Get<IUserProfileModel>().GetCurrency(CurrencyType.Respect).Value);
        }


        private void DisplayButtonValue()
        {
            if(m_valueText == null) return;
            
            m_valueText.text = string.Format(m_dynamicParameter.ValueFormat, m_dynamicParameter.Value.AbbreviateNumber());
        }
        

        private void DisplayButtonPrice()
        {
            m_adsPrice.SetActive(IsAdUpgrade);
            m_defaultPrice.SetActive(!IsAdUpgrade);
            m_priceText.text = m_dynamicParameter.IsMaxLevel ? StringConstants.UIText.MaxLevel : m_dynamicParameter.Price.AbbreviateNumber();
        }

        
        private void DisplayButtonActivity(float respect)
        {
            bool isInteractable = (respect >= m_dynamicParameter.Price && !m_dynamicParameter.IsMaxLevel) || IsAdUpgrade;

            for (int i = 0; i < m_uiInteractableElements.Count; i++)
            {
                UIInteractableView interactableView = m_uiInteractableElements[i];
                if (interactableView is ButtonInteractable && m_dynamicParameter.IsMaxLevel)
                {
                    interactableView.SetInteractable(false);
                    continue;
                }
                
                interactableView.SetInteractable(isInteractable);
            }
        }
        
        #endregion
    }
}
