using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace EagleMoney.NET.Library
{
    public struct CurrencyProvider : ICurrencyProvider
    {
        private IDictionary<string, string> _map;
        
        private List<CurrencyDTO> _currencies;
        
        public bool TryGetCurrencySymbol(
            string isoCurrencyCode,
            out string symbol)
        {
            if (_map != null && _map.Any())
            {
                return _map.TryGetValue(isoCurrencyCode, out symbol);
            }

            _map = GetCurrencySymbols();

            return _map.TryGetValue(isoCurrencyCode,out symbol);
        }
        
        public List<string> CurrencyCodes
        {
            get
            {
                return GetCurrencies()
                    .Select(c => c.Code)
                    .OrderBy(c => c)
                    .ToList();
            }
        }
        
        public List<CurrencyDTO> GetCurrencies()
        {
            if (_currencies != null && _currencies.Any())
            {
                return _currencies;
            }
            
            var fileName = "list_one.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var iso4217FilePath = Path.Combine(currentDirectory, "ISO-4217", fileName);
            
            XElement iso4217 = XElement.Load(iso4217FilePath);
            
            _currencies = iso4217.Descendants("CcyNtry")
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
                .Select(x => new CurrencyDTO
                {
                    Code = x.Key,
                    Number = x.First().CurrencyNumber,
                    Sign = "",
                    DefaultFractionDigits = int.Parse(x.First().CurrencyMinorUnits),
                    Countries = x.Select(y => y.CountryName).ToHashSet()
                })
                .ToList();

            var currencies = this._currencies;

            foreach (var curr in _currencies)
            {
                if (TryGetCurrencySymbol(curr.Code, out var symbol))
                {
                    curr.Sign = symbol;
                }
            }

            return _currencies;
        }
        
        // Credit: spender - 3 Digit currency code to currency symbol (https://stackoverflow.com/a/12374378/1961386)
        private IDictionary<string, string> GetCurrencySymbols()
        {
            IDictionary<string,string> currencyCodeSymbols = CultureInfo
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

            return currencyCodeSymbols;
        }
    }
    
    public class CurrencyDTO
    {
        public string Code { get; set; }
        
        public string Number { get; set; }
        
        public string Sign { get; set; }
        
        public int DefaultFractionDigits { get; set; }
        
        public HashSet<string> Countries { get; set; }
    }
}