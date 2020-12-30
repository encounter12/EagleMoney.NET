using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace EagleMoney.NET.Library
{
    // Credit: spender - 3 Digit currency code to currency symbol (https://stackoverflow.com/a/12374378/1961386)
    public static class CurrencyTools
    {
        private static IDictionary<string,string> map;
        
        static CurrencyTools()
        {
            map = CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(culture => {
                    try
                    {
                        return new RegionInfo(culture.Name);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(ri => ri != null)
                .GroupBy(ri => ri.ISOCurrencySymbol)
                .ToDictionary(x => x.Key, x => x.First().CurrencySymbol);
        }
        public static bool TryGetCurrencySymbol(
            string isoCurrencyCode, 
            out string symbol)
        {
            return map.TryGetValue(isoCurrencyCode,out symbol);
        }
        
        public static List<string> CurrencyCodes = GetCurrencies()
            .Select(c => c.Code)
            .OrderBy(c => c)
            .ToList();
        
        public static List<Currency> GetCurrencies()
        {
            var fileName = "list_one.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var iso4217FilePath = Path.Combine(currentDirectory, "ISO-4217", fileName);
            
            XElement iso4217 = XElement.Load(iso4217FilePath);
            
            List<Currency> currencies = iso4217.Descendants("CcyNtry")
                .Select(x => new
                {
                    CountryName =  x.Element("CtryNm")?.Value,
                    CurrencyName = x.Element("CcyNm")?.Value,
                    CurrencyCode = x.Element("Ccy")?.Value,
                    CurrencyNumber = x.Element("CcyNbr")?.Value,
                    CurrencyMinorUnits = x.Element("CcyMnrUnts")?.Value
                })
                .Where(x => x.CurrencyMinorUnits != null && x.CurrencyMinorUnits != "N.A.")
                .GroupBy(x => x.CurrencyCode)
                .Select(x => new Currency
                {
                    Code = x.Key,
                    Number = x.First().CurrencyNumber,
                    Sign = "",
                    DefaultFractionDigits = int.Parse(x.First().CurrencyMinorUnits),
                    Countries = x.Select(y => y.CountryName).ToHashSet()
                })
                .ToList();

            return currencies;
        }
    }
}