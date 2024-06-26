﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using steamPy.Db;

#nullable disable

namespace steamPy.Migrations
{
    [DbContext(typeof(SteamPyDbContext))]
    [Migration("20240405153154_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.28");

            modelBuilder.Entity("steamPy.Models.GamePriceInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("AllLowPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("GameId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GameName")
                        .HasColumnType("TEXT");

                    b.Property<string>("GameNameCN")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("SteamPrice")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("SteamPriceHistory")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("SteamPriceNow")
                        .HasColumnType("TEXT");

                    b.Property<string>("SteamUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GamePriceInfos");
                });

            modelBuilder.Entity("steamPy.Models.GamePriceInfoHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("AllLowPrice")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("AvgPrice")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("DayLowPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("GameId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GameName")
                        .HasColumnType("TEXT");

                    b.Property<string>("GameNameCN")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PriceDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("SteamPrice")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("SteamPriceHistory")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("SteamPriceNow")
                        .HasColumnType("TEXT");

                    b.Property<string>("SteamUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GamePriceInfoHistories");
                });

            modelBuilder.Entity("steamPy.Models.UserInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Salt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("steamPy.Models.UserWatch", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("GameId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GameName")
                        .HasColumnType("TEXT");

                    b.Property<string>("GameNameCN")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("SendMailPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("SteamUrl")
                        .HasColumnType("TEXT");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("UserWatchList");
                });
#pragma warning restore 612, 618
        }
    }
}
