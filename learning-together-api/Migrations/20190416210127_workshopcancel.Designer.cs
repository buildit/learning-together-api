﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using learning_together_api.Data;

namespace learningtogetherapi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190416210127_workshopcancel")]
    partial class workshopcancel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("learning_together_api.Data.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("category","admin");
                });

            modelBuilder.Entity("learning_together_api.Data.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentDisciplineId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ParentDisciplineId");

                    b.ToTable("disciplines","admin");
                });

            modelBuilder.Entity("learning_together_api.Data.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("locations","admin");
                });

            modelBuilder.Entity("learning_together_api.Data.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("roles","admin");
                });

            modelBuilder.Entity("learning_together_api.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("LastName");

                    b.Property<int?>("LocationId");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<int?>("RoleId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("RoleId");

                    b.ToTable("users","admin");
                });

            modelBuilder.Entity("learning_together_api.Data.UserInterest", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("DisciplineId");

                    b.HasKey("UserId", "DisciplineId");

                    b.HasIndex("DisciplineId");

                    b.ToTable("userinterests","admin");
                });

            modelBuilder.Entity("learning_together_api.Data.Workshop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Cancelled");

                    b.Property<string>("Description");

                    b.Property<int>("EducatorId");

                    b.Property<DateTime>("End");

                    b.Property<string>("ImageUrl");

                    b.Property<int>("LocationId");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Webex");

                    b.HasKey("Id");

                    b.HasIndex("EducatorId");

                    b.HasIndex("LocationId");

                    b.ToTable("workshops","workshop");
                });

            modelBuilder.Entity("learning_together_api.Data.WorkshopAttendee", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("WorkshopId");

                    b.HasKey("UserId", "WorkshopId");

                    b.HasIndex("WorkshopId");

                    b.ToTable("workshopattendees","workshop");
                });

            modelBuilder.Entity("learning_together_api.Data.WorkshopTopic", b =>
                {
                    b.Property<int>("WorkshopId");

                    b.Property<int>("DisciplineId");

                    b.HasKey("WorkshopId", "DisciplineId");

                    b.HasIndex("DisciplineId");

                    b.ToTable("workshoptopics","workshop");
                });

            modelBuilder.Entity("learning_together_api.Data.Discipline", b =>
                {
                    b.HasOne("learning_together_api.Data.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("learning_together_api.Data.Discipline", "ParentDiscipline")
                        .WithMany()
                        .HasForeignKey("ParentDisciplineId");
                });

            modelBuilder.Entity("learning_together_api.Data.User", b =>
                {
                    b.HasOne("learning_together_api.Data.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("learning_together_api.Data.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("learning_together_api.Data.UserInterest", b =>
                {
                    b.HasOne("learning_together_api.Data.Discipline", "Discipline")
                        .WithMany("UserInterests")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("learning_together_api.Data.User", "User")
                        .WithMany("UserInterests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("learning_together_api.Data.Workshop", b =>
                {
                    b.HasOne("learning_together_api.Data.User", "Educator")
                        .WithMany()
                        .HasForeignKey("EducatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("learning_together_api.Data.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("learning_together_api.Data.WorkshopAttendee", b =>
                {
                    b.HasOne("learning_together_api.Data.User", "User")
                        .WithMany("WorkshopAttendees")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("learning_together_api.Data.Workshop", "Workshop")
                        .WithMany("WorkshopAttendees")
                        .HasForeignKey("WorkshopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("learning_together_api.Data.WorkshopTopic", b =>
                {
                    b.HasOne("learning_together_api.Data.Discipline", "Discipline")
                        .WithMany("WorkshopTopics")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("learning_together_api.Data.Workshop", "Workshop")
                        .WithMany("WorkshopTopics")
                        .HasForeignKey("WorkshopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
