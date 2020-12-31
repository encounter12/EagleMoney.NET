namespace EagleMoney.NET.Library
{
    public interface IMoney
    {
        decimal Amount { get; }
        
        ICurrency MCurrency { get; init; }

        Money[] AllocateEven(int n);

        Money[] AllocateByRatios(int[] ratios);

        Money Percentage(decimal percent);

        string ToString();

        string ToString(string formattingLetter);

        string ToString(MoneyFormattingType formattingType);
    }
}