using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public readonly struct Country
    {
        public Country(string name, string codeAlpha2, string codeAlpha3, string numericCode, string currencyCode)
        {
            Name = name;
            CodeAlpha2 = codeAlpha2;
            CodeAlpha3 = codeAlpha3;
            NumericCode = numericCode;
            Currency = currencyCode;
        }
        
        public string Name { get; init;  }
        
        public string CodeAlpha2 { get; init;  }
        
        public string CodeAlpha3 { get; init; }
        
        public string NumericCode { get; init; }
        
        public string Currency { get; init; }
    }
}