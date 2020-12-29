using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public static class CountriesContainer
    {
        public static readonly HashSet<Country> Countries = new()
        {
            new Country("Afghanistan", CountryCode.AF.ToString(), "AFG", "004", CurrencyCode.AFN.ToString()),
            new Country("Åland Islands", CountryCode.AX.ToString(), "ALA", "248", CurrencyCode.EUR.ToString()),
            new Country("Bulgaria", CountryCode.BG.ToString(), "BGR", "1OO", CurrencyCode.BGN.ToString()),
            new Country("France", CountryCode.FR.ToString(), "FRA", "250", CurrencyCode.EUR.ToString()),
            new Country("Germany", CountryCode.DE.ToString(), "DEU", "276", CurrencyCode.EUR.ToString()),
            new Country("United Kingdom of Great Britain and Northern Ireland", CountryCode.GB.ToString(), "GBR", "826", CurrencyCode.GBP.ToString()),
            new Country("United States of America", CountryCode.US.ToString(), "USA", "840", CurrencyCode.USD.ToString())
        };
    }
}