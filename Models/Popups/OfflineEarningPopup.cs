using System;
using Data;
using Data.Configs;
using Enums;
using Extensions;
using Managers;
using Models.Income;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Models.Popups
{
    public class OfflineEarningPopup : BasePopup
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private Button m_claimX2Button;
        [SerializeField] private TextMeshProUGUI m_infoText = null;
        [SerializeField] private TextMeshProUGUI m_respectText = null;
        [SerializeField] private TextMeshProUGUI m_dollarsText = null;


        #endregion


        #region Unity Lifecycle

        private void OnEnable()
        {
            m_claimX2Button.onClick.AddListener(OnClaimX2ButtonClick);
        }


        private void OnDisable()
        {
            m_claimX2Button.onClick.RemoveListener(OnClaimX2ButtonClick);
        }

        #endregion


        
        #region Private Methods

        protected override void Initialize()
        {
            DisplayIncomeText();
        }


        protected override void OnCloseButtonClick()
        {
            base.OnCloseButtonClick();
            
            ServiceLocator.Instance.Get<IncomeService>().GiveIncomeFor(IncomeType.Offline);
        }


        protected override void OnClaimButtonClick()
        {
            PopupManager.Instance.HideCurrentPopup();
            AudioClipData audioClipData = ConfigManager.Instance.Get<AudioConfig>().CollectIncomeAudioClipData;
            AudioManager.Instance.PlaySoundFx(audioClipData);
            
            ServiceLocator.Instance.Get<IncomeService>().GiveIncomeFor(IncomeType.Offline);
        }


        private void OnClaimX2ButtonClick()
        {
            AdManager.Instance.ShowRewarded("offline_x2", () =>
            {
                PopupManager.Instance.HideCurrentPopup();
                AudioClipData audioClipData = ConfigManager.Instance.Get<AudioConfig>().CollectIncomeAudioClipData;
                AudioManager.Instance.PlaySoundFx(audioClipData);

                ServiceLocator.Instance.Get<IncomeService>().GiveIncomeFor(IncomeType.Offline, 2f);
            }, null);
        }


        private void DisplayIncomeText()
        {
            int maxOfflineMinutes = ConfigManager.Instance.Get<GameConfig>().MaxOfflineIncomeMinutes;
            int maxHours = Mathf.RoundToInt(maxOfflineMinutes / 60f);

            TimeSpan offlineTime = TimeManager.Instance.OfflineTime;
            if (offlineTime.TotalMinutes > maxOfflineMinutes)
            {
                offlineTime = new TimeSpan(maxHours, 0, 0);
            }
            
            string format = @"hh\:mm\:ss";
            m_infoText.text = string.Format(StringConstants.Formats.OfflineIncomeFormat, offlineTime.ToString(format), maxHours);
            
            OfflineIncome offlineIncome = (OfflineIncome) ServiceLocator.Instance.Get<IncomeService>().GetIncome(IncomeType.Offline);
            float respectOffline = offlineIncome.RespectIncome;
            float dollarsOffline = offlineIncome.DollarsIncome;

            m_respectText.text = string.Format(StringConstants.Formats.PlusIncomeFormat, respectOffline.AbbreviateNumber());
            m_dollarsText.text = string.Format(StringConstants.Formats.PlusIncomeFormat, dollarsOffline.AbbreviateNumber());
        }

        #endregion
    }
}
