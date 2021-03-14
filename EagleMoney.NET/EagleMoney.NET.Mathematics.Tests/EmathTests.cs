using System;
using EagleMoney.NET.Library.Mathematics;
using NUnit.Framework;

namespace EagleMoney.NET.Mathematics.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        [TestCase("2", "3", "5")]
        [TestCase("4", "8", "12")]
        [TestCase("5", "3", "8")]
        [TestCase("12", "12", "24")]
        [TestCase("1000", "500", "1500")]
        [TestCase("1245", "55", "1300")]
        public void Add_PositiveNaturalNumbers_ReturnsResult(string firstAddend, string secondAddend, string result)
        {
            Assert.AreEqual(result, EMath.Add(firstAddend, secondAddend));
        }
        
        [Test]
        [TestCase("12", "0", "12")]
        [TestCase("0", "12", "12")]
        [TestCase("5670", "0", "5670")]
        [TestCase("0", "5670", "5670")]
        public void Add_PositiveNaturalNumberAndZero_ReturnsResult(
            string firstAddend,
            string secondAddend,
            string result)
        {
            Assert.AreEqual(result, EMath.Add(firstAddend, secondAddend));
        }

        [Test]
        [TestCase("0001", "0999", "1000")]
        [TestCase("001", "001", "2")]
        [TestCase("0000001", "09", "10")]
        public void Add_PositiveNaturalNumbersHavingLeadingZeros_ReturnsResult(
            string firstAddend,
            string secondAddend,
            string result)
        {
            Assert.AreEqual(result, EMath.Add(firstAddend, secondAddend));
        }
        
        [Test]
        [TestCase("0", "0")]
        [TestCase("00", "0")]
        [TestCase("00", "000")]
        [TestCase("0", "00")]
        [TestCase("0", "000")]
        public void Add_ZeroAndZero_ReturnsZero(string firstAddend, string secondAddend)
        {
            Assert.AreEqual("0",EMath.Add(firstAddend, secondAddend));
        }
        
        [Test]
        public void Add_WhenOneAddendContainsLetter_ThrowsArgumentException()
        {
            ArgumentException argumentException = Assert.Throws<ArgumentException>(() => EMath.Add("5", "3a"));
            Assert.AreEqual(argumentException.Message, "The second addend is not a valid number");
        }
        
        [Test]
        [TestCase("4", "2", "2")]
        [TestCase("9", "1", "8")]
        [TestCase("50", "1", "49")]
        [TestCase("50", "15", "35")]
        [TestCase("1003", "4", "999")]
        [TestCase("1003", "004", "999")]
        [TestCase("001003", "004", "999")]
        [TestCase("1287", "37", "1250")]
        public void Subtract_PositiveIntegersMinuendGreaterThanSubtrahend_ReturnsResult(
            string minuend,
            string subtrahend,
            string result)
        {
            Assert.AreEqual(result, EMath.Subtract(minuend, subtrahend));
        }
        
        [Test]
        [TestCase("1", "3", "-2")]
        [TestCase("1", "30", "-29")]
        [TestCase("2", "4", "-2")]
        [TestCase("7", "12", "-5")]
        [TestCase("101", "102", "-1")]
        public void Subtract_PositiveIntegersMinuendLessThanSubtrahend_ReturnsResult(
            string minuend,
            string subtrahend,
            string result)
        {
            Assert.AreEqual(result, EMath.Subtract(minuend, subtrahend));
        }
        
        [Test]
        [TestCase("-5", "3", "-8")]
        [TestCase("-1", "3", "-4")]
        public void Subtract_MinuendNegativeSubtrahendPositive_ReturnsResult(
            string minuend,
            string subtrahend,
            string result)
        {
            Assert.AreEqual(result, EMath.Subtract(minuend, subtrahend));
        }
        
        [Test]
        [TestCase("1", "-3", "4")]
        [TestCase("05", "-3", "8")]
        public void Subtract_MinuendPositiveSubtrahendNegative_ReturnsResult(
            string minuend,
            string subtrahend,
            string result)
        {
            Assert.AreEqual(result, EMath.Subtract(minuend, subtrahend));
        }
        
        [Test]
        [TestCase("-1", "-3", "2")]
        [TestCase("-1002", "-3", "-999")]
        [TestCase("-001002", "-3", "-999")]
        [TestCase("-001002", "-003", "-999")]
        public void Subtract_MinuendNegativeSubtrahendNegative_ReturnsResult(
            string minuend,
            string subtrahend,
            string result)
        {
            Assert.AreEqual(result, EMath.Subtract(minuend, subtrahend));
        }
        
        [Test]
        public void IsValidNumber_WhenValidPositiveNaturalNumber_ReturnsTrue()
        {
            Assert.IsTrue(EMath.IsValidNumber("123"));
        }
        
        [Test]
        public void IsValidNumber_WhenDigitsAndLetters_ReturnsFalse()
        {
            Assert.IsFalse(EMath.IsValidNumber("123abc"));
        }
        
        [Test]
        public void IsValidNumber_WhenDigitsDotAndLetter_ReturnsFalse()
        {
            Assert.IsFalse(EMath.IsValidNumber("123.a"));
        }
        
        [Test]
        public void IsValidNumber_WhenValidDecimal_ReturnsTrue()
        {
            Assert.True(EMath.IsValidNumber("123.45"));
        }
        
        [Test]
        [TestCase("3", "5", -1)]
        [TestCase("1200", "5", 1)]
        [TestCase("12300", "12300", 0)]
        public void ComparePositiveNaturalNumbers_ReturnsResult(string firstDigit, string secondDigit, int result)
        {
            Assert.AreEqual(result, EMath.ComparePositiveNaturalNumbers(firstDigit, secondDigit));
        }
    }
}