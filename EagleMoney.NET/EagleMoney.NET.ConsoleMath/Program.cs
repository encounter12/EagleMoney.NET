using System;
using EagleMoney.NET.Library.Mathematics;

namespace EagleMoney.NET.ConsoleMath
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Ma.Add("12", "12"));
            
            Console.WriteLine(Ma.Add("12", "0"));
            
            Console.WriteLine(Ma.Add("5", "3"));
            
            Console.WriteLine(Ma.Add("1245", "55"));
            
            // Console.WriteLine(Ma.Add("5", "3a"));
        }
    }
}