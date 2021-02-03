using System.Collections.Generic;
using Data;
using Models.Currencies;

namespace Interfaces
{
    public interface IUserInventory
    {
        List<CityProgressData> CityProgressData { get; set; }
        List<Currency> Currencies { get; set; }
    }
}

