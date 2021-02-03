using TMPro;
using UnityEngine;

namespace View.UI.UIInteractable
{
    public class TextInteractable : UIInteractableView
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private TextMeshProUGUI m_text = null;

        [Header("Options")] 
        [SerializeField] private Color m_interactableColor = Color.white;
        [SerializeField] private Color m_disabledleColor = Color.black;

        #endregion



        #region Public Methods

        public override void SetInteractable(bool isInteractable)
        {
            m_text.color = isInteractable ? m_interactableColor : m_disabledleColor;
        }

        #endregion
    }
}

