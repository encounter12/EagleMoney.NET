using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EagleMoney.NET.Library.Currencies;

namespace EagleMoney.NET.Library.Crypto
{
    public class CryptoMoney : IMoney, IEquatable<CryptoMoney>, IComparable<CryptoMoney>, IComparable
    {
        private readonly BigInteger _amount;
        
        // Used constructor chaining, Credit:
        // DI-Friendly Library (Mark Seemann):
        // https://blog.ploeh.dk/2014/05/19/di-friendly-library/
        // Dependency Inject (DI) “friendly” library (Mark Seemann):
        // https://stackoverflow.com/questions/2045904/dependency-inject-di-friendly-library/2047657
        
        public CryptoMoney(decimal amount, string currencyCode)
        {
            if (amount < 0M)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }
            
            var currency = new CryptoCurrency(currencyCode);
            var centFactor = (int) Math.Pow(10, currency.DefaultFractionDigits);
            _amount = (BigInteger) Math.Round(amount * centFactor);
            Currency = currency;
        }
        
        public CryptoMoney(decimal amount, ICurrency currency)
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

        public CryptoMoney(decimal amount, MidpointRounding mode, string currencyCode)
        {
            if (amount < 0M)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }

            var currency = new CryptoCurrency(currencyCode);
            var centFactor = (int) Math.Pow(10, currency.DefaultFractionDigits);
            _amount = (BigInteger) Math.Round(amount * centFactor, mode);
            Currency = currency;
        }

        private CryptoMoney(BigInteger amount, ICurrency currency)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }
            
            _amount = amount;
            Currency = currency;
        }

        private int CentFactor => (int)Math.Pow(10, Currency.DefaultFractionDigits);

        public decimal Amount => (decimal)_amount / CentFactor;

        public ICurrency Currency { get; init; }

        //Credit: Martin Fowler and Matt Foemmel, Book: Patterns of Enterprise Application Architecture, p.494
        public IMoney[] AllocateEven(int n)
        {
            BigInteger[] allocatedInternalAmounts = AllocateCentsEven(_amount, n);

            var currency = Currency;
            var allocated = Array.ConvertAll(
                allocatedInternalAmounts, x => new CryptoMoney(x, currency));
            
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
                allocatedInternalAmounts, x => new CryptoMoney(x, currency));
            
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
            => new CryptoMoney((Amount / 100) * percent, Currency);
        
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
            return new CryptoMoney(amount, currencyCode);
        }
        
        // TODO: Complete method
        
        // public static Money Parse(string moneyStr)
        // {
        //     moneyStr = moneyStr.Trim();
        //     
        //     var amount = decimal.Parse(moneyStr);
        //     return new Money();
        // }

        public bool Equals(CryptoMoney other)
            => Currency.Equals(other?.Currency) && Amount == other?.Amount;
        
        public override bool Equals(object other)
        {
            var otherMoney = other as CryptoMoney;
            return otherMoney != null && Equals(otherMoney);
        }

        public override int GetHashCode()
            => HashCode.Combine(Amount, Currency);

        public int CompareTo(CryptoMoney other)
        {
            if (!Currency.Equals(other.Currency))
            {
                throw new InvalidOperationException("Cannot compare fiat money of different currencies");
            }

            return Equals(other) ? 0 : Amount.CompareTo(other.Amount);
        }

        public int CompareTo(object other)
        {
            if (!(other is CryptoMoney))
            {
                throw new InvalidOperationException("CompareTo() argument is not CryptoMoney");
            }

            return CompareTo((CryptoMoney) other);
        }

        public static bool operator ==(CryptoMoney m1, CryptoMoney m2)
            => !ReferenceEquals(m1, null) && !ReferenceEquals(m2, null) ?
                    m1.Equals(m2) : ReferenceEquals(m1, null) && ReferenceEquals(m2, null);

        public static bool operator !=(CryptoMoney m1, CryptoMoney m2)
            => !ReferenceEquals(m1, null) && !ReferenceEquals(m2, null) ? 
                    !m1.Equals(m2) : !ReferenceEquals(m1, null) ^ !ReferenceEquals(m2, null);
        
        public static bool operator ==(CryptoMoney m1, decimal m2Value)
            => m1?.Amount.Equals(m2Value) ?? false;
        
        public static bool operator !=(CryptoMoney m1, decimal m2Value)
            => !m1?.Amount.Equals(m2Value) ?? false;
        
        public static bool operator ==(decimal m1Value, CryptoMoney m2)
            => m2?.Amount.Equals(m1Value) ?? false;
        
        public static bool operator !=(decimal m1Value, CryptoMoney m2)
            => !m2?.Amount.Equals(m1Value) ?? false;

        public static bool operator <(CryptoMoney m1, CryptoMoney m2)
            => m1.CompareTo(m2) < 0;

        public static bool operator >(CryptoMoney m1, CryptoMoney m2)
            => m1.CompareTo(m2) > 0;
        
        public static bool operator <(CryptoMoney m1, decimal m2Value)
            => m1.Amount.CompareTo(m2Value) < 0;
        
        public static bool operator >(CryptoMoney m1, decimal m2Value)
            => m1.Amount.CompareTo(m2Value) > 0;
        
        public static bool operator <(decimal m1Value, CryptoMoney m2)
            => m1Value.CompareTo(m2.Amount) < 0;
        
        public static bool operator >(decimal m1Value, CryptoMoney m2)
            => m1Value.CompareTo(m2.Amount) > 0;

        public static bool operator <=(CryptoMoney m1, CryptoMoney m2)
            => m1 == m2 || m1 < m2;

        public static bool operator >=(CryptoMoney m1, CryptoMoney m2)
            => m1 == m2 || m1 > m2;
        
        public static bool operator <=(CryptoMoney m1, decimal m2Value)
            => m1.Amount == m2Value || m1.Amount < m2Value;
        
        public static bool operator >=(CryptoMoney m1, decimal m2Value)
            => m1.Amount == m2Value || m1.Amount > m2Value;
        
        public static bool operator <=(decimal m1Value, CryptoMoney m2)
            => m1Value == m2.Amount || m1Value < m2.Amount;
        
        public static bool operator >=(decimal m1Value, CryptoMoney m2)
            => m1Value == m2.Amount || m1Value > m2.Amount;

        public static CryptoMoney operator +(CryptoMoney m1, CryptoMoney m2)
        {
            if (!m1.Currency.Equals(m2.Currency))
            {
                throw new InvalidOperationException("Cannot add money having different currencies");
            }

            return new CryptoMoney(m1.Amount + m2.Amount, m1.Currency);
        }

        public static CryptoMoney operator +(CryptoMoney m1, decimal m2Value) 
            => new(m1.Amount + m2Value, m1.Currency);

        public static CryptoMoney operator +(decimal m1Value, CryptoMoney m2) 
            => new(m1Value + m2.Amount, m2.Currency);

        public static CryptoMoney operator -(CryptoMoney m1, CryptoMoney m2)
        {
            if (!m1.Currency.Equals(m2.Currency))
            {
                throw new InvalidOperationException("Cannot subtract money having different currencies");
            }

            return new CryptoMoney(m1.Amount - m2.Amount, m1.Currency);
        }

        public static CryptoMoney operator -(CryptoMoney m1, decimal m2Value)
            => new(m1.Amount - m2Value, m1.Currency);

        public static CryptoMoney operator -(decimal m1Value, CryptoMoney m2)
            => new(m1Value - m2.Amount, m2.Currency);

        public static CryptoMoney operator *(CryptoMoney m1, CryptoMoney m2)
        {
            if (!m1.Currency.Equals(m2.Currency))
            {
                throw new InvalidOperationException("Cannot multiply money having different currencies");
            }

            return new CryptoMoney(m1.Amount * m2.Amount, m1.Currency);
        }

        public static CryptoMoney operator *(CryptoMoney m1, decimal m2Value)
            => new(m1.Amount * m2Value, m1.Currency);

        public static CryptoMoney operator *(decimal m1Value, CryptoMoney m2)
            => new(m1Value * m2.Amount, m2.Currency);

        public static CryptoMoney operator /(CryptoMoney m1, CryptoMoney m2)
        {
            if (!m1.Currency.Equals(m2.Currency))
            {
                throw new InvalidOperationException("Cannot divide money having different currencies");
            }

            return new CryptoMoney(m1.Amount / m2.Amount, m1.Currency);
        }

        public static CryptoMoney operator /(CryptoMoney m1, decimal m2Value)
            => new(m1.Amount / m2Value, m1.Currency);

        public static CryptoMoney operator /(decimal m1Value, CryptoMoney m2)
            => new(m1Value / m2.Amount, m2.Currency);

        public static CryptoMoney operator %(CryptoMoney m, int divisor)
            => new(m.Amount % divisor, m.Currency);
        
        public static CryptoMoney operator +(CryptoMoney m)
            => new(m.Amount, m.Currency);
        
        public static CryptoMoney operator -(CryptoMoney m)
            => new(-m.Amount, m.Currency);

        public static CryptoMoney operator ++(CryptoMoney m)
            => new(m.Amount + 1M, m.Currency);

        public static CryptoMoney operator --(CryptoMoney m)
            => new(m.Amount - 1M, m.Currency);

        public static explicit operator decimal(CryptoMoney m) => m.Amount;
        
        public static CryptoMoney BTC(decimal amount) => new (amount, "BTC");
        // public static CryptoMoney BGN(decimal amount) => new (amount, Library.FiatCurrency.BGN);
    }
}