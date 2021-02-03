using System;
using Data;
using Data.Configs;
using Extensions;
using Interfaces;
using Managers;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class GameOptionsView : MonoBehaviour
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private Button m_backButton;
        [SerializeField] private Button m_optionsButton;
        [SerializeField] private Button m_soundButton;
        [SerializeField] private Button m_musicButton;

        [SerializeField] private Animator m_animator;

        [Header("Options")] 
        [SerializeField] private Sprite m_musicOnSprite;
        [SerializeField] private Sprite m_musicOffSprite;
        [SerializeField] private Sprite m_soundOnSprite;
        [SerializeField] private Sprite m_soundOffSprite;

        private bool m_isOptionsShowed;

        private readonly int IS_SHOWN_ANIMATION_KEY = Animator.StringToHash("IsShown");
        
        #endregion



        #region Unity Lifecycle

        private void OnEnable()
        {
            m_backButton.onClick.AddListener(OnOptionsButtonClick);
            m_optionsButton.onClick.AddListener(OnOptionsButtonClick);
            m_soundButton.onClick.AddListener(OnSoundButtonClick);
            m_musicButton.onClick.AddListener(OnMusicButtonClick);
        }


        private void OnDisable()
        {
            m_backButton.onClick.RemoveListener(OnOptionsButtonClick);
            m_optionsButton.onClick.RemoveListener(OnOptionsButtonClick);
            m_soundButton.onClick.RemoveListener(OnSoundButtonClick);
            m_musicButton.onClick.RemoveListener(OnMusicButtonClick);
        }


        private void Start()
        {
            bool isSoundOn = ServiceLocator.Instance.Get<IUserProfileModel>().IsSoundOn;
            bool isMusicOn = ServiceLocator.Instance.Get<IUserProfileModel>().IsMusicOn;

            DisplaySoundStatus(isSoundOn);
            DisplayMusicStatus(isMusicOn);
        }

        #endregion



        #region Private Methods

        private void OnOptionsButtonClick()
        {
            PlayClickAudio();
            
            m_isOptionsShowed = !m_isOptionsShowed;
            
            m_animator.SetBool(IS_SHOWN_ANIMATION_KEY, m_isOptionsShowed);
        }


        private void OnSoundButtonClick()
        {
            PlayClickAudio();

            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();
            userProfileModel.IsSoundOn = !userProfileModel.IsSoundOn;

            DisplaySoundStatus(userProfileModel.IsSoundOn);
        }


        private void OnMusicButtonClick()
        {
            PlayClickAudio();

            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();
            userProfileModel.IsMusicOn = !userProfileModel.IsMusicOn;

            DisplayMusicStatus(userProfileModel.IsMusicOn);
        }


        private void DisplaySoundStatus(bool isSoundOn)
        {
            m_soundButton.image.sprite = isSoundOn ? m_soundOnSprite : m_soundOffSprite;
            AudioManager.Instance.SetSoundAudioActive(isSoundOn);
        }


        private void DisplayMusicStatus(bool isMusicOn)
        {
            m_musicButton.image.sprite = isMusicOn ? m_musicOnSprite : m_musicOffSprite;
            AudioManager.Instance.SetMusicAudioActive(isMusicOn);
        }


        private void PlayClickAudio()
        {
            AudioClipData clickAudio = ConfigManager.Instance.Get<AudioConfig>().OpenPopupAudioClipData;
            AudioManager.Instance.PlaySoundFx(clickAudio);
        }

        #endregion
    }
}