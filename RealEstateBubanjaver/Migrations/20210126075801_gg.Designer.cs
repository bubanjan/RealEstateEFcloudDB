﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using realestateBubanjaEF;
using realestateBubanjaEF.Models;

namespace RealEstateBubanjaver_.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210126075801_gg")]
    partial class gg
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("realestateBubanjaEF.Estate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.Property<int>("Price");

                    b.Property<int>("Size");

                    b.Property<int?>("TypeID");

                    b.HasKey("ID");

                    b.HasIndex("TypeID");

                    b.ToTable("Estate");
                });

            modelBuilder.Entity("realestateBubanjaEF.Type", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Type");
                });

            modelBuilder.Entity("realestateBubanjaEF.Estate", b =>
                {
                    b.HasOne("realestateBubanjaEF.Type", "Type")
                        .WithMany()
                        .HasForeignKey("TypeID");
                });
#pragma warning restore 612, 618
        }
    }
}
