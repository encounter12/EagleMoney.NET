using System;
using System.Linq;
using EagleMoney.NET.Library.Mathematics;

namespace EagleMoney.NET.ConsoleMath
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(EMath.Add("12", "12"));
            
            Console.WriteLine(EMath.Add("12", "0"));
            
            Console.WriteLine(EMath.Add("5", "3"));
            
            Console.WriteLine(EMath.Add("1245", "55"));
            
            Console.WriteLine(EMath.Add("1000", "500"));
            
            Console.WriteLine(EMath.Add("0001", "0999"));

            Console.WriteLine(EMath.IsValidNumber("123"));
            Console.WriteLine(EMath.IsValidNumber("123abc"));
            Console.WriteLine(EMath.IsValidNumber("123.a"));
            Console.WriteLine(EMath.IsValidNumber("123.45"));
            
            // Console.WriteLine(Ma.Add("5", "3a")); //should throw an error
            
            string sum = EMath.GetFundamentalAdditionSum("2", "3");
            Console.WriteLine(sum);
            
            string sum2 = EMath.GetFundamentalAdditionSum("4", "8");
            Console.WriteLine(sum2);
            
            // string sum3 = EMath.GetFundamentalAdditionSum("4", "10"); //should throw an error

            int comparison = EMath.CompareTo("3", "5");
            Console.WriteLine(comparison);
            
            int comparison2 = EMath.CompareTo("1200", "5");
            Console.WriteLine(comparison2);
            
            int comparison3 = EMath.CompareTo("12300", "12300");
            Console.WriteLine(comparison3);
            
            string diff = EMath.Subtract("4", "2");
            Console.WriteLine(diff);
            
            string diff2 = EMath.Subtract("9", "1");
            Console.WriteLine(diff2);
            
            string diff3 = EMath.Subtract("50", "1");
            Console.WriteLine(diff3);
            
            string diff4 = EMath.Subtract("7", "12");
            Console.WriteLine(diff4);
            
            string diff5 = EMath.Subtract("101", "102");
            Console.WriteLine(diff5);
            
            string diff6 = EMath.Subtract("1003", "4");
            Console.WriteLine(diff6);
        }
    }
}