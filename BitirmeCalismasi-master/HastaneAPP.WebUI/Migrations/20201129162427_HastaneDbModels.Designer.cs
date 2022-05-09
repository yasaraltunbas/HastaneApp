﻿// <auto-generated />
using System;
using HastaneAPP.WebUI.Services.AppDbService.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HastaneAPP.Migrations
{
    [DbContext(typeof(HastaneDbContex))]
    [Migration("20201129162427_HastaneDbModels")]
    partial class HastaneDbModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HastaneAPP.WebUI.Models.Entity.ExtraHastalik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Answers")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("HastalarId")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HastalarId");

                    b.ToTable("extraHastaliks");
                });

            modelBuilder.Entity("HastaneAPP.WebUI.Models.Entity.Hastalar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Applicant")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Operation")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<string>("TCNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Hastalars");
                });

            modelBuilder.Entity("HastaneAPP.WebUI.Models.Entity.Operasyonlar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Operasyonlars");
                });

            modelBuilder.Entity("HastaneAPP.WebUI.Models.Entity.ExtraHastalik", b =>
                {
                    b.HasOne("HastaneAPP.WebUI.Models.Entity.Hastalar", null)
                        .WithMany("ExtraHastaliklar")
                        .HasForeignKey("HastalarId");
                });
#pragma warning restore 612, 618
        }
    }
}