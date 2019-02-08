using System;
using System.Collections.Generic;

namespace Portfolios.Dto
{
    public class PortfolioPositionAggregatedDto
    {
        public PortfolioPositionAggregatedDto(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        public string Value { get; set; }

        public decimal SharePercentage { get; set; }
    }
}
