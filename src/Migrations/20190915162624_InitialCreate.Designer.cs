﻿// <auto-generated />
using System;
using MediatrSample.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MediatrSample.Api.Migrations
{
    /// <summary>
    /// </summary>
    [DbContext(typeof(ApiDbContext))]
    [Migration("20190915162624_InitialCreate")]
    partial class InitialCreate
    {
        /// <summary>
        /// </summary>
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("MediatrSample.Api.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7f422462-d926-4837-b0e2-7d390a33f4e4"),
                            CreatedDate = new DateTime(2019, 9, 15, 18, 26, 24, 350, DateTimeKind.Local).AddTicks(5640),
                            Email = "salihigde@gmail.com",
                            Name = "Salih Igde"
                        });
                });

            modelBuilder.Entity("MediatrSample.Api.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CustomerId");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5635eb3b-5d2e-4339-b23a-665e375d2efe"),
                            CreatedDate = new DateTime(2019, 9, 15, 18, 26, 24, 356, DateTimeKind.Local).AddTicks(3970),
                            CustomerId = new Guid("7f422462-d926-4837-b0e2-7d390a33f4e4"),
                            Price = 1000m
                        },
                        new
                        {
                            Id = new Guid("afc54779-1641-4ab5-a124-992736bcb515"),
                            CreatedDate = new DateTime(2019, 9, 15, 18, 26, 24, 356, DateTimeKind.Local).AddTicks(5060),
                            CustomerId = new Guid("7f422462-d926-4837-b0e2-7d390a33f4e4"),
                            Price = 1200m
                        });
                });

            modelBuilder.Entity("MediatrSample.Api.Models.Order", b =>
                {
                    b.HasOne("MediatrSample.Api.Models.Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
