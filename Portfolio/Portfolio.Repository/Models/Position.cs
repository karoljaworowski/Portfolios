using Portfolios.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolios.Repository.Models
{
    [Table("Positions")]
    public class Position : Entity<int>
    {
        [MaxLength(12), MinLength(12)]
        [Required]
        public string ISIN { get; set; }

        [MaxLength(3), MinLength(3)]
        [Required]
        public string Currency { get; set; }

        [Required]
        public decimal MarketValue { get; set; }

        public string Name { get; set; }

        public PositionType Type { get; set; }

        [MaxLength(2), MinLength(2)]
        [Required]
        public string Country { get; set; }

        [Required]
        public double SharePercentage { get; set; }

        [Required]
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }
    }
}
