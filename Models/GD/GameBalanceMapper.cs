using System;
using System.Collections.Generic;
using Data.Configs;
using GoogleSheetsToUnity;
using UnityEngine;

namespace Models.GD
{
    [CreateAssetMenu(menuName = "Game Design/Game Balance Mapper", fileName = "GameBalanceMapper")]
    public class GameBalanceMapper : ScriptableObject
    {
        #region Fields

        [Header("Common")]
        [SerializeField] private string m_sheetName = "";
        
        [Header("Respect Buildings Parameters")]
        [SerializeField] private string m_respectWorksheetName = "";
        [SerializeField] private int m_startRespectRow = 0;
        [SerializeField] private List<ProductionBuildingConfig> m_respectConfigs;
        
        [Header("Respect Buildings Parameters")]
        [SerializeField] private string m_dollarsWorksheetName = "";
        [SerializeField] private int m_startDollarsRow = 0;
        [SerializeField] private List<ProductionBuildingConfig> m_dollarsConfigs;

        #endregion



        #region Properties

        public List<ProductionBuildingConfig> RespectConfigs => m_respectConfigs;
        
        public List<ProductionBuildingConfig> DollarsConfig => m_dollarsConfigs;

        #endregion



        #region Public Methods

        public void UpdateConfigs(Action onComplete)
        {
            UpdateRespectBuildings(onComplete);
        }

        #endregion



        #region Private Methods

        private void UpdateRespectBuildings(Action onComplete)
        {
            SpreadsheetManager.Read(new GSTU_Search(m_sheetName, m_respectWorksheetName), sheet =>
            {
                SetData(sheet, m_respectConfigs, m_startRespectRow);
                Debug.Log("[Balance Mapper] Respect Configs Updated");
                UpdateDollarsBuildings(onComplete);
            });
        }


        private void UpdateDollarsBuildings(Action onComplete)
        {
            SpreadsheetManager.Read(new GSTU_Search(m_sheetName, m_dollarsWorksheetName), sheet =>
            {
                SetData(sheet, m_dollarsConfigs, m_startDollarsRow);
                Debug.Log("[Balance Mapper] Dollars Configs Updated");
                onComplete?.Invoke();
            });
        }
        
        
        private void SetData(GstuSpreadSheet sheet, List<ProductionBuildingConfig> configs, int startRow)
        {
            for (int row = startRow, i = 0; i < configs.Count; i++, row++)
            {
                SetParameters(configs[i], sheet.rows[row]);
            }
        }


        private void SetParameters(ProductionBuildingConfig config, List<GSTU_Cell> cells)
        {
            config.UnlockPrice = float.Parse(cells[1].value.Replace(',','.'));
            config.AutoCollectingPrice = float.Parse(cells[2].value.Replace(',','.'));
            
            config.IncomeData.Parameter.firstValue = float.Parse(cells[3].value.Replace(',','.'));
            config.IncomeData.Parameter.multiplier = float.Parse(cells[4].value.Replace(',','.'));
            config.IncomeData.Price.firstValue = float.Parse(cells[5].value.Replace(',','.'));
            config.IncomeData.Price.multiplier = float.Parse(cells[6].value.Replace(',','.'));
            
            config.ProductionSpeedData.Parameter.firstValue = float.Parse(cells[7].value.Replace(',','.'));
            config.ProductionSpeedData.Parameter.multiplier = float.Parse(cells[8].value.Replace(',','.'));
            config.ProductionSpeedData.Price.firstValue = float.Parse(cells[9].value.Replace(',','.'));
            config.ProductionSpeedData.Price.multiplier = float.Parse(cells[10].value.Replace(',','.'));
        }

        #endregion
    }
}

