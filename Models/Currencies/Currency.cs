using System;
using Enums;
using UnityEngine;

namespace Models.Currencies
{
    public abstract class Currency
    {
        #region Fields

        public event Action<float> EventCurrencyValueChanged;

        private float m_value = 0;

        #endregion
        
        
        
        #region Properties
        
        public float Value
        {
            get => m_value;
            set
            {
                m_value = value > 0 ? value : 0;
                EventCurrencyValueChanged?.Invoke(m_value);
            }
        }
        
        public abstract CurrencyType CurrencyType { get; }
        
        public abstract string Name { get; }

        #endregion


        
        #region Public Methods

        public Currency(float amount)
        {
            Value = amount;
        }

        #endregion
    }
}

