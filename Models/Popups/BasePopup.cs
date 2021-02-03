using System;
using Data;
using Data.Configs;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Models.Popups
{
    public abstract class BasePopup : MonoBehaviour
    {
        #region Fields
        
        [Header("Base Components")]
        [SerializeField] protected Button m_closeButton = null;
        [SerializeField] protected Button m_claimButton = null;
        
        #endregion
        
        
        
        #region Public Methods

        public virtual void Show()
        {
            AudioClipData audioClipData = ConfigManager.Instance.Get<AudioConfig>().OpenPopupAudioClipData;
            AudioManager.Instance.PlaySoundFx(audioClipData);
            
            gameObject.SetActive(true);
            
            m_closeButton?.onClick.AddListener(OnCloseButtonClick);
            m_claimButton?.onClick.AddListener(OnClaimButtonClick);
            
            Initialize();
        }


        public virtual void Hide()
        {
            gameObject.SetActive(false);
            
            m_closeButton?.onClick.RemoveListener(OnCloseButtonClick);
            m_claimButton?.onClick.RemoveListener(OnClaimButtonClick);
        }

        #endregion



        #region Private Methods

        protected abstract void Initialize();

        
        protected virtual void OnCloseButtonClick()
        {
            PopupManager.Instance.HideCurrentPopup();

            PlayButtonClickAudio();
        }


        protected virtual void OnClaimButtonClick()
        {
            PopupManager.Instance.HideCurrentPopup();
            
            PlayButtonClickAudio();
        }


        protected void PlayButtonClickAudio()
        {
            AudioClipData audioClipData = ConfigManager.Instance.Get<AudioConfig>().ButtonClickAudioClipData;
            AudioManager.Instance.PlaySoundFx(audioClipData);
        }

        #endregion
    }
}

