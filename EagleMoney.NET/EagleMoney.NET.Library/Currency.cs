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
            var selectedCurrency = CurrencyTools.GetCurrencies()
                .FirstOrDefault(x => x.Code == currencyCode);

            if (selectedCurrency == default(Currency))
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
        {
            Code = code;
            Number = number;
            Sign = sign;
            DefaultFractionDigits = defaultFractionDigits;
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
        
        public static readonly string AED = CurrencyCode.AED.ToString();
        public static readonly string AFN = CurrencyCode.AFN.ToString();
        public static readonly string ALL = CurrencyCode.ALL.ToString();
        public static readonly string AMD = CurrencyCode.AMD.ToString();
        public static readonly string ANG = CurrencyCode.ANG.ToString();
        public static readonly string AOA = CurrencyCode.AOA.ToString();
        public static readonly string ARS = CurrencyCode.ARS.ToString();
        public static readonly string AUD = CurrencyCode.AUD.ToString();
        public static readonly string AWG = CurrencyCode.AWG.ToString();
        public static readonly string AZN = CurrencyCode.AZN.ToString();
        public static readonly string BAM = CurrencyCode.BAM.ToString();
        public static readonly string BBD = CurrencyCode.BBD.ToString();
        public static readonly string BDT = CurrencyCode.BDT.ToString();
        public static readonly string BGN = CurrencyCode.BGN.ToString();
        public static readonly string BHD = CurrencyCode.BHD.ToString();
        public static readonly string BIF = CurrencyCode.BIF.ToString();
        public static readonly string BMD = CurrencyCode.BMD.ToString();
        public static readonly string BND = CurrencyCode.BND.ToString();
        public static readonly string BOB = CurrencyCode.BOB.ToString();
        public static readonly string BOV = CurrencyCode.BOV.ToString();
        public static readonly string BRL = CurrencyCode.BRL.ToString();
        public static readonly string BSD = CurrencyCode.BSD.ToString();
        public static readonly string BTN = CurrencyCode.BTN.ToString();
        public static readonly string BWP = CurrencyCode.BWP.ToString();
        public static readonly string BYN = CurrencyCode.BYN.ToString();
        public static readonly string BZD = CurrencyCode.BZD.ToString();
        public static readonly string CAD = CurrencyCode.CAD.ToString();
        public static readonly string CDF = CurrencyCode.CDF.ToString();
        public static readonly string CHE = CurrencyCode.CHE.ToString();
        public static readonly string CHF = CurrencyCode.CHF.ToString();
        public static readonly string CHW = CurrencyCode.CHW.ToString();
        public static readonly string CLF = CurrencyCode.CLF.ToString();
        public static readonly string CLP = CurrencyCode.CLP.ToString();
        public static readonly string CNY = CurrencyCode.CNY.ToString();
        public static readonly string COP = CurrencyCode.COP.ToString();
        public static readonly string COU = CurrencyCode.COU.ToString();
        public static readonly string CRC = CurrencyCode.CRC.ToString();
        public static readonly string CUC = CurrencyCode.CUC.ToString();
        public static readonly string CUP = CurrencyCode.CUP.ToString();
        public static readonly string CVE = CurrencyCode.CVE.ToString();
        public static readonly string CZK = CurrencyCode.CZK.ToString();
        public static readonly string DJF = CurrencyCode.DJF.ToString();
        public static readonly string DKK = CurrencyCode.DKK.ToString();
        public static readonly string DOP = CurrencyCode.DOP.ToString();
        public static readonly string DZD = CurrencyCode.DZD.ToString();
        public static readonly string EGP = CurrencyCode.EGP.ToString();
        public static readonly string ERN = CurrencyCode.ERN.ToString();
        public static readonly string ETB = CurrencyCode.ETB.ToString();
        public static readonly string EUR = CurrencyCode.EUR.ToString();
        public static readonly string FJD = CurrencyCode.FJD.ToString();
        public static readonly string FKP = CurrencyCode.FKP.ToString();
        public static readonly string GBP = CurrencyCode.GBP.ToString();
        public static readonly string GEL = CurrencyCode.GEL.ToString();
        public static readonly string GHS = CurrencyCode.GHS.ToString();
        public static readonly string GIP = CurrencyCode.GIP.ToString();
        public static readonly string GMD = CurrencyCode.GMD.ToString();
        public static readonly string GNF = CurrencyCode.GNF.ToString();
        public static readonly string GTQ = CurrencyCode.GTQ.ToString();
        public static readonly string GYD = CurrencyCode.GYD.ToString();
        public static readonly string HKD = CurrencyCode.HKD.ToString();
        public static readonly string HNL = CurrencyCode.HNL.ToString();
        public static readonly string HRK = CurrencyCode.HRK.ToString();
        public static readonly string HTG = CurrencyCode.HTG.ToString();
        public static readonly string HUF = CurrencyCode.HUF.ToString();
        public static readonly string IDR = CurrencyCode.IDR.ToString();
        public static readonly string ILS = CurrencyCode.ILS.ToString();
        public static readonly string INR = CurrencyCode.INR.ToString();
        public static readonly string IQD = CurrencyCode.IQD.ToString();
        public static readonly string IRR = CurrencyCode.IRR.ToString();
        public static readonly string ISK = CurrencyCode.ISK.ToString();
        public static readonly string JMD = CurrencyCode.JMD.ToString();
        public static readonly string JOD = CurrencyCode.JOD.ToString();
        public static readonly string JPY = CurrencyCode.JPY.ToString();
        public static readonly string KES = CurrencyCode.KES.ToString();
        public static readonly string KGS = CurrencyCode.KGS.ToString();
        public static readonly string KHR = CurrencyCode.KHR.ToString();
        public static readonly string KMF = CurrencyCode.KMF.ToString();
        public static readonly string KPW = CurrencyCode.KPW.ToString();
        public static readonly string KRW = CurrencyCode.KRW.ToString();
        public static readonly string KWD = CurrencyCode.KWD.ToString();
        public static readonly string KYD = CurrencyCode.KYD.ToString();
        public static readonly string KZT = CurrencyCode.KZT.ToString();
        public static readonly string LAK = CurrencyCode.LAK.ToString();
        public static readonly string LBP = CurrencyCode.LBP.ToString();
        public static readonly string LKR = CurrencyCode.LKR.ToString();
        public static readonly string LRD = CurrencyCode.LRD.ToString();
        public static readonly string LSL = CurrencyCode.LSL.ToString();
        public static readonly string LYD = CurrencyCode.LYD.ToString();
        public static readonly string MAD = CurrencyCode.MAD.ToString();
        public static readonly string MDL = CurrencyCode.MDL.ToString();
        public static readonly string MGA = CurrencyCode.MGA.ToString();
        public static readonly string MKD = CurrencyCode.MKD.ToString();
        public static readonly string MMK = CurrencyCode.MMK.ToString();
        public static readonly string MNT = CurrencyCode.MNT.ToString();
        public static readonly string MOP = CurrencyCode.MOP.ToString();
        public static readonly string MRU = CurrencyCode.MRU.ToString();
        public static readonly string MUR = CurrencyCode.MUR.ToString();
        public static readonly string MVR = CurrencyCode.MVR.ToString();
        public static readonly string MWK = CurrencyCode.MWK.ToString();
        public static readonly string MXN = CurrencyCode.MXN.ToString();
        public static readonly string MXV = CurrencyCode.MXV.ToString();
        public static readonly string MYR = CurrencyCode.MYR.ToString();
        public static readonly string MZN = CurrencyCode.MZN.ToString();
        public static readonly string NAD = CurrencyCode.NAD.ToString();
        public static readonly string NGN = CurrencyCode.NGN.ToString();
        public static readonly string NIO = CurrencyCode.NIO.ToString();
        public static readonly string NOK = CurrencyCode.NOK.ToString();
        public static readonly string NPR = CurrencyCode.NPR.ToString();
        public static readonly string NZD = CurrencyCode.NZD.ToString();
        public static readonly string OMR = CurrencyCode.OMR.ToString();
        public static readonly string PAB = CurrencyCode.PAB.ToString();
        public static readonly string PEN = CurrencyCode.PEN.ToString();
        public static readonly string PGK = CurrencyCode.PGK.ToString();
        public static readonly string PHP = CurrencyCode.PHP.ToString();
        public static readonly string PKR = CurrencyCode.PKR.ToString();
        public static readonly string PLN = CurrencyCode.PLN.ToString();
        public static readonly string PYG = CurrencyCode.PYG.ToString();
        public static readonly string QAR = CurrencyCode.QAR.ToString();
        public static readonly string RON = CurrencyCode.RON.ToString();
        public static readonly string RSD = CurrencyCode.RSD.ToString();
        public static readonly string RUB = CurrencyCode.RUB.ToString();
        public static readonly string RWF = CurrencyCode.RWF.ToString();
        public static readonly string SAR = CurrencyCode.SAR.ToString();
        public static readonly string SBD = CurrencyCode.SBD.ToString();
        public static readonly string SCR = CurrencyCode.SCR.ToString();
        public static readonly string SDG = CurrencyCode.SDG.ToString();
        public static readonly string SEK = CurrencyCode.SEK.ToString();
        public static readonly string SGD = CurrencyCode.SGD.ToString();
        public static readonly string SHP = CurrencyCode.SHP.ToString();
        public static readonly string SLL = CurrencyCode.SLL.ToString();
        public static readonly string SOS = CurrencyCode.SOS.ToString();
        public static readonly string SRD = CurrencyCode.SRD.ToString();
        public static readonly string SSP = CurrencyCode.SSP.ToString();
        public static readonly string STN = CurrencyCode.STN.ToString();
        public static readonly string SVC = CurrencyCode.SVC.ToString();
        public static readonly string SYP = CurrencyCode.SYP.ToString();
        public static readonly string SZL = CurrencyCode.SZL.ToString();
        public static readonly string THB = CurrencyCode.THB.ToString();
        public static readonly string TJS = CurrencyCode.TJS.ToString();
        public static readonly string TMT = CurrencyCode.TMT.ToString();
        public static readonly string TND = CurrencyCode.TND.ToString();
        public static readonly string TOP = CurrencyCode.TOP.ToString();
        public static readonly string TRY = CurrencyCode.TRY.ToString();
        public static readonly string TTD = CurrencyCode.TTD.ToString();
        public static readonly string TWD = CurrencyCode.TWD.ToString();
        public static readonly string TZS = CurrencyCode.TZS.ToString();
        public static readonly string UAH = CurrencyCode.UAH.ToString();
        public static readonly string UGX = CurrencyCode.UGX.ToString();
        public static readonly string USD = CurrencyCode.USD.ToString();
        public static readonly string USN = CurrencyCode.USN.ToString();
        public static readonly string UYI = CurrencyCode.UYI.ToString();
        public static readonly string UYU = CurrencyCode.UYU.ToString();
        public static readonly string UYW = CurrencyCode.UYW.ToString();
        public static readonly string UZS = CurrencyCode.UZS.ToString();
        public static readonly string VES = CurrencyCode.VES.ToString();
        public static readonly string VND = CurrencyCode.VND.ToString();
        public static readonly string VUV = CurrencyCode.VUV.ToString();
        public static readonly string WST = CurrencyCode.WST.ToString();
        public static readonly string XAF = CurrencyCode.XAF.ToString();
        public static readonly string XCD = CurrencyCode.XCD.ToString();
        public static readonly string XOF = CurrencyCode.XOF.ToString();
        public static readonly string XPF = CurrencyCode.XPF.ToString();
        public static readonly string YER = CurrencyCode.YER.ToString();
        public static readonly string ZAR = CurrencyCode.ZAR.ToString();
        public static readonly string ZMW = CurrencyCode.ZMW.ToString();
        public static readonly string ZWL = CurrencyCode.ZWL.ToString();
    }
}