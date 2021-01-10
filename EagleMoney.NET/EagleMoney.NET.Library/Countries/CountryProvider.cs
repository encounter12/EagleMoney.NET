using System.Collections.Generic;

namespace EagleMoney.NET.Library.Countries
{
    public struct CountryProvider : ICountryProvider
    {
        // ISO 3166 - Country codes
        // https://www.iso.org/obp/ui/#search/code/
        // https://www.iso.org/obp/ui/#iso:pub:PUB500001:en
        // ISO 3166-1: https://en.wikipedia.org/wiki/ISO_3166-1
        public IEnumerable<Country> GetCountries()
        {
            var countries = new List<Country>
            {
                new Country
                {
                    ShortName = "AFGHANISTAN",
                    ShortNameLowerCase = "Afghanistan",
                    FullName = "the Islamic Republic of Afghanistan",
                    CodeAlpha2 = "AF",
                    CodeAlpha3 = "AFG",
                    NumericCode = "004"
                },
                new Country
                {
                    ShortName = "ALBANIA",
                    ShortNameLowerCase = "Albania",
                    FullName = "the Republic of Albania",
                    CodeAlpha2 = "AL",
                    CodeAlpha3 = "ALB",
                    NumericCode = "008"
                },
                new Country
                {
                    ShortName = "ALGERIA",
                    ShortNameLowerCase = "Algeria",
                    FullName = "the People's Democratic Republic of Algeria",
                    CodeAlpha2 = "DZ",
                    CodeAlpha3 = "DZA",
                    NumericCode = "012"
                },
                new Country
                {
                    ShortName = "AMERICAN SAMOA",
                    ShortNameLowerCase = "American Samoa",
                    FullName = "",
                    CodeAlpha2 = "AS",
                    CodeAlpha3 = "ASM",
                    NumericCode = "016"
                },
                new Country
                {
                    ShortName = "ANDORRA",
                    ShortNameLowerCase = "Andorra",
                    FullName = "the Principality of Andorra",
                    CodeAlpha2 = "AD",
                    CodeAlpha3 = "AND",
                    NumericCode = "020"
                },
                new Country
                {
                    ShortName = "ANGOLA",
                    ShortNameLowerCase = "Angola",
                    FullName = "the Republic of Angola",
                    CodeAlpha2 = "AO",
                    CodeAlpha3 = "AGO",
                    NumericCode = "024"
                },
                new Country
                {
                    ShortName = "ANGUILLA",
                    ShortNameLowerCase = "Anguilla",
                    FullName = "",
                    CodeAlpha2 = "AI",
                    CodeAlpha3 = "AIA",
                    NumericCode = "660"
                },
                new Country
                {
                    ShortName = "ANTARCTICA",
                    ShortNameLowerCase = "Antarctica",
                    FullName = "",
                    CodeAlpha2 = "AQ",
                    CodeAlpha3 = "ATA",
                    NumericCode = "010"
                },
                new Country
                {
                    ShortName = "ANTIGUA AND BARBUDA",
                    ShortNameLowerCase  = "Antigua and Barbuda",
                    FullName = "",
                    CodeAlpha2 = "AG",
                    CodeAlpha3 = "ATG",
                    NumericCode = "028"
                },
                new Country
                {
                    ShortName = "ARGENTINA",
                    ShortNameLowerCase = "Argentina",
                    FullName = "the Argentine Republic",
                    CodeAlpha2 = "AR",
                    CodeAlpha3 = "ARG",
                    NumericCode = "032"
                },
                new Country
                {
                    ShortName = "ARMENIA",
                    ShortNameLowerCase = "Armenia",
                    FullName = "the Republic of Armenia",
                    CodeAlpha2 = "AM",
                    CodeAlpha3 = "ARM",
                    NumericCode = "051"
                },
                new Country
                {
                    ShortName = "ARUBA",
                    ShortNameLowerCase = "Aruba",
                    FullName = "",
                    CodeAlpha2 = "AW",
                    CodeAlpha3 = "ABW",
                    NumericCode = "533"
                },
                new Country
                {
                    ShortName = "AUSTRALIA",
                    ShortNameLowerCase = "Australia",
                    FullName = "",
                    CodeAlpha2 = "AU",
                    CodeAlpha3 = "AUS",
                    NumericCode = "036"
                },
                new Country
                {
                    ShortName = "AUSTRIA",
                    ShortNameLowerCase = "Austria",
                    FullName = "the Republic of Austria",
                    CodeAlpha2 = "AT",
                    CodeAlpha3 = "AUT",
                    NumericCode = "040"
                },
                new Country
                {
                    ShortName = "AZERBAIJAN",
                    ShortNameLowerCase = "Azerbaijan",
                    FullName = "the Republic of Azerbaijan",
                    CodeAlpha2 = "AZ",
                    CodeAlpha3 = "AZE",
                    NumericCode = "031"
                },
                new Country
                {
                    ShortName = "BAHAMAS",
                    ShortNameLowerCase = "Bahamas (the)",
                    FullName = "the Commonwealth of the Bahamas",
                    CodeAlpha2 = "BS",
                    CodeAlpha3 = "BHS",
                    NumericCode = "044"
                },
                new Country
                {
                    ShortName = "BAHRAIN",
                    ShortNameLowerCase = "Bahrain",
                    FullName = "the Kingdom of Bahrain",
                    CodeAlpha2 = "BH",
                    CodeAlpha3 = "BHR",
                    NumericCode = "048"
                },
                new Country
                {
                    ShortName = "BANGLADESH",
                    ShortNameLowerCase = "Bangladesh",
                    FullName = "the People's Republic of Bangladesh",
                    CodeAlpha2 = "BD",
                    CodeAlpha3 = "BGD",
                    NumericCode = "050"
                },
                new Country
                {
                    ShortName = "BARBADOS",
                    ShortNameLowerCase = "Barbados",
                    FullName = "",
                    CodeAlpha2 = "BB",
                    CodeAlpha3 = "BRB",
                    NumericCode = "052"
                },
                new Country
                {
                    ShortName = "BELARUS",
                    ShortNameLowerCase = "Belarus",
                    FullName = "the Republic of Belarus",
                    CodeAlpha2 = "BY",
                    CodeAlpha3 = "BLR",
                    NumericCode = "112"
                },
                new Country
                {
                    ShortName = "BELGIUM",
                    ShortNameLowerCase = "Belgium",
                    FullName = "the Kingdom of Belgium",
                    CodeAlpha2 = "BE",
                    CodeAlpha3 = "BEL",
                    NumericCode = "056"
                },
                new Country
                {
                    ShortName = "BELIZE",
                    ShortNameLowerCase = "Belize",
                    FullName = "",
                    CodeAlpha2 = "BZ",
                    CodeAlpha3 = "BLZ",
                    NumericCode = "084"
                },
                new Country
                {
                    ShortName = "BENIN",
                    ShortNameLowerCase = "Benin",
                    FullName = "the Republic of Benin",
                    CodeAlpha2 = "BJ",
                    CodeAlpha3 = "BEN",
                    NumericCode = "204"
                },
                new Country
                {
                    ShortName = "BERMUDA",
                    ShortNameLowerCase = "Bermuda",
                    FullName = "",
                    CodeAlpha2 = "BM",
                    CodeAlpha3 = "BMU",
                    NumericCode = "060"
                },
                new Country
                {
                    ShortName = "ÅLAND ISLANDS",
                    ShortNameLowerCase = "Åland Islands",
                    FullName = "",
                    CodeAlpha2 = "AX",
                    CodeAlpha3 = "ALA",
                    NumericCode = "248"
                },
                new Country
                {
                    ShortName = "BHUTAN",
                    ShortNameLowerCase = "Bhutan",
                    FullName = "the Kingdom of Bhutan",
                    CodeAlpha2 = "BT",
                    CodeAlpha3 = "BTN",
                    NumericCode = "064"
                },
                new Country
                {
                    ShortName = "BOLIVIA (PLURINATIONAL STATE OF)",
                    ShortNameLowerCase = "Bolivia (Plurinational State of)",
                    FullName = "the Plurinational State of Bolivia",
                    CodeAlpha2 = "BO",
                    CodeAlpha3 = "BOL",
                    NumericCode = "068"
                },
                new Country
                {
                    ShortName = "BONAIRE, SINT EUSTATIUS AND SABA",
                    ShortNameLowerCase = "Bonaire, Sint Eustatius and Saba",
                    FullName = "",
                    CodeAlpha2 = "BQ",
                    CodeAlpha3 = "BES",
                    NumericCode = "535"
                },
                new Country
                {
                    ShortName = "BOSNIA AND HERZEGOVINA",
                    ShortNameLowerCase = "Bosnia and Herzegovina",
                    FullName = "",
                    CodeAlpha2 = "BA",
                    CodeAlpha3 = "BIH",
                    NumericCode = "070"
                },
                new Country
                {
                    ShortName = "BOTSWANA",
                    ShortNameLowerCase = "Botswana",
                    FullName = "the Republic of Botswana",
                    CodeAlpha2 = "BW",
                    CodeAlpha3 = "BWA",
                    NumericCode = "072"
                },
                new Country
                {
                    ShortName = "BOUVET ISLAND",
                    ShortNameLowerCase = "Bouvet Island",
                    FullName = "",
                    CodeAlpha2 = "BV",
                    CodeAlpha3 = "BVT",
                    NumericCode = "074"
                },
                new Country
                {
                    ShortNameLowerCase = "Brazil",
                    FullName = "the Federative Republic of Brazil",
                    CodeAlpha2 = "BR",
                    CodeAlpha3 = "BRA",
                    NumericCode = "076"
                },
                new Country
                {
                    ShortNameLowerCase = "British Indian Ocean Territory (the)",
                    FullName = "",
                    CodeAlpha2 = "IO",
                    CodeAlpha3 = "IOT",
                    NumericCode = "086"
                },
                new Country
                {
                    ShortNameLowerCase = "Brunei Darussalam",
                    FullName = "",
                    CodeAlpha2 = "BN",
                    CodeAlpha3 = "BRN",
                    NumericCode = "096"
                },
                new Country
                {
                    ShortNameLowerCase = "Bulgaria",
                    FullName = "the Republic of Bulgaria",
                    CodeAlpha2 = "BG",
                    CodeAlpha3 = "BGR",
                    NumericCode = "100"
                },
                new Country
                {
                    ShortNameLowerCase = "Burkina Faso",
                    FullName = "",
                    CodeAlpha2 = "BF",
                    CodeAlpha3 = "BFA",
                    NumericCode = "854"
                },
                new Country
                {
                    ShortNameLowerCase = "Burundi",
                    FullName = "the Republic of Burundi",
                    CodeAlpha2 = "BI",
                    CodeAlpha3 = "BDI",
                    NumericCode = "108"
                },
                new Country
                {
                    ShortNameLowerCase = "Cabo Verde",
                    FullName = "the Republic of Cabo Verde",
                    CodeAlpha2 = "CV",
                    CodeAlpha3 = "CPV",
                    NumericCode = "132"
                },
                new Country
                {
                    ShortNameLowerCase = "Cambodia",
                    FullName = "the Kingdom of Cambodia",
                    CodeAlpha2 = "KH",
                    CodeAlpha3 = "KHM",
                    NumericCode = "116"
                },
                new Country
                {
                    ShortNameLowerCase = "Cameroon",
                    FullName = "the Republic of Cameroon",
                    CodeAlpha2 = "CM",
                    CodeAlpha3 = "CMR",
                    NumericCode = "120"
                },
                new Country
                {
                    ShortNameLowerCase = "Canada",
                    FullName = "",
                    CodeAlpha2 = "CA",
                    CodeAlpha3 = "CAN",
                    NumericCode = "124"
                },
                new Country
                {
                    ShortNameLowerCase = "Cayman Islands (the)",
                    FullName = "",
                    CodeAlpha2 = "KY",
                    CodeAlpha3 = "CYM",
                    NumericCode = "136"
                },
                new Country
                {
                    ShortNameLowerCase = "Central African Republic (the)",
                    FullName = "the Central African Republic",
                    CodeAlpha2 = "CF",
                    CodeAlpha3 = "CAF",
                    NumericCode = "140"
                },
                new Country
                {
                    ShortNameLowerCase = "Chad",
                    FullName = "the Republic of Chad",
                    CodeAlpha2 = "TD",
                    CodeAlpha3 = "TCD",
                    NumericCode = "148"
                },
                new Country
                {
                    ShortNameLowerCase = "Chad",
                    FullName = "the Republic of Chad",
                    CodeAlpha2 = "TD",
                    CodeAlpha3 = "TCD",
                    NumericCode = "148"
                },
                new Country
                {
                    ShortNameLowerCase = "Chile",
                    FullName = "the Republic of Chile",
                    CodeAlpha2 = "CL",
                    CodeAlpha3 = "CHL",
                    NumericCode = "152"
                },
                new Country
                {
                    ShortNameLowerCase = "China",
                    FullName = "the People's Republic of China",
                    CodeAlpha2 = "CN",
                    CodeAlpha3 = "CHN",
                    NumericCode = "156"
                },
                new Country
                {
                    ShortNameLowerCase = "Christmas Island",
                    FullName = "",
                    CodeAlpha2 = "CX",
                    CodeAlpha3 = "CXR",
                    NumericCode = "162"
                },
                new Country
                {
                    ShortNameLowerCase = "Cocos (Keeling) Islands (the)",
                    FullName = "",
                    CodeAlpha2 = "CC",
                    CodeAlpha3 = "CCK",
                    NumericCode = "166"
                },
                new Country
                {
                    ShortNameLowerCase = "Colombia",
                    FullName = "the Republic of Colombia",
                    CodeAlpha2 = "CO",
                    CodeAlpha3 = "COL",
                    NumericCode = "170"
                },
                new Country
                {
                    ShortNameLowerCase = "Comoros (the)",
                    FullName = "the Union of the Comoros",
                    CodeAlpha2 = "KM",
                    CodeAlpha3 = "COM",
                    NumericCode = "174"
                },
                new Country
                {
                    ShortNameLowerCase = "Congo (the Democratic Republic of the)",
                    FullName = "the Democratic Republic of the Congo",
                    CodeAlpha2 = "CD",
                    CodeAlpha3 = "COD",
                    NumericCode = "180"
                },
                new Country
                {
                    ShortNameLowerCase  = "Congo (the)",
                    FullName = "the Republic of the Congo",
                    CodeAlpha2 = "CG",
                    CodeAlpha3 = "COG",
                    NumericCode = "178"
                },
                new Country
                {
                    ShortNameLowerCase = "Cook Islands (the)",
                    FullName = "",
                    CodeAlpha2 = "CK",
                    CodeAlpha3 = "COK",
                    NumericCode = "184"
                },
                new Country
                {
                    ShortNameLowerCase = "United States of America (the)",
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