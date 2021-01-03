using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public interface ICurrencyProvider
    {
        IEnumerable<CurrencyDTO> GetCurrencies();
    }
}