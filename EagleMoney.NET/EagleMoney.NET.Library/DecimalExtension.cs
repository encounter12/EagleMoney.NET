using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EagleMoney.NET.Library
{
    // Credit: jignesh - Format decimal as currency based on currency code - https://stackoverflow.com/a/30628398/1961386
    public static class DecimalExtension
    {
        private static readonly Dictionary<string, CultureInfo> IsoCurrenciesToACultureMap =
            CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(c => new {c, new RegionInfo(c.Name).ISOCurrencySymbol})
                .GroupBy(x => x.ISOCurrencySymbol)
                .ToDictionary(g => g.Key, g => g.First().c, StringComparer.OrdinalIgnoreCase);

        public static string FormatCurrency(this decimal amount, string currencyCode)
        {
            CultureInfo culture;
            if (IsoCurrenciesToACultureMap.TryGetValue(currencyCode, out culture))
            {
                return string.Format(culture, "{0:C}", amount);
            }
                
            return amount.ToString("0.00");
        }
    }
}