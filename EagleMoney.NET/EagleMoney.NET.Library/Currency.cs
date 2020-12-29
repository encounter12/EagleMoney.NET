// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EagleMoney.NET.Library
{
    public readonly struct Currency : IEquatable<Currency>
    {
        public Currency(string currencyCode)
        {
            Code = currencyCode;
            var selectedCurrency = worldCurrencies.FirstOrDefault(x => x.Code == currencyCode);

            if (selectedCurrency == default(Currency))
            {
                throw new InvalidOperationException(
                    $"No currency {currencyCode} exists in the list. Use the overloaded constructor to define custom currency.");
            }

            Number = selectedCurrency.Number;
            Sign = selectedCurrency.Sign;
            DefaultFractionDigits = selectedCurrency.DefaultFractionDigits;
            Countries = GetCountries(currencyCode);
        }
        
        public Currency(Country.Codes countryCode)
        {
            var selectedCountry = Country.Countries.FirstOrDefault(x => x.CodeAlpha2 == countryCode.ToString());
            var selectedCurrency = worldCurrencies.FirstOrDefault(x => x.Code == selectedCountry.CurrencyCode);

            if (selectedCurrency == default(Currency))
            {
                throw new InvalidOperationException(
                    $"No currency {selectedCountry.CurrencyCode} for country: {selectedCountry.Name} exists. Use the overloaded constructor to define custom currency.");
            }

            Code = selectedCurrency.Code;
            Number = selectedCurrency.Number;
            Sign = selectedCurrency.Sign;
            DefaultFractionDigits = selectedCurrency.DefaultFractionDigits;
            Countries = GetCountries(selectedCurrency.Code);
        }
        
        public Currency(string code, int number, string sign, int defaultFractionDigits)
        {
            Code = code;
            Number = number;
            Sign = sign;
            DefaultFractionDigits = defaultFractionDigits;
            Countries = GetCountries(code);
        }

        public string Code { get; init;  }

        public int Number { get; init;  }

        public string Sign { get; init; }
        
        public int DefaultFractionDigits { get; init; }
        
        public HashSet<Country> Countries { get; init; }

        private static HashSet<Country> GetCountries(string code)
        {
            var countries = Country.Countries
                .Where(c => c.CurrencyCode == code)
                .ToHashSet();

            return countries;
        }
        
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(nameof(Currency));
            stringBuilder.Append(" { ");

            PrintMembers(stringBuilder);

            stringBuilder.Append(" }");
            return stringBuilder.ToString();
        }

        private void PrintMembers(StringBuilder builder)
        {
            builder.Append(nameof(Code));
            builder.Append(" = ");
            builder.Append(Code);

            builder.Append(", ");

            builder.Append(nameof(Number));
            builder.Append(" = ");
            builder.Append(Number);
            
            builder.Append(", ");
            
            builder.Append(nameof(Sign));
            builder.Append(" = ");
            builder.Append(Sign);
            
            builder.Append(", ");
            
            builder.Append(nameof(DefaultFractionDigits));
            builder.Append(" = ");
            builder.Append(DefaultFractionDigits);
        }
        
        public bool Equals(Currency other)
            => Code == other.Code && 
               Number == other.Number && 
               Sign == other.Sign && 
               DefaultFractionDigits == other.DefaultFractionDigits;

        public override bool Equals(object other)
        {
            var otherCurrency = other as Currency?;
            return otherCurrency.HasValue && Equals(otherCurrency.Value);
        }

        public override int GetHashCode()
            => HashCode.Combine(Code, Number, Sign, DefaultFractionDigits);

        public static bool operator ==(Currency? c1, Currency? c2)
            => c1.HasValue && c2.HasValue ? c1.Equals(c2) : !c1.HasValue && !c2.HasValue;

        public static bool operator !=(Currency? c1, Currency? c2)
            =>!(c1 == c2);
        
        public const string AFN = "AFN";

        public const string BGN = "BGN";
        
        public const string EUR = "EUR";
        
        public const string GBP = "GBP";
        
        public const string USD = "USD";

        private static readonly Currency[] worldCurrencies =
        {
            new ("AFN", 971, "", 2),
            new ("BGN", 975, "", 2),
            new ("EUR", 978, "€", 2),
            new ("GBP", 826, "£", 2),
            new ("USD", 840, "$", 2)
        };
    }
}