﻿// <auto-generated />
using System;
using HCIProject02.Core.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HCIProject02.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("ArrangementAttraction", b =>
                {
                    b.Property<Guid>("ArrangementsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AttractionsId")
                        .HasColumnType("TEXT");

                    b.HasKey("ArrangementsId", "AttractionsId");

                    b.HasIndex("AttractionsId");

                    b.ToTable("ArrangementAttraction");
                });

            modelBuilder.Entity("HCIProject02.Core.Model.Arrangement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ClientId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("ReturnTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("TripPlan")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("HotelId");

                    b.ToTable("Arrangments");
                });

            modelBuilder.Entity("HCIProject02.Core.Model.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArrangementId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BookingTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PassengerId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("ArrangementId");

                    b.HasIndex("PassengerId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("HCIProject02.Core.Model.PointOfInterest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImagePath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double?>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PointOfInterests");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PointOfInterest");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("HCIProject02.Core.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("HCIProject02.Core.Model.Attraction", b =>
                {
                    b.HasBaseType("HCIProject02.Core.Model.PointOfInterest");

                    b.HasDiscriminator().HasValue("Attraction");
                });

            modelBuilder.Entity("HCIProject02.Core.Model.Hotel", b =>
                {
                    b.HasBaseType("HCIProject02.Core.Model.PointOfInterest");

                    b.Property<int>("NumberOfStars")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Hotel");
                });

            modelBuilder.Entity("HCIProject02.Core.Model.Restaurant", b =>
                {
                    b.HasBaseType("HCIProject02.Core.Model.PointOfInterest");

                    b.HasDiscriminator().HasValue("Restaurant");
                });

            modelBuilder.Entity("HCIProject02.Core.Model.Agent", b =>
                {
                    b.HasBaseType("HCIProject02.Core.Model.User");

                    b.HasDiscriminator().HasValue("Agent");
                });

            modelBuilder.Entity("HCIProject02.Core.Model.Client", b =>
                {
                    b.HasBaseType("HCIProject02.Core.Model.User");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("ArrangementAttraction", b =>
                {
                    b.HasOne("HCIProject02.Core.Model.Arrangement", null)
                        .WithMany()
                        .HasForeignKey("ArrangementsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HCIProject02.Core.Model.Attraction", null)
                        .WithMany()
                        .HasForeignKey("AttractionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HCIProject02.Core.Model.Arrangement", b =>
                {
                    b.HasOne("HCIProject02.Core.Model.Client", null)
                        .WithMany("Arrangements")
                        .HasForeignKey("ClientId");

                    b.HasOne("HCIProject02.Core.Model.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("HCIProject02.Core.Model.Booking", b =>
                {
                    b.HasOne("HCIProject02.Core.Model.Arrangement", "Arrangement")
                        .WithMany()
                        .HasForeignKey("ArrangementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HCIProject02.Core.Model.Client", "Passenger")
                        .WithMany()
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Arrangement");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("HCIProject02.Core.Model.Client", b =>
                {
                    b.Navigation("Arrangements");
                });
#pragma warning restore 612, 618
        }
    }
}