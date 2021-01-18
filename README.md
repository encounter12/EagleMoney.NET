# EagleMoney.NET
A simple library implementing Money Value Object in .NET

<h3>Instantiation:</h3>

```csharp
// FiatMoney
var m1 = FiatMoney.USD(15.43M);
var m2 = new FiatMoney(45.23M, FiatCurrency.BGN);
var m3 = new FiatMoney(14.2M, CountryCode.US);

// CryptoMoney
var m5 = CryptoMoney.BTC(12.5M);
var m6 = new CryptoMoney(12.5M, CryptoCurrency.BTC);
var m7 = new CryptoMoney(12.5M, new CryptoCurrency("BTC"));
```