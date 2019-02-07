using System;
using System.Collections.Generic;

namespace Portfolios.Dto
{
    public class PortfolioDto
    {
        public string ISIN { get; set; }

        public DateTime Date { get; set; }

        public string Currency { get; set; }

        public decimal MarketValue { get; set; }

        public IEnumerable<PositionDto> Positions { get; set; }
    }
}
