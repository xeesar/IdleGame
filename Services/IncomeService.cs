using System.Collections.Generic;
using Enums;
using Models.Income;

namespace Services
{
    public class IncomeService
    {
        #region Fields

        private Dictionary<IncomeType, Income> m_incomes = new Dictionary<IncomeType, Income>();

        #endregion
        
        
        
        #region Public Methods

        public void GiveIncomeFor(IncomeType incomeType, float multiplier = 1f)
        {
            Income income = GetIncome(incomeType);
            income.Give(multiplier);

            m_incomes.Remove(incomeType);
        }
        
        
        public Income GetIncome(IncomeType type)
        {
            if (m_incomes.ContainsKey(type))
            {
                return m_incomes[type];
            }
            
            Income income = CreateIncome(type);
            m_incomes.Add(type, income);

            return income;
        }
        
        #endregion



        #region Private Methods

        private Income CreateIncome(IncomeType incomeType)
        {
            switch (incomeType)
            {
                case IncomeType.PerPixel:
                    return new PerPixelIncome();
                case IncomeType.Offline:
                    return new OfflineIncome();
                default:
                    return new PerPixelIncome();
            }
        }

        #endregion
    }
}

