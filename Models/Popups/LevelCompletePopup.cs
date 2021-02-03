using Data;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Models.Screens;
using Services;
using TMPro;
using UnityEngine;

namespace Models.Popups
{
    public class LevelCompletePopup : BasePopup
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private TextMeshProUGUI m_dollarsText = null;
        [SerializeField] private TextMeshProUGUI m_respectText = null;

        #endregion



        #region Private Methods

        protected override void Initialize()
        {
            DisplayDollarsText();
            DisplayRespectText();
        }


        protected override void OnClaimButtonClick()
        {
            base.OnClaimButtonClick();

            GiveReward();
            
            LoadingScreen loadingScreen = ScreensManager.Instance.GetScreen<LoadingScreen>();
            loadingScreen.Initialize(SceneType.Core);
            loadingScreen.Show();
        }


        protected override void OnCloseButtonClick()
        {
            base.OnCloseButtonClick();

            GiveReward();
        }


        private void GiveReward()
        {
//            ServiceLocator.Instance.Get<IncomeService>().GiveIncomeFor(IncomeType.PerGraffiti);
//
//            DynamicParametersManager.Instance.Get(DynamicParameterType.RespectIncomePerLevel).Upgrade();
//            DynamicParametersManager.Instance.Get(DynamicParameterType.DollarsIncomePerLevel).Upgrade();
        }

        private void DisplayDollarsText()
        {
//            float reward = DynamicParametersManager.Instance.Get(DynamicParameterType.DollarsIncomePerLevel).Value;
//
//            m_dollarsText.text = reward.AbbreviateNumber();
        }


        private void DisplayRespectText()
        {
//            float reward = DynamicParametersManager.Instance.Get(DynamicParameterType.RespectIncomePerLevel).Value;
//
//            m_respectText.text = reward.AbbreviateNumber();
        }

        #endregion
    }
}
