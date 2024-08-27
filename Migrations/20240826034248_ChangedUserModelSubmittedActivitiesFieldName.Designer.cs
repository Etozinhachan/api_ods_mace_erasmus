﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_ods_mace_erasmus.data;

#nullable disable

namespace api_ods_mace_erasmus.Migrations
{
    [DbContext(typeof(DbDataContext))]
    [Migration("20240826034248_ChangedUserModelSubmittedActivitiesFieldName")]
    partial class ChangedUserModelSubmittedActivitiesFieldName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("api_ods_mace_erasmus.models.Activity", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<byte>("activity_state")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("explanation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("image_uris")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("latitude")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("longitude")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ods")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("user_id")
                        .HasColumnType("char(36)");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("api_ods_mace_erasmus.models.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("passHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("salt")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("api_ods_mace_erasmus.models.Activity", b =>
                {
                    b.HasOne("api_ods_mace_erasmus.models.User", "submited_by_user")
                        .WithMany("submitted_activities")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("submited_by_user");
                });

            modelBuilder.Entity("api_ods_mace_erasmus.models.User", b =>
                {
                    b.Navigation("submitted_activities");
                });
#pragma warning restore 612, 618
        }
    }
}
