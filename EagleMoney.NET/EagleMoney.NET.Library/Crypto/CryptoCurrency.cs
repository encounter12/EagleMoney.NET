// ReSharper disable InconsistentNaming
using System;
using System.Collections.Generic;
using System.Text;
using EagleMoney.NET.Library.Currencies;

namespace EagleMoney.NET.Library.Crypto
{
    public readonly struct CryptoCurrency : ICurrency, IEquatable<CryptoCurrency>
    {
        public CryptoCurrency(string currencyCode) : 
            this(currencyCode, new CryptoCurrencyProvider())
        {
        }
        
        public CryptoCurrency(string currencyCode, ICryptoCurrencyProvider cryptoCurrencyProvider)
        {
            this = cryptoCurrencyProvider.GetCurrency(currencyCode);
        }
        
        public CryptoCurrency(string code, int defaultFractionDigits)
        {
            Code = code;
            DefaultFractionDigits = defaultFractionDigits;
            Symbol = "";
            Name = "";
            MinorUnit = "";
            HashAlgorithm = "";
            ConsensusMechanism = "";
            Founder = "";
            ProgrammingLanguages = new List<string>();
        }
        
        public CryptoCurrency(string code, int defaultFractionDigits, string symbol)
        {
            Code = code;
            DefaultFractionDigits = defaultFractionDigits;
            Symbol = symbol;
            Name = "";
            MinorUnit = "";
            HashAlgorithm = "";
            ConsensusMechanism = "";
            Founder = "";
            ProgrammingLanguages = new List<string>();
        }
        
        public CryptoCurrency(string code, int defaultFractionDigits, string symbol, string name)
        {
            Code = code;
            DefaultFractionDigits = defaultFractionDigits;
            Symbol = symbol;
            Name = name;
            MinorUnit = "";
            HashAlgorithm = "";
            ConsensusMechanism = "";
            Founder = "";
            ProgrammingLanguages = new List<string>();
        }

        public CryptoCurrency(string code, int defaultFractionDigits, string symbol, string name, string minorUnit)
        {
            Code = code;
            DefaultFractionDigits = defaultFractionDigits;
            Symbol = symbol;
            Name = name;
            MinorUnit = minorUnit;
            HashAlgorithm = "";
            ConsensusMechanism = "";
            Founder = "";
            ProgrammingLanguages = new List<string>();
        }
        
        public CryptoCurrency(
            string code,
            int defaultFractionDigits,
            string symbol,
            string name,
            string minorUnit,
            string hashAlgorithm)
        {
            Code = code;
            DefaultFractionDigits = defaultFractionDigits;
            Symbol = symbol;
            Name = name;
            MinorUnit = minorUnit;
            HashAlgorithm = hashAlgorithm;
            ConsensusMechanism = "";
            Founder = "";
            ProgrammingLanguages = new List<string>();
        }
        
        public CryptoCurrency(
            string code,
            int defaultFractionDigits,
            string symbol,
            string name,
            string minorUnit,
            string hashAlgorithm,
            string consensusMechanism)
        {
            Code = code;
            DefaultFractionDigits = defaultFractionDigits;
            Symbol = symbol;
            Name = name;
            MinorUnit = minorUnit;
            HashAlgorithm = hashAlgorithm;
            ConsensusMechanism = consensusMechanism;
            Founder = "";
            ProgrammingLanguages = new List<string>();
        }
        
        public string Code { get; init; }
        
        public int DefaultFractionDigits { get; init; }
        
        public string Symbol { get; init; }
        
        public string Name { get; init; }

        public string MinorUnit { get; init; }
        
        public string HashAlgorithm { get; init; }
        
        public string ConsensusMechanism { get; init; }
        
        public string Founder { get; init; }
        
        public IEnumerable<string> ProgrammingLanguages { get; init; }
        
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(nameof(CryptoCurrency));
            stringBuilder.Append(" { ");

            PrintMembers(stringBuilder);

            stringBuilder.Append(" }");
            return stringBuilder.ToString();
        }

        private void PrintMembers(StringBuilder builder)
        {
            builder.Append(nameof(Name));
            builder.Append(" = ");
            builder.Append(Name);

            builder.Append(", ");
            
            builder.Append(nameof(Code));
            builder.Append(" = ");
            builder.Append(Code);

            builder.Append(", ");

            builder.Append(nameof(Symbol));
            builder.Append(" = ");
            builder.Append(Symbol);
            
            builder.Append(", ");
            
            builder.Append(nameof(DefaultFractionDigits));
            builder.Append(" = ");
            builder.Append(DefaultFractionDigits);
        }
        
        public bool Equals(CryptoCurrency other)
            =>
                Name == other.Name &&
                Code == other.Code &&
                Symbol == other.Symbol &&
                MinorUnit == other.MinorUnit &&
                DefaultFractionDigits == other.DefaultFractionDigits &&
                HashAlgorithm == other.HashAlgorithm &&
                ConsensusMechanism == other.ConsensusMechanism &&
                Founder == other.Founder;

        public override bool Equals(object other)
        {
            var otherCurrency = other as CryptoCurrency?;
            return otherCurrency.HasValue && Equals(otherCurrency.Value);
        }

        public override int GetHashCode()
            => HashCode.Combine(Code, Symbol, DefaultFractionDigits);

        public static bool operator ==(CryptoCurrency? c1, CryptoCurrency? c2)
            => c1.HasValue && c2.HasValue ? c1.Equals(c2) : !c1.HasValue && !c2.HasValue;

        public static bool operator !=(CryptoCurrency? c1, CryptoCurrency? c2)
            =>!(c1 == c2);

        public const string BTC = "BTC";
        
        public const string ETH = "ETH";
    }
}