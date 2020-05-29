using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples.FinanceModel
{
	public class FinanceModelContext : DbContext
	{
		public static ILoggerFactory Logger = null;
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Activity> Activities { get; set; }
		public DbSet<CashActivity> CashActivities { get; set; }
		public DbSet<SharesActivity> SharesActivities { get; set; }
		public DbSet<SharesInActivity> SharesInActivities { get; set; }
		public DbSet<SharesOutActivity> SharesOutActivities { get; set; }
		public DbSet<Lot> Lots { get; set; }
		public DbSet<LotAssignment> LotAssignments { get; set; }
		public DbSet<Security> Securities { get; set; }
		public DbSet<Price> Prices { get; set; }
		public DbSet<CashDividend> Dividends { get; set; }
		public DbSet<QuarterlyReport> QuarterlyReports { get; set; }
		public DbSet<FinancialResult> FinancialResults { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// The path is relative to the main assembly (.exe).
			optionsBuilder.UseSqlite(@"DataSource=finance_sample.db")
				.EnableSensitiveDataLogging();

			// Register our console logger.
			if (Logger != null)
				optionsBuilder.UseLoggerFactory(Logger);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}
	}
}
