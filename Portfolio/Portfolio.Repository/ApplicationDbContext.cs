using Microsoft.EntityFrameworkCore;
using Portfolios.Common.Enums;
using Portfolios.Repository.Models;
using System;

namespace Portfolios.Repository
{

    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        //public virtual DbSet<User> Users { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Position>()
                .Property(p => p.Type)
                .HasConversion(v => v.ToString(), v => (PositionType)Enum.Parse(typeof(PositionType), v));
        }
    }
}
