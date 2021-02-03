using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Extensions;
using Interfaces;
using Models.Bonuses;
using UnityEngine;

namespace Managers
{
    public class BonusManager : Singleton<BonusManager>, IManager
    {
        [Header("Options")] 
        [SerializeField] private List<Bonus> m_bonuses;

        private List<Bonus> m_activeBonuses = new List<Bonus>();

        private void OnEnable()
        {
            UpdateManager.EventOnUpdate += OnUpdate;
        }


        private void OnDisable()
        {
            UpdateManager.EventOnUpdate -= OnUpdate;
        }
        

        private void OnUpdate(float deltaTime)
        {
            HandleBonuses(Time.deltaTime);
        }


        public void Initialize()
        {
            
        }


        public void ActivateBonus(BonusType bonusType)
        {
            for (int i = 0; i < m_activeBonuses.Count; i++)
            {
                if (m_activeBonuses[i].Type == bonusType)
                {
                    m_activeBonuses[i].ActivateBonus();
                    return;
                }
            }

            Bonus bonusToAdd = m_bonuses.FirstOrDefault(bonus => bonus.Type == bonusType);

            if (bonusToAdd != null)
            {
                m_activeBonuses.Add(bonusToAdd);
                bonusToAdd.ActivateBonus();
            }
        }


        public float GetBonusValue(BonusType bonusType)
        {
            for (int i = 0; i < m_activeBonuses.Count; i++)
            {
                if (m_activeBonuses[i].Type == bonusType)
                {
                    return m_activeBonuses[i].Value;
                }
            }
            
            return 1;
        }


        public bool IsBonusActive(BonusType bonusType)
        {
            for (int i = 0; i < m_activeBonuses.Count; i++)
            {
                if (m_activeBonuses[i].Type == bonusType)
                {
                    return true;
                }
            }

            return false;
        }
        
        
        private void HandleBonuses(float deltaTime)
        {
            int bonusesCount = m_activeBonuses.Count - 1;

            for (int i = bonusesCount; i >= 0; i--)
            {
                if(m_activeBonuses[i].HandleBonus(deltaTime)) continue;

                m_activeBonuses.RemoveAt(i);
            }
        }
    }
}

