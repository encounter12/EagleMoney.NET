using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public struct CountryProvider : ICountryProvider
    {
        public IEnumerable<Country> GetCountries()
        {
            var countries = new List<Country>
            {
                new Country("Afghanistan", "AF", "AFG", "004"),
                new Country("Albania", "AL", "ALB", "008"),
                new Country("Algeria", "DZ", "DZA", "012"),
                new Country("American Samoa", "AS", "ASM", "016"),
                new Country("Andorra", "AD", "AND", "020"),
                new Country("Angola", "AO", "AGO", "024"),
                new Country("Anguilla",  "AI", "AIA", "660"),
                new Country("Antarctica", "AQ", "ATA", "010"),
                new Country("Antigua and Barbuda", "AG", "ATG", "028"),
                new Country("Argentina", "AR", "ARG", "032"),
                new Country("Armenia", "AM", "AFG", "004"),
                new Country("United States of America (the)", "US", "USA","840")
            };
            
            return countries;
        }
    }
}