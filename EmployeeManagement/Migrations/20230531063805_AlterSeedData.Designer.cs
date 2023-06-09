﻿// <auto-generated />
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManagement.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230531063805_AlterSeedData")]
    partial class AlterSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmployeeManagement.Models.Employee", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Department");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            id = 2,
                            Department = 4,
                            Email = "mou@gmail.com",
                            Name = "Moshumee Mollika Moue"
                        },
                        new
                        {
                            id = 3,
                            Department = 2,
                            Email = "Rihan@gmail.com",
                            Name = "Ibrahim Rihan Ayat"
                        },
                        new
                        {
                            id = 4,
                            Department = 1,
                            Email = "hena@gmail.com",
                            Name = "Hena"
                        },
                        new
                        {
                            id = 5,
                            Department = 1,
                            Email = "safia@gmail.com",
                            Name = "Safia"
                        },
                        new
                        {
                            id = 6,
                            Department = 4,
                            Email = "safin@gmail.com",
                            Name = "Safin"
                        },
                        new
                        {
                            id = 7,
                            Department = 4,
                            Email = "ayan@gmail.com",
                            Name = "Ayan"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
