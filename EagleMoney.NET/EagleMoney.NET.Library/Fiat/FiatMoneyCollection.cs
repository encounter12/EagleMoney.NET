using System;
using System.Collections.ObjectModel;

namespace EagleMoney.NET.Library.Fiat
{
    public class FiatMoneyCollection : Collection<FiatMoney>
    {
        private readonly FiatCurrency _fiatCurrency;

        public FiatMoneyCollection(FiatCurrency currency)
        {
            _fiatCurrency = currency;
        }
        
        protected override void InsertItem (int index, FiatMoney item)
        {
            Validate(item);
            base.InsertItem (index, item);
        }
        
        protected override void SetItem (int index, FiatMoney item)
        {
            Validate(item);
            base.SetItem (index, item);
        }
        
        private void Validate(FiatMoney item)
        {
            if (!item.Currency.Equals(_fiatCurrency))
            {
                throw new InvalidOperationException(
                    $"Money with fiatCurrency different than the default: {_fiatCurrency.Code} can't be added to MoneyCollection");
            }
        }
    }
}