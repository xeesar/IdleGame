using Enums;
using Extensions;
using Interfaces;
using Models.Currencies;
using Services;
using TMPro;
using UnityEngine;

namespace View.UI
{
    public class CurrencyView : MonoBehaviour
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private TextMeshProUGUI m_currencyText = null;

        [Header("Components")] 
        [SerializeField] private CurrencyType m_currencyType = CurrencyType.None;
        
        private Currency m_currency = null;

        #endregion



        #region Unity Lifecycle

        private void OnEnable()
        {
            m_currency = ServiceLocator.Instance.Get<IUserProfileModel>().GetCurrency(m_currencyType);
            
            m_currency.EventCurrencyValueChanged += DisplayCurrency;
            
            DisplayCurrency(m_currency.Value);
        }


        private void OnDisable()
        {
            m_currency.EventCurrencyValueChanged -= DisplayCurrency;
        }

        #endregion



        #region Private Methods

        private void DisplayCurrency(float value)
        {
            m_currencyText.text = value.AbbreviateNumber();
        }

        #endregion
    }
}

