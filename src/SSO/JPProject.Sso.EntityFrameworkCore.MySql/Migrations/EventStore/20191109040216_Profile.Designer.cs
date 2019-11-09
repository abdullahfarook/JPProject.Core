﻿// <auto-generated />
using System;
using JPProject.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JPProject.Sso.EntityFrameworkCore.MySql.Migrations.EventStore
{
    [DbContext(typeof(EventStoreContext))]
    [Migration("20191109040216_Profile")]
    partial class Profile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("JPProject.Domain.Core.Events.EventDetails", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Metadata")
                        .HasColumnType("longtext");

                    b.HasKey("EventId");

                    b.ToTable("StoredEventDetails");
                });

            modelBuilder.Entity("JPProject.Domain.Core.Events.StoredEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AggregateId")
                        .HasColumnType("longtext");

                    b.Property<int>("EventType")
                        .HasColumnType("int");

                    b.Property<string>("LocalIpAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<string>("MessageType")
                        .HasColumnName("Action")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("RemoteIpAddress")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnName("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("User")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("StoredEvent");
                });

            modelBuilder.Entity("JPProject.Domain.Core.Events.EventDetails", b =>
                {
                    b.HasOne("JPProject.Domain.Core.Events.StoredEvent", "Event")
                        .WithOne("Details")
                        .HasForeignKey("JPProject.Domain.Core.Events.EventDetails", "EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
