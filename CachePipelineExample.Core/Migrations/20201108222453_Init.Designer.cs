﻿// <auto-generated />
using System;
using CachePipelineExample.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CachePipelineExample.Core.Migrations
{
    [DbContext(typeof(ContextCore))]
    [Migration("20201108222453_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CachePipelineExample.Core.BasicEntityImpl", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Field")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BasicEntityImpls");
                });

            modelBuilder.Entity("CachePipelineExample.Core.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CachePipelineExample.Core.CourseJoinProfessor", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProfessorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("History_CourseHistoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("History_ProfessorHistoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CourseId", "ProfessorId");

                    b.HasIndex("History_CourseHistoryId");

                    b.HasIndex("History_ProfessorHistoryId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("CourseJoinProfessors");
                });

            modelBuilder.Entity("CachePipelineExample.Core.CourseJoinStudent", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("History_CourseHistoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("History_StudentHistoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("History_CourseHistoryId");

                    b.HasIndex("History_StudentHistoryId");

                    b.HasIndex("StudentId");

                    b.ToTable("CourseJoinStudents");
                });

            modelBuilder.Entity("CachePipelineExample.Core.Professor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("CachePipelineExample.Core.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("History_BasicEntityImpl", b =>
                {
                    b.Property<Guid>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Field")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HistoryChangerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("HistoryInsertDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("HistoryStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HistoryTransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("HistoryId");

                    b.ToTable("History_BasicEntityImpl");
                });

            modelBuilder.Entity("History_Course", b =>
                {
                    b.Property<Guid>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HistoryChangerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("HistoryInsertDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("HistoryStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HistoryTransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HistoryId");

                    b.ToTable("History_Course");
                });

            modelBuilder.Entity("History_CourseJoinProfessor", b =>
                {
                    b.Property<Guid>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HistoryChangerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("HistoryInsertDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("HistoryStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HistoryTransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProfessorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("HistoryId");

                    b.HasIndex("CourseId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("History_CourseJoinProfessor");
                });

            modelBuilder.Entity("History_CourseJoinStudent", b =>
                {
                    b.Property<Guid>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HistoryChangerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("HistoryInsertDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("HistoryStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HistoryTransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("HistoryId");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("History_CourseJoinStudent");
                });

            modelBuilder.Entity("History_Professor", b =>
                {
                    b.Property<Guid>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HistoryChangerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("HistoryInsertDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("HistoryStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HistoryTransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HistoryId");

                    b.ToTable("History_Professor");
                });

            modelBuilder.Entity("History_Student", b =>
                {
                    b.Property<Guid>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HistoryChangerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("HistoryInsertDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("HistoryStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HistoryTransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HistoryId");

                    b.ToTable("History_Student");
                });

            modelBuilder.Entity("CachePipelineExample.Core.CourseJoinProfessor", b =>
                {
                    b.HasOne("CachePipelineExample.Core.Course", "Course")
                        .WithMany("CourseJoinProfessors")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("History_Course", null)
                        .WithMany("CourseJoinProfessors")
                        .HasForeignKey("History_CourseHistoryId");

                    b.HasOne("History_Professor", null)
                        .WithMany("CourseJoinProfessor")
                        .HasForeignKey("History_ProfessorHistoryId");

                    b.HasOne("CachePipelineExample.Core.Professor", "Professor")
                        .WithMany("CourseJoinProfessor")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CachePipelineExample.Core.CourseJoinStudent", b =>
                {
                    b.HasOne("CachePipelineExample.Core.Course", "Course")
                        .WithMany("CourseJoinStudents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("History_Course", null)
                        .WithMany("CourseJoinStudents")
                        .HasForeignKey("History_CourseHistoryId");

                    b.HasOne("History_Student", null)
                        .WithMany("CourseJoinStudents")
                        .HasForeignKey("History_StudentHistoryId");

                    b.HasOne("CachePipelineExample.Core.Student", "Student")
                        .WithMany("CourseJoinStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("History_CourseJoinProfessor", b =>
                {
                    b.HasOne("CachePipelineExample.Core.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CachePipelineExample.Core.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("History_CourseJoinStudent", b =>
                {
                    b.HasOne("CachePipelineExample.Core.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CachePipelineExample.Core.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
