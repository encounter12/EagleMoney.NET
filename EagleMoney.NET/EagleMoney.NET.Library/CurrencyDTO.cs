using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public class CurrencyDTO
    {
        public string Code { get; }
        
        public string Name { get; }
        
        public string Number { get; }
        
        public string Sign { get; }
        
        public int DefaultFractionDigits { get; }
        
        public HashSet<string> Countries { get; }

        public CurrencyDTO(string code, string name, string number, string sign, int defaultFractionDigits, HashSet<string> countries)
        {
            Code = code;
            Name = name;
            Number = number;
            Sign = sign;
            DefaultFractionDigits = defaultFractionDigits;
            Countries = countries;
        }
        
        public CurrencyDTO(string code, int defaultFractionDigits)
        {
            Code = code;
            Name = "";
            Number = "";
            Sign = "";
            DefaultFractionDigits = defaultFractionDigits;
            Countries = new HashSet<string>();
        }
    }
}