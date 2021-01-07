using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public struct CountryProvider : ICountryProvider
    {
        // https://www.iso.org/obp/ui/#iso:pub:PUB500001:en
        // https://www.iso.org/obp/ui/#search/code/
        public IEnumerable<Country> GetCountries()
        {
            var countries = new List<Country>
            {
                new Country
                {
                  Name  = "Afghanistan",
                  FullName = "the Islamic Republic of Afghanistan",
                  CodeAlpha2 = "AF",
                  CodeAlpha3 = "AFG",
                  NumericCode = "004"
                },
                new Country
                {
                    Name  = "Albania",
                    FullName = "the Republic of Albania",
                    CodeAlpha2 = "AL",
                    CodeAlpha3 = "ALB",
                    NumericCode = "008"
                },
                new Country
                {
                    Name  = "Algeria",
                    FullName = "the People's Democratic Republic of Algeria",
                    CodeAlpha2 = "DZ",
                    CodeAlpha3 = "DZA",
                    NumericCode = "012"
                },
                new Country
                {
                    Name  = "American Samoa",
                    FullName = "",
                    CodeAlpha2 = "AS",
                    CodeAlpha3 = "ASM",
                    NumericCode = "016"
                },
                new Country
                {
                    Name  = "Andorra",
                    FullName = "the Principality of Andorra",
                    CodeAlpha2 = "AD",
                    CodeAlpha3 = "AND",
                    NumericCode = "020"
                },
                new Country
                {
                    Name  = "Angola",
                    FullName = "the Republic of Angola",
                    CodeAlpha2 = "AO",
                    CodeAlpha3 = "AGO",
                    NumericCode = "024"
                },
                new Country
                {
                    Name  = "Anguilla",
                    FullName = "",
                    CodeAlpha2 = "AI",
                    CodeAlpha3 = "AIA",
                    NumericCode = "660"
                },
                new Country
                {
                    Name  = "Antarctica",
                    FullName = "",
                    CodeAlpha2 = "AQ",
                    CodeAlpha3 = "ATA",
                    NumericCode = "010"
                },
                new Country
                {
                    Name  = "Antigua and Barbuda",
                    FullName = "",
                    CodeAlpha2 = "AG",
                    CodeAlpha3 = "ATG",
                    NumericCode = "028"
                },
                new Country
                {
                    Name  = "Argentina",
                    FullName = "the Argentine Republic",
                    CodeAlpha2 = "AR",
                    CodeAlpha3 = "ARG",
                    NumericCode = "032"
                },
                new Country
                {
                    Name  = "Armenia",
                    FullName = "the Republic of Armenia",
                    CodeAlpha2 = "AM",
                    CodeAlpha3 = "ARM",
                    NumericCode = "051"
                },
                new Country
                {
                    Name  = "Aruba",
                    FullName = "",
                    CodeAlpha2 = "AW",
                    CodeAlpha3 = "ABW",
                    NumericCode = "533"
                },
                new Country
                {
                    Name  = "Australia",
                    FullName = "",
                    CodeAlpha2 = "AU",
                    CodeAlpha3 = "AUS",
                    NumericCode = "036"
                },
                new Country
                {
                    Name  = "Austria",
                    FullName = "the Republic of Austria",
                    CodeAlpha2 = "AT",
                    CodeAlpha3 = "AUT",
                    NumericCode = "040"
                },
                new Country
                {
                    Name  = "Azerbaijan",
                    FullName = "the Republic of Azerbaijan",
                    CodeAlpha2 = "AZ",
                    CodeAlpha3 = "AZE",
                    NumericCode = "031"
                },
                new Country
                {
                    Name  = "Bahamas (the)",
                    FullName = "the Commonwealth of the Bahamas",
                    CodeAlpha2 = "BS",
                    CodeAlpha3 = "BHS",
                    NumericCode = "044"
                },
                new Country
                {
                    Name  = "Bahrain",
                    FullName = "the Kingdom of Bahrain",
                    CodeAlpha2 = "BH",
                    CodeAlpha3 = "BHR",
                    NumericCode = "048"
                },
                new Country
                {
                    Name  = "Bangladesh",
                    FullName = "the People's Republic of Bangladesh",
                    CodeAlpha2 = "BD",
                    CodeAlpha3 = "BGD",
                    NumericCode = "050"
                },
                new Country
                {
                    Name  = "Barbados",
                    FullName = "",
                    CodeAlpha2 = "BB",
                    CodeAlpha3 = "BRB",
                    NumericCode = "052"
                },
                new Country
                {
                    Name  = "Belarus",
                    FullName = "the Republic of Belarus",
                    CodeAlpha2 = "BY",
                    CodeAlpha3 = "BLR",
                    NumericCode = "112"
                },
                new Country
                {
                    Name  = "Belgium",
                    FullName = "the Kingdom of Belgium",
                    CodeAlpha2 = "BE",
                    CodeAlpha3 = "BEL",
                    NumericCode = "056"
                },
                new Country
                {
                    Name  = "Belize",
                    FullName = "",
                    CodeAlpha2 = "BZ",
                    CodeAlpha3 = "BLZ",
                    NumericCode = "084"
                },
                new Country
                {
                    Name  = "Benin",
                    FullName = "the Republic of Benin",
                    CodeAlpha2 = "BJ",
                    CodeAlpha3 = "BEN",
                    NumericCode = "204"
                },
                new Country
                {
                    Name  = "Bermuda",
                    CodeAlpha2 = "BM",
                    CodeAlpha3 = "BMU",
                    NumericCode = "060"
                },
                new Country
                {
                    Name  = "Åland Islands",
                    CodeAlpha2 = "AX",
                    CodeAlpha3 = "ALA",
                    NumericCode = "248"
                },
                new Country
                {
                    Name  = "United States of America (the)",
                    FullName = "the United States of America",
                    CodeAlpha2 = "US",
                    CodeAlpha3 = "USA",
                    NumericCode = "840"
                },
            };
            
            return countries;
        }
    }
}