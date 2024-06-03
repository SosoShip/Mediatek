﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SVE.Mediatek.Dal;

#nullable disable

namespace SVE.Mediatek.DAL.Migrations
{
    [DbContext(typeof(MediatekContext))]
    [Migration("20240516102442_addManagerFeatures")]
    partial class addManagerFeatures
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SVE.Mediatek.DAL.Entities.AbsenceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("BeginDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("Reason")
                        .HasColumnType("int");

                    b.Property<int?>("StaffEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StaffEntityId");

                    b.ToTable("Absence");
                });

            modelBuilder.Entity("SVE.Mediatek.DAL.Entities.StaffEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Staffs");

                    b.HasDiscriminator<string>("Discriminator").HasValue("StaffEntity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SVE.Mediatek.DAL.Entities.ManagerEntity", b =>
                {
                    b.HasBaseType("SVE.Mediatek.DAL.Entities.StaffEntity");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("ManagerEntity");
                });

            modelBuilder.Entity("SVE.Mediatek.DAL.Entities.AbsenceEntity", b =>
                {
                    b.HasOne("SVE.Mediatek.DAL.Entities.StaffEntity", null)
                        .WithMany("AbsenceList")
                        .HasForeignKey("StaffEntityId");
                });

            modelBuilder.Entity("SVE.Mediatek.DAL.Entities.StaffEntity", b =>
                {
                    b.Navigation("AbsenceList");
                });
#pragma warning restore 612, 618
        }
    }
}
