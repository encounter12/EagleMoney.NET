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
        }
    }
}