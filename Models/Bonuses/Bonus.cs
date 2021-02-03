using Enums;
using UnityEngine;

namespace Models.Bonuses
{
    [CreateAssetMenu(menuName = "Models/Bonus", fileName = "BaseBonus")]
    public class Bonus : ScriptableObject
    {
        [Header("Options")] 
        [SerializeField] private BonusType m_bonusType = BonusType.None;
        [SerializeField] private float m_duration = 0;
        [SerializeField] private float m_value = 0;

        private float m_currentTime = 0;
        
        public BonusType Type => m_bonusType;

        public float CurrentTime => m_currentTime;
        
        public float Value => m_value;


        public void ActivateBonus()
        {
            m_currentTime = m_duration;
        }


        public bool HandleBonus(float deltaTime)
        {
            m_currentTime -= deltaTime;

            return m_currentTime > 0;
        }
    }
}
