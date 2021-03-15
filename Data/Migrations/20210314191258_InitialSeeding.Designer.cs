﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(WmContext))]
    [Migration("20210314191258_InitialSeeding")]
    partial class InitialSeeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Measurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MeasuringPointId")
                        .HasColumnType("int");

                    b.Property<int>("ReadingStatusId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeasuringPointId");

                    b.HasIndex("ReadingStatusId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Domain.MeasuringPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Code")
                        .HasMaxLength(64)
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Place")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<int?>("RouteId")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasMaxLength(95)
                        .HasColumnType("nvarchar(95)");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("MeasuringPoints");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Number = "1",
                            Place = "Test Place Name One",
                            Street = "Test Street Name One"
                        },
                        new
                        {
                            Id = 2,
                            Number = "2",
                            Place = "Test Place Name Two",
                            Street = "Test Street Name Two"
                        },
                        new
                        {
                            Id = 3,
                            Number = "3",
                            Place = "Test Place Name One",
                            Street = "Test Street Name Three"
                        });
                });

            modelBuilder.Entity("Domain.ReadingStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("ReadingStatuses");
                });

            modelBuilder.Entity("Domain.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Domain.WaterMeter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("MaxValue")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.Property<int>("MeasuringPointId")
                        .HasColumnType("int");

                    b.Property<int?>("StartingValue")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeasuringPointId")
                        .IsUnique();

                    b.ToTable("WaterMeters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            MaxValue = 100,
                            MeasuringPointId = 1
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            MaxValue = 100,
                            MeasuringPointId = 3
                        },
                        new
                        {
                            Id = 3,
                            IsActive = true,
                            MaxValue = 100,
                            MeasuringPointId = 3
                        });
                });

            modelBuilder.Entity("Domain.Measurement", b =>
                {
                    b.HasOne("Domain.MeasuringPoint", "MeasuringPoint")
                        .WithMany("Measurements")
                        .HasForeignKey("MeasuringPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.ReadingStatus", null)
                        .WithMany("Measurements")
                        .HasForeignKey("ReadingStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeasuringPoint");
                });

            modelBuilder.Entity("Domain.MeasuringPoint", b =>
                {
                    b.HasOne("Domain.Route", "Route")
                        .WithMany("MeasuringPoints")
                        .HasForeignKey("RouteId");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Domain.WaterMeter", b =>
                {
                    b.HasOne("Domain.MeasuringPoint", "MeasuringPoint")
                        .WithOne("WaterMeter")
                        .HasForeignKey("Domain.WaterMeter", "MeasuringPointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeasuringPoint");
                });

            modelBuilder.Entity("Domain.MeasuringPoint", b =>
                {
                    b.Navigation("Measurements");

                    b.Navigation("WaterMeter");
                });

            modelBuilder.Entity("Domain.ReadingStatus", b =>
                {
                    b.Navigation("Measurements");
                });

            modelBuilder.Entity("Domain.Route", b =>
                {
                    b.Navigation("MeasuringPoints");
                });
#pragma warning restore 612, 618
        }
    }
}
