﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReserbizAPP.LIB.DbContexts;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    [DbContext(typeof(ReserbizClientDataContext))]
    [Migration("20191104154710_IntroducedSaltColumnOnTableAccountsAndTenant")]
    partial class IntroducedSaltColumnOnTableAccountsAndTenant
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReserbizAPP.LIB.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateDeactivated");

                    b.Property<DateTime>("DateDeleted");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.AccountStatement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContractId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateDeactivated");

                    b.Property<DateTime>("DateDeleted");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<float>("ElectricBill");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<float>("Penalty");

                    b.Property<float>("Rate");

                    b.Property<float>("WaterBill");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("AccountStatements");
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.AccountStatementMiscellaneous", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountStatementId");

                    b.Property<float>("Amount");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateDeactivated");

                    b.Property<DateTime>("DateDeleted");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AccountStatementId");

                    b.ToTable("AccountStatementMiscellaneous");
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.ContactPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactNumber");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateDeactivated");

                    b.Property<DateTime>("DateDeleted");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<string>("PhotoUrl");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("ContactPersons");
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateDeactivated");

                    b.Property<DateTime>("DateDeleted");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<int>("DurationUnit");

                    b.Property<int>("DurationValue");

                    b.Property<DateTime>("EffectiveDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsOpenContract");

                    b.Property<int>("TenantId");

                    b.Property<int>("TermId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.HasIndex("TermId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.PaymentBreakdown", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountStatementId");

                    b.Property<float>("Amount");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateDeactivated");

                    b.Property<DateTime>("DateDeleted");

                    b.Property<DateTime>("DateReceived");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<int>("ReceivedById");

                    b.HasKey("Id");

                    b.HasIndex("AccountStatementId");

                    b.HasIndex("ReceivedById");

                    b.ToTable("PaymentBreakdowns");
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.SpaceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableSlot");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateDeactivated");

                    b.Property<DateTime>("DateDeleted");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name");

                    b.Property<float>("Rate");

                    b.HasKey("Id");

                    b.ToTable("SpaceTypes");
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("ContactNumber");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateDeactivated");

                    b.Property<DateTime>("DateDeleted");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.Term", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdvancedPaymentDurationValue");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateDeactivated");

                    b.Property<DateTime>("DateDeleted");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<int>("DepositPaymentDurationValue");

                    b.Property<int>("DurationUnit");

                    b.Property<float>("ElectricBillAmount");

                    b.Property<bool>("ExcludeElectricBill");

                    b.Property<bool>("ExcludeWaterBill");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<int>("MaximumNumberOfOccupants");

                    b.Property<string>("Name");

                    b.Property<int>("PenaltyAmountPerDurationUnit");

                    b.Property<int>("PenaltyEffectiveAfterDurationUnit");

                    b.Property<int>("PenaltyEffectiveAfterDurationValue");

                    b.Property<float>("PenaltyValue");

                    b.Property<int>("PenaltyValueType");

                    b.Property<float>("Rate");

                    b.Property<int>("SpaceTypeId");

                    b.Property<float>("WaterBillAmount");

                    b.HasKey("Id");

                    b.HasIndex("SpaceTypeId");

                    b.ToTable("Terms");
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.TermMiscellaneous", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateDeactivated");

                    b.Property<DateTime>("DateDeleted");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name");

                    b.Property<int>("TermId");

                    b.HasKey("Id");

                    b.HasIndex("TermId");

                    b.ToTable("TermMiscellaneous");
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.AccountStatement", b =>
                {
                    b.HasOne("ReserbizAPP.LIB.Models.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.AccountStatementMiscellaneous", b =>
                {
                    b.HasOne("ReserbizAPP.LIB.Models.AccountStatement", "AccountStatement")
                        .WithMany("AccountStatementMiscellaneous")
                        .HasForeignKey("AccountStatementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.ContactPerson", b =>
                {
                    b.HasOne("ReserbizAPP.LIB.Models.Tenant", "Tenant")
                        .WithMany("ContactPersons")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.Contract", b =>
                {
                    b.HasOne("ReserbizAPP.LIB.Models.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReserbizAPP.LIB.Models.Term", "Term")
                        .WithMany()
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.PaymentBreakdown", b =>
                {
                    b.HasOne("ReserbizAPP.LIB.Models.AccountStatement", "AccountStatement")
                        .WithMany()
                        .HasForeignKey("AccountStatementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReserbizAPP.LIB.Models.Account", "ReceivedBy")
                        .WithMany()
                        .HasForeignKey("ReceivedById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.Term", b =>
                {
                    b.HasOne("ReserbizAPP.LIB.Models.SpaceType", "SpaceType")
                        .WithMany()
                        .HasForeignKey("SpaceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReserbizAPP.LIB.Models.TermMiscellaneous", b =>
                {
                    b.HasOne("ReserbizAPP.LIB.Models.Term", "Term")
                        .WithMany("TermMiscellaneous")
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
