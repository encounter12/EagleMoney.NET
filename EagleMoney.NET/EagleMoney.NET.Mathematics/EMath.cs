using System;
using System.Linq;
using System.Text;

namespace EagleMoney.NET.Library.Mathematics
{
    public static class EMath
    {
        // For now the method only adds natural numbers
        // TODO: add fractional numbers
        public static string Add(string numberOne, string numberTwo)
        {
            if (string.IsNullOrWhiteSpace(numberOne))
            {
                throw new ArgumentException($"The first number: {numberOne} is null or whitespace");
            }

            if (string.IsNullOrWhiteSpace(numberTwo))
            {
                throw new ArgumentException($"The second number: {numberTwo} is null or whitespace");
            }

            if (!IsValidNumber(numberOne))
            {
                throw new ArgumentException("The first addend is not a valid number");
            }
            
            if (!IsValidNumber(numberTwo))
            {
                throw new ArgumentException("The second addend is not a valid number");
            }
            
            string sumSign = string.Empty;
            
            if (numberOne.StartsWith("-") && numberTwo.StartsWith("-"))
            {
                sumSign = "-";
                numberOne = numberOne.Substring(1);
                numberTwo = numberTwo.Substring(1);
            }
            else if (!numberOne.StartsWith("-") && numberTwo.StartsWith("-"))
            {
                numberTwo = numberTwo.Substring(1);
                string difference = Subtract(numberOne, numberTwo);
                return difference;
            }
            else if (numberOne.StartsWith("-") && !numberTwo.StartsWith("-"))
            {
                numberOne = numberOne.Substring(1);
                string difference = Subtract(numberTwo, numberOne);
                return difference;
            }

            string sum;
            
            var digitSeparator = ".";

            if (numberOne.Contains(digitSeparator) || numberTwo.Contains(digitSeparator))
            {
                sum = AddFractionalNumbers(numberOne, numberTwo, digitSeparator);
            }
            else
            {
                sum = AddPositiveNaturalNumbers(numberOne, numberTwo);
            }

            if (!string.IsNullOrEmpty(sumSign))
            {
                sum = sum.Insert(0, sumSign);
            }

            sum = RemoveLeadingZeros(sum);
            
            return sum;
        }

        private static string AddFractionalNumbers(string numberOne, string numberTwo, string digitSeparator)
        {
            string numberOneFraction = string.Empty;
            string numberTwoFraction = string.Empty;
                
            if (numberOne.Contains(digitSeparator))
            {
                numberOneFraction = GetFraction(numberOne);
            }

            if (numberTwo.Contains(digitSeparator))
            {
                numberTwoFraction = GetFraction(numberTwo);
            }

            var fractionSum = AddPositiveNaturalNumbers(numberOneFraction, numberTwoFraction, true);

            string fractionCarry = string.Empty;

            int largestFractionLength = numberOneFraction.Length > numberTwoFraction.Length
                ? numberOneFraction.Length
                : numberTwoFraction.Length;
            
            if (fractionSum.Length > largestFractionLength)
            {
                fractionCarry = "1";
                fractionSum = fractionSum.Substring(1);
            }

            string numberOneWholePart = numberOne;

            if (numberOne.Contains(digitSeparator))
            {
                numberOneWholePart = GetWholeNumber(numberOne, digitSeparator);
            }
                
            string numberTwoWholePart = numberTwo;
                
            if (numberTwo.Contains(digitSeparator))
            {
                numberTwoWholePart = GetWholeNumber(numberTwo, digitSeparator);
            }

            if (!string.IsNullOrEmpty(fractionCarry))
            {
                numberOneWholePart = AddPositiveNaturalNumbers(numberOneWholePart, fractionCarry);
            }

            string wholeNumbersSum = AddPositiveNaturalNumbers(numberOneWholePart, numberTwoWholePart);

            if (fractionSum.All(x => x == '0'))
            {
                return wholeNumbersSum;
            }
            else
            {
                string fractionalNumberSum = $"{wholeNumbersSum}{digitSeparator}{fractionSum}";

                return fractionalNumberSum;
            }
        }

        private static string GetFraction(string number)
        {
            int dotIndex = number.IndexOf(".", StringComparison.Ordinal);
            return number.Substring(dotIndex + 1);
        }

        private static string GetWholeNumber(string number, string separator)
        {
            return number.Substring(0, number.IndexOf(separator, StringComparison.Ordinal));
        }

        private static string AddPositiveNaturalNumbers(string numberOne, string numberTwo, bool areFractions = false)
        {
            if (areFractions)
            {
                (numberOne, numberTwo) = PadRightZeros(numberOne, numberTwo);
            }
            else
            {
                (numberOne, numberTwo) = PadLeftZeros(numberOne, numberTwo);
            }
            
            var sumSb = new StringBuilder();
            string carry = "0";
            string sum = "0";

            for (int i = numberOne.Length - 1; i >= 0 ; i--)
            {
                string digitSum = AddDigits(numberOne[i].ToString(), numberTwo[i].ToString());
                
                switch (digitSum.Length)
                {
                    case 1:
                    {
                        if (carry != "0")
                        {
                            digitSum = AddDigits(digitSum, carry);

                            carry = digitSum.Length > 1 ? digitSum[0].ToString() : "0";
                        }
                    
                        sumSb.Insert(0, digitSum.Length > 1 ? digitSum[1] : digitSum[0]);
                        break;
                    }
                    case 2:
                    {
                        var memorizedDigitTemp = digitSum[0].ToString();
                    
                        if (carry != "0")
                        {
                            digitSum = AddDigits(digitSum[1].ToString(), carry);
                        }
                    
                        carry = memorizedDigitTemp;
                        sumSb.Insert(0, digitSum[1]);
                        break;
                    }
                    default:
                        throw new InvalidOperationException();
                }
            }

            if (carry != "0")
            {
                sumSb.Insert(0, carry);
            }

            return sumSb.ToString();
        }
        
        private static (string numberOne, string numberTwo) PadLeftZeros(string numberOne, string numberTwo)
        {
            if (numberOne.Length < numberTwo.Length)
            {
                numberOne = numberOne.PadLeft(numberTwo.Length, '0');
            }
            else if (numberOne.Length > numberTwo.Length)
            {
                numberTwo = numberTwo.PadLeft(numberOne.Length, '0');
            }
            
            return (numberOne, numberTwo);
        }

        private static (string numberOne, string numberTwo) PadRightZeros(string numberOne, string numberTwo)
        {
            if (numberOne.Length > numberTwo.Length)
            {
                numberTwo = numberTwo.PadRight(numberOne.Length, '0');
            }
            else if (numberOne.Length < numberTwo.Length)
            {
                numberOne = numberOne.PadRight(numberTwo.Length, '0');
            }

            return (numberOne, numberTwo);
        }

        private static string AddDigits(string digitOne, string digitTwo)
        {
            if (digitOne.Length != 1 || !char.IsDigit(digitOne[0]))
            {
                throw new ArgumentException($"The first argument: {digitOne} is not a digit");
            }
            
            if (digitTwo.Length != 1 || !char.IsDigit(digitTwo[0]))
            {
                throw new ArgumentException($"The second argument: {digitTwo} is not a digit");
            }

            string sum = GetFundamentalAdditionSum(digitOne, digitTwo);
            
            if (sum != null)
            {
                return sum;
            }
            else
            {
                throw new InvalidOperationException("Cannot find the digits addition result in the addition table");
            }
        }

        public static string Subtract(string minuend, string subtrahend)
        {
            if (minuend.StartsWith("-") && !subtrahend.StartsWith("-"))
            {
                minuend = minuend.Substring(1);
                string sum = Add(minuend, subtrahend);
                return sum.Insert(0, "-");
            }
            else if (!minuend.StartsWith("-") && subtrahend.StartsWith("-"))
            {
                subtrahend = subtrahend.Substring(1);
                string sum = Add(minuend, subtrahend);
                return sum;
            }
            else if (minuend.StartsWith("-") && subtrahend.StartsWith("-"))
            {
                minuend = minuend.Substring(1);
                subtrahend = subtrahend.Substring(1);
                Swap(ref minuend, ref subtrahend);
            }
            
            int comparison = ComparePositiveNaturalNumbers(minuend, subtrahend);

            bool diffNegative = false;

            if (comparison == -1)
            {
                diffNegative = true;
                Swap(ref minuend, ref subtrahend);
            }
            
            (string minuendWithZeros, string subtrahendWithZeros) = 
                PadLeftZeros(minuend, subtrahend);

            var minuendWithZerosSb = new StringBuilder(minuendWithZeros);
            var differenceSb = new StringBuilder();
            
            for (int i = minuendWithZerosSb.Length - 1; i >= 0; i--)
            {
                string digitDifference = FundamentalSubtract(
                    minuendWithZerosSb[i].ToString(), subtrahendWithZeros[i].ToString());

                if (digitDifference != null)
                {
                    differenceSb.Insert(0, digitDifference);
                }
                else
                {
                    int zeroCount = i - 1;
                    
                    while (minuendWithZerosSb[zeroCount] == '0')
                    {
                        zeroCount--;
                    }
                    
                    string tempDiff = FundamentalSubtract(
                        minuendWithZerosSb[zeroCount].ToString(), "1");

                    minuendWithZerosSb[zeroCount] = tempDiff.ToCharArray()[0];

                    for (int j = zeroCount + 1; j <= i - 1; j++)
                    {
                        minuendWithZerosSb[j] = '9';
                    }

                    string carry = "1";
                    var tempDigitMinuend = carry + minuendWithZerosSb[i];

                    string tempDigitDifference = FundamentalSubtract(
                        tempDigitMinuend, subtrahendWithZeros[i].ToString());
                    
                    differenceSb.Insert(0, tempDigitDifference);
                }
            }

            string result = differenceSb.ToString().TrimStart('0');

            if (diffNegative)
            {
                return "-" + result;
            }
            
            return result;
        }

        private static void Swap(ref string numberOne, ref string numberTwo)
        {
            string temp = null;
            temp = numberOne;
            numberOne = numberTwo;
            numberTwo = temp;
        }

        public static int ComparePositiveNaturalNumbers(string firstNumber, string secondNumber)
        {
            if (firstNumber.Length > secondNumber.Length)
            {
                return 1;
            }
            else if (firstNumber.Length < secondNumber.Length)
            {
                return -1;
            }
            
            int digitsEqualCount = 0;

            for (int i = 0; i < firstNumber.Length; i++)
            {
                string difference = FundamentalSubtract(
                    firstNumber[i].ToString(), secondNumber[i].ToString());

                if (difference == null)
                {
                    return -1;
                }
                else if (difference == "0")
                {
                    digitsEqualCount++;
                }
                else
                {
                    return 1;
                }
            }

            if (firstNumber.Length == digitsEqualCount)
            {
                return 0;
            }

            return 1;
        }

        private static string FundamentalSubtract(string minuend, string subtrahend)
        {
            for (int i = 0; i < AdditionTable.GetLength(0); i++)
            {
                if (AdditionTable[i, 2] == minuend && AdditionTable[i, 0] == subtrahend)
                {
                    return AdditionTable[i, 1];
                }
            }
            
            return null;
        }

        public static bool IsValidNumber(string numberStr)
        {
            int decimalSeparatorCount = 0;

            if (numberStr.StartsWith("-"))
            {
                numberStr = numberStr.Substring(1);
            }
            
            foreach (char c in numberStr)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }

                if (c == '.')
                {
                    decimalSeparatorCount++;
                }
            }

            return decimalSeparatorCount <= 1;
        }
        
        private static string GetFundamentalAdditionSum(string numberOne, string numberTwo)
        {
            if (string.IsNullOrWhiteSpace(numberOne) || numberOne.Length > 1)
            {
                throw new ArgumentException(
                    $"Number one: {numberOne} is not valid. " +
                    "Only one digit numbers are allowed as argument.");
            }
            
            if (string.IsNullOrWhiteSpace(numberTwo) || numberTwo.Length > 1)
            {
                throw new ArgumentException(
                    $"Number two: {numberTwo} is not valid. " + 
                    "Only one digit numbers are allowed as argument.");
            }

            for (int i = 0; i < AdditionTable.GetLength(0); i++)
            {
                if (AdditionTable[i, 0] == numberOne && AdditionTable[i, 1] == numberTwo)
                {
                    return AdditionTable[i, 2];
                }
            }
            
            return null;
        }
        
        private static string RemoveLeadingZeros(string numberStr)
        {
            numberStr = numberStr.TrimStart('0');
            numberStr = numberStr.Length > 0 ? numberStr : "0";
            return numberStr;
        }

        private static readonly string[,] AdditionTable = new string[100, 3]
        {
            {"0", "0", "0"},
            {"0", "1", "1"},
            {"0", "2", "2"},
            {"0", "3", "3"},
            {"0", "4", "4"},
            {"0", "5", "5"},
            {"0", "6", "6"},
            {"0", "7", "7"},
            {"0", "8", "8"},
            {"0", "9", "9"},
            {"1", "0", "1"},
            {"1", "1", "2"},
            {"1", "2", "3"},
            {"1", "3", "4"},
            {"1", "4", "5"},
            {"1", "5", "6"},
            {"1", "6", "7"},
            {"1", "7", "8"},
            {"1", "8", "9"},
            {"1", "9", "10"},
            {"2", "0", "2"},
            {"2", "1", "3"},
            {"2", "2", "4"},
            {"2", "3", "5"},
            {"2", "4", "6"},
            {"2", "5", "7"},
            {"2", "6", "8"},
            {"2", "7", "9"},
            {"2", "8", "10"},
            {"2", "9", "11"},
            {"3", "0", "3"},
            {"3", "1", "4"},
            {"3", "2", "5"},
            {"3", "3", "6"},
            {"3", "4", "7"},
            {"3", "5", "8"},
            {"3", "6", "9"},
            {"3", "7", "10"},
            {"3", "8", "11"},
            {"3", "9", "12"},
            {"4", "0", "4"},
            {"4", "1", "5"},
            {"4", "2", "6"},
            {"4", "3", "7"},
            {"4", "4", "8"},
            {"4", "5", "9"},
            {"4", "6", "10"},
            {"4", "7", "11"},
            {"4", "8", "12"},
            {"4", "9", "13"},
            {"5", "0", "5"},
            {"5", "1", "6"},
            {"5", "2", "7"},
            {"5", "3", "8"},
            {"5", "4", "9"},
            {"5", "5", "10"},
            {"5", "6", "11"},
            {"5", "7", "12"},
            {"5", "8", "13"},
            {"5", "9", "14"},
            {"6", "0", "6"},
            {"6", "1", "7"},
            {"6", "2", "8"},
            {"6", "3", "9"},
            {"6", "4", "10"},
            {"6", "5", "11"},
            {"6", "6", "12"},
            {"6", "7", "13"},
            {"6", "8", "14"},
            {"6", "9", "15"},
            {"7", "0", "7"},
            {"7", "1", "8"},
            {"7", "2", "9"},
            {"7", "3", "10"},
            {"7", "4", "11"},
            {"7", "5", "12"},
            {"7", "6", "13"},
            {"7", "7", "14"},
            {"7", "8", "15"},
            {"7", "9", "16"},
            {"8", "0", "8"},
            {"8", "1", "9"},
            {"8", "2", "10"},
            {"8", "3", "11"},
            {"8", "4", "12"},
            {"8", "5", "13"},
            {"8", "6", "14"},
            {"8", "7", "15"},
            {"8", "8", "16"},
            {"8", "9", "17"},
            {"9", "0", "9"},
            {"9", "1", "10"},
            {"9", "2", "11"},
            {"9", "3", "12"},
            {"9", "4", "13"},
            {"9", "5", "14"},
            {"9", "6", "15"},
            {"9", "7", "16"},
            {"9", "8", "17"},
            {"9", "9", "18"}
        };
        
        private enum Comparison 
        {
            LessThan = -1,
            Equal = 0,
            GreaterThan = 1
        }; 
    }
}