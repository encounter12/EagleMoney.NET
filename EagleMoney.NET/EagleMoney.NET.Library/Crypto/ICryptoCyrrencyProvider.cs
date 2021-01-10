using System.Collections.Generic;

namespace EagleMoney.NET.Library.Crypto
{
    public interface ICryptoCurrencyProvider
    {
        CryptoCurrency GetCurrency(string currencyCode);
    }
}