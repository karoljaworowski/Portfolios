using System.Linq;

namespace Portfolios.Common.Validators
{
    public static class CurrencyCodeValidator
    {
        private static readonly int currencyLength = 3;

        /// <summary>
        /// Validate if Currency code is valid ISO code
        /// now I check only length and if char is a letter
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <returns></returns>
        public static bool Validate(string currencyCode)
        {
            bool result = true;

            result &= currencyCode.Length == currencyLength;

            result &= currencyCode.All(char.IsLetter);

            return result;
        }
    }
}
