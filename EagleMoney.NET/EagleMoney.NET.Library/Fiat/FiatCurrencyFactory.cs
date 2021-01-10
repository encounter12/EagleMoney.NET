using System;
using System.Collections.Generic;
using System.Linq;
using EagleMoney.NET.Library.Countries;
using EagleMoney.NET.Library.Countries.Enums;
using EagleMoney.NET.Library.Currencies;

namespace EagleMoney.NET.Library.Fiat
{
    public class FiatCurrencyFactory
    {
        private readonly ICurrencyProvider _currencyProvider;
        private readonly ICountryProvider _countryProvider;
        
        public FiatCurrencyFactory(ICurrencyProvider currencyProvider, ICountryProvider countryProvider)
        {
            _currencyProvider = currencyProvider;
            _countryProvider = countryProvider;
        }

        public FiatCurrency CreateCurrency(string currencyCode)
        {
            CurrencyCountriesBasicInfo currencyBasicInfo = _currencyProvider
                .GetCurrencies()
                .FirstOrDefault(x => x.Code == currencyCode);

            if (currencyBasicInfo == null)
            {
                throw new InvalidOperationException(
                    $"No currency {currencyCode} exists in the list. Use the overloaded constructor to define custom currency.");
            }

            var fiatCurrency = new FiatCurrency
            {
                Code = currencyBasicInfo.Code,
                Name = currencyBasicInfo.Name,
                Number = currencyBasicInfo.Number,
                Symbol = currencyBasicInfo.Sign,
                DefaultFractionDigits = currencyBasicInfo.DefaultFractionDigits,
                Countries = GetCountries(currencyBasicInfo, _countryProvider)
            };

            return fiatCurrency;
        }

        public FiatCurrency CreateCurrency(CountryCode countryCode)
        {
            var countryName = _countryProvider.GetCountries()
                .Single(x => x.CodeAlpha2 == countryCode.ToString())
                .ShortNameLowerCase
                .ToUpperInvariant();

            FiatCurrency fiatCurrency = GetCurrencyDetailedInfo(countryName);

            return fiatCurrency;
        }
        
        public FiatCurrency CreateCurrency(CountryCodeAlpha3 countryCodeAlpha3)
        {
            var countryName = _countryProvider.GetCountries()
                .Single(x => x.CodeAlpha3 == countryCodeAlpha3.ToString())
                .ShortNameLowerCase
                .ToUpperInvariant();

            FiatCurrency fiatCurrency = GetCurrencyDetailedInfo(countryName);

            return fiatCurrency;
        }

        private FiatCurrency GetCurrencyDetailedInfo(string countryName)
        {
            var currencyBasicInfo = _currencyProvider.GetCurrencies()
                .First(x =>
                    x.Countries.Any(c => c.ToUpperInvariant() == countryName));
            
            var fiatCurrency = new FiatCurrency
            {
                Code = currencyBasicInfo.Code,
                Name = currencyBasicInfo.Name,
                Number = currencyBasicInfo.Number,
                Symbol = currencyBasicInfo.Sign,
                DefaultFractionDigits = currencyBasicInfo.DefaultFractionDigits,
                Countries = GetCountries(currencyBasicInfo, _countryProvider)
            };

            return fiatCurrency;
        }
        
        private HashSet<Country> GetCountries(
            CurrencyCountriesBasicInfo currencyBasicInfo, ICountryProvider countryProvider)
        {
            return currencyBasicInfo.Countries.GroupJoin(
                countryProvider.GetCountries(),
                currCountry => currCountry.ToUpperInvariant(),
                country => country.ShortNameLowerCase.ToUpperInvariant(),
                (currCountry, country) =>
                {
                    var enumerable = country.ToList();
                    return new Country
                    {
                        ShortNameLowerCase = currCountry,
                        CodeAlpha2 = enumerable.SingleOrDefault().CodeAlpha2 ?? "",
                        CodeAlpha3 = enumerable.SingleOrDefault().CodeAlpha3 ?? "",
                        NumericCode = enumerable.SingleOrDefault().NumericCode ?? ""
                    };
                }).ToHashSet();
        }
    }
}