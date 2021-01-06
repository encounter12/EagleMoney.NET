using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace EagleMoney.NET.Library
{
    public struct CurrencyProvider : ICurrencyProvider
    {
        private IDictionary<string, string> _currencyCodeSymbols;
        
        private List<CurrencyCountriesBasicInfo> _currencies;
        
        public bool TryGetCurrencySymbol(
            string isoCurrencyCode,
            out string symbol)
        {
            if (_currencyCodeSymbols != null && _currencyCodeSymbols.Any())
            {
                return _currencyCodeSymbols.TryGetValue(isoCurrencyCode, out symbol);
            }

            _currencyCodeSymbols = GetCurrencyCodesCurrencySymbols();

            return _currencyCodeSymbols.TryGetValue(isoCurrencyCode,out symbol);
        }
        
        public List<string> CurrencyCodes
        {
            get
            {
                if (_currencies != null && _currencies.Any())
                {
                    return _currencies.Select(c => c.Code).ToList();
                }

                return GetCurrencies()
                    .Select(c => c.Code)
                    .ToList();
            }
        }
        
        public IEnumerable<CurrencyCountriesBasicInfo> GetCurrencies()
        {
            if (_currencies != null && _currencies.Any())
            {
                return _currencies;
            }

            if (_currencyCodeSymbols == null || !_currencyCodeSymbols.Any())
            {
                _currencyCodeSymbols = GetCurrencyCodesCurrencySymbols();
            }

            var fileName = "list_one.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var iso4217FilePath = Path.Combine(currentDirectory, "ISO-4217", fileName);
            
            XElement iso4217 = XElement.Load(iso4217FilePath);

            var currenciesIso4217 = iso4217.Descendants("CcyNtry")
                .Select(x => new
                {
                    CountryName = x.Element("CtryNm")?.Value,
                    CurrencyName = x.Element("CcyNm")?.Value,
                    CurrencyCode = x.Element("Ccy")?.Value,
                    CurrencyNumber = x.Element("CcyNbr")?.Value,
                    CurrencyMinorUnits = x.Element("CcyMnrUnts")?.Value
                })
                .Where(x => x.CurrencyMinorUnits != null && x.CurrencyMinorUnits != "N.A.")
                .GroupBy(x => x.CurrencyCode)
                .Select(x => new
                {
                    Code = x.Key,
                    Name = x.First().CurrencyName,
                    Number = x.First().CurrencyNumber,
                    Sign = "",
                    DefaultFractionDigits = int.Parse(x.First().CurrencyMinorUnits),
                    Countries = x.Select(y => y.CountryName).ToHashSet()
                });
            
            _currencies = 
                currenciesIso4217
                .GroupJoin(
                    _currencyCodeSymbols,
                    currencyDto => currencyDto.Code, 
                    map => map.Key, 
                    (currDto, currCodeSymbols) => 
                        new CurrencyCountriesBasicInfo(
                            currDto.Code,
                            currDto.Name,
                            currDto.Number, 
                            currCodeSymbols.SingleOrDefault().Value, 
                            currDto.DefaultFractionDigits, 
                            currDto.Countries)
                    )
                .OrderBy(c => c.Code)
                .ToList();

            return _currencies;
        }
        
        // Credit: spender - 3 Digit currency code to currency symbol (https://stackoverflow.com/a/12374378/1961386)
        private IDictionary<string, string> GetCurrencyCodesCurrencySymbols()
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
}