using ConsoleSamples.Finance_Sample;
using EFSamples.FinanceModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
	public class UT_FinanceLogic : IClassFixture<UT_FinanceLogic_Fixture>
	{
		private readonly ITestOutputHelper Output;
		UT_FinanceLogic_Fixture Fixture;
		FinanceLogic StaticFinLog;

		public UT_FinanceLogic(ITestOutputHelper cout, UT_FinanceLogic_Fixture fixture)
		{
			Output = cout;
			Fixture = fixture;
			StaticFinLog = new FinanceLogic(UT_FinanceLogic_Fixture.StaticDbFilename);
		}

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public void GetAllAccounts(bool openOnly)
		{
			// ASSEMBLE

			// ACT
			List<Account> actAccts = StaticFinLog.GetAllAccounts(openOnly);

			// ASSERT
			int expCount = openOnly ? 2 : 3;
			Assert.Equal(expCount, actAccts.Count);
		}
	}

	public class UT_FinanceLogic_Fixture
	{
		//static public string StaticDbFilename
		static public string StaticDbFilename = "ut_static_financelogic.db";
		public UT_FinanceLogic_Fixture()
		{
			StaticDbFilename = "ut_static_financelogic.db";

			// If the static test database does not exist, create it.
			if (File.Exists(StaticDbFilename))
			{
				using var context = new FinanceModelContext(StaticDbFilename);
				// This will update the database if it is missing migrations.
				context.Database.Migrate();

				// Make any adjustments as required by new migrations.
			}
			else
			{
				using var context = new FinanceModelContext(StaticDbFilename);

				// This creates the database when it does not exist.
				context.Database.Migrate();

				// Create three "empty" Accounts.
				// SaveChanges after each one so that we know their IDs.
				Account acct1 = new Account()
				{
					Institution = "First Bank Of Unit Testing",
					Name = "Standard Brokerage Account",
					Closed = false,
				};
				context.Accounts.Add(acct1);
				context.SaveChanges();

				context.Accounts.Add(new Account()
				{
					Institution = "Second Bank Of Unit Testing",
					Name = "Roth IRA Account",
					Closed = false,
				});
				context.SaveChanges();

				context.Accounts.Add(new Account()
				{
					Institution = "Third Bank Of Unit Testing",
					Name = "Traditional IRA Account",
					Closed = true,
				});
				context.SaveChanges();
			}
		}
	}
}
