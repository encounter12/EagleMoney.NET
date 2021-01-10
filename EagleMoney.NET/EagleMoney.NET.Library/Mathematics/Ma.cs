using System;
using System.Collections.Generic;
using System.Text;

namespace EagleMoney.NET.Library.Mathematics
{
    public static class Ma
    {
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

            numberOne = RemoveZeros(numberOne);
            numberTwo = RemoveZeros(numberTwo);

            (numberOne, numberTwo) = AddLeadingZeros(numberOne, numberTwo);

            var sumSb = new StringBuilder();
            string memorizedDigit = "0";

            for (int i = numberOne.Length - 1; i >= 0 ; i--)
            {
                string digitSum = AddDigits(numberOne[i].ToString(), numberTwo[i].ToString());

                if (i == 0 && digitSum.Length > 1)
                {
                    sumSb.Insert(0, digitSum);
                    return sumSb.ToString();
                }

                if (digitSum.Length == 1)
                {
                    if (memorizedDigit != "0")
                    {
                        digitSum = AddDigits(digitSum, memorizedDigit);
                        memorizedDigit = "0";
                    }
                    
                    sumSb.Insert(0, digitSum);
                }
                else if (digitSum.Length == 2)
                {
                    var memorizedDigitTemp = digitSum[0].ToString();
                    
                    if (memorizedDigit != "0")
                    {
                        digitSum = AddDigits(digitSum[1].ToString(), memorizedDigit);
                    }
                    
                    memorizedDigit = memorizedDigitTemp;
                    sumSb.Insert(0, digitSum[1]);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            return sumSb.ToString();
        }

        private static string RemoveZeros(string numberStr)
        {
            numberStr = numberStr.Trim('0');
            numberStr = numberStr.Length > 0 ? numberStr : "0";
            return numberStr;
        }
        
        private static bool IsValidNumber(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }
            return true;
        }

        private static (string numberOne, string numberTwo) AddLeadingZeros(string numberOne, string numberTwo)
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
            
            string digitOneDigitTwo = digitOne + digitTwo;

            if (additionDictionary.TryGetValue(digitOneDigitTwo, out string sum))
            {
                return sum;
            }
            else
            {
                throw new InvalidOperationException("Cannot find the digits addition result in the addition table");
            }
        }

        private static Dictionary<string, string> additionDictionary = new Dictionary<string, string>
        {
            { "00", "0" },
            { "01", "1" },
            { "02", "2" },
            { "03", "3" },
            { "04", "4" },
            { "05", "5" },
            { "06", "6" },
            { "07", "7" },
            { "08", "8" },
            { "09", "9" },
            { "10", "1" },
            { "11", "2" },
            { "12", "3" },
            { "13", "4" },
            { "14", "5" },
            { "15", "6" },
            { "16", "7" },
            { "17", "8" },
            { "18", "9" },
            { "19", "10" },
            { "20", "2" },
            { "21", "3" },
            { "22", "4" },
            { "23", "5" },
            { "24", "6" },
            { "25", "7" },
            { "26", "8" },
            { "27", "9" },
            { "28", "10" },
            { "29", "11" },
            { "30", "3" },
            { "31", "4" },
            { "32", "5" },
            { "33", "6" },
            { "34", "7" },
            { "35", "8" },
            { "36", "9" },
            { "37", "10" },
            { "38", "11" },
            { "39", "12" },
            { "40", "4" },
            { "41", "5" },
            { "42", "6" },
            { "43", "7" },
            { "44", "8" },
            { "45", "9" },
            { "46", "10" },
            { "47", "11" },
            { "48", "12" },
            { "49", "13" },
            { "50", "5" },
            { "51", "6" },
            { "52", "7" },
            { "53", "8" },
            { "54", "9" },
            { "55", "10" },
            { "56", "11" },
            { "57", "12" },
            { "58", "13" },
            { "59", "14" },
            { "60", "6" },
            { "61", "7" },
            { "62", "8" },
            { "63", "9" },
            { "64", "10" },
            { "65", "11" },
            { "66", "12" },
            { "67", "13" },
            { "68", "14" },
            { "69", "15" },
            { "70", "7" },
            { "71", "8" },
            { "72", "9" },
            { "73", "10" },
            { "74", "11" },
            { "75", "12" },
            { "76", "13" },
            { "77", "14" },
            { "78", "15" },
            { "79", "16" },
            { "80", "8" },
            { "81", "9" },
            { "82", "10" },
            { "83", "11" },
            { "84", "12" },
            { "85", "13" },
            { "86", "14" },
            { "87", "15" },
            { "88", "16" },
            { "89", "17" },
            { "90", "9" },
            { "91", "10" },
            { "92", "11" },
            { "93", "12" },
            { "94", "13" },
            { "95", "14" },
            { "96", "15" },
            { "97", "16" },
            { "98", "17" },
            { "99", "18" }
        };
    }
}