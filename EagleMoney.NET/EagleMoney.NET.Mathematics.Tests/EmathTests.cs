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
        public void Add_2And3_Returns5()
        {
            Assert.That(EMath.Add("2", "3"), Is.EqualTo("5"));
        }
        
        [Test]
        public void Add_4And8_Returns5()
        {
            Assert.That(EMath.Add("4", "8"), Is.EqualTo("12"));
        }

        [Test]
        public void Add_12And12_Returns24()
        {
            Assert.That(EMath.Add("12", "12"), Is.EqualTo("24"));
        }
        
        [Test]
        public void Add_12And0_Returns12()
        {
            Assert.That(EMath.Add("12", "0"), Is.EqualTo("12"));
        }
        
        [Test]
        public void Add_5And3_Returns8()
        {
            Assert.That(EMath.Add("5", "3"), Is.EqualTo("8"));
        }
        
        [Test]
        public void Add_1245And55_Returns1300()
        {
            Assert.That(EMath.Add("1245", "55"), Is.EqualTo("1300"));
        }
        
        [Test]
        public void Add_1000And500_Returns1500()
        {
            Assert.That(EMath.Add("1000", "500"), Is.EqualTo("1500"));
        }
        
        [Test]
        public void Add_0001And0999_Returns1000()
        {
            Assert.That(EMath.Add("0001", "0999"), Is.EqualTo("1000"));
        }
        
        [Test]
        public void Add_0And0_Returns0()
        {
            Assert.That(EMath.Add("0", "0"), Is.EqualTo("0"));
        }
        
        [Test]
        public void Add_001And001_Returns0()
        {
            Assert.That(EMath.Add("001", "001"), Is.EqualTo("2"));
        }
        
        [Test]
        public void Add_5And_3a_ThrowsArgumentException()
        {
            ArgumentException argumentException = Assert.Throws<ArgumentException>(() => EMath.Add("5", "3a"));
            Assert.That(argumentException.Message, Is.EqualTo("The second addend is not a valid number"));
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
    }
}