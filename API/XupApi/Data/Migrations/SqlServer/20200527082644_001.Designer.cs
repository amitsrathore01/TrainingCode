﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XupApi.Helpers;

namespace XupApi.Data.Migrations.SqlServer
{
    [DbContext(typeof(DataContext))]
    [Migration("20200527082644_001")]
    partial class _001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("XupApi.Entities.CheckRegister", b =>
                {
                    b.Property<Guid>("CheckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<string>("FrequencyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsScheduled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CheckId");

                    b.ToTable("CheckRegister");
                });

            modelBuilder.Entity("XupApi.Entities.CheckRun", b =>
                {
                    b.Property<Guid>("CheckRunId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CheckRegisterCheckId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastRunOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("RunTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CheckRunId");

                    b.HasIndex("CheckRegisterCheckId");

                    b.ToTable("CheckRun");
                });

            modelBuilder.Entity("XupApi.Entities.CheckRun", b =>
                {
                    b.HasOne("XupApi.Entities.CheckRegister", "CheckRegister")
                        .WithMany()
                        .HasForeignKey("CheckRegisterCheckId");
                });
#pragma warning restore 612, 618
        }
    }
}