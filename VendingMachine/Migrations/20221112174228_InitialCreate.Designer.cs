// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VendingMachineApplication.Models;

namespace VendingMachineApplication.Migrations
{
    [DbContext(typeof(VendingMachineContext))]
    [Migration("20221112174228_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VendingMachineApplication.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.Property<int?>("VendingMachineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VendingMachineId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("VendingMachineApplication.Models.VendingMachine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Credits")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("VendingMachines");
                });

            modelBuilder.Entity("VendingMachineApplication.Models.Product", b =>
                {
                    b.HasOne("VendingMachineApplication.Models.VendingMachine", null)
                        .WithMany("Products")
                        .HasForeignKey("VendingMachineId");
                });

            modelBuilder.Entity("VendingMachineApplication.Models.VendingMachine", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
