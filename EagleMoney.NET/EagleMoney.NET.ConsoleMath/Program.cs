using System;
using System.Linq;
using EagleMoney.NET.Library.Mathematics;

namespace EagleMoney.NET.ConsoleMath
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(EMath.Add("5", "-3"));
            
            Console.WriteLine(EMath.Add("-5", "3"));
            
            Console.WriteLine(EMath.Add("-1", "-3"));
            
            Console.WriteLine(EMath.Add("5.2", "3.4"));
            
            Console.WriteLine(EMath.Add("2.81", "578.211"));
            
            Console.WriteLine(EMath.Add("-2.5", "-2.5"));
        }
    }
}