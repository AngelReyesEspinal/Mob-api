﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Context;

namespace Models.Migrations
{
    [DbContext(typeof(MobDbContext))]
    [Migration("20200628185838_adding-permissions")]
    partial class addingpermissions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("FileName");

                    b.Property<string>("OriginalName");

                    b.Property<string>("Path");

                    b.HasKey("Id");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("Models.Entities.Evaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name");

                    b.Property<int>("SubjectId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("Models.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<int?>("DocumentId");

                    b.Property<int>("EvaluationId");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.Property<int>("UserId");

                    b.Property<string>("Wildcard");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("EvaluationId");

                    b.HasIndex("UserId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Models.Entities.QuestionAnswerOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<bool>("IsCorrect");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("QuestionAnswerOption");
                });

            modelBuilder.Entity("Models.Entities.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<int>("DocumentId");

                    b.Property<string>("Name");

                    b.Property<string>("SecretKey");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("UserId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.Entities.Evaluation", b =>
                {
                    b.HasOne("Models.Entities.Subject", "Subject")
                        .WithMany("Evaluations")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Entities.Question", b =>
                {
                    b.HasOne("Models.Entities.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId");

                    b.HasOne("Models.Entities.Evaluation", "Evaluation")
                        .WithMany("Questions")
                        .HasForeignKey("EvaluationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Entities.QuestionAnswerOption", b =>
                {
                    b.HasOne("Models.Entities.Question", "Question")
                        .WithMany("QuestionAnswerOptions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Entities.Subject", b =>
                {
                    b.HasOne("Models.Entities.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
