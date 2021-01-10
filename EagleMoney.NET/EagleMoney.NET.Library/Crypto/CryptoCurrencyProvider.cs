using System.Collections.Generic;
using System.Linq;
using EagleMoney.NET.Library.Crypto.Enums;

namespace EagleMoney.NET.Library.Crypto
{
    public class CryptoCurrencyProvider : ICryptoCurrencyProvider
    {
        // Sources:
        // All Cryptocurrencies - https://coinmarketcap.com/all/views/all/
        // Top CryptoCurrencies (By Market Cap) - https://www.advfn.com/cryptocurrency
        // List of cryptocurrencies - https://en.wikipedia.org/wiki/List_of_cryptocurrencies
        private IEnumerable<CryptoCurrency> GetCurrencies()
        {
            var cryptocurrencies = new List<CryptoCurrency>
            {
                new CryptoCurrency
                {
                    Name = "Bitcoin",
                    Code = "BTC",
                    HashAlgorithm = HashAlgorithm.SHA256d.ToString(),
                    MinorUnit = "Satoshi",
                    ConsensusMechanism = ConsensusMechanism.ProofOfWork.ToString(),
                    ProgrammingLanguages = new List<string>
                    {
                        ProgrammingLanguage.CPlusPlus.ToString()
                    },
                    Founder = "Satoshi Nakamoto"
                },
                new CryptoCurrency
                {
                    Name = "Ethereum",
                    Code = "ETH",
                    HashAlgorithm = HashAlgorithm.Ethash.ToString(),
                    MinorUnit = "Gwei",
                    ConsensusMechanism = ConsensusMechanism.ProofOfWork.ToString(),
                    ProgrammingLanguages = new List<string>
                    {
                        ProgrammingLanguage.CPlusPlus.ToString(), 
                        ProgrammingLanguage.Go.ToString()
                    },
                    Founder = "Vitalik Buterin"
                }
            };
            
            return cryptocurrencies;
        }

        public CryptoCurrency GetCurrency(string currencyCode)
        {
            var currency = this.GetCurrencies().First(c => c.Code == currencyCode);
            return currency;
        }
    }
}