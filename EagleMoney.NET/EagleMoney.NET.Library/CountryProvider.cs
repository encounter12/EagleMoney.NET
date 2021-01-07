using System.Collections.Generic;

namespace EagleMoney.NET.Library
{
    public struct CountryProvider : ICountryProvider
    {
        // ISO 3166 - Country codes
        // https://www.iso.org/obp/ui/#search/code/
        // https://www.iso.org/obp/ui/#iso:pub:PUB500001:en
        public IEnumerable<Country> GetCountries()
        {
            var countries = new List<Country>
            {
                new Country
                {
                  Name = "Afghanistan",
                  FullName = "the Islamic Republic of Afghanistan",
                  CodeAlpha2 = "AF",
                  CodeAlpha3 = "AFG",
                  NumericCode = "004"
                },
                new Country
                {
                    Name = "Albania",
                    FullName = "the Republic of Albania",
                    CodeAlpha2 = "AL",
                    CodeAlpha3 = "ALB",
                    NumericCode = "008"
                },
                new Country
                {
                    Name = "Algeria",
                    FullName = "the People's Democratic Republic of Algeria",
                    CodeAlpha2 = "DZ",
                    CodeAlpha3 = "DZA",
                    NumericCode = "012"
                },
                new Country
                {
                    Name = "American Samoa",
                    FullName = "",
                    CodeAlpha2 = "AS",
                    CodeAlpha3 = "ASM",
                    NumericCode = "016"
                },
                new Country
                {
                    Name = "Andorra",
                    FullName = "the Principality of Andorra",
                    CodeAlpha2 = "AD",
                    CodeAlpha3 = "AND",
                    NumericCode = "020"
                },
                new Country
                {
                    Name = "Angola",
                    FullName = "the Republic of Angola",
                    CodeAlpha2 = "AO",
                    CodeAlpha3 = "AGO",
                    NumericCode = "024"
                },
                new Country
                {
                    Name = "Anguilla",
                    FullName = "",
                    CodeAlpha2 = "AI",
                    CodeAlpha3 = "AIA",
                    NumericCode = "660"
                },
                new Country
                {
                    Name = "Antarctica",
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
                    Name = "Argentina",
                    FullName = "the Argentine Republic",
                    CodeAlpha2 = "AR",
                    CodeAlpha3 = "ARG",
                    NumericCode = "032"
                },
                new Country
                {
                    Name = "Armenia",
                    FullName = "the Republic of Armenia",
                    CodeAlpha2 = "AM",
                    CodeAlpha3 = "ARM",
                    NumericCode = "051"
                },
                new Country
                {
                    Name = "Aruba",
                    FullName = "",
                    CodeAlpha2 = "AW",
                    CodeAlpha3 = "ABW",
                    NumericCode = "533"
                },
                new Country
                {
                    Name = "Australia",
                    FullName = "",
                    CodeAlpha2 = "AU",
                    CodeAlpha3 = "AUS",
                    NumericCode = "036"
                },
                new Country
                {
                    Name = "Austria",
                    FullName = "the Republic of Austria",
                    CodeAlpha2 = "AT",
                    CodeAlpha3 = "AUT",
                    NumericCode = "040"
                },
                new Country
                {
                    Name = "Azerbaijan",
                    FullName = "the Republic of Azerbaijan",
                    CodeAlpha2 = "AZ",
                    CodeAlpha3 = "AZE",
                    NumericCode = "031"
                },
                new Country
                {
                    Name = "Bahamas (the)",
                    FullName = "the Commonwealth of the Bahamas",
                    CodeAlpha2 = "BS",
                    CodeAlpha3 = "BHS",
                    NumericCode = "044"
                },
                new Country
                {
                    Name = "Bahrain",
                    FullName = "the Kingdom of Bahrain",
                    CodeAlpha2 = "BH",
                    CodeAlpha3 = "BHR",
                    NumericCode = "048"
                },
                new Country
                {
                    Name = "Bangladesh",
                    FullName = "the People's Republic of Bangladesh",
                    CodeAlpha2 = "BD",
                    CodeAlpha3 = "BGD",
                    NumericCode = "050"
                },
                new Country
                {
                    Name = "Barbados",
                    FullName = "",
                    CodeAlpha2 = "BB",
                    CodeAlpha3 = "BRB",
                    NumericCode = "052"
                },
                new Country
                {
                    Name = "Belarus",
                    FullName = "the Republic of Belarus",
                    CodeAlpha2 = "BY",
                    CodeAlpha3 = "BLR",
                    NumericCode = "112"
                },
                new Country
                {
                    Name = "Belgium",
                    FullName = "the Kingdom of Belgium",
                    CodeAlpha2 = "BE",
                    CodeAlpha3 = "BEL",
                    NumericCode = "056"
                },
                new Country
                {
                    Name = "Belize",
                    FullName = "",
                    CodeAlpha2 = "BZ",
                    CodeAlpha3 = "BLZ",
                    NumericCode = "084"
                },
                new Country
                {
                    Name = "Benin",
                    FullName = "the Republic of Benin",
                    CodeAlpha2 = "BJ",
                    CodeAlpha3 = "BEN",
                    NumericCode = "204"
                },
                new Country
                {
                    Name = "Bermuda",
                    FullName = "",
                    CodeAlpha2 = "BM",
                    CodeAlpha3 = "BMU",
                    NumericCode = "060"
                },
                new Country
                {
                    Name = "Åland Islands",
                    FullName = "",
                    CodeAlpha2 = "AX",
                    CodeAlpha3 = "ALA",
                    NumericCode = "248"
                },
                new Country
                {
                    Name = "Bhutan",
                    FullName = "the Kingdom of Bhutan",
                    CodeAlpha2 = "BT",
                    CodeAlpha3 = "BTN",
                    NumericCode = "064"
                },
                new Country
                {
                    Name = "Bolivia (Plurinational State of)",
                    FullName = "the Plurinational State of Bolivia",
                    CodeAlpha2 = "BO",
                    CodeAlpha3 = "BOL",
                    NumericCode = "068"
                },
                new Country
                {
                    Name = "Bonaire, Sint Eustatius and Saba",
                    FullName = "",
                    CodeAlpha2 = "BQ",
                    CodeAlpha3 = "BES",
                    NumericCode = "535"
                },
                new Country
                {
                    Name = "Bosnia and Herzegovina",
                    FullName = "",
                    CodeAlpha2 = "BA",
                    CodeAlpha3 = "BIH",
                    NumericCode = "070"
                },
                new Country
                {
                    Name = "Botswana",
                    FullName = "the Republic of Botswana",
                    CodeAlpha2 = "BW",
                    CodeAlpha3 = "BWA",
                    NumericCode = "072"
                },
                new Country
                {
                    Name = "Bouvet Island",
                    FullName = "",
                    CodeAlpha2 = "BV",
                    CodeAlpha3 = "BVT",
                    NumericCode = "074"
                },
                new Country
                {
                    Name = "Brazil",
                    FullName = "the Federative Republic of Brazil",
                    CodeAlpha2 = "BR",
                    CodeAlpha3 = "BRA",
                    NumericCode = "076"
                },
                new Country
                {
                    Name = "British Indian Ocean Territory (the)",
                    FullName = "",
                    CodeAlpha2 = "IO",
                    CodeAlpha3 = "IOT",
                    NumericCode = "086"
                },
                new Country
                {
                    Name = "Brunei Darussalam",
                    FullName = "",
                    CodeAlpha2 = "BN",
                    CodeAlpha3 = "BRN",
                    NumericCode = "096"
                },
                new Country
                {
                    Name = "Bulgaria",
                    FullName = "the Republic of Bulgaria",
                    CodeAlpha2 = "BG",
                    CodeAlpha3 = "BGR",
                    NumericCode = "100"
                },
                new Country
                {
                    Name = "Burkina Faso",
                    FullName = "",
                    CodeAlpha2 = "BF",
                    CodeAlpha3 = "BFA",
                    NumericCode = "854"
                },
                new Country
                {
                    Name = "Burundi",
                    FullName = "the Republic of Burundi",
                    CodeAlpha2 = "BI",
                    CodeAlpha3 = "BDI",
                    NumericCode = "108"
                },
                new Country
                {
                    Name = "Cabo Verde",
                    FullName = "the Republic of Cabo Verde",
                    CodeAlpha2 = "CV",
                    CodeAlpha3 = "CPV",
                    NumericCode = "132"
                },
                new Country
                {
                    Name = "Cambodia",
                    FullName = "the Kingdom of Cambodia",
                    CodeAlpha2 = "KH",
                    CodeAlpha3 = "KHM",
                    NumericCode = "116"
                },
                new Country
                {
                    Name = "Cameroon",
                    FullName = "the Republic of Cameroon",
                    CodeAlpha2 = "CM",
                    CodeAlpha3 = "CMR",
                    NumericCode = "120"
                },
                new Country
                {
                    Name = "Canada",
                    FullName = "",
                    CodeAlpha2 = "CA",
                    CodeAlpha3 = "CAN",
                    NumericCode = "124"
                },
                new Country
                {
                    Name = "Cayman Islands (the)",
                    FullName = "",
                    CodeAlpha2 = "KY",
                    CodeAlpha3 = "CYM",
                    NumericCode = "136"
                },
                new Country
                {
                    Name = "Central African Republic (the)",
                    FullName = "the Central African Republic",
                    CodeAlpha2 = "CF",
                    CodeAlpha3 = "CAF",
                    NumericCode = "140"
                },
                new Country
                {
                    Name = "Chad",
                    FullName = "the Republic of Chad",
                    CodeAlpha2 = "TD",
                    CodeAlpha3 = "TCD",
                    NumericCode = "148"
                },
                new Country
                {
                    Name = "Chad",
                    FullName = "the Republic of Chad",
                    CodeAlpha2 = "TD",
                    CodeAlpha3 = "TCD",
                    NumericCode = "148"
                },
                new Country
                {
                    Name = "Chile",
                    FullName = "the Republic of Chile",
                    CodeAlpha2 = "CL",
                    CodeAlpha3 = "CHL",
                    NumericCode = "152"
                },
                new Country
                {
                    Name = "China",
                    FullName = "the People's Republic of China",
                    CodeAlpha2 = "CN",
                    CodeAlpha3 = "CHN",
                    NumericCode = "156"
                },
                new Country
                {
                    Name = "Christmas Island",
                    FullName = "",
                    CodeAlpha2 = "CX",
                    CodeAlpha3 = "CXR",
                    NumericCode = "162"
                },
                new Country
                {
                    Name  = "United States of America (the)",
                    FullName = "the United States of America",
                    CodeAlpha2 = "US",
                    CodeAlpha3 = "USA",
                    NumericCode = "840"
                }
            };
            
            return countries;
        }
    }
}