using UnityEngine;

namespace Data.Configs
{
    [CreateAssetMenu(menuName = "Configs/Game Config", fileName = "GameConfig")]
    public class GameConfig : BaseConfig
    {
        #region Fields

        [Header("Start Game Parameters")] 
        [SerializeField] private int m_startDollarsAmount = 0;
        [SerializeField] private int m_startRespectAmmount = 7;
        
        
        [Header("End Game Parameters")] 
        [SerializeField] private float m_delayBeforeEndGame = 2f;
        [SerializeField] private float m_scaffoldingHideDuration = 3f;
        [SerializeField] private float m_scaffoldingYOffset = 3f;

        [Header("Offline Game Parameters")] 
        [SerializeField] private int m_minMinutesForGetOfflineIncome;
        [SerializeField] private int m_maxOfflineIncomeMinutes;

        [Header("Ad Config")] 
        [SerializeField] private int m_upgradesCountForAdsUpgrade = 3;
        [SerializeField] private int m_changeMainSceneCountForAd = 3;

        #endregion



        #region Properties

        public float DelayBeforeEndGame => m_delayBeforeEndGame;

        public float ScaffoldingHideDuration => m_scaffoldingHideDuration;

        public float ScaffoldingYOffset => m_scaffoldingYOffset;

        public int StartDollars => m_startDollarsAmount;

        public int StartRespect => m_startRespectAmmount;

        public int MinMinutesForGetOfflineIncome => m_minMinutesForGetOfflineIncome;

        public int MaxOfflineIncomeMinutes => m_maxOfflineIncomeMinutes;

        public int UpgradesCountForAdsUpgrade => m_upgradesCountForAdsUpgrade;

        public int ChangeMainSceneCountForAd => m_changeMainSceneCountForAd;

        #endregion
    }
}