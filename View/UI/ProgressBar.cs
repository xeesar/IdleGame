using System;
using DG.Tweening;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class ProgressBar : MonoBehaviour
    {
        #region Fields

        public event Action EventProgressCompleted;
        
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI m_progressText = null;
        [SerializeField] private Image m_progressFilling = null;

        [Header("Options")] 
        [SerializeField] private float m_fillingTime = 1f;
        [SerializeField] private AnimationCurve m_fillingCurve = null;
        
        private int m_currentValue = 0;
        
        #endregion



        #region Properties

        public int MinValue { get; set; }
        
        public int MaxValue { get; set; }

        public int CurrentValue
        {
            get => m_currentValue;
            set =>  m_currentValue = Mathf.Clamp(value, MinValue, MaxValue);
        }

        #endregion



        #region Public Methods

        public void ResetProgress()
        {
            CurrentValue = 0;
            
            DisplayProgressText();
            m_progressFilling.fillAmount = (float)CurrentValue / MaxValue;
        }
        
        
        public void DisplayProgress()
        {
            DisplayProgressText();
            DisplayProgressFilling();
        }

        #endregion



        #region Private Methods

        private void DisplayProgressText()
        {
            string text = string.Format(StringConstants.Formats.ProgressFormat, Mathf.RoundToInt((float)CurrentValue / MaxValue * 100));

            m_progressText.text = text;
        }


        private void DisplayProgressFilling()
        {
            float progress = (float)CurrentValue / MaxValue;
            DOTween.Kill(this);

            m_progressFilling.DOFillAmount(progress, m_fillingTime).SetEase(m_fillingCurve).SetId(this).OnComplete(
                () =>
                {
                    if (progress >= 1)
                    {
                        EventProgressCompleted?.Invoke();
                    }
                });
        }

        #endregion
    }
}
