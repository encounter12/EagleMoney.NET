namespace EagleMoney.NET.Library.Countries
{
    public readonly struct Country
    {
        public string ShortName { get; init; }
        
        public string ShortNameLowerCase { get; init; }
        
        public string FullName { get; init; }
        
        public string CodeAlpha2 { get; init; }
        
        public string CodeAlpha3 { get; init; }
        
        public string NumericCode { get; init; }
    }
}