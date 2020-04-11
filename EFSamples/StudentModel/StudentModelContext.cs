using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples.StudentModel
{
	public class StudentModelContext : DbContext
	{
		public DbSet<Student> Students { get; set; }
		public DbSet<StudentAddress> StudentAddresses { get; set; }
		public DbSet<StudentAddressFKAnnotation> StudentAddressFKAnnotations { get; set; }
		public DbSet<StudentAddressUseFluent> StudentAddressUseFluents { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// The path is relative to the main assembly (.exe).
			optionsBuilder.UseSqlite(@"DataSource=students_sample.db")
				.EnableSensitiveDataLogging();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure StudentForeignKey as the FK for StudentAddressUseFluent.
			modelBuilder.Entity<Student>()
				.HasOne(s => s.FluentAddress)
				.WithOne(fa => fa.Student)
				.HasForeignKey<StudentAddressUseFluent>(fa => fa.StudentForeignKey);
		}
	}
}
