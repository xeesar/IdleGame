using System;
using Data;
using Data.Configs;
using DG.Tweening;
using Enums;
using Extensions;
using Interfaces;
using Managers;
using Models.Districts;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ProductionBuildingView : DistrictElement
    {
        #region Fields

        public event Action EventOnUnlockClick;
        public event Action EventOnCollectClick;

        [Header("Building View Components")] 
        [SerializeField] private SpriteRenderer m_buildingRenderer;
        
        [Header("Locked UI Components")] 
        [SerializeField] private GameObject m_lockedUI = null;
        [SerializeField] private Image m_unlockCurrencyImage;
        [SerializeField] private TextMeshProUGUI m_priceText = null;
        [SerializeField] private Button m_unlockButton = null;

        [Header("Drawing UI Components")] 
        [SerializeField] private GameObject m_drawingUI = null;
        [SerializeField] private Slider m_drawingSlider = null;
        [SerializeField] private TextMeshProUGUI m_drawingText = null;

        [Header("Production UI Components")]
        [SerializeField] private GameObject m_productionUI = null;
        [SerializeField] private Image m_productionFilling = null;
        [SerializeField] private TextMeshProUGUI m_productionText = null;
        
        [Header("Income UI Components")]
        [SerializeField] private GameObject m_incomeUI = null;
        [SerializeField] private Transform m_incomePanel = null;
        [SerializeField] private TextMeshProUGUI m_incomeText = null;
        [SerializeField] private Image m_currencyImage;
        [SerializeField] private Button m_collectButton;
        [SerializeField] private CanvasGroup m_canvasGroup = null;
        [SerializeField] private Vector3 m_moveOffset;
        [SerializeField] private float m_collectDelay = 0.25f;
        [SerializeField] private float m_collectTime = 1f;

        [Header("Sprites")]
        [SerializeField] private Sprite m_dollarSprite;
        [SerializeField] private Sprite m_respectSprite;

        private Collider2D m_collider;
        
        #endregion



        #region Properties

        public Vector3 UnlockButtonPosition => m_unlockButton.GetComponent<RectTransform>().anchoredPosition;
        
        protected override SpriteRenderer SpriteRenderer => m_buildingRenderer;

        #endregion



        #region Unity Lifecycle

        public void Start()
        {
            m_unlockButton.onClick.AddListener(() =>
            {
                if (ServiceLocator.Instance.Get<IUserProfileModel>().TutorialStage == (int) TutorialType.BuyHouse)
                {
                    ServiceLocator.Instance.Get<IUserProfileModel>().TutorialStage += 1;
                }
                
                EventOnUnlockClick?.Invoke();
            });

            m_collectButton.onClick.AddListener(() =>
            {
                EventOnCollectClick?.Invoke();
            });
        }

        #endregion



        #region Public Methods

        public void InitBuilding(Sprite sprite)
        {
            m_buildingRenderer.sprite = sprite;
            m_collider = m_buildingRenderer.gameObject.AddComponent<PolygonCollider2D>();
        }

        public void SetBuildingSprite(Sprite sprite)
        {
            m_buildingRenderer.sprite = sprite;
        }


        public override void SetElementActive(bool isActive)
        {
            base.SetElementActive(isActive);

            m_collider.enabled = isActive;
        }
        

        public void SetLockedUIActive(bool isActive)
        {
            if (isActive)
            {
                AudioClipData audioClipData = ConfigManager.Instance.Get<AudioConfig>().OpenPopupAudioClipData;
                AudioManager.Instance.PlaySoundFx(audioClipData);
            }
            
            m_lockedUI.SetActive(isActive);
        }

        
        public void SetUnlockButtonStatus(bool isInteractable)
        {
            m_unlockButton.interactable = isInteractable;
        }
        

        public void SetDrawingUIActive(bool isActive)
        {
            m_drawingUI.SetActive(isActive);
        }
        
        
        public void SetProductionUIActive(bool isActive)
        {
            m_productionUI.SetActive(isActive);
        }

        
        public void SetIncomeUIActive(bool isActive)
        {
            m_incomeUI.SetActive(isActive);
        }


        public void DisplayUnlockPrice(float price, CurrencyType currencyType)
        {
            m_priceText.text = price.AbbreviateNumber();
            
            m_unlockCurrencyImage.sprite = currencyType == CurrencyType.Dollar ? m_dollarSprite : m_respectSprite;
        }
        
        public void DisplayDrawingProgress(float progress)
        {
            m_drawingSlider.value = progress;
            m_drawingText.text = string.Format(StringConstants.Formats.ProgressFormat, Mathf.RoundToInt(progress * 100));
        }


        public void DisplayProductionProgress(float progress)
        {
            m_productionFilling.fillAmount = progress;
            m_productionText.text = string.Format(StringConstants.Formats.ProgressFormat, Mathf.RoundToInt(progress * 100));
        }


        public void DisplayIncome(float income, CurrencyType currencyType)
        {
            m_canvasGroup.alpha = 1f;
            m_incomePanel.localPosition = Vector3.zero;
            m_incomeText.text = income.AbbreviateNumber();

            m_currencyImage.sprite = currencyType == CurrencyType.Dollar ? m_dollarSprite : m_respectSprite;
        }


        public void DisplayCollectAnimation(Action onComplete)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(GetMovingTween()).Join(GetFadeTween()).SetDelay(m_collectDelay).OnComplete(() => onComplete?.Invoke());
        }


        public void DisplayUnlockParticle()
        {
            ParticleManager.Instance.SpawnParticle(ParticleType.Income, transform);
        }


        #endregion



        #region Private Methods

        private Tween GetMovingTween()
        {
            return m_incomePanel.DOLocalMove(m_incomePanel.localPosition + m_moveOffset, m_collectTime);
        }


        private Tween GetFadeTween()
        {
            return m_canvasGroup.DOFade(0, m_collectTime);
        }

        #endregion
    }
}