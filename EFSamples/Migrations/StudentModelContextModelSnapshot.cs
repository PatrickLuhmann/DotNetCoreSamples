﻿// <auto-generated />
using EFSamples.StudentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFSamples.Migrations
{
    [DbContext(typeof(StudentModelContext))]
    partial class StudentModelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("EFSamples.StudentModel.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("StudentName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("EFSamples.StudentModel.StudentAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address2")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ZipCode")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("StudentAddresses");
                });

            modelBuilder.Entity("EFSamples.StudentModel.StudentAddressFKAnnotation", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address2")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.Property<int>("ZipCode")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("StudentAddressFKAnnotations");
                });

            modelBuilder.Entity("EFSamples.StudentModel.StudentAddressUseFluent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address2")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.Property<int>("StudentForeignKey")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ZipCode")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StudentForeignKey")
                        .IsUnique();

                    b.ToTable("StudentAddressUseFluents");
                });

            modelBuilder.Entity("EFSamples.StudentModel.StudentAddress", b =>
                {
                    b.HasOne("EFSamples.StudentModel.Student", "Student")
                        .WithOne("Address")
                        .HasForeignKey("EFSamples.StudentModel.StudentAddress", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EFSamples.StudentModel.StudentAddressFKAnnotation", b =>
                {
                    b.HasOne("EFSamples.StudentModel.Student", "Student")
                        .WithOne("AnnotationAddress")
                        .HasForeignKey("EFSamples.StudentModel.StudentAddressFKAnnotation", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EFSamples.StudentModel.StudentAddressUseFluent", b =>
                {
                    b.HasOne("EFSamples.StudentModel.Student", "Student")
                        .WithOne("FluentAddress")
                        .HasForeignKey("EFSamples.StudentModel.StudentAddressUseFluent", "StudentForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
