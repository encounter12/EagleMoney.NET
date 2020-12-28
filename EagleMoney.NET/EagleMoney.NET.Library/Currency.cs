// ReSharper disable InconsistentNaming

using System;
using System.Linq;
using System.Text;

namespace EagleMoney.NET.Library
{
    public readonly struct Currency : IEquatable<Currency>
    {
        public Currency(string code)
        {
            Code = code;
            var selectedCurrency = worldCurrencies.FirstOrDefault(x => x.Code == code);

            if (selectedCurrency == default(Currency))
            {
                throw new InvalidOperationException(
                    $"No currency {code} exists in the list. Use the overloaded constructor to define custom currency.");
            }

            Number = selectedCurrency.Number;
            Sign = selectedCurrency.Sign;
            DefaultFractionDigits = selectedCurrency.DefaultFractionDigits;
        }
        
        public Currency(string code, int number, string sign, int defaultFractionDigits)
        {
            Code = code;
            Number = number;
            Sign = sign;
            DefaultFractionDigits = defaultFractionDigits;
        }

        public string Code { get; }

        public int Number { get; }

        public string Sign { get; }
        
        public int DefaultFractionDigits { get; }
        
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

        public const string USD = "USD";

        public const string EUR = "EUR";

        public const string BGN = "BGN";

        private static readonly Currency[] worldCurrencies =
        {
            new ("USD", 1, "$", 2),
            new ("EUR", 2, "€", 2),
            new ("BGN", 3, "", 2)
        };
    }
}