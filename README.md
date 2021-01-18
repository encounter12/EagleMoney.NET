# EagleMoney.NET
A simple library implementing Money Value Object in .NET

<h3>Instantiation:</h3>

```csharp
// FiatMoney
var m1 = FiatMoney.USD(15.40M);
var m2 = new FiatMoney(15.40M, FiatCurrency.USD);
var m3 = new FiatMoney(15.40M, CountryCode.US);

// CryptoMoney
var m4 = CryptoMoney.BTC(12.5M);
var m5 = new CryptoMoney(12.5M, CryptoCurrency.BTC);
var m6 = new CryptoMoney(12.5M, new CryptoCurrency("BTC"));
```

<p>* When you perform arithmetic operations either both operands should be of one money type, or one money type and decimal / int.</p>

<h3>Addition:</h3>

```csharp
// FiatMoney
var m1 = FiatMoney.USD(120M);
var m2 = FiatMoney.USD(45.50M);

var m3 = m1 + m2;

// CryptoMoney
var m4 = CryptoMoney.BTC(5M);
CryptoMoney m5 = m4 + 4.25M;
```

<h3>Subtraction:</h3>

```csharp
// FiatMoney
var m1 = FiatMoney.USD(120M);
var m2 = FiatMoney.USD(45.50M);

var m3 = m1 - m2;

// CryptoMoney
var m4 = CryptoMoney.BTC(5M);
CryptoMoney m5 = m4 - 4.25M;
```