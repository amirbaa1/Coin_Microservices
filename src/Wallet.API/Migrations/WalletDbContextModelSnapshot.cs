﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wallet.API.Data;

#nullable disable

namespace Wallet.API.Migrations
{
    [DbContext(typeof(WalletDbContext))]
    partial class WalletDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Wallet.API.Model.WalletCoinModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("NameCoin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PriceUSD")
                        .HasColumnType("float");

                    b.Property<int?>("WalletModelId")
                        .HasColumnType("int");

                    b.Property<double>("coinPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("WalletModelId");

                    b.ToTable("WalletCoinModel");
                });

            modelBuilder.Entity("Wallet.API.Model.WalletModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("walletModels");
                });

            modelBuilder.Entity("Wallet.API.Model.WalletCoinModel", b =>
                {
                    b.HasOne("Wallet.API.Model.WalletModel", null)
                        .WithMany("walletCoins")
                        .HasForeignKey("WalletModelId");
                });

            modelBuilder.Entity("Wallet.API.Model.WalletModel", b =>
                {
                    b.Navigation("walletCoins");
                });
#pragma warning restore 612, 618
        }
    }
}
