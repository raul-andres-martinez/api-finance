﻿// <auto-generated />
using System;
using Finance.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Finance.Persistence.Migrations
{
    [DbContext(typeof(FinanceContext))]
    partial class FinanceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Finance.Domain.Models.Entities.Expense", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamptz");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<int?>("FrequencyInDays")
                        .HasMaxLength(3)
                        .HasColumnType("integer");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("varchar");

                    b.Property<bool>("Recurring")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Uid");

                    b.HasIndex("UserId");

                    b.ToTable("expenses", (string)null);
                });

            modelBuilder.Entity("Finance.Domain.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Uid");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Finance.Domain.Models.Entities.Expense", b =>
                {
                    b.HasOne("Finance.Domain.Models.Entities.User", "User")
                        .WithMany("Expenses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Finance.Domain.Models.Entities.User", b =>
                {
                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}
