// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace EagleMoney.NET.Library
{ 
    public readonly struct Money : IMoney, IEquatable<Money>, IComparable<Money>, IComparable
    {
        private readonly BigInteger _amount;
        
        // Used constructor chaining, Credit:
        // DI-Friendly Library (Mark Seemann):
        // https://blog.ploeh.dk/2014/05/19/di-friendly-library/
        // Dependency Inject (DI) “friendly” library (Mark Seemann):
        // https://stackoverflow.com/questions/2045904/dependency-inject-di-friendly-library/2047657
        
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
        
        public Money(decimal amount, CountryCode countryCode)
        {
            if (amount < 0M)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount), amount, "Amount should be equal or greater than zero");
            }
            
            var currency = new Currency(countryCode);
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

        public static Money Parse(string moneyStr, string currencyCode)
        {
            var amount = decimal.Parse(moneyStr);
            return new Money(amount, currencyCode);
        }
        
        // TODO: Complete method
        
        // public static Money Parse(string moneyStr)
        // {
        //     moneyStr = moneyStr.Trim();
        //     
        //     var amount = decimal.Parse(moneyStr);
        //     return new Money();
        // }

        public bool Equals(Money other)
            => Currency.Equals(other.Currency) && Amount == other.Amount;
        
        public override bool Equals(object other)
        {
            var otherMoney = other as Money?;
            return otherMoney.HasValue && Equals(otherMoney.Value);
        }

        public override int GetHashCode()
            => HashCode.Combine(Amount, Currency);

        public int CompareTo(Money other)
        {
            if (!Currency.Equals(other.Currency))
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
            if (!m1.Currency.Equals(m2.Currency))
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
            if (!m1.Currency.Equals(m2.Currency))
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
            if (!m1.Currency.Equals(m2.Currency))
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
            if (!m1.Currency.Equals(m2.Currency))
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
        
        public static Money AFN(decimal amount) => new (amount, Library.Currency.AFN);
        public static Money BGN(decimal amount) => new (amount, Library.Currency.BGN);
        public static Money CAD(decimal amount) => new(amount, Library.Currency.CAD);
        public static Money CHF(decimal amount) => new(amount, Library.Currency.CHF);
        public static Money EUR(decimal amount) => new (amount, Library.Currency.EUR);
        public static Money GBP(decimal amount) => new (amount, Library.Currency.GBP);
        public static Money MKD(decimal amount) => new (amount, Library.Currency.MKD);
        public static Money SAR(decimal amount) => new (amount, Library.Currency.SAR);
        public static Money USD(decimal amount) => new (amount, Library.Currency.USD);
        
        public static Money AOA(decimal amount, MidpointRounding mode) => new(amount, mode, Library.Currency.AOA);
        public static Money BGN(decimal amount, MidpointRounding mode) => new(amount, mode, Library.Currency.BGN);
        public static Money CHF(decimal amount, MidpointRounding mode) => new(amount, mode, Library.Currency.CHF);
        
    }
}