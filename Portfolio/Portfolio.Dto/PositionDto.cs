using Portfolios.Common.Enums;

namespace Portfolios.Dto
{
    public class PositionDto
    {
        public string ISIN { get; set; }

        public string Currency { get; set; }

        public decimal MarketValue { get; set; }

        public string Name { get; set; }

        public PositionType Type { get; set; }

        public string Country { get; set; }

        public float SharePercentage { get; set; }

        public PortfolioDto Portfolio { get; set; }
    }
}
