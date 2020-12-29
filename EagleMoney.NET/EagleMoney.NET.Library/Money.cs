// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EagleMoney.NET.Library
{
    // TODO: bool Equals(Money other) - throw InvalidOperationException if the two currencies are different
    
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
    
    // TODO: Consider overloading money constructor with new parameter for rounding - see Math.Round(Decimal, MidpointRounding)
    
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
        
        public static Money USD(decimal amount) 
            => new(amount, Currency.USD);
        
        public static Money USD(decimal amount, MidpointRounding mode) 
            => new(amount, mode, Currency.USD);

        public static Money EUR(decimal amount) 
            => new(amount, Currency.EUR);
        
        public static Money EUR(decimal amount, MidpointRounding mode) 
            => new(amount, mode, Currency.EUR);
        
        public static Money BGN(decimal amount) 
            => new(amount, Currency.BGN);
        
        public static Money BGN(decimal amount, MidpointRounding mode) 
            => new(amount, mode, Currency.BGN);

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
    }
}