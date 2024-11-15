﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Monitoring.Data.Database;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Monitoring.Data.Migrations
{
    [DbContext(typeof(MonitoringDbContext))]
    [Migration("20241114164101_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Monitoring.Domain.Building", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("LocationX")
                        .HasColumnType("integer")
                        .HasColumnName("locationX");

                    b.Property<int>("LocationY")
                        .HasColumnType("integer")
                        .HasColumnName("locationY");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("buildings", (string)null);
                });

            modelBuilder.Entity("Monitoring.Domain.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Audience")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("audience");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.HasKey("Id");

                    b.ToTable("notification", (string)null);
                });

            modelBuilder.Entity("Monitoring.Domain.Sensor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BatteryLevel")
                        .HasColumnType("integer")
                        .HasColumnName("battery_level");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uuid")
                        .HasColumnName("building_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("LocationX")
                        .HasColumnType("integer")
                        .HasColumnName("location_x");

                    b.Property<int>("LocationY")
                        .HasColumnType("integer")
                        .HasColumnName("location_y");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("photo_url");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("sensors", (string)null);
                });

            modelBuilder.Entity("Monitoring.Domain.SensorRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<int>("Humidity")
                        .HasColumnType("integer")
                        .HasColumnName("humidity");

                    b.Property<Guid>("SensorId")
                        .HasColumnType("uuid")
                        .HasColumnName("sensor_id");

                    b.Property<int>("Temperature")
                        .HasColumnType("integer")
                        .HasColumnName("temperature");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("sensor_records", (string)null);
                });

            modelBuilder.Entity("Monitoring.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("email");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(44)
                        .HasColumnType("character varying(44)")
                        .HasColumnName("password_hash");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("character varying(24)")
                        .HasColumnName("password_salt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Monitoring.Domain.Building", b =>
                {
                    b.HasOne("Monitoring.Domain.User", "User")
                        .WithMany("Buildings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Monitoring.Domain.Sensor", b =>
                {
                    b.HasOne("Monitoring.Domain.Building", "Building")
                        .WithMany("Sensors")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("Monitoring.Domain.SensorRecord", b =>
                {
                    b.HasOne("Monitoring.Domain.Sensor", "Sensor")
                        .WithMany("SensorRecords")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("Monitoring.Domain.Building", b =>
                {
                    b.Navigation("Sensors");
                });

            modelBuilder.Entity("Monitoring.Domain.Sensor", b =>
                {
                    b.Navigation("SensorRecords");
                });

            modelBuilder.Entity("Monitoring.Domain.User", b =>
                {
                    b.Navigation("Buildings");
                });
#pragma warning restore 612, 618
        }
    }
}
