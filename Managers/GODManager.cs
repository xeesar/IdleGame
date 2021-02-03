using System;
using DG.Tweening;
using Extensions;
using Interfaces;
using Models.Screens;
using UnityEngine;

namespace Managers
{
    public class GODManager : Singleton<GODManager>, IManager
    {
        #region Fields

        [Header("Options")] 
        [SerializeField] private float m_timeForReset = 1f;
        [SerializeField] private float m_tapSecondsForCombination = 0;
        [SerializeField] private int m_tapsCountToCheat = 0;

        private float m_tapTime = 0;
        private int m_tapsCount;

        #endregion



        #region Unity Lifecycle

        private void OnEnable()
        {
            UpdateManager.EventOnUpdate += OnUpdate;
        }


        private void OnDisable()
        {
            UpdateManager.EventOnUpdate -= OnUpdate;
        }

        #endregion



        #region Public Methods

        public void Initialize()
        {
            
        }

        #endregion



        #region Private Methods

        private void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnCheatMenuActive();
            }
            
            if (Input.GetMouseButton(0) && m_tapTime < m_tapSecondsForCombination)
            {
                m_tapsCount = 0;
                m_tapTime += Time.deltaTime;
            }
            
            if (Input.GetMouseButtonUp(0) && m_tapTime < m_tapSecondsForCombination)
            {
                ResetCombination();
                return;
            }

            bool isCanCombination = m_tapTime >= m_tapSecondsForCombination;

            if (isCanCombination && Input.GetMouseButtonUp(0))
            {
                StartResetSequence();
            }
            else if (isCanCombination && Input.GetMouseButtonDown(0))
            {
                m_tapsCount++;
                DOTween.Kill(this);
            }
            else if (m_tapsCount >= m_tapsCountToCheat)
            {
                OnCheatMenuActive();
            }
        }
        
        
        private void StartResetSequence()
        {
            DOTween.Sequence().SetDelay(m_timeForReset).OnComplete(ResetCombination).SetId(this);
        }


        private void OnCheatMenuActive()
        {
            ScreensManager.Instance.ShowScreen<GODScreen>();
            ResetCombination();
        }


        private void ResetCombination()
        {
            m_tapTime = 0;
            m_tapsCount = 0;
        }

        #endregion
    }
}
