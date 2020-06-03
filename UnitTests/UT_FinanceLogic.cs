using ConsoleSamples.Finance_Sample;
using EFSamples.FinanceModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
			StaticFinLog = new FinanceLogic(fixture.StaticDbFilename);
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

			// Must do a shallow compare, so can't use Account.Equals().
			Assert.Equal(1, actAccts[0].Id);
			Assert.Empty(actAccts[0].Events);
			Assert.Equal(Fixture.Account1.Institution, actAccts[0].Institution);
			Assert.Equal(Fixture.Account1.Name, actAccts[0].Name);
			Assert.False(actAccts[0].Closed);

			Assert.Equal(2, actAccts[1].Id);
			Assert.Empty(actAccts[1].Events);
			Assert.Equal(Fixture.Account2.Institution, actAccts[1].Institution);
			Assert.Equal(Fixture.Account2.Name, actAccts[1].Name);
			Assert.False(actAccts[1].Closed);

			if (!openOnly)
			{
				Assert.Equal(3, actAccts[2].Id);
				Assert.Empty(actAccts[2].Events);
				Assert.Equal(Fixture.Account3.Institution, actAccts[2].Institution);
				Assert.Equal(Fixture.Account3.Name, actAccts[2].Name);
				Assert.True(actAccts[2].Closed);
			}
		}

		[Fact]
		public void GetAccount_Basic()
		{
			// ACT
			Account actAcct1 = StaticFinLog.GetAccount(1);
			Account actAcct2 = StaticFinLog.GetAccount(2);
			Account actAcct3 = StaticFinLog.GetAccount(3);

			// ASSERT
			Assert.Equal(Fixture.Account1, actAcct1);
			Assert.Equal(Fixture.Account2, actAcct2);
			Assert.Equal(Fixture.Account3, actAcct3);
		}
	}

	public class UT_FinanceLogic_Fixture
	{
		readonly public string StaticDbFilename = "ut_static_financelogic.db";

		readonly public Account Account1;
		readonly public Account Account2;
		readonly public Account Account3;

		public UT_FinanceLogic_Fixture()
		{
			// If the static test database does not exist, create it.
			if (File.Exists(StaticDbFilename))
			{
				using var context = new FinanceModelContext(StaticDbFilename);
				// This will update the database if it is missing migrations.
				context.Database.Migrate();

				// Make any adjustments as required by new migrations.

				// Get our static data from the database (mostly to populate the Id field).
				List<Account> allAccounts = context.Accounts
					.Include(a => a.Events)
						.ThenInclude(e => e.Dividend)
					.ToList();
				foreach (Account acct in allAccounts)
				{
					foreach (Event ev in acct.Events)
					{
						context.Entry(ev).Collection(e => e.Activities).Load();
						foreach (Activity act in ev.Activities)
						{
							if (act is SharesInActivity siAct)
							{
								context.Entry(siAct).Reference(sia => sia.Lot).Load();
								context.Entry(siAct.Lot).Collection(l => l.LotAssignments).Load();
							}
							else if (act is SharesOutActivity soAct)
								context.Entry(soAct).Collection(soa => soa.LotAssignments).Load();
						}
					}
				}
				Account1 = allAccounts.Find(a => a.Id == 1);
				Account2 = allAccounts.Find(a => a.Id == 2);
				Account3 = allAccounts.Find(a => a.Id == 3);
			}
			else
			{
				// Static data for the database.
				Account1 = new Account()
				{
					Institution = "First Bank Of Unit Testing",
					Name = "Standard Brokerage Account",
					Closed = false,
				};
				Event event1 = new Event("Deposit")
				{
					DividendId = null,
					Date = DateTime.Parse("1/1/1998").Date,
					Note = "Initial deposit",
				};
				event1.Activities.Add(new CashActivity()
				{
					Note = "Deposit",
					Amount = 10000M,
				});
				Account1.Events.Add(event1);

				Account2 = new Account()
				{
					Institution = "Second Bank Of Unit Testing",
					Name = "Roth IRA Account",
					Closed = false,
				};

				Account3 = new Account()
				{
					Institution = "Third Bank Of Unit Testing",
					Name = "Traditional IRA Account",
					Closed = true,
				};

				using var context = new FinanceModelContext(StaticDbFilename);

				// This creates the database when it does not exist.
				context.Database.Migrate();

				// Fill the database with our static data.

				// Create three Accounts.
				// SaveChanges after each one so that we are sure of their IDs.
				context.Accounts.Add(Account1);
				context.SaveChanges();
				context.Accounts.Add(Account2);
				context.SaveChanges();
				context.Accounts.Add(Account3);
				context.SaveChanges();


			}
		}
	}
}
