using System.Collections;
using Data.Configs;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Models.Screens
{
    public class LoadingScreen : BaseScreen
    {
        #region Fields
        
        [Header("Components")] 
        [SerializeField] private Image m_fillingImage = null;

        [Header("Options")]
        [SerializeField] private AnimationCurve m_fillingCurve = null;

        [SerializeField] private float m_delayBeforeStart = 1f;
        [SerializeField] private float m_fillingDuration = 1f;
        [SerializeField] private float m_devastationDuration = 0.5f;

        private int m_changeMainSceneCount = 0;
        
        private SceneType m_loadSceneType;
        
        #endregion
        
        
        
        #region Public Methods

        public override void Show()
        {
            base.Show();

            if (m_loadSceneType == SceneType.Core || m_loadSceneType == SceneType.CoreView)
            {
                m_changeMainSceneCount++;
            }
            
            if (m_changeMainSceneCount >= ConfigManager.Instance.Get<GameConfig>().ChangeMainSceneCountForAd)
            {
                AdManager.Instance.ShowInterstitial("loading_screen");
                m_changeMainSceneCount = 0;
            }
            
            StartFillingAnimation();
        }


        public void Initialize(SceneType loadSceneType)
        {
            m_loadSceneType = loadSceneType;
        }

        #endregion



        #region Private Methods

        private void StartFillingAnimation()
        {
            DOTween.Kill(this);
            
            m_fillingImage.DOFillAmount(1f, m_fillingDuration).SetDelay(m_delayBeforeStart).SetEase(m_fillingCurve).SetId(this)
                .OnComplete(() => StartCoroutine(UnloadActiveScene()));
        }


        private void StartDevastationAnimation()
        {
            m_fillingImage.DOFillAmount(0, m_devastationDuration).SetEase(m_fillingCurve).SetId(this)
                .OnComplete(OnLoaded);
        }


        private void OnLoaded()
        {
            StartCoroutine(LoadScene((int) m_loadSceneType));
        }


        private IEnumerator LoadScene(int index)
        {
            yield return SceneManager.LoadSceneAsync(index);
            
            Hide();
        }


        private IEnumerator UnloadActiveScene()
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            StartDevastationAnimation();
        }
        
        #endregion
    }
}
