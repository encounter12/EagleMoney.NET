namespace EagleMoney.NET.Library
{
    public static class CurrenciesContainer
    {
        public static readonly Currency[] Currencies =
        {
            new (CurrencyCode.AFN.ToString(), 971, "", 2),
            new (CurrencyCode.BGN.ToString(), 975, "", 2),
            new (CurrencyCode.EUR.ToString(), 978, "€", 2),
            new (CurrencyCode.GBP.ToString(), 826, "£", 2),
            new (CurrencyCode.USD.ToString(), 840, "$", 2)
        };
    }
}