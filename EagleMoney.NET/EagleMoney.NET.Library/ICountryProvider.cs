using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public interface ICountryProvider
    {
        IEnumerable<Country> GetCountries();
    }
}