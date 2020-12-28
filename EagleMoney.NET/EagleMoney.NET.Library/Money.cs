using System;
using System.Numerics;
using System.Text;

namespace EagleMoney.NET.Library
{
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
        public Money[] Allocate(int n)
        {
            BigInteger[] allocatedInternalAmounts = AllocateEven(_amount, n);

            var currency = Currency;
            var allocated = Array.ConvertAll(
                allocatedInternalAmounts, x => new Money(x, currency));
            
            return allocated;
        }

        private BigInteger[] AllocateEven(BigInteger amount, int n)
        {
            BigInteger lowResult = amount / n;
            BigInteger highResult = lowResult + 1;
            
            BigInteger[] results = new BigInteger[n];
            int remainder = (int)amount % n;

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

        // ReSharper disable once InconsistentNaming
        public static Money USD(decimal moneyValue) => new(moneyValue, Currency.USD);
        
        // ReSharper disable once InconsistentNaming
        public static Money EUR(decimal moneyValue) => new(moneyValue, Currency.EUR);
        
        // ReSharper disable once InconsistentNaming
        public static Money BGN(decimal moneyValue) => new(moneyValue, Currency.BGN);

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
            => new(m.Amount + 1, m.Currency);

        public static Money operator --(Money m)
            => new(m.Amount - 1M, m.Currency);

        public static explicit operator decimal(Money m) => m.Amount;
    }
}