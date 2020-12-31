// ReSharper disable InconsistentNaming
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EagleMoney.NET.Library
{
    public class Currency : ICurrency, IEquatable<Currency>
    {
        public Currency(string currencyCode) : this(currencyCode, new CurrencyProvider())
        {
        }
        
        public Currency(string currencyCode, ICurrencyProvider currencyProvider)
        {
            Code = currencyCode;
            
            var selectedCurrency = currencyProvider
                .GetCurrencies()
                .FirstOrDefault(x => x.Code == currencyCode);

            if (selectedCurrency == null)
            {
                throw new InvalidOperationException(
                    $"No currency {currencyCode} exists in the list. Use the overloaded constructor to define custom currency.");
            }

            Number = selectedCurrency.Number;
            Sign = selectedCurrency.Sign;
            DefaultFractionDigits = selectedCurrency.DefaultFractionDigits;
            Countries = selectedCurrency.Countries;
        }

        public Currency(string code, string number, string sign, int defaultFractionDigits)
        {
            Code = code;
            Number = number;
            Sign = sign;
            DefaultFractionDigits = defaultFractionDigits;
            Countries = new HashSet<string>();
        }
        
        public Currency(string code, string number, string sign, int defaultFractionDigits, HashSet<string> countries) 
            : this(code, number, sign, defaultFractionDigits)
        {
            Countries = countries;
        }

        public string Code { get; init;  }

        public string Number { get; init;  }

        public string Sign { get; init; }
        
        public int DefaultFractionDigits { get; init; }
        
        public HashSet<string> Countries { get; init; }

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
            => Code == other?.Code && 
               Number == other?.Number &&
               Sign == other?.Sign && 
               DefaultFractionDigits == (other?.DefaultFractionDigits ?? -1);

        public override bool Equals(object other)
        {
            var otherCurrency = other as Currency;
            return !object.ReferenceEquals(otherCurrency, null) && Equals(otherCurrency);
        }

        public override int GetHashCode()
            => HashCode.Combine(Code, Number, Sign, DefaultFractionDigits);

        public static bool operator ==(Currency c1, Currency c2)
            => object.ReferenceEquals(c1, null) ? object.ReferenceEquals(c2, null) : c1.Equals(c2);

        public static bool operator !=(Currency? c1, Currency? c2)
            =>!(c1 == c2);
        
        public static string AED => "AED";
        public static string AFN => "AFN";
        public static string ALL => "ALL";
        public static string AMD => "AMD";
        public static string ANG => "ANG";
        public static string AOA => "AOA";
        public static string ARS => "ARS";
        public static string AUD => "AUD";
        public static string AWG => "AWG";
        public static string AZN => "AZN";
        public static string BAM => "BAM";
        public static string BBD => "BBD";
        public static string BDT => "BDT";
        public static string BGN => "BGN";
        public static string BHD => "BHD";
        public static string BIF => "BIF";
        public static string BMD => "BMD";
        public static string BND => "BND";
        public static string BOB => "BOB";
        public static string BOV => "BOV";
        public static string BRL => "BRL";
        public static string BSD => "BSD";
        public static string BTN => "BTN";
        public static string BWP => "BWP";
        public static string BYN => "BYN";
        public static string BZD => "BZD";
        public static string CAD => "CAD";
        public static string CDF => "CDF";
        public static string CHE => "CHE";
        public static string CHF => "CHF";
        public static string CHW => "CHW";
        public static string CLF => "CLF";
        public static string CLP => "CLP";
        public static string CNY => "CNY";
        public static string COP => "COP";
        public static string COU => "COU";
        public static string CRC => "CRC";
        public static string CUC => "CUC";
        public static string CUP => "CUP";
        public static string CVE => "CVE";
        public static string CZK => "CZK";
        public static string DJF => "DJF";
        public static string DKK => "DKK";
        public static string DOP => "DOP";
        public static string DZD => "DZD";
        public static string EGP => "EGP";
        public static string ERN => "ERN";
        public static string ETB => "ETB";
        public static string EUR => "EUR";
        public static string FJD => "FJD";
        public static string FKP => "FKP";
        public static string GBP => "GBP";
        public static string GEL => "GEL";
        public static string GHS => "GHS";
        public static string GIP => "GIP";
        public static string GMD => "GMD";
        public static string GNF => "GNF";
        public static string GTQ => "GTQ";
        public static string GYD => "GYD";
        public static string HKD => "HKD";
        public static string HNL => "HNL";
        public static string HRK => "HRK";
        public static string HTG => "HTG";
        public static string HUF => "HUF";
        public static string IDR => "IDR";
        public static string ILS => "ILS";
        public static string INR => "INR";
        public static string IQD => "IQD";
        public static string IRR => "IRR";
        public static string ISK => "ISK";
        public static string JMD => "JMD";
        public static string JOD => "JOD";
        public static string JPY => "JPY";
        public static string KES => "KES";
        public static string KGS => "KGS";
        public static string KHR => "KHR";
        public static string KMF => "KMF";
        public static string KPW => "KPW";
        public static string KRW => "KRW";
        public static string KWD => "KWD";
        public static string KYD => "KYD";
        public static string KZT => "KZT";
        public static string LAK => "LAK";
        public static string LBP => "LBP";
        public static string LKR => "LKR";
        public static string LRD => "LRD";
        public static string LSL => "LSL";
        public static string LYD => "LYD";
        public static string MAD => "MAD";
        public static string MDL => "MDL";
        public static string MGA => "MGA";
        public static string MKD => "MKD";
        public static string MMK => "MMK";
        public static string MNT => "MNT";
        public static string MOP => "MOP";
        public static string MRU => "MRU";
        public static string MUR => "MUR";
        public static string MVR => "MVR";
        public static string MWK => "MWK";
        public static string MXN => "MXN";
        public static string MXV => "MXV";
        public static string MYR => "MYR";
        public static string MZN => "MZN";
        public static string NAD => "NAD";
        public static string NGN => "NGN";
        public static string NIO => "NIO";
        public static string NOK => "NOK";
        public static string NPR => "NPR";
        public static string NZD => "NZD";
        public static string OMR => "OMR";
        public static string PAB => "PAB";
        public static string PEN => "PEN";
        public static string PGK => "PGK";
        public static string PHP => "PHP";
        public static string PKR => "PKR";
        public static string PLN => "PLN";
        public static string PYG => "PYG";
        public static string QAR => "QAR";
        public static string RON => "RON";
        public static string RSD => "RSD";
        public static string RUB => "RUB";
        public static string RWF => "RWF";
        public static string SAR => "SAR";
        public static string SBD => "SBD";
        public static string SCR => "SCR";
        public static string SDG => "SDG";
        public static string SEK => "SEK";
        public static string SGD => "SGD";
        public static string SHP => "SHP";
        public static string SLL => "SLL";
        public static string SOS => "SOS";
        public static string SRD => "SRD";
        public static string SSP => "SSP";
        public static string STN => "STN";
        public static string SVC => "SVC";
        public static string SYP => "SYP";
        public static string SZL => "SZL";
        public static string THB => "THB";
        public static string TJS => "TJS";
        public static string TMT => "TMT";
        public static string TND => "TND";
        public static string TOP => "TOP";
        public static string TRY => "TRY";
        public static string TTD => "TTD";
        public static string TWD => "TWD";
        public static string TZS => "TZS";
        public static string UAH => "UAH";
        public static string UGX => "UGX";
        public static string USD => "USD";
        public static string USN => "USN";
        public static string UYI => "UYI";
        public static string UYU => "UYU";
        public static string UYW => "UYW";
        public static string UZS => "UZS";
        public static string VES => "VES";
        public static string VND => "VND";
        public static string VUV => "VUV";
        public static string WST => "WST";
        public static string XAF => "XAF";
        public static string XCD => "XCD";
        public static string XOF => "XOF";
        public static string XPF => "XPF";
        public static string YER => "YER";
        public static string ZAR => "ZAR";
        public static string ZMW => "ZMW";
        public static string ZWL => "ZWL";
    }
}