﻿// <auto-generated />
using System;
using HRMS.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HRMS.Persistance.Migrations
{
    [DbContext(typeof(HRMSContext))]
    partial class HRMSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HRMS.Core.Model.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<int>("CountryId");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsValid");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("PostalCode");

                    b.Property<int>("RegionId");

                    b.Property<string>("StreetName");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("RegionId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("HRMS.Core.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsValid");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("RegionId");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("RegionId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("HRMS.Core.Model.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsValid");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("NIPT");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("HRMS.Core.Model.CompanySite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CompanyId");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsValid");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<Guid>("SiteId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("SiteId");

                    b.ToTable("CompanySite");
                });

            modelBuilder.Entity("HRMS.Core.Model.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsValid");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("HRMS.Core.Model.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AddressId");

                    b.Property<DateTime>("BithDate");

                    b.Property<Guid>("ContactId");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Email");

                    b.Property<bool>("IsValid");

                    b.Property<string>("LastName");

                    b.Property<string>("Mobile");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ContactId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("HRMS.Core.Model.Organigram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsCeo");

                    b.Property<bool>("IsValid");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<Guid?>("RespondsToId");

                    b.HasKey("Id");

                    b.HasIndex("RespondsToId");

                    b.ToTable("Organigram");
                });

            modelBuilder.Entity("HRMS.Core.Model.OrganigramEmployee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("BrutoAmountInMonth");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<Guid>("EmployeeId");

                    b.Property<DateTime?>("EndDate");

                    b.Property<bool>("IsValid");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<decimal>("NetAmountInMonth");

                    b.Property<Guid>("OrganigramId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OrganigramId");

                    b.ToTable("OrganigramEmployee");
                });

            modelBuilder.Entity("HRMS.Core.Model.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsValid");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("HRMS.Core.Model.Site", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AddressId");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsValid");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Site");
                });

            modelBuilder.Entity("HRMS.Core.Model.Address", b =>
                {
                    b.HasOne("HRMS.Core.Model.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HRMS.Core.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HRMS.Core.Model.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HRMS.Core.Model.City", b =>
                {
                    b.HasOne("HRMS.Core.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HRMS.Core.Model.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HRMS.Core.Model.CompanySite", b =>
                {
                    b.HasOne("HRMS.Core.Model.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HRMS.Core.Model.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HRMS.Core.Model.Employee", b =>
                {
                    b.HasOne("HRMS.Core.Model.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HRMS.Core.Model.Employee", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HRMS.Core.Model.Organigram", b =>
                {
                    b.HasOne("HRMS.Core.Model.Organigram", "RespondsTo")
                        .WithMany()
                        .HasForeignKey("RespondsToId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HRMS.Core.Model.OrganigramEmployee", b =>
                {
                    b.HasOne("HRMS.Core.Model.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HRMS.Core.Model.Organigram", "Organigram")
                        .WithMany()
                        .HasForeignKey("OrganigramId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HRMS.Core.Model.Region", b =>
                {
                    b.HasOne("HRMS.Core.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HRMS.Core.Model.Site", b =>
                {
                    b.HasOne("HRMS.Core.Model.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
