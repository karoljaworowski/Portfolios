using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolios.Repository.Models
{
    [Table("Portfolios")]
    public class Portfolio : Entity<int>
    {
        [MaxLength(12), MinLength(12)]
        [Required]
        public string ISIN { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        [MaxLength(3), MinLength(3)]
        [Required]
        public string Currency { get; set; }

        [Required]
        public decimal MarketValue {get;set;}

        public ICollection<Position> Positions { get; set; }
    }
}
