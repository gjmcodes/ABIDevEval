﻿// <auto-generated />
using System;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20250121145923_AddSalesAndValueObjects")]
    partial class AddSalesAndValueObjects
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("boolean");

                    b.Property<int>("DiscountPercentage")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("SaleTotal")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Sales", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.ValueObjects.SaleItemVO", b =>
                {
                    b.Property<Guid>("SaleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("ProductCategory")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint");

                    b.HasKey("SaleId", "ProductId");

                    b.ToTable("SaleItems", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sale", b =>
                {
                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.SaleBranchVO", "Branch", b1 =>
                        {
                            b1.Property<Guid>("SaleId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("BranchId")
                                .HasColumnType("uuid");

                            b1.Property<string>("BranchName")
                                .IsRequired()
                                .HasMaxLength(300)
                                .HasColumnType("character varying(300)");

                            b1.HasKey("SaleId", "BranchId");

                            b1.HasIndex("SaleId")
                                .IsUnique();

                            b1.ToTable("SalesBranches", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("SaleId");
                        });

                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.SaleCustomerVO", "SaleCustomer", b1 =>
                        {
                            b1.Property<Guid>("SaleId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uuid");

                            b1.Property<string>("CustomerEmail")
                                .IsRequired()
                                .HasMaxLength(254)
                                .HasColumnType("character varying(254)");

                            b1.Property<string>("CustomerName")
                                .IsRequired()
                                .HasMaxLength(300)
                                .HasColumnType("character varying(300)");

                            b1.HasKey("SaleId", "CustomerId");

                            b1.HasIndex("SaleId")
                                .IsUnique();

                            b1.ToTable("SalesCustomers", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("SaleId");
                        });

                    b.Navigation("Branch")
                        .IsRequired();

                    b.Navigation("SaleCustomer")
                        .IsRequired();
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.ValueObjects.SaleItemVO", b =>
                {
                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Sale", "Sale")
                        .WithMany("Items")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sale", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
