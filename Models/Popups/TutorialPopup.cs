using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Models.Popups
{
    public class TutorialPopup : BasePopup
    {
        [Header("Components")] 
        [SerializeField] private Animator m_animator;
        [SerializeField] private List<GameObject> m_tutorials;

        public void InitializeTutorial(TutorialType tutorialType)
        {
            for (int i = 0; i < m_tutorials.Count; i++)
            {
                m_tutorials[i].SetActive(i == (int)tutorialType);
            }
        }
        
        
        protected override void Initialize()
        {
            
        }


        public override void Show()
        {
            m_animator.SetTrigger("Show");
            gameObject.SetActive(true);
        }
        

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

