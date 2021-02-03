using System;
using System.Collections.Generic;
using Extensions;
using Interfaces;
using Models.Screens;
using UnityEngine;

namespace Managers
{
    public class ScreensManager : Singleton<ScreensManager>, IManager
    {
        #region Fields

        [Header("Components")]
        [SerializeField] private List<BaseScreen> m_screensPrefabs = new List<BaseScreen>();

        [Header("Options")]
        [SerializeField] private Transform m_screensParent = null;

        private Dictionary<Type, BaseScreen> m_screens = new Dictionary<Type, BaseScreen>();


        private BaseScreen m_currentScreen;

        #endregion



        #region Properties

        public BaseScreen CurrentScreen => m_currentScreen;
        
        #endregion



        #region Public Methods

        public void Initialize()
        {
            InitScreens();
        }
        

        public void ShowScreen<T>() where T : BaseScreen
        {
            if (m_currentScreen != null)
            {
                HideCurrentScreen();
            }
            
            m_currentScreen = GetScreen<T>();
            m_currentScreen.Show();
        }
        
        
        public void ShowScreen(Type screenType)
        {
            if (m_currentScreen != null)
            {
                HideCurrentScreen();
            }
            
            m_currentScreen = GetScreen(screenType);
            m_currentScreen.Show();
        }
        

        public T GetScreen<T>() where T : BaseScreen
        {
            Type screenType = typeof(T);

            if (m_screens.ContainsKey(screenType))
            {
                return (T)m_screens[screenType];
            }

            return null;
        }
        
        
        public BaseScreen GetScreen(Type screenType)
        {
            foreach (var screen in m_screens)
            {
                if (screen.Value.GetType() == screenType)
                {
                    return screen.Value;
                }
            }

            return null;
        }

        #endregion
        
        
        
        #region Private Methods
        
        private void InitScreens()
        {
            for (int i = 0; i < m_screensPrefabs.Count; i++)
            {
                SpawnScreen(m_screensPrefabs[i]);
            }
        }

        
        private void SpawnScreen(BaseScreen screenPrefab)
        {
            BaseScreen screen = Instantiate(screenPrefab, m_screensParent);
            screen.gameObject.SetActive(false);
            
            m_screens.Add(screen.GetType(), screen);
        }

        
        private void HideCurrentScreen()
        {
            m_currentScreen.Hide();
        }

        #endregion
    }
}
