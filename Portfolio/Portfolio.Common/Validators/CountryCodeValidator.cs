using System.Linq;

namespace Portfolios.Common.Validators
{
    public static class CountryCodeValidator
    {
        private static readonly int countryCodeLength = 2;

        /// <summary>
        /// Validate if country code is valid
        /// now I check only length and if char is letter
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public static bool Validate(string countryCode)
        {
            bool result = true;

            result &= countryCode.Length == countryCodeLength;
        
            result &= countryCode.All(char.IsLetter);

            return result;
        }
    }
}
