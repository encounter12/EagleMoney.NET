namespace EagleMoney.NET.Library
{
    public readonly struct Country
    {
        public string Name { get; init; }
        
        public string CodeAlpha2 { get; init; }
        
        public string CodeAlpha3 { get; init; }
        
        public string NumericCode { get; init; }

        public Country(string name, string codeAlpha2, string codeAlpha3, string numericCode)
        {
            Name = name;
            CodeAlpha2 = codeAlpha2;
            CodeAlpha3 = codeAlpha3;
            NumericCode = numericCode;
        }
    }
}