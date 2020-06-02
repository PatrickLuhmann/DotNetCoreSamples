using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples.FinanceModel
{
	public class FinanceModelContext : DbContext
	{
		public string DbConnectionString = @"DataSource=finance_sample.db";
		public static ILoggerFactory Logger = null;

		#region Tables
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
		#endregion

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// The path is relative to the main assembly (.exe).
			optionsBuilder.UseSqlite(DbConnectionString)
				.EnableSensitiveDataLogging();

			// Register our console logger.
			if (Logger != null)
				optionsBuilder.UseLoggerFactory(Logger);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}

		public FinanceModelContext(string dbConnect)
		{
			DbConnectionString = dbConnect;
		}

		public FinanceModelContext() { }
	}
}
