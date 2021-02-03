using System.Collections.Generic;
using Data;
using Interfaces;
using Models.Currencies;

namespace Models.UserProfile
{
    public class UserInventory : IUserInventory
    {
        #region Properties

        public List<CityProgressData> CityProgressData { get; set; }
        
        public List<Currency> Currencies { get; set; }

        #endregion
    }
}

