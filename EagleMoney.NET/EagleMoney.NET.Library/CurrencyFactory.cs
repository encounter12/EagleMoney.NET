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

        public Currency CreateCurrency(string currencyCode)
        {
            CurrencyDTO currencyDto = _currencyProvider
                .GetCurrencies()
                .FirstOrDefault(x => x.Code == currencyCode);

            if (currencyDto == null)
            {
                throw new InvalidOperationException(
                    $"No currency {currencyCode} exists in the list. Use the overloaded constructor to define custom currency.");
            }

            var currency = new Currency
            {
                Code = currencyDto.Code,
                Name = currencyDto.Name,
                Number = currencyDto.Number,
                Sign = currencyDto.Sign,
                DefaultFractionDigits = currencyDto.DefaultFractionDigits,
                Countries = GetCountries(currencyDto, _countryProvider)
            };

            return currency;
        }

        public Currency CreateCurrency(CountryCode countryCode)
        {
            var countryName = _countryProvider.GetCountries()
                .Single(x => x.CodeAlpha3 == countryCode.ToString())
                .Name
                .ToUpperInvariant();

            var currencyDto = _currencyProvider.GetCurrencies()
                .First(x =>
                    x.Countries.Any(c => c.ToUpperInvariant() == countryName));
            
            var currency = new Currency
            {
                Code = currencyDto.Code,
                Name = currencyDto.Name,
                Number = currencyDto.Number,
                Sign = currencyDto.Sign,
                DefaultFractionDigits = currencyDto.DefaultFractionDigits,
                Countries = GetCountries(currencyDto, _countryProvider)
            };

            return currency;
        }
        
        private HashSet<Country> GetCountries(CurrencyDTO currencyDto, ICountryProvider countryProvider)
        {
            return currencyDto.Countries.GroupJoin(
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