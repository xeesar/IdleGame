using System;
using System.Collections.Generic;
using Extensions;
using Interfaces;
using Models.Popups;
using UnityEngine;

namespace Managers
{
    public class PopupManager : Singleton<PopupManager>, IManager
    {
        #region Fields

        [Header("Components")]
        [SerializeField] private List<BasePopup> m_popupsPrefabs = new List<BasePopup>();

        [Header("Options")]
        [SerializeField] private Transform m_popupsParent = null;

        private Dictionary<Type, BasePopup> m_popups = new Dictionary<Type, BasePopup>();


        private BasePopup m_currentPopup;

        private Queue<BasePopup> m_popupsQueue = new Queue<BasePopup>();
        
        #endregion



        #region Properties

        public BasePopup CurrentPopup => m_currentPopup;

        #endregion
        
        
        
        #region Public Methods

        public void Initialize()
        {
            InitPopups();
        }
        

        public void ShowPopup<T>() where T : BasePopup
        {
            m_popupsQueue.Enqueue(GetPopup<T>());
            
            if (m_currentPopup != null)
            {
                return;
            }

            ShowNextPopup();
        }
        
                
        public void HideCurrentPopup()
        {
            m_currentPopup?.Hide();
            m_currentPopup = null;
            
            if (m_popupsQueue.Count > 0)
            {
                ShowNextPopup();
            }
        }


        public T GetPopup<T>() where T : BasePopup
        {
            Type screenType = typeof(T);

            if (m_popups.ContainsKey(screenType))
            {
                return (T)m_popups[screenType];
            }

            return null;
        }

        #endregion
        
        
        
        #region Private Methods

        private void InitPopups()
        {
            for (int i = 0; i < m_popupsPrefabs.Count; i++)
            {
                SpawnPopup(m_popupsPrefabs[i]);
            }
        }


        private void SpawnPopup(BasePopup popupPrefab)
        {
            BasePopup popup = Instantiate(popupPrefab, m_popupsParent);
            popup.gameObject.SetActive(false);
            
            m_popups.Add(popup.GetType(), popup);
        }


        private void ShowNextPopup()
        {
            m_currentPopup = m_popupsQueue.Dequeue();
            m_currentPopup.Show();
        }
        
        #endregion
    }
}

