using UnityEngine;

namespace Data.Configs
{
    [CreateAssetMenu(menuName = "Configs/Audio Config", fileName = "AudioConfig")]
    public class AudioConfig : BaseConfig
    {
        [SerializeField] private AudioClipData m_buttonClickAudioClipData;
        [SerializeField] private AudioClipData m_upgradeParameterAudioClipData;
        [SerializeField] private AudioClipData m_openPopupAudioClipData;
        [SerializeField] private AudioClipData m_collectIncomeAudioClipData;
        [SerializeField] private bool m_isStopPlayingImmideately;

        public AudioClipData ButtonClickAudioClipData => m_buttonClickAudioClipData;
        public AudioClipData UpgradeParameterAudioClipData => m_upgradeParameterAudioClipData;
        public AudioClipData OpenPopupAudioClipData => m_openPopupAudioClipData;
        public AudioClipData CollectIncomeAudioClipData => m_collectIncomeAudioClipData;

        public bool IsStopPlayingImmideately => m_isStopPlayingImmideately;
    }
}