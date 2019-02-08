﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Portfolios.Repository;

namespace Portfolios.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190208110441_ChangeSharePercentageType")]
    partial class ChangeSharePercentageType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Portfolios.Repository.Models.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<DateTime>("Date");

                    b.Property<string>("ISIN")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<decimal>("MarketValue");

                    b.HasKey("Id");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("Portfolios.Repository.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<string>("ISIN")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<decimal>("MarketValue");

                    b.Property<string>("Name");

                    b.Property<int>("PortfolioId");

                    b.Property<double>("SharePercentage");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Portfolios.Repository.Models.Position", b =>
                {
                    b.HasOne("Portfolios.Repository.Models.Portfolio", "Portfolio")
                        .WithMany("Positions")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
