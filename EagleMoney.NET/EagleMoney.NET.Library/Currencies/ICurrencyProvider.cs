using System.Collections.Generic;

namespace EagleMoney.NET.Library.Currencies
{
    public interface ICurrencyProvider
    {
        IEnumerable<CurrencyCountriesBasicInfo> GetCurrencies();
    }
}