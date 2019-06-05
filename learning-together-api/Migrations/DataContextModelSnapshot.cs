﻿// <auto-generated />

namespace learningtogetherapi.Migrations
{
    using System;
    using learning_together_api.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                b.ToTable("category", "admin");

                b.HasData(
                    new
                    {
                        Id = 1,
                        Name = "Professional Development"
                    },
                    new
                    {
                        Id = 2,
                        Name = "Emotional Intelligence"
                    },
                    new
                    {
                        Id = 3,
                        Name = "Teamwork"
                    },
                    new
                    {
                        Id = 4,
                        Name = "Leadership"
                    },
                    new
                    {
                        Id = 5,
                        Name = "Design"
                    },
                    new
                    {
                        Id = 6,
                        Name = "Analytics"
                    },
                    new
                    {
                        Id = 7,
                        Name = "Culture"
                    },
                    new
                    {
                        Id = 8,
                        Name = "Agile / Lean"
                    },
                    new
                    {
                        Id = 9,
                        Name = "Artificial Intelligence"
                    },
                    new
                    {
                        Id = 10,
                        Name = "Technology"
                    });
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

                b.ToTable("disciplines", "admin");
            });

            modelBuilder.Entity("learning_together_api.Data.Location", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Name");

                b.HasKey("Id");

                b.ToTable("locations", "admin");

                b.HasData(
                    new
                    {
                        Id = 2,
                        Name = "London"
                    },
                    new
                    {
                        Id = 1,
                        Name = "Brooklyn"
                    },
                    new
                    {
                        Id = 3,
                        Name = "Edinburgh"
                    },
                    new
                    {
                        Id = 4,
                        Name = "Dublin"
                    },
                    new
                    {
                        Id = 5,
                        Name = "Denver"
                    },
                    new
                    {
                        Id = 6,
                        Name = "Dallas"
                    });
            });

            modelBuilder.Entity("learning_together_api.Data.Role", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Name");

                b.HasKey("Id");

                b.ToTable("roles", "admin");

                b.HasData(
                    new
                    {
                        Id = 1,
                        Name = "Creative Technologist"
                    },
                    new
                    {
                        Id = 2,
                        Name = "Frontend"
                    },
                    new
                    {
                        Id = 3,
                        Name = "Backend"
                    },
                    new
                    {
                        Id = 4,
                        Name = "Fullstack"
                    },
                    new
                    {
                        Id = 5,
                        Name = "Design"
                    },
                    new
                    {
                        Id = 6,
                        Name = "Product"
                    },
                    new
                    {
                        Id = 7,
                        Name = "Delivery"
                    },
                    new
                    {
                        Id = 8,
                        Name = "Leadership"
                    });
            });

            modelBuilder.Entity("learning_together_api.Data.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool?>("Deactivated");

                b.Property<string>("DirectoryName");

                b.Property<string>("FirstName");

                b.Property<string>("ImageUrl");

                b.Property<DateTime>("LastLogin");

                b.Property<string>("LastName");

                b.Property<int?>("LocationId");

                b.Property<string>("OrganizationId");

                b.Property<int?>("RoleId");

                b.Property<string>("Username");

                b.HasKey("Id");

                b.HasIndex("LocationId");

                b.HasIndex("RoleId");

                b.ToTable("users", "admin");
            });

            modelBuilder.Entity("learning_together_api.Data.UserInterest", b =>
            {
                b.Property<int>("UserId");

                b.Property<int>("DisciplineId");

                b.HasKey("UserId", "DisciplineId");

                b.HasIndex("DisciplineId");

                b.ToTable("userinterests", "admin");
            });

            modelBuilder.Entity("learning_together_api.Data.Workshop", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool?>("Cancelled");

                b.Property<int?>("CategoryId");

                b.Property<DateTime>("CreatedDate");

                b.Property<string>("Description");

                b.Property<int>("EducatorId");

                b.Property<DateTime>("End");

                b.Property<string>("ImageUrl");

                b.Property<int>("LocationId");

                b.Property<DateTime>("ModifiedDate");

                b.Property<string>("Name");

                b.Property<string>("RobinEventId");

                b.Property<string>("Room");

                b.Property<DateTime>("Start");

                b.Property<string>("Webex");

                b.HasKey("Id");

                b.HasIndex("CategoryId");

                b.HasIndex("EducatorId");

                b.HasIndex("LocationId");

                b.ToTable("workshops", "workshop");
            });

            modelBuilder.Entity("learning_together_api.Data.WorkshopAttendee", b =>
            {
                b.Property<int>("UserId");

                b.Property<int>("WorkshopId");

                b.Property<DateTime>("CreatedDate");

                b.HasKey("UserId", "WorkshopId");

                b.HasIndex("WorkshopId");

                b.ToTable("workshopattendees", "workshop");
            });

            modelBuilder.Entity("learning_together_api.Data.WorkshopTopic", b =>
            {
                b.Property<int>("WorkshopId");

                b.Property<int>("DisciplineId");

                b.HasKey("WorkshopId", "DisciplineId");

                b.HasIndex("DisciplineId");

                b.ToTable("workshoptopics", "workshop");
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
                b.HasOne("learning_together_api.Data.Category", "Category")
                    .WithMany()
                    .HasForeignKey("CategoryId");

                b.HasOne("learning_together_api.Data.User", "Educator")
                    .WithMany("WorkshopsTeaching")
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
                    .WithMany("WorkshopsAttending")
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