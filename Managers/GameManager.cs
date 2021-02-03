using System;
using Enums;
using Interfaces;
using Models.GameFactory;
using Models.Popups;
using Models.Screens;
using Services;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour, IManager
    {
        #region Fields

        [Header("Options")] 
        [SerializeField] private GameType m_gameType = GameType.Default;

        #endregion
        
        
        
        #region Public Methods

        public void Initialize()
        {
            StartGame();
        }

        #endregion
        
        
        
        #region Private Methods

        private void StartGame()
        {
            ScreensManager.Instance.GetScreen<GameScreen>().SetBackButtonActive(true);
            
            CreateGame();
        }

        
        private void EndGame()
        {
        }


        private void CreateGame()
        {
            BaseGameFactory gameFactory = GetGameFactory();
            gameFactory.CreateGame();
        }


        private BaseGameFactory GetGameFactory()
        {
            LevelManager levelManager = LevelManager.Instance;
            
            if (m_gameType == GameType.Default)
            {
                return new DefaultGameFactory(Instantiate(levelManager.Graffiti), levelManager.Building.Holst);
            }
            
            if (m_gameType == GameType.View)
            {
                return new ViewGameFactory(Instantiate(levelManager.Graffiti), levelManager.Building.Holst);
            }

            return null;
        }

        #endregion
    }
}

