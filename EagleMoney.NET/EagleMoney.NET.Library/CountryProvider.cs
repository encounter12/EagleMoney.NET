using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public struct CountryProvider : ICountryProvider
    {
        public IEnumerable<Country> GetCountries()
        {
            var countries = new List<Country>
            {
                new()
                {
                    Name = "Afghanistan",
                    CodeAlpha2 = "AF",
                    CodeAlpha3 = "AFG",
                    NumericCode = "004"
                }
            };
            
            return countries;
        }
    }
}