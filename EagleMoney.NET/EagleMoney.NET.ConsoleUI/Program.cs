using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EagleMoney.NET.Library;
using EagleMoney.NET.Library.Countries;
using EagleMoney.NET.Library.Countries.Enums;
using EagleMoney.NET.Library.Crypto;
using EagleMoney.NET.Library.Currencies;
using EagleMoney.NET.Library.Fiat;

namespace EagleMoney.NET.ConsoleUI
{
    internal class Program
    {
        private static void Main()
        {
            var m1 = new FiatMoney(120M, new FiatCurrency(FiatCurrency.USD));

            Console.WriteLine($"m1.ToString(): {m1}");
            Console.WriteLine($"m1.Amount: {m1.Amount}");
            Console.WriteLine($"m1.Currency: {m1.Currency}");

            var m2 = new FiatMoney(120M, FiatCurrency.USD);

            Console.WriteLine($"Are m1 and m2 equal: {m1.Equals(m2)}");
            Console.WriteLine($"Is m1 equal to null: {m1.Equals(null)}");
            Console.WriteLine($"Is m2 equal to 'gosho': {m1.Equals("gosho")}");
            Console.WriteLine($"Is m2 equal to 5: {m1.Equals(5)}");

            int? nullableVar = 14;
            Console.WriteLine($"Is m2 equal to nullableVar: {m1.Equals(nullableVar)}");

            Console.WriteLine($"m1 == m2: {m1 == m2}");
            Console.WriteLine($"m1 == m1: {m1 == m1}");
            Console.WriteLine($"m1 != m2: {m1 != m2}");

            FiatMoney m3 = null;
            FiatMoney m4 = null;

            //Console.WriteLine($"m3.Equals(m4): {m3.Equals(m4)}");
            Console.WriteLine($"m3 == m4: {m3 == m4}");
            Console.WriteLine($"(object)null == (object)null: {(object) null == (object) null}");

            FiatMoney m5 = null;
            FiatMoney m6 = new FiatMoney(65M, FiatCurrency.BGN);

            //Console.WriteLine($"m5.Equals(m5): {m5.Equals(m5)}");

            Console.WriteLine($"m6 == m5: {m6 == m5}");
            Console.WriteLine($"m5 == m6: {m5 == m6}");
            Console.WriteLine($"m5 != m6: {m5 != m6}");

            if (m5 is FiatMoney otherMoney1)
            {
                Console.WriteLine($"otherMoney: {otherMoney1}");
            }

            Console.WriteLine($"m5 is FiatMoney anotherMoney1: {m5 is FiatMoney anotherMoney1}");

            if (m6 is FiatMoney otherMoney2)
            {
                Console.WriteLine($"otherMoney2: {otherMoney2}");
            }

            Console.WriteLine($"m6 is FiatMoney anotherMoney2: {m6 is FiatMoney anotherMoney2}");

            var m7 = new FiatMoney(42, FiatCurrency.EUR);
            FiatMoney m8 = new FiatMoney(42, FiatCurrency.EUR);
            Console.WriteLine($"m7.Equals(m8): {m7.Equals(m8)}");
            
            FiatMoney m10 = new FiatMoney(143, FiatCurrency.EUR);
            //Console.WriteLine($"m9.Equals(m10): {m9.Equals(m10)}");

            var m11 = new FiatMoney(470, FiatCurrency.EUR);
            var m12 = new FiatMoney(140, FiatCurrency.EUR);
            Console.WriteLine($"m11 > m12: {m11 > m12}");
            Console.WriteLine($"m11 >= m12: {m11 >= m12}");
            Console.WriteLine($"m11 < m12: {m11 < m12}");
            Console.WriteLine($"m11 <= m12: {m11 <= m12}");

            // FiatMoney m13 = null;
            // var m14 = new FiatMoney(140, FiatCurrency.EUR);
            // Console.WriteLine($"m13 > m14: {m13 > m14}");
            // Console.WriteLine($"m14 > m13: {m14 > m13}");
            // Console.WriteLine($"m13 > m13: {m13 > m13}");
            // Console.WriteLine($"m13 == m13: {m13 == m13}");

            var m15 = new FiatMoney(30, FiatCurrency.EUR);
            FiatMoney m16 = new FiatMoney(70, FiatCurrency.EUR);
            Console.WriteLine($"m15 > m16: {m15 > m16}");
            Console.WriteLine($"m15 < m16: {m15 < m16}");

            var m17 = new FiatMoney(30.5M, FiatCurrency.EUR);
            var m18 = new FiatMoney(70, FiatCurrency.EUR);
            var m19 = m17 + m18;
            Console.WriteLine($"m19 = m17 + m18: {m19}");

            m19 += 10;
            Console.WriteLine($"m19 += 10: {m19}");

            var m20 = m19 + 10;
            Console.WriteLine($"m20 = m19 + 10: {m20}");

            var m21 = new FiatMoney(20.5M, FiatCurrency.BGN);
            var m22 = new FiatMoney(3M, FiatCurrency.BGN);
            var m23 = m21 * m22;
            Console.WriteLine($"m23 = m21 * m22: {m23}");

            var m24 = new FiatMoney(48.9M, FiatCurrency.BGN);
            var m25 = new FiatMoney(3M, FiatCurrency.BGN);
            var m26 = m24 / m25;
            Console.WriteLine($"m26 = m24 / m25: {m26}");

            var m27 = m24 / 3;
            Console.WriteLine($"m27 = m24 / 3: {m27}");

            var m28 = 90 / m25;
            Console.WriteLine($"m28 = 90 / m25: {m28}");

            var m29 = new FiatMoney(3M, FiatCurrency.BGN);
            var m30 = 100 / m29;
            Console.WriteLine($"m30 = 100 / m29: {m30}");

            var m30MoneyValue = (decimal) m30;
            Console.WriteLine($"m30MoneyValue = (decimal) m30: {m30MoneyValue}");

            // var m31 = new FiatMoney();
            // Console.WriteLine($"m31.ToString(): {m31.ToString()}");

            var m32 = new FiatMoney(5.5M, FiatCurrency.BGN);
            var m33 = m32 % 2;
            Console.WriteLine($"m33 = m32 % 3: {m33}");
            Console.WriteLine($"5.5M % 2: {5.5M % 2}");

            m33++;
            Console.WriteLine($"m33++: {m33}");
            Console.WriteLine($"m33++: {m33++}");
            Console.WriteLine($"++m33: {++m33}");
            Console.WriteLine($"m33 > 0: {m33 > 0}");
            Console.WriteLine($"m33 < 0: {m33 < 0}");
            Console.WriteLine($"m33 == 0: {m33 == 0}");
            Console.WriteLine($"0 == m33: {0 == m33}");

            var n1 = new FiatMoney(1M, FiatCurrency.BGN);

            Console.WriteLine($"n1 == 1: {n1 == 1}");
            Console.WriteLine($"1 == n1: {1 == n1}");
            
            Console.WriteLine($"n1 < 2: {n1 < 2}");
            Console.WriteLine($"2 > n1: {2 > n1}");
            
            Console.WriteLine($"n1 > 2: {n1 > 2}");
            Console.WriteLine($"2 < n1: {2 < n1}");
            
            Console.WriteLine($"n1 >= 2: {n1 >= 2}");
            Console.WriteLine($"n1 <= 2: {n1 <= 2}");
            
            Console.WriteLine($"n1 >= 1: {n1 >= 1}");
            Console.WriteLine($"n1 <= 1: {n1 <= 1}");
            
            Console.WriteLine($"1 >= n1: {1 >= n1}");
            Console.WriteLine($"1 <= n1: {1 <= n1}");

            var m33Casted = (decimal) m33;
            Console.WriteLine($"(decimal) m33: {m33Casted}");

            var emptyList = new List<int>();
            Console.WriteLine($"Is m2 equal to emptyList: {m1.Equals(emptyList)}");

            List<int> nullList = null;
            Console.WriteLine($"Is m2 equal to nullList: {m1.Equals(nullList)}");

            Console.WriteLine($"m1.GetHashCode(): {m1.GetHashCode()}");
            Console.WriteLine($"m1.GetHashCode(): {m1.GetHashCode()}");

            Console.WriteLine($"m2.GetHashCode(): {m2.GetHashCode()}");
            Console.WriteLine($"m2.GetHashCode(): {m2.GetHashCode()}");

            Console.WriteLine($"Are m1 and m2 Hash Codes equal: {m1.GetHashCode() == m2.GetHashCode()}");

            var searchedMoney = new FiatMoney(10M, FiatCurrency.BGN);

            var moneyList = new List<FiatMoney>
            {
                new(10M, FiatCurrency.EUR),
                new(10M, FiatCurrency.BGN),
                new(15.5M, FiatCurrency.BGN),
                new(120.50M, FiatCurrency.BGN),
                new(10M, FiatCurrency.BGN)
            };

            var moneyExists = moneyList.Contains(searchedMoney);

            Console.WriteLine($"moneyList.Contains(searchedMoney): {moneyExists}");

            var indexOfSearchedMoney = moneyList.IndexOf(searchedMoney);
            Console.WriteLine($"moneyList.IndexOf(searchedMoney): {indexOfSearchedMoney}");

            var lastIndexOfSearchedMoney = moneyList.LastIndexOf(searchedMoney);
            Console.WriteLine($"moneyList.IndexOf(searchedMoney): {lastIndexOfSearchedMoney}");

            var m34 = new FiatMoney(10M, FiatCurrency.BGN);

            // var dict4 = new Dictionary<FiatMoney, string>()
            // {
            //     { m34, "John"},
            //     { m34, "John"}
            // };

            //dict.Add(new FiatMoney(10M, CurrencyCode.Bgn), "Gosho");

            var dict = new Dictionary<FiatMoney, string>
            {
                [m34] = "John",
                [m34] = "John"
            };

            var containsMoneyKey = dict.ContainsKey(m34);

            Console.WriteLine($"dict.ContainsKey(new FiatMoney(10, CurrencyCode.BGN)):{containsMoneyKey}");
            Console.WriteLine($"dict.Count:{dict.Count}");

            var dict2 = new Dictionary<FiatMoney, string>
            {
                {new FiatMoney(10M, FiatCurrency.BGN), "Mike"},
                {new FiatMoney(15M, FiatCurrency.BGN), "John"},
                {new FiatMoney(25.5M, FiatCurrency.BGN), "Lisa"}
            };

            var containsMoneyKey2 = dict2.ContainsKey(new FiatMoney(15M, FiatCurrency.BGN));
            Console.WriteLine($"dict2.ContainsKey(new FiatMoney(15M, CurrencyCode.BGN)):{containsMoneyKey2}");

            var containsMoneyKey3 = dict2.ContainsKey(new FiatMoney(41.5M, FiatCurrency.BGN));
            Console.WriteLine($"dict2.ContainsKey(new FiatMoney(41.5M, CurrencyCode.BGN)):{containsMoneyKey3}");

            var containsMoneyKey4 = dict2.ContainsKey(new FiatMoney(10M, FiatCurrency.USD));
            Console.WriteLine($"dict2.ContainsKey(new FiatMoney(10M, CurrencyCode.USD)):{containsMoneyKey4}");

            //var m35 = new FiatMoney(-4.5M, CurrencyCode.BGN);
            
            // var m36 = new FiatMoney(8.5M, CurrencyCode.BGN);
            // var m37 = new FiatMoney(4.5M, CurrencyCode.USD);
            //
            // Console.WriteLine($"m36 > m37: {m36 > m37}");

            // var moneyCollection = new FiatMoneyCollection(CurrencyCode.USD)
            // {
            //     new FiatMoney(15.5M, CurrencyCode.EUR),
            //     new FiatMoney(40.5M, CurrencyCode.USD)
            // };

            var moneyCollection2 = new FiatMoneyCollection(new FiatCurrency(FiatCurrency.USD))
            {
                new(15.5M, FiatCurrency.USD), 
                new(40.5M, FiatCurrency.USD), 
                new(130.72M, FiatCurrency.USD)
            };

            // moneyCollection2.Add(new FiatMoney(130.72M, FiatCurrency.AED));


            foreach (var money in moneyCollection2)
            {
                Console.WriteLine(money.ToString());
            }

            Console.WriteLine("-----------");

            var moneyCollection3 = moneyCollection2.Where(m => m.Amount == 130.72M);

            foreach (var money in moneyCollection3)
            {
                Console.WriteLine(money.ToString());
            }

            Console.WriteLine($"FiatMoney.USD(24.5M).ToString():{FiatMoney.USD(24.5M)}");
            Console.WriteLine($"FiatMoney.EUR(24.5M).ToString():{FiatMoney.EUR(24.5M)}");
            Console.WriteLine($"FiatMoney.BGN(24.5M).ToString():{FiatMoney.BGN(24.5M)}");
            
            var e = new FiatCurrency(FiatCurrency.USD);
            Console.WriteLine($"new FiatCurrency(FiatCurrency.USD).ToString():{e}");

            var d = new FiatCurrency("EUR");
            Console.WriteLine($"new FiatCurrency(FiatCurrency.USD).ToString():{d}");

            var m40 = new FiatMoney(0.3M, FiatCurrency.BGN);

            IMoney[] allocated = m40.AllocateEven(3);

            foreach (var item in allocated)
            {
                Console.WriteLine($"allocated element: {item.Amount} {item.Currency.Code}");
            }

            Console.WriteLine("----------------------");
            
            var m41 = new FiatMoney(0.5M, FiatCurrency.BGN);

            IMoney[] allocated2 = m41.AllocateEven(3);

            foreach (var item in allocated2)
            {
                Console.WriteLine($"allocated element: {item.Amount} {item.Currency.Code}");
            }
            
            Console.WriteLine("----------------------");
            
            var m42 = new FiatMoney(0.05M, FiatCurrency.BGN);

            IMoney[] allocated3 = m42.AllocateEven(3);

            foreach (var item in allocated3)
            {
                Console.WriteLine($"allocated element: {item.Amount} {item.Currency.Code}");
            }

            var m43 = new FiatMoney(12.45M, new FiatCurrency("Bitcoin", 2, "‡", "Bitcoin"));

            Console.WriteLine($"m43 (custom currency): {m43}");
            
            var m44 = new FiatMoney(15M, new FiatCurrency("Bitcoin", 2, "‡", "Bitcoin"));

            var m45 = m43 + m44;

            Console.WriteLine($"Adding custom currencies (m43 + m44):{m45.Amount} {m45.Currency.Code}");

            int[] allocation = {3, 10, 1, 4, 3};

            IMoney[] allocatedByRatios = FiatMoney.USD(100M).AllocateByRatios(allocation);

            for (int i = 0; i < allocatedByRatios.Length; i++)
            {
                Console.WriteLine($"Allocated by ratio {i}: {allocatedByRatios[i].Amount} {allocatedByRatios[i].Currency.Code}");
            }

            var m46 = FiatMoney.USD(10M);
            var m47 = m46.Percentage(20);
            Console.WriteLine($"m46.Percentage(20): {m47.Amount} {m47.Currency.Code}");
            
            m47 = m46.Percentage(21.5M);
            Console.WriteLine($"m46.Percentage(20): {m47.Amount} {m47.Currency.Code}");
            
            var m48 = new FiatMoney(3.445446M, MidpointRounding.AwayFromZero, FiatCurrency.USD);
            Console.WriteLine($"m48 - MidpointRounding.AwayFromZero: {m48.Amount} {m48.Currency.Code}");

            var m49 = new FiatMoney(124.5M, new FiatCurrency("AFN"));

            Console.WriteLine(m49);
            
            var m51 = FiatMoney.BGN(380.52M, MidpointRounding.AwayFromZero);

            Console.WriteLine(m51);

            var m52 = FiatMoney.USD(124.43M);

            Console.WriteLine(m51 == m52);

            // var m53 = FiatMoney.USD(79228162514264337593543950335M);
            
            // var m53 = FiatMoney.USD(Decimal.MaxValue / 99);

            var m54 = new FiatMoney(124.5M, new FiatCurrency("EUR"));

            foreach (var x in ((FiatCurrency)m54.Currency).Countries)
            {
                Console.WriteLine(x);
            }

            var m55 = new FiatMoney(12.3M, FiatCurrency.AFN);

            Console.WriteLine($"{m55.Amount} {m55.Currency}");
            
            Console.WriteLine("FiatCurrency Symbol: {0}",
                NumberFormatInfo.GetInstance(new CultureInfo("en-GB")).CurrencySymbol);
            
            // Gets a NumberFormatInfo associated with the en-US culture.
            NumberFormatInfo nfi = new CultureInfo( "en-US", false ).NumberFormat;

            // Displays a negative value with the default number of decimal digits (2).
            Int64 myInt = -1234;
            Console.WriteLine( myInt.ToString( "C", nfi ) );

            // Displays the same value with four decimal digits.
            nfi.CurrencyDecimalDigits = 4;
            Console.WriteLine( myInt.ToString( "C", nfi ) );
            
            // CultureInfo[] custom = CultureInfo.GetCultures(CultureTypes.UserCustomCulture);
            // if (custom.Length == 0) {
            //     Console.WriteLine("There are no user-defined custom cultures.");
            // }
            // else {
            //     Console.WriteLine("Custom cultures:");
            //     foreach (var culture in custom)
            //         Console.WriteLine("   {0} -- {1}", culture.Name, culture.DisplayName);
            // }
            //
            // CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            //
            // foreach (var ci in cultures)
            // {
            //     Console.WriteLine($"{ci.Name} - {ci.DisplayName}");
            // }

            string symbol;
            var currencyProvider = new CurrencyProvider();
            
            bool symbolExists = currencyProvider.TryGetCurrencySymbol("USD", out symbol);
            
            bool symbolExists2 = currencyProvider.TryGetCurrencySymbol("USD", out symbol);

            Console.WriteLine(symbol);

            string symbol2;
            if(new CurrencyProvider().TryGetCurrencySymbol("BGN", out symbol2))
            {
                Console.WriteLine("symbol2 is {0}", symbol2);
            }

            // foreach (var curr in CurrencyTools.CurrencyCodes)
            // {
            //     Console.WriteLine(curr);
            // }

            var m56 = FiatMoney.CAD(120m);

            Console.WriteLine(m56);

            var m57 = FiatMoney.SAR(15.25M);

            var m58 = FiatMoney.AOA(15.34M, MidpointRounding.ToZero);

            Console.WriteLine(m58);

            Console.WriteLine($"m58.ToString('C'): {m58.ToString("C")}");

            IMoney[] allocatedEven = m58.AllocateEven(5);

            foreach (var ae in allocatedEven)
            {
                Console.WriteLine($"{ae.Amount} { ae.Currency.Symbol} {ae.Currency.Code}");
            }

            foreach (var ab in allocatedEven)
            {
                Console.WriteLine(ab.ToString("C"));
            }

            var m59 = FiatMoney.USD(12.34m);
            Console.WriteLine(m59.ToString("C"));
            
            var m60 = FiatMoney.EUR(40.15M);
            Console.WriteLine(m60.ToString("C"));
            
            var m61 = FiatMoney.GBP(72.13m);
            Console.WriteLine(m61.ToString("C"));
            
            var m62 = FiatMoney.BGN(83.95m);
            Console.WriteLine(m62.ToString("C"));
            
            var m63 = FiatMoney.CHF(100000.23m);
            Console.WriteLine(m63.ToString("C"));
            
            var m64 = FiatMoney.AFN(125.34m);
            Console.WriteLine(m64.ToString("C"));
            
            var m65 = FiatMoney.USD(1234.56m);
            Console.WriteLine(m65);
            
            var m66 = FiatMoney.USD(432100000009.24m);
            Console.WriteLine($"{m66.Amount} {m66.Currency.Symbol}");

            Console.WriteLine($"{(m66 + 0.01m).Amount}");

            var m67 = new FiatMoney(12.45m, FiatCurrency.BGN);

            var m68 = FiatMoney.MKD(12.45m);

            Console.WriteLine(m68);
            Console.WriteLine(m68.ToString("C"));
            
            var m69 = FiatMoney.BGN(12.45m);
            Console.WriteLine(m69.ToString("C"));

            var m70 = FiatMoney.Parse("678.423", FiatCurrency.BGN);

            Console.WriteLine(m70);
            Console.WriteLine(m70.ToString("C"));

            IMoney m71 = new FiatMoney(12.456m, FiatCurrency.AED);

            Console.WriteLine(m71);
            
            var m72 = new FiatMoney(
                14.32m,
                new FiatCurrency(
                    "GAC", 
                    new FiatCurrencyFactory(new CustomCurrencyProvider(), new CustomCountriesProvider())));

            Console.WriteLine(m72);
            
            Console.WriteLine(m72.ToString("C"));

            var m73 = FiatMoney.USD(15.43M);

            foreach (var ca in ((FiatCurrency)m73.Currency).Countries)
            {
                Console.WriteLine($"{ca.ShortNameLowerCase} {ca.CodeAlpha2} {ca.CodeAlpha3}");
            }

            var m74 = new FiatMoney(14.2m, CountryCode.US);

            Console.WriteLine(m74);
            Console.WriteLine(m74.ToString("C"));

            var m75 = new FiatMoney(156.5m, CountryCodeAlpha3.USA);

            foreach (var country in ((FiatCurrency)m75.Currency).Countries)
            {
                Console.WriteLine($"currencyCountry: {country.ShortNameLowerCase}");
            }

            var m76 = new FiatMoney(45.23m, FiatCurrency.BGN);

            foreach (var country in ((FiatCurrency)m76.Currency).Countries)
            {
                Console.WriteLine(
                    $"{country.ShortNameLowerCase} {country.CodeAlpha2} {country.NumericCode} {country.FullName}");
            }

            var m77 = new FiatMoney(12.5m, new CryptoCurrency(CryptoCurrency.BTC));

            Console.WriteLine(m77);

            var m78 = FiatMoney.USD(12.5M);

            FiatMoney m79 = m78;
            
            Console.WriteLine(m78.Equals(m79));

            Console.WriteLine(object.ReferenceEquals(m78, m79));
            
            Console.WriteLine(object.ReferenceEquals(m78.Currency, m79.Currency));
            
            Console.WriteLine(m78.Amount.Equals(m79.Amount));
        }
    }
    
    public struct CustomCurrencyProvider : ICurrencyProvider
    {
        public IEnumerable<CurrencyCountriesBasicInfo> GetCurrencies()
        {
            return new List<CurrencyCountriesBasicInfo>
            {
                new CurrencyCountriesBasicInfo(
                    "GAC", 
                    "",
                    "321", 
                    "^", 
                    2, 
                    new HashSet<string>
                    {
                        "Bulgaria",
                        "Tanzania"
                    })
            };
        }
    }
    
    public struct CustomCountriesProvider : ICountryProvider
    {
        public IEnumerable<Country> GetCountries()
        {
            return new List<Country>
            {
                new Country
                {
                    ShortNameLowerCase = "Bulgaria",
                    CodeAlpha2 = "DA",
                    CodeAlpha3 = "DAS",
                    NumericCode = "005"
                }
            };
        }
    }
}