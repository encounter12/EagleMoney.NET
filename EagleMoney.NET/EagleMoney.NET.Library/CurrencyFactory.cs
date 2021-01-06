using System;
using System.Collections.Generic;
using System.Linq;

namespace EagleMoney.NET.Library
{
    public class CurrencyFactory
    {
        private readonly ICurrencyProvider _currencyProvider;
        private readonly ICountryProvider _countryProvider;
        
        public CurrencyFactory(ICurrencyProvider currencyProvider, ICountryProvider countryProvider)
        {
            _currencyProvider = currencyProvider;
            _countryProvider = countryProvider;
        }

        public CurrencyDetailedInfo CreateCurrency(string currencyCode)
        {
            CurrencyCountriesBasicInfo currencyBasicInfo = _currencyProvider
                .GetCurrencies()
                .FirstOrDefault(x => x.Code == currencyCode);

            if (currencyBasicInfo == null)
            {
                throw new InvalidOperationException(
                    $"No currency {currencyCode} exists in the list. Use the overloaded constructor to define custom currency.");
            }

            var currencyDetailedInfo = new CurrencyDetailedInfo
            {
                Code = currencyBasicInfo.Code,
                Name = currencyBasicInfo.Name,
                Number = currencyBasicInfo.Number,
                Sign = currencyBasicInfo.Sign,
                DefaultFractionDigits = currencyBasicInfo.DefaultFractionDigits,
                Countries = GetCountries(currencyBasicInfo, _countryProvider)
            };

            return currencyDetailedInfo;
        }

        public CurrencyDetailedInfo CreateCurrency(CountryCode countryCode)
        {
            var countryName = _countryProvider.GetCountries()
                .Single(x => x.CodeAlpha2 == countryCode.ToString())
                .Name
                .ToUpperInvariant();

            CurrencyDetailedInfo currencyDetailed = GetCurrencyDetailedInfo(countryName);

            return currencyDetailed;
        }
        
        public CurrencyDetailedInfo CreateCurrency(CountryCodeAlpha3 countryCodeAlpha3)
        {
            var countryName = _countryProvider.GetCountries()
                .Single(x => x.CodeAlpha3 == countryCodeAlpha3.ToString())
                .Name
                .ToUpperInvariant();

            CurrencyDetailedInfo currencyDetailed = GetCurrencyDetailedInfo(countryName);

            return currencyDetailed;
        }

        private CurrencyDetailedInfo GetCurrencyDetailedInfo(string countryName)
        {
            var currencyBasicInfo = _currencyProvider.GetCurrencies()
                .First(x =>
                    x.Countries.Any(c => c.ToUpperInvariant() == countryName));
            
            var currencyDetailed = new CurrencyDetailedInfo
            {
                Code = currencyBasicInfo.Code,
                Name = currencyBasicInfo.Name,
                Number = currencyBasicInfo.Number,
                Sign = currencyBasicInfo.Sign,
                DefaultFractionDigits = currencyBasicInfo.DefaultFractionDigits,
                Countries = GetCountries(currencyBasicInfo, _countryProvider)
            };

            return currencyDetailed;
        }
        
        private HashSet<Country> GetCountries(
            CurrencyCountriesBasicInfo currencyBasicInfo, ICountryProvider countryProvider)
        {
            return currencyBasicInfo.Countries.GroupJoin(
                countryProvider.GetCountries(),
                currCountry => currCountry.ToUpperInvariant(),
                country => country.Name.ToUpperInvariant(),
                (currCountry, country) =>
                {
                    var enumerable = country.ToList();
                    return new Country
                    {
                        Name = currCountry,
                        CodeAlpha2 = enumerable.SingleOrDefault().CodeAlpha2 ?? "",
                        CodeAlpha3 = enumerable.SingleOrDefault().CodeAlpha3 ?? "",
                        NumericCode = enumerable.SingleOrDefault().NumericCode ?? ""
                    };
                }).ToHashSet();
        }
    }
}