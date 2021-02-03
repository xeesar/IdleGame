using UnityEngine;
using UnityEngine.UI;

namespace View.UI.UIInteractable
{
    public class ImageInteractable : UIInteractableView
    {
        #region Fields

        [Header("Components")]
        [SerializeField] private Image m_image = null;

        [Header("Options")] 
        [SerializeField] private Sprite m_interactableSprite = null;
        [SerializeField] private Sprite m_disabledSprite = null;

        #endregion
        
        
        
        #region Public Methods

        public override void SetInteractable(bool isInteractable)
        {
            m_image.sprite = isInteractable ? m_interactableSprite : m_disabledSprite;
        }

        #endregion
    }
}

