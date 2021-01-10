namespace EagleMoney.NET.Library.Currencies
{
    public interface ICurrency
    {
        public string Code { get; init;  }
        
        public string Name { get; init; }

        public string Symbol { get; init; }
        
        public int DefaultFractionDigits { get; init; }
        
        public string MinorUnit { get; init; }
    }
}