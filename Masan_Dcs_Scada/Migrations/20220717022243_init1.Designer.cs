﻿// <auto-generated />
using System;
using Masan_Dcs_Scada.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Masan_Dcs_Scada.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220717022243_init1")]
    partial class init1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Masan_Dcs_Scada.Models.Account", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Role")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Name");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Masan_Dcs_Scada.Models.HeadShift", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Code");

                    b.ToTable("HeadShifts");
                });

            modelBuilder.Entity("Masan_Dcs_Scada.Models.Product", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Code");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Masan_Dcs_Scada.Models.Shift", b =>
                {
                    b.Property<int>("ShiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("HeadShiftCode1")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("HeadShiftCode2")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("HeadShiftCode3")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("ShiftStartTime1")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("ShiftStartTime2")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("ShiftStartTime3")
                        .HasColumnType("datetime");

                    b.HasKey("ShiftId");

                    b.HasIndex("HeadShiftCode1");

                    b.HasIndex("HeadShiftCode2");

                    b.HasIndex("HeadShiftCode3");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("Masan_Dcs_Scada.Models.Shift", b =>
                {
                    b.HasOne("Masan_Dcs_Scada.Models.HeadShift", "HeadShift1")
                        .WithMany()
                        .HasForeignKey("HeadShiftCode1");

                    b.HasOne("Masan_Dcs_Scada.Models.HeadShift", "HeadShift2")
                        .WithMany()
                        .HasForeignKey("HeadShiftCode2");

                    b.HasOne("Masan_Dcs_Scada.Models.HeadShift", "HeadShift3")
                        .WithMany()
                        .HasForeignKey("HeadShiftCode3");
                });
#pragma warning restore 612, 618
        }
    }
}
