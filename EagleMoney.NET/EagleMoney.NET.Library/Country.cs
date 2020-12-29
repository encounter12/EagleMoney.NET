// ReSharper disable InconsistentNaming
using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public readonly struct Country
    {
        public Country(string name, string codeAlpha2, string codeAlpha3, string numericCode, string currencyCode)
        {
            Name = name;
            CodeAlpha2 = codeAlpha2;
            CodeAlpha3 = codeAlpha3;
            NumericCode = numericCode;
            CurrencyCode = currencyCode;
        }
        
        public string Name { get; init;  }
        
        public string CodeAlpha2 { get; init;  }
        
        public string CodeAlpha3 { get; init; }
        
        public string NumericCode { get; init; }
        
        public string CurrencyCode { get; init; }

        public enum Codes
        {
            AF,
            AX,
            BG,
            FR,
            DE,
            GB,
            US
        }
        
        public static readonly HashSet<Country> Countries = new()
        {
            new Country("Afghanistan", Codes.AF.ToString(), "AFG", "004", "AFN"),
            new Country("Åland Islands", Codes.AX.ToString(), "ALA", "248", "EUR"),
            new Country("Bulgaria", Codes.BG.ToString(), "BGR", "1OO", "BGN"),
            new Country("France", Codes.FR.ToString(), "FRA", "250", "EUR"),
            new Country("Germany", Codes.DE.ToString(), "DEU", "276", "EUR"),
            new Country("United Kingdom of Great Britain and Northern Ireland", Codes.GB.ToString(), "GBR", "826", "GBP"),
            new Country("United States of America", Codes.US.ToString(), "USA", "840", "USD")
        };
    }
}