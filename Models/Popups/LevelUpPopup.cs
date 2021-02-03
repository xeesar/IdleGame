using System;
using Data;
using Enums;
using Extensions;
using Managers;
using Services;
using TMPro;
using UnityEngine;

namespace Models.Popups
{
    public class LevelUpPopup : BasePopup
    {
        #region Fields

        public event Action EventLevelUpCompleted;
        
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI m_respectText = null;
        
        #endregion


        
        #region Private Method

        protected override void Initialize()
        {
            DisplayRespectText();
        }


        protected override void OnClaimButtonClick()
        {
            base.OnClaimButtonClick();
            
//            ServiceLocator.Instance.Get<IncomeService>().GiveIncomeFor(IncomeType.PerLevelUp);
//            
//            DynamicParametersManager.Instance.Get(DynamicParameterType.ArtistsCount).Upgrade();
//            DynamicParametersManager.Instance.Get(DynamicParameterType.RespectIncomePerLevelUp).Upgrade();
//            DynamicParametersManager.Instance.Get(DynamicParameterType.ExperienceForLevelUp).Upgrade();

            PopupManager.Instance.HideCurrentPopup();
            EventLevelUpCompleted?.Invoke();
        }
        
        
        private void DisplayRespectText()
        {
//            float reward = DynamicParametersManager.Instance.Get(DynamicParameterType.RespectIncomePerLevelUp).Value;
//            string text = string.Format(StringConstants.Formats.PlusIncomeFormat, reward.AbbreviateNumber());
            
//            m_respectText.text = text;
        }

        #endregion
    }
}
