using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EagleMoney.NET.Library.Countries;
using EagleMoney.NET.Library.Countries.Enums;
using EagleMoney.NET.Library.Currencies;

namespace EagleMoney.NET.Library.Fiat
{
    public class FiatMoney : IMoney, IEquatable<FiatMoney>, IComparable<FiatMoney>, IComparable
    {
        private readonly BigInteger _amount;
        
        // Used constructor chaining, Credit:
        // DI-Friendly Library (Mark Seemann):
        // https://blog.ploeh.dk/2014/05/19/di-friendly-library/
        // Dependency Inject (DI) “friendly” library (Mark Seemann):
        // https://stackoverflow.com/questions/2045904/dependency-inject-di-friendly-library/2047657
        
        public FiatMoney(decimal amount, string currencyCode)
        {
            if (amount < 0M)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }
            
            var currency = new FiatCurrency(currencyCode);
            var centFactor = (int) Math.Pow(10, currency.DefaultFractionDigits);
            _amount = (BigInteger) Math.Round(amount * centFactor);
            Currency = currency;
        }
        
        public FiatMoney(decimal amount, ICurrency currency)
        {
            if (amount < 0M)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }

            var centFactor = (int) Math.Pow(10, currency.DefaultFractionDigits);
            _amount = (BigInteger) Math.Round(amount * centFactor);
            Currency = currency;
        }
        
        public FiatMoney(decimal amount, CountryCode countryCode)
        {
            if (amount < 0M)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }
            
            var currency = new FiatCurrency(countryCode);
            var centFactor = (int) Math.Pow(10, currency.DefaultFractionDigits);
            _amount = (BigInteger) Math.Round(amount * centFactor);
            Currency = currency;
        }
        
        public FiatMoney(decimal amount, CountryCodeAlpha3 codeAlpha3)
        {
            if (amount < 0M)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }
            
            var currency = new FiatCurrency(codeAlpha3);
            var centFactor = (int) Math.Pow(10, currency.DefaultFractionDigits);
            _amount = (BigInteger) Math.Round(amount * centFactor);
            Currency = currency;
        }

        public FiatMoney(decimal amount, MidpointRounding mode, string currencyCode)
        {
            if (amount < 0M)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }

            var currency = new FiatCurrency(currencyCode);
            var centFactor = (int) Math.Pow(10, currency.DefaultFractionDigits);
            _amount = (BigInteger) Math.Round(amount * centFactor, mode);
            Currency = currency;
        }

        private FiatMoney(BigInteger amount, ICurrency currency)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }
            
            _amount = amount;
            Currency = currency;
        }
        
        // private static int[] Cents => new[] { 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000 };
        // private int CentFactor => Cents[FiatCurrency.DefaultFractionDigits];
        
        private int CentFactor => (int)Math.Pow(10, Currency.DefaultFractionDigits);

        public decimal Amount => (decimal)_amount / CentFactor;

        public ICurrency Currency { get; init; }

        //Credit: Martin Fowler and Matt Foemmel, Book: Patterns of Enterprise Application Architecture, p.494
        public IMoney[] AllocateEven(int n)
        {
            BigInteger[] allocatedInternalAmounts = AllocateCentsEven(_amount, n);

            var currency = Currency;
            var allocated = Array.ConvertAll(
                allocatedInternalAmounts, x => new FiatMoney(x, currency));
            
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
        public IMoney[] AllocateByRatios(int[] ratios)
        {
            BigInteger[] allocatedInternalAmounts = AllocateCentsByRatios(_amount, ratios);

            var currency = Currency;
            var allocated = Array.ConvertAll(
                allocatedInternalAmounts, x => new FiatMoney(x, currency));
            
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

        public IMoney Percentage(decimal percent) 
            => new FiatMoney((Amount / 100) * percent, Currency);
        
        public override string ToString()
            => $"{Amount} {Currency.Code}";
        
        public string ToString(string formattingLetter)
            => formattingLetter?.ToUpper() == "C" ?
                Amount.FormatCurrency(Currency.Code) : $"{Amount} {Currency.Code}";
        
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

        public static IMoney Parse(string moneyStr, string currencyCode)
        {
            var amount = decimal.Parse(moneyStr);
            return new FiatMoney(amount, currencyCode);
        }
        
        // TODO: Complete method
        
        // public static FiatMoney Parse(string moneyStr)
        // {
        //     moneyStr = moneyStr.Trim();
        //     
        //     var amount = decimal.Parse(moneyStr);
        //     return new FiatMoney();
        // }

        public bool Equals(FiatMoney other)
            => Currency.Equals(other?.Currency) && Amount == other?.Amount;
        
        public override bool Equals(object other)
        {
            var otherMoney = other as FiatMoney;
            return otherMoney != null && Equals(otherMoney);
        }

        public override int GetHashCode()
            => HashCode.Combine(Amount, Currency);

        public int CompareTo(FiatMoney other)
        {
            if (!Currency.Equals(other.Currency))
            {
                throw new InvalidOperationException("Cannot compare fiat money of different currencies");
            }

            return Equals(other) ? 0 : Amount.CompareTo(other.Amount);
        }

        public int CompareTo(object other)
        {
            if (!(other is FiatMoney))
            {
                throw new InvalidOperationException("CompareTo() argument is not FiatMoney");
            }

            return CompareTo((FiatMoney) other);
        }

        public static bool operator ==(FiatMoney m1, FiatMoney m2)
            => !ReferenceEquals(m1, null) && !ReferenceEquals(m2, null) ?
                    m1.Equals(m2) : ReferenceEquals(m1, null) && ReferenceEquals(m2, null);

        public static bool operator !=(FiatMoney m1, FiatMoney m2)
            => !ReferenceEquals(m1, null) && !ReferenceEquals(m2, null) ? 
                    !m1.Equals(m2) : !ReferenceEquals(m1, null) ^ !ReferenceEquals(m2, null);
        
        public static bool operator ==(FiatMoney m1, decimal m2Value)
            => m1?.Amount.Equals(m2Value) ?? false;
        
        public static bool operator !=(FiatMoney m1, decimal m2Value)
            => !m1?.Amount.Equals(m2Value) ?? false;
        
        public static bool operator ==(decimal m1Value, FiatMoney m2)
            => m2?.Amount.Equals(m1Value) ?? false;
        
        public static bool operator !=(decimal m1Value, FiatMoney m2)
            => !m2?.Amount.Equals(m1Value) ?? false;

        public static bool operator <(FiatMoney m1, FiatMoney m2)
            => m1.CompareTo(m2) < 0;

        public static bool operator >(FiatMoney m1, FiatMoney m2)
            => m1.CompareTo(m2) > 0;
        
        public static bool operator <(FiatMoney m1, decimal m2Value)
            => m1.Amount.CompareTo(m2Value) < 0;
        
        public static bool operator >(FiatMoney m1, decimal m2Value)
            => m1.Amount.CompareTo(m2Value) > 0;
        
        public static bool operator <(decimal m1Value, FiatMoney m2)
            => m1Value.CompareTo(m2.Amount) < 0;
        
        public static bool operator >(decimal m1Value, FiatMoney m2)
            => m1Value.CompareTo(m2.Amount) > 0;

        public static bool operator <=(FiatMoney m1, FiatMoney m2)
            => m1 == m2 || m1 < m2;

        public static bool operator >=(FiatMoney m1, FiatMoney m2)
            => m1 == m2 || m1 > m2;
        
        public static bool operator <=(FiatMoney m1, decimal m2Value)
            => m1.Amount == m2Value || m1.Amount < m2Value;
        
        public static bool operator >=(FiatMoney m1, decimal m2Value)
            => m1.Amount == m2Value || m1.Amount > m2Value;
        
        public static bool operator <=(decimal m1Value, FiatMoney m2)
            => m1Value == m2.Amount || m1Value < m2.Amount;
        
        public static bool operator >=(decimal m1Value, FiatMoney m2)
            => m1Value == m2.Amount || m1Value > m2.Amount;

        public static FiatMoney operator +(FiatMoney m1, FiatMoney m2)
        {
            if (!m1.Currency.Equals(m2.Currency))
            {
                throw new InvalidOperationException("Cannot add money having different currencies");
            }

            return new FiatMoney(m1.Amount + m2.Amount, m1.Currency);
        }

        public static FiatMoney operator +(FiatMoney m1, decimal m2Value) 
            => new(m1.Amount + m2Value, m1.Currency);

        public static FiatMoney operator +(decimal m1Value, FiatMoney m2) 
            => new(m1Value + m2.Amount, m2.Currency);

        public static FiatMoney operator -(FiatMoney m1, FiatMoney m2)
        {
            if (!m1.Currency.Equals(m2.Currency))
            {
                throw new InvalidOperationException("Cannot subtract money having different currencies");
            }

            return new FiatMoney(m1.Amount - m2.Amount, m1.Currency);
        }

        public static FiatMoney operator -(FiatMoney m1, decimal m2Value)
            => new(m1.Amount - m2Value, m1.Currency);

        public static FiatMoney operator -(decimal m1Value, FiatMoney m2)
            => new(m1Value - m2.Amount, m2.Currency);

        public static FiatMoney operator *(FiatMoney m1, FiatMoney m2)
        {
            if (!m1.Currency.Equals(m2.Currency))
            {
                throw new InvalidOperationException("Cannot multiply money having different currencies");
            }

            return new FiatMoney(m1.Amount * m2.Amount, m1.Currency);
        }

        public static FiatMoney operator *(FiatMoney m1, decimal m2Value)
            => new(m1.Amount * m2Value, m1.Currency);

        public static FiatMoney operator *(decimal m1Value, FiatMoney m2)
            => new(m1Value * m2.Amount, m2.Currency);

        public static FiatMoney operator /(FiatMoney m1, FiatMoney m2)
        {
            if (!m1.Currency.Equals(m2.Currency))
            {
                throw new InvalidOperationException("Cannot divide money having different currencies");
            }

            return new FiatMoney(m1.Amount / m2.Amount, m1.Currency);
        }

        public static FiatMoney operator /(FiatMoney m1, decimal m2Value)
            => new(m1.Amount / m2Value, m1.Currency);

        public static FiatMoney operator /(decimal m1Value, FiatMoney m2)
            => new(m1Value / m2.Amount, m2.Currency);

        public static FiatMoney operator %(FiatMoney m, int divisor)
            => new(m.Amount % divisor, m.Currency);
        
        public static FiatMoney operator +(FiatMoney m)
            => new(m.Amount, m.Currency);
        
        public static FiatMoney operator -(FiatMoney m)
            => new(-m.Amount, m.Currency);

        public static FiatMoney operator ++(FiatMoney m)
            => new(m.Amount + 1M, m.Currency);

        public static FiatMoney operator --(FiatMoney m)
            => new(m.Amount - 1M, m.Currency);

        public static explicit operator decimal(FiatMoney m) => m.Amount;
        
        public static FiatMoney AFN(decimal amount) => new (amount, FiatCurrency.AFN);
        public static FiatMoney BGN(decimal amount) => new (amount, FiatCurrency.BGN);
        public static FiatMoney CAD(decimal amount) => new(amount, FiatCurrency.CAD);
        public static FiatMoney CHF(decimal amount) => new(amount, FiatCurrency.CHF);
        public static FiatMoney EUR(decimal amount) => new (amount, FiatCurrency.EUR);
        public static FiatMoney GBP(decimal amount) => new (amount, FiatCurrency.GBP);
        public static FiatMoney MKD(decimal amount) => new (amount, FiatCurrency.MKD);
        public static FiatMoney SAR(decimal amount) => new (amount, FiatCurrency.SAR);
        public static FiatMoney USD(decimal amount) => new (amount, FiatCurrency.USD);
        
        public static FiatMoney AOA(decimal amount, MidpointRounding mode) => new(amount, mode, FiatCurrency.AOA);
        public static FiatMoney BGN(decimal amount, MidpointRounding mode) => new(amount, mode, FiatCurrency.BGN);
        public static FiatMoney CHF(decimal amount, MidpointRounding mode) => new(amount, mode, FiatCurrency.CHF);
        
    }
}