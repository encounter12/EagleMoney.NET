using System.Collections.Generic;

namespace EagleMoney.NET.Library.Countries
{
    public interface ICountryProvider
    {
        IEnumerable<Country> GetCountries();
    }
}