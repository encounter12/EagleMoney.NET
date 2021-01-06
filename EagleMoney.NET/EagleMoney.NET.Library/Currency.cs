// ReSharper disable InconsistentNaming
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EagleMoney.NET.Library
{
    public readonly struct Currency : IEquatable<Currency>
    {
        public Currency(CountryCode countryCode) 
            : this(countryCode, new CurrencyFactory(new CurrencyProvider(), new CountryProvider()))
        {
        }
        
        public Currency(CountryCode countryCode, CurrencyFactory currencyFactory)
        {
            var currency = currencyFactory.CreateCurrency(countryCode);

            Code = currency.Code;
            Name = currency.Name;
            Number = currency.Number;
            Sign = currency.Sign;
            DefaultFractionDigits = currency.DefaultFractionDigits;
            Countries = currency.Countries;
        }
        
        public Currency(string currencyCode) : 
            this(currencyCode, new CurrencyFactory( new CurrencyProvider(), new CountryProvider()))
        {
        }
        
        public Currency(string currencyCode, CurrencyFactory currencyFactory)
        {
            var currency = currencyFactory.CreateCurrency(currencyCode);

            Code = currency.Code;
            Name = currency.Name;
            Number = currency.Number;
            Sign = currency.Sign;
            DefaultFractionDigits = currency.DefaultFractionDigits;
            Countries = currency.Countries;
        }
        
        public Currency(string code, int defaultFractionDigits)
        {
            Code = code;
            Name = "";
            Number = "-1";
            Sign = "";
            DefaultFractionDigits = defaultFractionDigits;
            Countries = new HashSet<Country>();
        }
        
        public Currency(string code, string sign, int defaultFractionDigits)
        {
            Code = code;
            Name = "";
            Number = "";
            Sign = sign;
            DefaultFractionDigits = defaultFractionDigits;
            Countries = new HashSet<Country>();
        }
        
        public Currency(string code, string name, string sign, int defaultFractionDigits)
        {
            Code = code;
            Name = name;
            Number = "";
            Sign = sign;
            DefaultFractionDigits = defaultFractionDigits;
            Countries = new HashSet<Country>();
        }

        public Currency(string code, string name, string number, string sign, int defaultFractionDigits)
        {
            Code = code;
            Name = name;
            Number = number;
            Sign = sign;
            DefaultFractionDigits = defaultFractionDigits;
            Countries = new HashSet<Country>();
        }
        
        public Currency(string code, string name, string number, string sign, int defaultFractionDigits, HashSet<Country> countries) 
            : this(code, name, number, sign, defaultFractionDigits)
        {
            Countries = countries;
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

        public string Code { get; init;  }
        
        public string Name { get; init; }

        public string Number { get; init;  }

        public string Sign { get; init; }
        
        public int DefaultFractionDigits { get; init; }
        
        public HashSet<Country> Countries { get; init; }

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
        
        public const string AED = "AED";
        public const string AFN = "AFN";
        public const string ALL = "ALL";
        public const string AMD = "AMD";
        public const string ANG = "ANG";
        public const string AOA = "AOA";
        public const string ARS = "ARS";
        public const string AUD = "AUD";
        public const string AWG = "AWG";
        public const string AZN = "AZN";
        public const string BAM = "BAM";
        public const string BBD = "BBD";
        public const string BDT = "BDT";
        public const string BGN = "BGN";
        public const string BHD = "BHD";
        public const string BIF = "BIF";
        public const string BMD = "BMD";
        public const string BND = "BND";
        public const string BOB = "BOB";
        public const string BOV = "BOV";
        public const string BRL = "BRL";
        public const string BSD = "BSD";
        public const string BTN = "BTN";
        public const string BWP = "BWP";
        public const string BYN = "BYN";
        public const string BZD = "BZD";
        public const string CAD = "CAD";
        public const string CDF = "CDF";
        public const string CHE = "CHE";
        public const string CHF = "CHF";
        public const string CHW = "CHW";
        public const string CLF = "CLF";
        public const string CLP = "CLP";
        public const string CNY = "CNY";
        public const string COP = "COP";
        public const string COU = "COU";
        public const string CRC = "CRC";
        public const string CUC = "CUC";
        public const string CUP = "CUP";
        public const string CVE = "CVE";
        public const string CZK = "CZK";
        public const string DJF = "DJF";
        public const string DKK = "DKK";
        public const string DOP = "DOP";
        public const string DZD = "DZD";
        public const string EGP = "EGP";
        public const string ERN = "ERN";
        public const string ETB = "ETB";
        public const string EUR = "EUR";
        public const string FJD = "FJD";
        public const string FKP = "FKP";
        public const string GBP = "GBP";
        public const string GEL = "GEL";
        public const string GHS = "GHS";
        public const string GIP = "GIP";
        public const string GMD = "GMD";
        public const string GNF = "GNF";
        public const string GTQ = "GTQ";
        public const string GYD = "GYD";
        public const string HKD = "HKD";
        public const string HNL = "HNL";
        public const string HRK = "HRK";
        public const string HTG = "HTG";
        public const string HUF = "HUF";
        public const string IDR = "IDR";
        public const string ILS = "ILS";
        public const string INR = "INR";
        public const string IQD = "IQD";
        public const string IRR = "IRR";
        public const string ISK = "ISK";
        public const string JMD = "JMD";
        public const string JOD = "JOD";
        public const string JPY = "JPY";
        public const string KES = "KES";
        public const string KGS = "KGS";
        public const string KHR = "KHR";
        public const string KMF = "KMF";
        public const string KPW = "KPW";
        public const string KRW = "KRW";
        public const string KWD = "KWD";
        public const string KYD = "KYD";
        public const string KZT = "KZT";
        public const string LAK = "LAK";
        public const string LBP = "LBP";
        public const string LKR = "LKR";
        public const string LRD = "LRD";
        public const string LSL = "LSL";
        public const string LYD = "LYD";
        public const string MAD = "MAD";
        public const string MDL = "MDL";
        public const string MGA = "MGA";
        public const string MKD = "MKD";
        public const string MMK = "MMK";
        public const string MNT = "MNT";
        public const string MOP = "MOP";
        public const string MRU = "MRU";
        public const string MUR = "MUR";
        public const string MVR = "MVR";
        public const string MWK = "MWK";
        public const string MXN = "MXN";
        public const string MXV = "MXV";
        public const string MYR = "MYR";
        public const string MZN = "MZN";
        public const string NAD = "NAD";
        public const string NGN = "NGN";
        public const string NIO = "NIO";
        public const string NOK = "NOK";
        public const string NPR = "NPR";
        public const string NZD = "NZD";
        public const string OMR = "OMR";
        public const string PAB = "PAB";
        public const string PEN = "PEN";
        public const string PGK = "PGK";
        public const string PHP = "PHP";
        public const string PKR = "PKR";
        public const string PLN = "PLN";
        public const string PYG = "PYG";
        public const string QAR = "QAR";
        public const string RON = "RON";
        public const string RSD = "RSD";
        public const string RUB = "RUB";
        public const string RWF = "RWF";
        public const string SAR = "SAR";
        public const string SBD = "SBD";
        public const string SCR = "SCR";
        public const string SDG = "SDG";
        public const string SEK = "SEK";
        public const string SGD = "SGD";
        public const string SHP = "SHP";
        public const string SLL = "SLL";
        public const string SOS = "SOS";
        public const string SRD = "SRD";
        public const string SSP = "SSP";
        public const string STN = "STN";
        public const string SVC = "SVC";
        public const string SYP = "SYP";
        public const string SZL = "SZL";
        public const string THB = "THB";
        public const string TJS = "TJS";
        public const string TMT = "TMT";
        public const string TND = "TND";
        public const string TOP = "TOP";
        public const string TRY = "TRY";
        public const string TTD = "TTD";
        public const string TWD = "TWD";
        public const string TZS = "TZS";
        public const string UAH = "UAH";
        public const string UGX = "UGX";
        public const string USD = "USD";
        public const string USN = "USN";
        public const string UYI = "UYI";
        public const string UYU = "UYU";
        public const string UYW = "UYW";
        public const string UZS = "UZS";
        public const string VES = "VES";
        public const string VND = "VND";
        public const string VUV = "VUV";
        public const string WST = "WST";
        public const string XAF = "XAF";
        public const string XCD = "XCD";
        public const string XOF = "XOF";
        public const string XPF = "XPF";
        public const string YER = "YER";
        public const string ZAR = "ZAR";
        public const string ZMW = "ZMW";
        public const string ZWL = "ZWL";
    }
}