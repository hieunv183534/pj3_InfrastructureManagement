﻿// <auto-generated />
using System;
using InfrastructureManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InfrastructureManagement.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220929134218_init31")]
    partial class init31
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("InfrastructureManagement.Core.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("InfrastructureManagement.Core.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("Level")
                        .HasColumnType("int");

                    b.Property<string>("MetaDatas")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Categorys");
                });

            modelBuilder.Entity("InfrastructureManagement.Core.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CategoryCode")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("MinScore")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MoreDetail")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("NumOfDay")
                        .HasColumnType("int");

                    b.Property<int?>("QualityScore")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("InfrastructureManagement.Core.Entities.ItemLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LogData")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LogDetail")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemLogs");
                });

            modelBuilder.Entity("InfrastructureManagement.Core.Entities.MapItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsFixed")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("char(36)");

                    b.Property<int>("RelationshipType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("ParentId");

                    b.ToTable("MapItems");
                });

            modelBuilder.Entity("InfrastructureManagement.Core.Entities.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("PositionId")
                        .HasColumnType("char(36)");

                    b.Property<string>("PositionString")
                        .HasColumnType("longtext");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Reply")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ReporterId")
                        .HasColumnType("char(36)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Urls")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PositionId");

                    b.HasIndex("ReporterId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("InfrastructureManagement.Core.Entities.TokenAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TokenAccounts");
                });

            modelBuilder.Entity("InfrastructureManagement.Core.Entities.Item", b =>
                {
                    b.HasOne("InfrastructureManagement.Core.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("InfrastructureManagement.Core.Entities.ItemLog", b =>
                {
                    b.HasOne("InfrastructureManagement.Core.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("InfrastructureManagement.Core.Entities.MapItem", b =>
                {
                    b.HasOne("InfrastructureManagement.Core.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfrastructureManagement.Core.Entities.Item", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("InfrastructureManagement.Core.Entities.Report", b =>
                {
                    b.HasOne("InfrastructureManagement.Core.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("InfrastructureManagement.Core.Entities.Item", "PositionItem")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfrastructureManagement.Core.Entities.Account", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId");

                    b.Navigation("Category");

                    b.Navigation("PositionItem");

                    b.Navigation("Reporter");
                });
#pragma warning restore 612, 618
        }
    }
}