using Controllers;
using DG.Tweening;
using Models.States.CityCamera;
using UnityEngine;
using UnityEngine.UI;

namespace Models.Popups
{
    public class NewDistrictPopup : BasePopup
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private Button m_visitDistrictButton;

        [Header("Options")] 
        [SerializeField] private float m_displayingDuration;
        
        private CityCameraController m_cityCameraController;
        
        #endregion



        #region Properties

        public Vector3 TargetDistrictPosition { get; set; }

        #endregion



        #region Public Methods

        public override void Show()
        {
            gameObject.SetActive(true);

            Initialize();
        }


        public override void Hide()
        {
            base.Hide();
            
            m_visitDistrictButton.onClick.RemoveListener(OnVisitNewDistrictButtonClicked);

        }

        #endregion



        #region Private Methods

        protected override void Initialize()
        {
            m_visitDistrictButton.onClick.AddListener(OnVisitNewDistrictButtonClicked);

            DOTween.Sequence().SetDelay(m_displayingDuration).OnComplete(Hide);
        }
        

        private void OnVisitNewDistrictButtonClicked()
        {
            if (m_cityCameraController == null)
            {
                m_cityCameraController = FindObjectOfType<CityCameraController>();
            }
            
            m_cityCameraController.ChangeCameraState(new TargetMoveCameraState(TargetDistrictPosition));
            
            Hide();
        }

        #endregion
        
    }
}
