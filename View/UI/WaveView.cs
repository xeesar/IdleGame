using System;
using DG.Tweening;
using Managers;
using Services;
using UnityEngine;

namespace View.UI
{
    public class WaveView : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private GameObject m_waveObject;

        [SerializeField] private float m_waveSpeed;
        [SerializeField] private AnimationCurve m_waveCurve;


        private void OnEnable()
        {
            UpdateManager.EventOnUpdate += OnUpdate;
        }


        private void OnDisable()
        {
            UpdateManager.EventOnUpdate -= OnUpdate;
        }


        private void OnUpdate(float deltaTime)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 screenPos = Input.mousePosition;
            
                ShowWave(screenPos);
            }
        }


        private void ShowWave(Vector2 position)
        {
            DOTween.Kill(this);
            m_waveObject.SetActive(true);

            Transform waveTransform = transform;
            
            waveTransform.position = position;
            waveTransform.localScale = Vector3.zero;
            waveTransform.DOScale(Vector3.one, m_waveSpeed).SetEase(m_waveCurve).OnComplete(() =>
            {
                m_waveObject.SetActive(false);
            }).SetId(this);
        }
    }
}

