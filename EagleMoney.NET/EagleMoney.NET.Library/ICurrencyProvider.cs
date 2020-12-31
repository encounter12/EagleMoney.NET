using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public interface ICurrencyProvider
    {
        bool TryGetCurrencySymbol(string isoCurrencyCode, out string symbol);
        
        List<string> CurrencyCodes { get; }

        List<CurrencyDTO> GetCurrencies();
    }
}