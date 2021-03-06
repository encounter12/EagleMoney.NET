# EagleMoney.NET
A simple library implementing Money Value Object in .NET.

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

<h3>Multiplication:</h3>

```csharp
// FiatMoney
var m1 = FiatMoney.USD(120M);
var m2 = FiatMoney.USD(45.50M);

var m3 = m1 * m2;

// CryptoMoney
var m4 = CryptoMoney.BTC(5M);
CryptoMoney m5 = m4  * 4.25M;
```

<h3>Division:</h3>

```csharp
// FiatMoney
var m1 = FiatMoney.USD(120M);
var m2 = FiatMoney.USD(45.50M);

var m3 = m1 / m2;

// CryptoMoney
var m4 = CryptoMoney.BTC(5M);
CryptoMoney m5 = m4  / 4.25M;
```

<h3>Allocation</h3>

<div>
    Allocates money value into n equal or almost equal parts (if amount cannot be equally split)
</div>

```csharp
// n = 3
var m1 = FiatMoney.USD(0.5M);
IMoney[] allocated = m1.AllocateEven(3);
Console.WriteLine(String.Join(", ", allocated.Select(a => a.Amount))); // Prints: 0.17, 0.17, 0.16
```

<h3>ToString():</h3>

```csharp
// FiatMoney
var m1 = FiatMoney.USD(120.5M);

Console.WriteLine(m1.ToString()); // displays: 120 USD
Console.WriteLine(m1.ToString("C")); // displays localized money string: $120.50
```

References:

<p>
    <a href="https://martinfowler.com/eaaCatalog/money.html" target="_blank">Martin Fowler (P of EAA Catalog): Money</a><br />
    <a href="https://martinfowler.com/bliki/ValueObject.html">Martin Fowler: ValueObject</a><br />
    <a href="https://martinfowler.com/bliki/EvansClassification.html">Martin Fowler - EvansClassification</a><br />
    <a href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects">Implement value objects (.NET microservices - Architecture e-book)</a><br />
    <a href="https://en.wikipedia.org/wiki/Value_object">Wikipedia: Value object</a><br />
    <a href="http://wiki.c2.com/?ValueObjectsShouldBeImmutable">Ward Cunningham Wiki: Value Objects Should Be Immutable</a>
</p>