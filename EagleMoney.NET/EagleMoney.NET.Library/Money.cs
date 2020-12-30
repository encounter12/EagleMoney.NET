// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EagleMoney.NET.Library
{
    // TODO: bool Equals(Money other) - throw InvalidOperationException if the two currencies are different
    // discarded - if there is such validation List<T>.Contains(T) throws an error - List could be used as Money bag.
    
    // TODO: Implement Parse()
    // e.g. Money.Parse("180 USD"), Money.Parse("180USD"), Money.Parse("USD180.24"), Money.Parse("$180.24"), Money.Parse("180.24$")
    
    // TODO: Create static methods for all world currencies, e.g. Money.USD(decimal amount) - see ISO4217: https://www.iso.org/iso-4217-currency-codes.html
    // TODO: Add all world currencies details to array: worldCurrencies
        
    // TODO: public Property or method for getting the internal amount (_amount)
    
    // TODO: Add Arithmetic operations Pow(), Sqrt() - using Math.Pow(), Math.Sqrt()
        
    // TODO: Arithmetics with string numbers - Consider overloading constructor with string amount parameter
    // Implement arithmetics with numbers as strings
    
    // TODO: ToString() - Consider formatting - decimal separator (Globalization, Localization). Add ToString() with C1, C2 arguments
        
    // TODO: Consider adding constructor overload with parameter _amount type: double
    
    // TODO: Operator overloading (>, <, >=, <=) - add support for comparing Nullable Money objects
    
    // TODO: Currency: Add custom currencies to the list of worldCurrencies (for the scope of the variable)
    
    // TODO: Implement ToJson() method
    
    // TODO: Should money constructor allow negative values for amount?

    // TODO: Consider adding checked (OverflowException) for decimal 
    
    // TODO: Write unit tests (NUnit)
    
    // TODO: Write documentation on GitHub
    
    public readonly struct Money : IEquatable<Money>, IComparable<Money>, IComparable
    {
        private readonly BigInteger _amount;

        public Money(decimal amount, string currencyCode)
        {
            if (amount < 0M)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }

            var currency = new Currency(currencyCode);
            int centFactor = Cents[currency.DefaultFractionDigits];
            _amount = (BigInteger) Math.Round(amount * centFactor);
            Currency = currency;
        }
        
        public Money(decimal amount, Currency currency)
        {
            if (amount < 0M)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }

            int centFactor = Cents[currency.DefaultFractionDigits];
            _amount = (BigInteger) Math.Round(amount * centFactor);
            Currency = currency;
        }

        public Money(decimal amount, MidpointRounding mode, string currencyCode)
        {
            if (amount < 0M)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }

            var currency = new Currency(currencyCode);
            int centFactor = Cents[currency.DefaultFractionDigits];
            _amount = (BigInteger) Math.Round(amount * centFactor, mode);
            Currency = currency;
        }

        private Money(BigInteger amount, Currency currency)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }
            
            _amount = amount;
            Currency = currency;
        }
        
        private static int[] Cents => new[] {1, 10, 100, 1000};

        private int CentFactor => Cents[Currency.DefaultFractionDigits];

        public decimal Amount => (decimal)_amount / CentFactor;

        public Currency Currency { get; init; }

        //Credit: Martin Fowler and Matt Foemmel, Book: Patterns of Enterprise Application Architecture, p.494
        public Money[] AllocateEven(int n)
        {
            BigInteger[] allocatedInternalAmounts = AllocateCentsEven(_amount, n);

            var currency = Currency;
            var allocated = Array.ConvertAll(
                allocatedInternalAmounts, x => new Money(x, currency));
            
            return allocated;
        }

        private static BigInteger[] AllocateCentsEven(BigInteger centsAmount, int n)
        {
            BigInteger lowResult = centsAmount / n;
            BigInteger highResult = lowResult + 1;
            
            var results = new BigInteger[n];
            var remainder = (int)centsAmount % n;

            for (int i = 0; i < remainder; i++)
            {
                results[i] = highResult;
            }

            for (int i = remainder; i < n; i++)
            {
                results[i] = lowResult;
            }

            return results;
        }

        //Credit: Martin Fowler and Matt Foemmel, Book: Patterns of Enterprise Application Architecture, p.494
        public Money[] AllocateByRatios(int[] ratios)
        {
            BigInteger[] allocatedInternalAmounts = AllocateCentsByRatios(_amount, ratios);

            var currency = Currency;
            var allocated = Array.ConvertAll(
                allocatedInternalAmounts, x => new Money(x, currency));
            
            return allocated;
        }
        
        private static BigInteger[] AllocateCentsByRatios(BigInteger centsAmount, IReadOnlyList<int> ratios)
        {
            int total = ratios.Sum();

            BigInteger remainder = centsAmount;
            
            var results = new BigInteger[ratios.Count];

            for (int i = 0; i < results.Length; i++)
            {
                results[i] = centsAmount * ratios[i] / total;
                remainder -= results[i];
            }

            for (int i = 0; i < remainder; i++)
            {
                results[i]++;
            }
            
            return results;
        }

        public Money Percentage(decimal percent) 
            => new ((Amount / 100) * percent, Currency);

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(nameof(Money));
            stringBuilder.Append(" { ");

            PrintMembers(stringBuilder);

            stringBuilder.Append(" }");
            return stringBuilder.ToString();
        }

        private void PrintMembers(StringBuilder builder)
        {
            builder.Append(nameof(Amount));
            builder.Append(" = ");
            builder.Append(Amount);

            builder.Append(", ");

            builder.Append(nameof(Currency));
            builder.Append(" = ");
            builder.Append(Currency.ToString());
        }

        public string ToString(MoneyFormattingType formattingType)
        {
            var moneyString = formattingType switch
            {
                MoneyFormattingType.MoneyValueCurrencyCode => $"{Amount} {Currency.Code}",
                MoneyFormattingType.CurrencyCodeMoneyValue => $"{Currency.Code} {Amount}",
                _ => string.Empty
            };

            return moneyString;
        }

        public bool Equals(Money other)
            => Currency == other.Currency && Amount == other.Amount;

        public override bool Equals(object other)
        {
            var otherMoney = other as Money?;
            return otherMoney.HasValue && Equals(otherMoney.Value);
        }

        public override int GetHashCode()
            => HashCode.Combine(Amount, Currency);

        public int CompareTo(Money other)
        {
            if (Currency != other.Currency)
            {
                throw new InvalidOperationException("Cannot compare money of different currencies");
            }

            return Equals(other) ? 0 : Amount.CompareTo(other.Amount);
        }

        public int CompareTo(object other)
        {
            if (!(other is Money))
            {
                throw new InvalidOperationException("CompareTo() argument is not Money");
            }

            return CompareTo((Money) other);
        }

        public static bool operator ==(Money? m1, Money? m2)
            => m1.HasValue && m2.HasValue ? m1.Equals(m2) : !m1.HasValue && !m2.HasValue;

        public static bool operator !=(Money? m1, Money? m2)
            => m1.HasValue && m2.HasValue ? !m1.Equals(m2) : m1.HasValue ^ m2.HasValue;
        
        public static bool operator ==(Money? m1, decimal m2Value)
            => m1?.Amount.Equals(m2Value) ?? false;
        
        public static bool operator !=(Money? m1, decimal m2Value)
            => !m1?.Amount.Equals(m2Value) ?? false;
        
        public static bool operator ==(decimal m1Value, Money? m2)
            => m2?.Amount.Equals(m1Value) ?? false;
        
        public static bool operator !=(decimal m1Value, Money? m2)
            => !m2?.Amount.Equals(m1Value) ?? false;

        public static bool operator <(Money m1, Money m2)
            => m1.CompareTo(m2) < 0;

        public static bool operator >(Money m1, Money m2)
            => m1.CompareTo(m2) > 0;
        
        public static bool operator <(Money m1, decimal m2Value)
            => m1.Amount.CompareTo(m2Value) < 0;
        
        public static bool operator >(Money m1, decimal m2Value)
            => m1.Amount.CompareTo(m2Value) > 0;
        
        public static bool operator <(decimal m1Value, Money m2)
            => m1Value.CompareTo(m2.Amount) < 0;
        
        public static bool operator >(decimal m1Value, Money m2)
            => m1Value.CompareTo(m2.Amount) > 0;

        public static bool operator <=(Money m1, Money m2)
            => m1 == m2 || m1 < m2;

        public static bool operator >=(Money m1, Money m2)
            => m1 == m2 || m1 > m2;
        
        public static bool operator <=(Money m1, decimal m2Value)
            => m1.Amount == m2Value || m1.Amount < m2Value;
        
        public static bool operator >=(Money m1, decimal m2Value)
            => m1.Amount == m2Value || m1.Amount > m2Value;
        
        public static bool operator <=(decimal m1Value, Money m2)
            => m1Value == m2.Amount || m1Value < m2.Amount;
        
        public static bool operator >=(decimal m1Value, Money m2)
            => m1Value == m2.Amount || m1Value > m2.Amount;

        public static Money operator +(Money m1, Money m2)
        {
            if (m1.Currency != m2.Currency)
            {
                throw new InvalidOperationException("Cannot add money having different currencies");
            }

            return new Money(m1.Amount + m2.Amount, m1.Currency);
        }

        public static Money operator +(Money m1, decimal m2Value) 
            => new(m1.Amount + m2Value, m1.Currency);

        public static Money operator +(decimal m1Value, Money m2) 
            => new(m1Value + m2.Amount, m2.Currency);

        public static Money operator -(Money m1, Money m2)
        {
            if (m1.Currency != m2.Currency)
            {
                throw new InvalidOperationException("Cannot subtract money having different currencies");
            }

            return new Money(m1.Amount - m2.Amount, m1.Currency);
        }

        public static Money operator -(Money m1, decimal m2Value)
            => new(m1.Amount - m2Value, m1.Currency);

        public static Money operator -(decimal m1Value, Money m2)
            => new(m1Value - m2.Amount, m2.Currency);

        public static Money operator *(Money m1, Money m2)
        {
            if (m1.Currency != m2.Currency)
            {
                throw new InvalidOperationException("Cannot multiply money having different currencies");
            }

            return new Money(m1.Amount * m2.Amount, m1.Currency);
        }

        public static Money operator *(Money m1, decimal m2Value)
            => new(m1.Amount * m2Value, m1.Currency);

        public static Money operator *(decimal m1Value, Money m2)
            => new(m1Value * m2.Amount, m2.Currency);

        public static Money operator /(Money m1, Money m2)
        {
            if (m1.Currency != m2.Currency)
            {
                throw new InvalidOperationException("Cannot divide money having different currencies");
            }

            return new Money(m1.Amount / m2.Amount, m1.Currency);
        }

        public static Money operator /(Money m1, decimal m2Value)
            => new(m1.Amount / m2Value, m1.Currency);

        public static Money operator /(decimal m1Value, Money m2)
            => new(m1Value / m2.Amount, m2.Currency);

        public static Money operator %(Money m, int divisor)
            => new(m.Amount % divisor, m.Currency);
        
        public static Money operator +(Money m)
            => new(m.Amount, m.Currency);
        
        public static Money operator -(Money m)
            => new(-m.Amount, m.Currency);

        public static Money operator ++(Money m)
            => new(m.Amount + 1M, m.Currency);

        public static Money operator --(Money m)
            => new(m.Amount - 1M, m.Currency);

        public static explicit operator decimal(Money m) => m.Amount;
        
        public static Money AED(decimal amount) => new (amount, Currency.AED);
        public static Money AFN(decimal amount) => new (amount, Currency.AFN);
        public static Money ALL(decimal amount) => new (amount, Currency.ALL);
        public static Money AMD(decimal amount) => new (amount, Currency.AMD);
        public static Money ANG(decimal amount) => new (amount, Currency.ANG);
        public static Money AOA(decimal amount) => new (amount, Currency.AOA);
        public static Money ARS(decimal amount) => new (amount, Currency.ARS);
        public static Money AUD(decimal amount) => new (amount, Currency.AUD);
        public static Money AWG(decimal amount) => new (amount, Currency.AWG);
        public static Money AZN(decimal amount) => new (amount, Currency.AZN);
        public static Money BAM(decimal amount) => new (amount, Currency.BAM);
        public static Money BBD(decimal amount) => new (amount, Currency.BBD);
        public static Money BDT(decimal amount) => new (amount, Currency.BDT);
        public static Money BGN(decimal amount) => new (amount, Currency.BGN);
        public static Money BHD(decimal amount) => new (amount, Currency.BHD);
        public static Money BIF(decimal amount) => new (amount, Currency.BIF);
        public static Money BMD(decimal amount) => new (amount, Currency.BMD);
        public static Money BND(decimal amount) => new (amount, Currency.BND);
        public static Money BOB(decimal amount) => new (amount, Currency.BOB);
        public static Money BOV(decimal amount) => new (amount, Currency.BOV);
        public static Money BRL(decimal amount) => new (amount, Currency.BRL);
        public static Money BSD(decimal amount) => new (amount, Currency.BSD);
        public static Money BTN(decimal amount) => new (amount, Currency.BTN);
        public static Money BWP(decimal amount) => new (amount, Currency.BWP);
        public static Money BYN(decimal amount) => new (amount, Currency.BYN);
        public static Money BZD(decimal amount) => new (amount, Currency.BZD);
        public static Money CAD(decimal amount) => new (amount, Currency.CAD);
        public static Money CDF(decimal amount) => new (amount, Currency.CDF);
        public static Money CHE(decimal amount) => new (amount, Currency.CHE);
        public static Money CHF(decimal amount) => new (amount, Currency.CHF);
        public static Money CHW(decimal amount) => new (amount, Currency.CHW);
        public static Money CLF(decimal amount) => new (amount, Currency.CLF);
        public static Money CLP(decimal amount) => new (amount, Currency.CLP);
        public static Money CNY(decimal amount) => new (amount, Currency.CNY);
        public static Money COP(decimal amount) => new (amount, Currency.COP);
        public static Money COU(decimal amount) => new (amount, Currency.COU);
        public static Money CRC(decimal amount) => new (amount, Currency.CRC);
        public static Money CUC(decimal amount) => new (amount, Currency.CUC);
        public static Money CUP(decimal amount) => new (amount, Currency.CUP);
        public static Money CVE(decimal amount) => new (amount, Currency.CVE);
        public static Money CZK(decimal amount) => new (amount, Currency.CZK);
        public static Money DJF(decimal amount) => new (amount, Currency.DJF);
        public static Money DKK(decimal amount) => new (amount, Currency.DKK);
        public static Money DOP(decimal amount) => new (amount, Currency.DOP);
        public static Money DZD(decimal amount) => new (amount, Currency.DZD);
        public static Money EGP(decimal amount) => new (amount, Currency.EGP);
        public static Money ERN(decimal amount) => new (amount, Currency.ERN);
        public static Money ETB(decimal amount) => new (amount, Currency.ETB);
        public static Money EUR(decimal amount) => new (amount, Currency.EUR);
        public static Money FJD(decimal amount) => new (amount, Currency.FJD);
        public static Money FKP(decimal amount) => new (amount, Currency.FKP);
        public static Money GBP(decimal amount) => new (amount, Currency.GBP);
        public static Money GEL(decimal amount) => new (amount, Currency.GEL);
        public static Money GHS(decimal amount) => new (amount, Currency.GHS);
        public static Money GIP(decimal amount) => new (amount, Currency.GIP);
        public static Money GMD(decimal amount) => new (amount, Currency.GMD);
        public static Money GNF(decimal amount) => new (amount, Currency.GNF);
        public static Money GTQ(decimal amount) => new (amount, Currency.GTQ);
        public static Money GYD(decimal amount) => new (amount, Currency.GYD);
        public static Money HKD(decimal amount) => new (amount, Currency.HKD);
        public static Money HNL(decimal amount) => new (amount, Currency.HNL);
        public static Money HRK(decimal amount) => new (amount, Currency.HRK);
        public static Money HTG(decimal amount) => new (amount, Currency.HTG);
        public static Money HUF(decimal amount) => new (amount, Currency.HUF);
        public static Money IDR(decimal amount) => new (amount, Currency.IDR);
        public static Money ILS(decimal amount) => new (amount, Currency.ILS);
        public static Money INR(decimal amount) => new (amount, Currency.INR);
        public static Money IQD(decimal amount) => new (amount, Currency.IQD);
        public static Money IRR(decimal amount) => new (amount, Currency.IRR);
        public static Money ISK(decimal amount) => new (amount, Currency.ISK);
        public static Money JMD(decimal amount) => new (amount, Currency.JMD);
        public static Money JOD(decimal amount) => new (amount, Currency.JOD);
        public static Money JPY(decimal amount) => new (amount, Currency.JPY);
        public static Money KES(decimal amount) => new (amount, Currency.KES);
        public static Money KGS(decimal amount) => new (amount, Currency.KGS);
        public static Money KHR(decimal amount) => new (amount, Currency.KHR);
        public static Money KMF(decimal amount) => new (amount, Currency.KMF);
        public static Money KPW(decimal amount) => new (amount, Currency.KPW);
        public static Money KRW(decimal amount) => new (amount, Currency.KRW);
        public static Money KWD(decimal amount) => new (amount, Currency.KWD);
        public static Money KYD(decimal amount) => new (amount, Currency.KYD);
        public static Money KZT(decimal amount) => new (amount, Currency.KZT);
        public static Money LAK(decimal amount) => new (amount, Currency.LAK);
        public static Money LBP(decimal amount) => new (amount, Currency.LBP);
        public static Money LKR(decimal amount) => new (amount, Currency.LKR);
        public static Money LRD(decimal amount) => new (amount, Currency.LRD);
        public static Money LSL(decimal amount) => new (amount, Currency.LSL);
        public static Money LYD(decimal amount) => new (amount, Currency.LYD);
        public static Money MAD(decimal amount) => new (amount, Currency.MAD);
        public static Money MDL(decimal amount) => new (amount, Currency.MDL);
        public static Money MGA(decimal amount) => new (amount, Currency.MGA);
        public static Money MKD(decimal amount) => new (amount, Currency.MKD);
        public static Money MMK(decimal amount) => new (amount, Currency.MMK);
        public static Money MNT(decimal amount) => new (amount, Currency.MNT);
        public static Money MOP(decimal amount) => new (amount, Currency.MOP);
        public static Money MRU(decimal amount) => new (amount, Currency.MRU);
        public static Money MUR(decimal amount) => new (amount, Currency.MUR);
        public static Money MVR(decimal amount) => new (amount, Currency.MVR);
        public static Money MWK(decimal amount) => new (amount, Currency.MWK);
        public static Money MXN(decimal amount) => new (amount, Currency.MXN);
        public static Money MXV(decimal amount) => new (amount, Currency.MXV);
        public static Money MYR(decimal amount) => new (amount, Currency.MYR);
        public static Money MZN(decimal amount) => new (amount, Currency.MZN);
        public static Money NAD(decimal amount) => new (amount, Currency.NAD);
        public static Money NGN(decimal amount) => new (amount, Currency.NGN);
        public static Money NIO(decimal amount) => new (amount, Currency.NIO);
        public static Money NOK(decimal amount) => new (amount, Currency.NOK);
        public static Money NPR(decimal amount) => new (amount, Currency.NPR);
        public static Money NZD(decimal amount) => new (amount, Currency.NZD);
        public static Money OMR(decimal amount) => new (amount, Currency.OMR);
        public static Money PAB(decimal amount) => new (amount, Currency.PAB);
        public static Money PEN(decimal amount) => new (amount, Currency.PEN);
        public static Money PGK(decimal amount) => new (amount, Currency.PGK);
        public static Money PHP(decimal amount) => new (amount, Currency.PHP);
        public static Money PKR(decimal amount) => new (amount, Currency.PKR);
        public static Money PLN(decimal amount) => new (amount, Currency.PLN);
        public static Money PYG(decimal amount) => new (amount, Currency.PYG);
        public static Money QAR(decimal amount) => new (amount, Currency.QAR);
        public static Money RON(decimal amount) => new (amount, Currency.RON);
        public static Money RSD(decimal amount) => new (amount, Currency.RSD);
        public static Money RUB(decimal amount) => new (amount, Currency.RUB);
        public static Money RWF(decimal amount) => new (amount, Currency.RWF);
        public static Money SAR(decimal amount) => new (amount, Currency.SAR);
        public static Money SBD(decimal amount) => new (amount, Currency.SBD);
        public static Money SCR(decimal amount) => new (amount, Currency.SCR);
        public static Money SDG(decimal amount) => new (amount, Currency.SDG);
        public static Money SEK(decimal amount) => new (amount, Currency.SEK);
        public static Money SGD(decimal amount) => new (amount, Currency.SGD);
        public static Money SHP(decimal amount) => new (amount, Currency.SHP);
        public static Money SLL(decimal amount) => new (amount, Currency.SLL);
        public static Money SOS(decimal amount) => new (amount, Currency.SOS);
        public static Money SRD(decimal amount) => new (amount, Currency.SRD);
        public static Money SSP(decimal amount) => new (amount, Currency.SSP);
        public static Money STN(decimal amount) => new (amount, Currency.STN);
        public static Money SVC(decimal amount) => new (amount, Currency.SVC);
        public static Money SYP(decimal amount) => new (amount, Currency.SYP);
        public static Money SZL(decimal amount) => new (amount, Currency.SZL);
        public static Money THB(decimal amount) => new (amount, Currency.THB);
        public static Money TJS(decimal amount) => new (amount, Currency.TJS);
        public static Money TMT(decimal amount) => new (amount, Currency.TMT);
        public static Money TND(decimal amount) => new (amount, Currency.TND);
        public static Money TOP(decimal amount) => new (amount, Currency.TOP);
        public static Money TRY(decimal amount) => new (amount, Currency.TRY);
        public static Money TTD(decimal amount) => new (amount, Currency.TTD);
        public static Money TWD(decimal amount) => new (amount, Currency.TWD);
        public static Money TZS(decimal amount) => new (amount, Currency.TZS);
        public static Money UAH(decimal amount) => new (amount, Currency.UAH);
        public static Money UGX(decimal amount) => new (amount, Currency.UGX);
        public static Money USD(decimal amount) => new (amount, Currency.USD);
        public static Money USN(decimal amount) => new (amount, Currency.USN);
        public static Money UYI(decimal amount) => new (amount, Currency.UYI);
        public static Money UYU(decimal amount) => new (amount, Currency.UYU);
        public static Money UYW(decimal amount) => new (amount, Currency.UYW);
        public static Money UZS(decimal amount) => new (amount, Currency.UZS);
        public static Money VES(decimal amount) => new (amount, Currency.VES);
        public static Money VND(decimal amount) => new (amount, Currency.VND);
        public static Money VUV(decimal amount) => new (amount, Currency.VUV);
        public static Money WST(decimal amount) => new (amount, Currency.WST);
        public static Money XAF(decimal amount) => new (amount, Currency.XAF);
        public static Money XCD(decimal amount) => new (amount, Currency.XCD);
        public static Money XOF(decimal amount) => new (amount, Currency.XOF);
        public static Money XPF(decimal amount) => new (amount, Currency.XPF);
        public static Money YER(decimal amount) => new (amount, Currency.YER);
        public static Money ZAR(decimal amount) => new (amount, Currency.ZAR);
        public static Money ZMW(decimal amount) => new (amount, Currency.ZMW);
        public static Money ZWL(decimal amount) => new (amount, Currency.ZWL);
        
        
        public static Money AED(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.AED);
        public static Money AFN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.AFN);
        public static Money ALL(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.ALL);
        public static Money AMD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.AMD);
        public static Money ANG(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.ANG);
        public static Money AOA(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.AOA);
        public static Money ARS(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.ARS);
        public static Money AUD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.AUD);
        public static Money AWG(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.AWG);
        public static Money AZN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.AZN);
        public static Money BAM(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BAM);
        public static Money BBD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BBD);
        public static Money BDT(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BDT);
        public static Money BGN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BGN);
        public static Money BHD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BHD);
        public static Money BIF(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BIF);
        public static Money BMD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BMD);
        public static Money BND(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BND);
        public static Money BOB(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BOB);
        public static Money BOV(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BOV);
        public static Money BRL(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BRL);
        public static Money BSD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BSD);
        public static Money BTN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BTN);
        public static Money BWP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BWP);
        public static Money BYN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BYN);
        public static Money BZD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.BZD);
        public static Money CAD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CAD);
        public static Money CDF(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CDF);
        public static Money CHE(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CHE);
        public static Money CHF(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CHF);
        public static Money CHW(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CHW);
        public static Money CLF(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CLF);
        public static Money CLP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CLP);
        public static Money CNY(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CNY);
        public static Money COP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.COP);
        public static Money COU(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.COU);
        public static Money CRC(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CRC);
        public static Money CUC(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CUC);
        public static Money CUP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CUP);
        public static Money CVE(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CVE);
        public static Money CZK(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.CZK);
        public static Money DJF(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.DJF);
        public static Money DKK(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.DKK);
        public static Money DOP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.DOP);
        public static Money DZD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.DZD);
        public static Money EGP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.EGP);
        public static Money ERN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.ERN);
        public static Money ETB(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.ETB);
        public static Money EUR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.EUR);
        public static Money FJD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.FJD);
        public static Money FKP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.FKP);
        public static Money GBP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.GBP);
        public static Money GEL(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.GEL);
        public static Money GHS(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.GHS);
        public static Money GIP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.GIP);
        public static Money GMD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.GMD);
        public static Money GNF(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.GNF);
        public static Money GTQ(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.GTQ);
        public static Money GYD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.GYD);
        public static Money HKD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.HKD);
        public static Money HNL(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.HNL);
        public static Money HRK(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.HRK);
        public static Money HTG(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.HTG);
        public static Money HUF(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.HUF);
        public static Money IDR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.IDR);
        public static Money ILS(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.ILS);
        public static Money INR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.INR);
        public static Money IQD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.IQD);
        public static Money IRR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.IRR);
        public static Money ISK(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.ISK);
        public static Money JMD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.JMD);
        public static Money JOD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.JOD);
        public static Money JPY(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.JPY);
        public static Money KES(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.KES);
        public static Money KGS(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.KGS);
        public static Money KHR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.KHR);
        public static Money KMF(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.KMF);
        public static Money KPW(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.KPW);
        public static Money KRW(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.KRW);
        public static Money KWD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.KWD);
        public static Money KYD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.KYD);
        public static Money KZT(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.KZT);
        public static Money LAK(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.LAK);
        public static Money LBP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.LBP);
        public static Money LKR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.LKR);
        public static Money LRD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.LRD);
        public static Money LSL(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.LSL);
        public static Money LYD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.LYD);
        public static Money MAD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MAD);
        public static Money MDL(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MDL);
        public static Money MGA(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MGA);
        public static Money MKD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MKD);
        public static Money MMK(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MMK);
        public static Money MNT(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MNT);
        public static Money MOP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MOP);
        public static Money MRU(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MRU);
        public static Money MUR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MUR);
        public static Money MVR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MVR);
        public static Money MWK(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MWK);
        public static Money MXN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MXN);
        public static Money MXV(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MXV);
        public static Money MYR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MYR);
        public static Money MZN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.MZN);
        public static Money NAD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.NAD);
        public static Money NGN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.NGN);
        public static Money NIO(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.NIO);
        public static Money NOK(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.NOK);
        public static Money NPR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.NPR);
        public static Money NZD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.NZD);
        public static Money OMR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.OMR);
        public static Money PAB(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.PAB);
        public static Money PEN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.PEN);
        public static Money PGK(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.PGK);
        public static Money PHP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.PHP);
        public static Money PKR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.PKR);
        public static Money PLN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.PLN);
        public static Money PYG(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.PYG);
        public static Money QAR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.QAR);
        public static Money RON(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.RON);
        public static Money RSD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.RSD);
        public static Money RUB(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.RUB);
        public static Money RWF(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.RWF);
        public static Money SAR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SAR);
        public static Money SBD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SBD);
        public static Money SCR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SCR);
        public static Money SDG(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SDG);
        public static Money SEK(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SEK);
        public static Money SGD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SGD);
        public static Money SHP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SHP);
        public static Money SLL(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SLL);
        public static Money SOS(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SOS);
        public static Money SRD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SRD);
        public static Money SSP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SSP);
        public static Money STN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.STN);
        public static Money SVC(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SVC);
        public static Money SYP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SYP);
        public static Money SZL(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.SZL);
        public static Money THB(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.THB);
        public static Money TJS(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.TJS);
        public static Money TMT(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.TMT);
        public static Money TND(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.TND);
        public static Money TOP(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.TOP);
        public static Money TRY(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.TRY);
        public static Money TTD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.TTD);
        public static Money TWD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.TWD);
        public static Money TZS(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.TZS);
        public static Money UAH(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.UAH);
        public static Money UGX(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.UGX);
        public static Money USD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.USD);
        public static Money USN(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.USN);
        public static Money UYI(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.UYI);
        public static Money UYU(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.UYU);
        public static Money UYW(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.UYW);
        public static Money UZS(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.UZS);
        public static Money VES(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.VES);
        public static Money VND(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.VND);
        public static Money VUV(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.VUV);
        public static Money WST(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.WST);
        public static Money XAF(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.XAF);
        public static Money XCD(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.XCD);
        public static Money XOF(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.XOF);
        public static Money XPF(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.XPF);
        public static Money YER(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.YER);
        public static Money ZAR(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.ZAR);
        public static Money ZMW(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.ZMW);
        public static Money ZWL(decimal amount, MidpointRounding mode) => new(amount, mode, Currency.ZWL);
    }
}