using EagleMoney.NET.Library.Currencies;

namespace EagleMoney.NET.Library
{
    public interface IMoney
    {
        decimal Amount { get; }
        
        ICurrency Currency { get; init; }

        IMoney[] AllocateEven(int n);

        IMoney[] AllocateByRatios(int[] ratios);

        IMoney Percentage(decimal percent);

        string ToString();

        string ToString(string formattingLetter);

        string ToString(MoneyFormattingType formattingType);
    }
}