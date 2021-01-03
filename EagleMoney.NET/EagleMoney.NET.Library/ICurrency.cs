using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public interface ICurrency
    { 
        string Code { get; init;  }

        string Number { get; init;  }

        string Sign { get; init; }
        
        int DefaultFractionDigits { get; init; }
        
        HashSet<string> Countries { get; init; }

        string ToString();
    }
}