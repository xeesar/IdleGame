using Data;
using Data.Configs;
using Extensions;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class AudioManager : Singleton<AudioManager>, IManager
    {
        [Header("Components")] 
        [SerializeField] private AudioSource m_backgroundAudioSource;
        [SerializeField] private AudioSource m_soundFxAudioSource;

        private AudioConfig m_audioConfig;
        
        public void Initialize()
        {
            m_audioConfig = ConfigManager.Instance.Get<AudioConfig>();
        }
        

        public void PlayBackgroundAudio(AudioClipData audioClipData)
        {
            m_backgroundAudioSource.clip = audioClipData.Clip;
            m_backgroundAudioSource.volume = audioClipData.Volume;
            
            m_backgroundAudioSource.Play();
        }


        public void PlaySoundFx(AudioClipData audioClipData)
        {
            if(m_soundFxAudioSource.isPlaying && !m_audioConfig.IsStopPlayingImmideately) return;
            
            m_soundFxAudioSource.clip = audioClipData.Clip;
            m_soundFxAudioSource.volume = audioClipData.Volume;
            
            m_soundFxAudioSource.Play();
        }


        public void SetSoundAudioActive(bool isActive)
        {
            m_soundFxAudioSource.mute = !isActive;
        }


        public void SetMusicAudioActive(bool isActive)
        {
            m_backgroundAudioSource.mute = !isActive;
        }
    }
}
