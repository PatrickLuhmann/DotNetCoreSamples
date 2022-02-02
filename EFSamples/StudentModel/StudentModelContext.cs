using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples.StudentModel
{
	public class StudentModelContext : DbContext
	{
		#region Schema
		public DbSet<Student> Students { get; set; }
		public DbSet<StudentAddress> StudentAddresses { get; set; }
		public DbSet<StudentAddressFKAnnotation> StudentAddressFKAnnotations { get; set; }
		public DbSet<StudentAddressUseFluent> StudentAddressUseFluents { get; set; }
		public DbSet<Grade> Grades { get; set; }
		#endregion

		private string? DbPath = null;

		#region Constructor and Configuration
		// This is intended to be used only at design time.
		public StudentModelContext(DbContextOptions<StudentModelContext> options) : base(options) { }

		public StudentModelContext(string path)
		{
			DbPath = path;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// Only call UseSqlite if we got the path from the constructor.
			if (DbPath != null)
			{
				// The path is relative to the main assembly (.exe).
				optionsBuilder.UseSqlite($"Data Source={DbPath}")
					.EnableSensitiveDataLogging();
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure StudentForeignKey as the FK for StudentAddressUseFluent.
			modelBuilder.Entity<Student>()
				.HasOne(s => s.FluentAddress)
				.WithOne(fa => fa.Student)
				.HasForeignKey<StudentAddressUseFluent>(fa => fa.StudentForeignKey);

			// Define the owned type in the Address entities.
			modelBuilder.Entity<StudentAddress>()
				.OwnsOne(sa => sa.HomeAddress);
			modelBuilder.Entity<StudentAddressFKAnnotation>()
				.OwnsOne(sa => sa.HomeAddress);
			modelBuilder.Entity<StudentAddressUseFluent>()
				.OwnsOne(sa => sa.HomeAddress);
		}
		#endregion
	}

	public class StudentModelContextFactory : IDesignTimeDbContextFactory<StudentModelContext>
	{
		public StudentModelContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<StudentModelContext>();
			optionsBuilder.UseSqlite("Data Source=student-design-time.db");
			// Note that we don't do EnableSensitiveDataLogging() or anything else here
			// because those things don't make sense at design time.

			return new StudentModelContext(optionsBuilder.Options);
		}
	}
}
