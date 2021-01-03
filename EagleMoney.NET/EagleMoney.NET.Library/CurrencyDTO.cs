using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public class CurrencyDTO
    {
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public string Number { get; set; }
        
        public string Sign { get; set; }
        
        public int DefaultFractionDigits { get; set; }
        
        public HashSet<string> Countries { get; set; }
    }
}