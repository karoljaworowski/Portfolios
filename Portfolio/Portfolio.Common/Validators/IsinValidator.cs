using System.Linq;

namespace Portfolios.Common.Validators
{
    public static class IsinValidator
    {
        private static readonly int IsinLength = 12;

        /// <summary>
        /// Validate if ISIN code is valid
        /// now I check only length and is char is alfanumerical
        /// </summary>
        /// <param name="ISIN"></param>
        /// <returns></returns>
        public static bool Validate(string ISIN)
        {
            bool result = true;

            result &= ISIN.Length == IsinLength;

            result &= ISIN.All(char.IsLetterOrDigit);

            return result;
        }
    }
}
