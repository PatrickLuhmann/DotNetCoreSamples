using EFSamples.FinanceModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleSamples.Finance_Sample
{
	class FinanceSample : IConsoleSample
	{
		public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(b => { b.AddConsole(); });
		private string DbFilename = "finance_sample.db";
		private FinanceLogic FinLogic;

		public void Run()
		{
			Console.WriteLine("Welcome to the Finance Sample.");
			Console.WriteLine();

			// Create a logger for EFCore to use.
			//FinanceModelContext.Logger = MyLoggerFactory;

			// Create the FinanceLogic object.
			FinLogic = new FinanceLogic(DbFilename);

			// Update the database, if necessary.
			using (var context = new FinanceModelContext())
			{
				context.Database.Migrate();
			}

			bool quit = false;

			while (!quit)
			{
				Console.WriteLine("Commands");
				Console.WriteLine("1. Go to the Accounts Menu");
				Console.WriteLine("2. Go to the Securities Menu");
				Console.WriteLine("LE: List all events");
				Console.WriteLine("LA. List all activities");
				Console.WriteLine("5. Activity Test");
				Console.WriteLine("Q. Quit");


				Console.Write("> ");
				string userInput = Console.ReadLine();

				switch (userInput.ToUpper())
				{
					case "1":
						AccountsMenu();
						break;
					case "2":
						SecuritiesMenu();
						break;
					case "LA":
						ListAllActivities();
						break;
					case "5":
						ActivityTest();
						break;
					case "Q":
						quit = true;
						break;
					case "LE":
						ListAllEvents();
						break;
					default:
						Console.WriteLine("ERROR: Command not recognized.");
						break;
				}
			}
		}

		private void AccountsMenu()
		{
			bool quit = false;

			while (!quit)
			{
				Console.WriteLine();
				Console.WriteLine("Accounts Menu");
				Console.WriteLine("=============");
				Console.WriteLine("1. List accounts");
				Console.WriteLine("2. Add a new account");
				Console.WriteLine("3. Delete an account");
				Console.WriteLine("4. Select Account");
				Console.WriteLine("Q. Quit");


				Console.Write("> ");
				string userInput = Console.ReadLine();

				int numChanged;
				switch (userInput.ToUpper())
				{
					case "1":
						ListAccounts();
						break;
					case "2":
						numChanged = AddAccount();
						Console.WriteLine($"  Number of records changed: {numChanged}.");
						break;
					case "3":
						numChanged = DeleteAccount();
						Console.WriteLine($"  Number of records changed: {numChanged}.");
						break;
					case "4":
						SelectAccount();
						break;
					case "Q":
						quit = true;
						break;
					default:
						Console.WriteLine("ERROR: Command not recognized.");
						break;
				}
			}
		}

		private int ListAccounts()
		{
			Console.WriteLine("Here are all of the accounts in the database:");
			int numAccts = -1;
			List<Account> accts = FinLogic.GetAllAccounts();
			foreach (Account acct in accts)
			{
				Console.WriteLine($"[{acct.Id:D4}] {acct.Name} - {acct.Institution} : "
					+ (acct.Closed ? "Closed " : "Open ")
					+ $"Num Events: {acct.Events.Count}");
			}
			return numAccts;
		}

		private int AddAccount()
		{
			int numChanged = -1;
			Console.Write("Enter the institution: ");
			string institution = Console.ReadLine();
			if (institution == "")
				return numChanged;
			Console.Write("Enter the name: ");
			string name = Console.ReadLine();
			if (name == "")
				return numChanged;
			Account acct = new Account()
			{
				Institution = institution,
				Name = name,
				Closed = false,
			};
			using (var context = new FinanceModelContext())
			{
				context.Accounts.Add(acct);
				numChanged = context.SaveChanges();
			}
			return numChanged;
		}

		private int DeleteAccount()
		{
			int numChanged = -1;
			Console.WriteLine("Enter the ID of the account to delete: ");
			string userInput = Console.ReadLine();
			Int64 id = Convert.ToInt64(userInput);
			using (var context = new FinanceModelContext())
			{
				// Get the Account.
				Account acct = context.Accounts.Find(id);
				if (acct == null)
				{
					Console.WriteLine($"ERROR: Account ID {id} not found.");
					return numChanged;
				}

				// Delete it.
				// Through experimentation, this does a cascade delete as expected;
				// you should double-check the migration code to make sure the
				// FKs are marked Cascade.
				// Strangely, this only reports a single record changed. I assume this
				// refers to the Account record. However, the Events and Activities are
				// also deleted. I don't know why they aren't included in the count.
				// If I manually load the Events, then I see a separate DELETE transactions
				// for each one. This seems very inefficient; this should be done on the server
				// side. It also makes me think that cascade delete is not really working
				// when Events are not loaded, since if the server was able to do the deletes
				// in the one case why wouldn't it be used in the other case?
				// I am not storing sensitive data so I will rely on cascade delete.
				//context.Entry(acct).Collection(a => a.Events).Load();

				context.Accounts.Remove(acct);
				numChanged = context.SaveChanges();
			}

			return numChanged;
		}

		private void SelectAccount()
		{
			Console.Write("Enter the ID of the account to select [0 to return]: ");
			string userInput = Console.ReadLine();
			if (userInput == "")
				return;
			Int64 id = Convert.ToInt64(userInput);
			if (id == 0)
				return;

			Account acct;
			acct = FinLogic.GetAccount(id);
			if (acct == null)
			{
				Console.WriteLine($"ERROR: Account ID {id} not found.");
				return;
			}

			Console.WriteLine("Selected Account");
			Console.WriteLine();
			Console.WriteLine($"[{acct.Id:D3}] {acct.Name} @ {acct.Institution}");
			Console.WriteLine("Status: " + (acct.Closed ? "Closed" : "Open"));
			Console.WriteLine($"Num Events [{acct.Events.Count:D3}]");
			Console.WriteLine("------------------");

			bool quit = false;
			do
			{
				Console.WriteLine();
				Console.WriteLine("Account Commands");
				Console.WriteLine("================");
				Console.WriteLine("LE. List events");
				Console.WriteLine("AE. Add a new event");
				Console.WriteLine("EE: Edit an existing event");
				Console.WriteLine("DE: Delete an existing event");
				Console.WriteLine("7. Edit account details");
				Console.WriteLine("Q. Quit this menu");

				Console.WriteLine();
				Console.Write("> ");
				userInput = Console.ReadLine();
				switch (userInput.ToUpper())
				{
					case "LE":
						ListEvents(acct);
						break;
					case "AE":
						AddEvent(acct);
						break;
					case "EE":
						EditEvent(acct);
						break;
					case "DE":
						DeleteEvent(acct);
						break;
					case "Q":
						quit = true;
						break;
					default:
						Console.WriteLine("ERROR: Command not recognized!");
						break;
				}
			} while (!quit);
		}
		private void ListEvents(Account account)
		{
			foreach (Event ev in account.Events)
			{
				Console.WriteLine($"[{ev.Id,3}] Acct: {ev.AccountId,3} Type: {ev.Type} Activities: {ev.Activities.Count,3} {ev.Note}");
				ListActivities(ev);
			}
		}
		private void AddEvent(Account account)
		{
			bool choosing = true;
			while (choosing)
			{
				Console.WriteLine();
				Console.WriteLine("What type of event are you recording?");
				Console.WriteLine("=====================================");
				Console.WriteLine("1.  Deposit");
				Console.WriteLine("2.  Withdrawal");
				Console.WriteLine("3.  Buy Security");
				Console.WriteLine("Q.  Quit this menu");
				Console.Write("> ");
				string userInput = Console.ReadLine();
				switch (userInput.ToUpper())
				{
					case "1":
						RecordCashOnlyEvent(account, "Deposit");
						choosing = false;
						break;
					case "2":
						RecordCashOnlyEvent(account, "Withdrawal");
						choosing = false;
						break;
					case "3":
						RecordSecurityEvent(account, "Buy");
						choosing = false;
						break;
					case "Q":
						choosing = false;
						break;
					default:
						Console.WriteLine("ERROR: Selection not valid!");
						break;
				}
			}
		}

		private void EditEvent(Account account)
		{

		}

		private void DeleteEvent(Account account)
		{
			Int64 id;
			bool choosing = true;
			while (choosing)
			{
				Console.WriteLine();
				Console.WriteLine("Enter the ID of the event you want to delete: ");
				string userInput = Console.ReadLine();
				if (userInput == "")
				{
					choosing = false;
					continue;
				}
				bool validNum = Int64.TryParse(userInput, out id);
				if (!validNum)
					Console.WriteLine("ERROR: Not an integer!");
				else if (id < 1)
					Console.WriteLine("ERROR: Not a valid record ID!");
				else
				{
					// Get the Event record.
					Event ev = account.Events.Find(e => e.Id == id);
					if (ev == null)
						Console.WriteLine("ERROR: Event not found in the account!");
					else
					{
						// This assumes that the Event can be deleted without consequence.
						// But some Events interact with other Events, which means that simply
						// removing the target Event will leave the database in an inconsistent
						// state.

						// TODO: For now, only allow the deletion of an Event if it has only one
						// Activity, and it is of type CashActivity.
						if (ev.Activities.Count != 1 ||
							!(ev.Activities[0] is CashActivity))
						{
							Console.WriteLine("ERROR: This type of Event cannot be deleted at this time!");
							continue;
						}

						using (var db = new FinanceModelContext())
						{
							// Tell the db that it knows the account.
							db.Attach(account);

							account.Events.Remove(ev);

							var num = db.SaveChanges();
							Console.WriteLine($"Number of records changed: {num}");

							choosing = false;
						}
					}
				}
			}
		}

		private void SecuritiesMenu()
		{
			bool quit = false;

			while (!quit)
			{
				Console.WriteLine();
				Console.WriteLine("Securities Menu");
				Console.WriteLine("=============");
				Console.WriteLine("1. List securities");
				Console.WriteLine("2. Add a new security");
				Console.WriteLine("3. Delete a security");
				Console.WriteLine("4. Select Security");
				Console.WriteLine("Q. Quit");


				Console.Write("> ");
				string userInput = Console.ReadLine();

				int numChanged;
				switch (userInput.ToUpper())
				{
					case "1":
						ListSecurities();
						break;
					case "2":
						numChanged = AddSecurity();
						Console.WriteLine($"  Number of records changed: {numChanged}.");
						break;
					case "3":
						numChanged = DeleteSecurity();
						Console.WriteLine($"  Number of records changed: {numChanged}.");
						break;
					case "4":
						SelectSecurity();
						break;
					case "Q":
						quit = true;
						break;
					default:
						Console.WriteLine("ERROR: Command not recognized.");
						break;
				}
			}
		}

		private int ListSecurities()
		{
			Console.WriteLine("Here are all of the securities in the database:");
			int numSecs = -1;
			using (var context = new FinanceModelContext())
			{
				List<Security> secs = context.Securities
					.Include(s => s.CashDividends)
					.ToList();
				numSecs = secs.Count;
				foreach (Security sec in secs)
				{
					Console.WriteLine($"[{sec.Id:D4}] {sec.Symbol} - {sec.Name} : "
						+ $"Num Cash Dividends: {sec.CashDividends.Count}");
				}
			}
			return numSecs;
		}

		private int AddSecurity()
		{
			int numChanged = -1;
			Console.Write("Enter the symbol: ");
			string symbol = Console.ReadLine();
			if (symbol == "")
				return numChanged;
			Console.Write("Enter the name: ");
			string name = Console.ReadLine();
			if (name == "")
				return numChanged;
			Security sec = new Security()
			{
				Symbol = symbol,
				Name = name,
			};
			using (var context = new FinanceModelContext())
			{
				context.Securities.Add(sec);
				numChanged = context.SaveChanges();
			}
			return numChanged;
		}

		private int DeleteSecurity()
		{
			int numChanged = -1;
			Console.WriteLine("Enter the ID of the security to delete: ");
			string userInput = Console.ReadLine();
			Int64 id = Convert.ToInt64(userInput);
			using (var context = new FinanceModelContext())
			{
				// Get the Security.
				Security sec = context.Securities.Find(id);
				if (sec == null)
				{
					Console.WriteLine($"ERROR: Security ID {id} not found.");
					return numChanged;
				}

				// Delete it.
				// Through experimentation, this does a cascade delete as expected;
				// you should double-check the migration code to make sure the
				// FKs are marked Cascade.
				context.Securities.Remove(sec);
				numChanged = context.SaveChanges();
			}

			return numChanged;
		}

		private void SelectSecurity()
		{
			Console.Write("Enter the ID of the security to select [0 to return]: ");
			string userInput = Console.ReadLine();
			if (userInput == "")
				return;
			Int64 id = Convert.ToInt64(userInput);
			if (id == 0)
				return;

			Security sec;
			using (var context = new FinanceModelContext())
			{
				// Get the Security from the database.
				sec = context.Securities.Find(id);
				if (sec == null)
				{
					Console.WriteLine($"ERROR: Security ID {id} not found.");
					return;
				}

				// Fetch the Security's collections from the database.

				// Don't fetch the Events yet.
				context.Entry(sec).Collection(s => s.CashDividends).Load();

				// TODO: Add Prices here, when implemented.
				// TODO: Add QuarterlyReports here, when implemented.
			}

			Console.WriteLine("Selected Security");
			Console.WriteLine();
			Console.WriteLine($"[{sec.Id:D3}] {sec.Symbol} - {sec.Name}");
			Console.WriteLine($"Number of Cash Dividends: {sec.CashDividends.Count:D3}");
			Console.WriteLine("------------------");

			bool quit = false;
			do
			{
				Console.WriteLine();
				Console.WriteLine("Security Commands");
				Console.WriteLine("================");
				Console.WriteLine("LCD: List cash dividends");
				Console.WriteLine("ACD: Add a new cash dividend");
				Console.WriteLine("ECD: Edit an existing cash dividend");
				Console.WriteLine("DCD: Delete an existing cash dividend");
				Console.WriteLine("E. Edit security details");
				Console.WriteLine("Q. Quit this menu");

				Console.WriteLine();
				Console.Write("> ");
				userInput = Console.ReadLine();
				string[] tokens = userInput.Split();
				switch (tokens[0].ToUpper())
				{
					case "LCD":
						ListDividends(sec);
						break;
					case "ACD":
						AddCashDividend(sec);
						break;
					case "ECD":
						EditCashDividend(sec);
						break;
					case "DCD":
						if (tokens.Length != 2)
						{
							Console.WriteLine("ERROR: Inccorect number of parameters! This command takes one parameter.");
							break;
						}
						Int64 divId = Convert.ToInt64(tokens[1]);
						if (divId == 0)
						{
							Console.WriteLine("ERROR: The parameter must be an integer!");
							break;
						}
						DeleteCashDividend(sec, divId);
						break;
					case "E":
						EditSecurity(sec);
						break;
					case "Q":
						quit = true;
						break;
					default:
						Console.WriteLine("ERROR: Command not recognized!");
						break;
				}
			} while (!quit);
		}

		private void ListDividends(Security sec)
		{
			foreach (CashDividend div in sec.CashDividends)
			{
				Console.WriteLine($"[{div.Id,3}] Security: {div.SecurityId,3} Ex-Div Date: {div.ExDivDate} Payment Date: {div.PaymentDate} Cash Amount: {div.PerShareAmount:C2}");
			}
		}

		private int AddCashDividend(Security sec)
		{
			int numChanges = 0;
			string userInput;

			Console.Write("Enter the ex-dividend date of the dividend [blank to cancel]: ");
			userInput = Console.ReadLine();
			if (userInput == "")
				return 0;
			DateTime exdivDate = Convert.ToDateTime(userInput);

			Console.Write("Enter the payment date of the dividend [blank to cancel]: ");
			userInput = Console.ReadLine();
			if (userInput == "")
				return 0;
			DateTime paymentDate = Convert.ToDateTime(userInput);

			Console.Write("Enter the amount per share of the dividend: ");
			userInput = Console.ReadLine();
			if (userInput == "")
				return 0;
			decimal amount = Convert.ToDecimal(userInput);

			CashDividend cashDividend = new CashDividend()
			{
				ExDivDate = exdivDate,
				PaymentDate = paymentDate,
				PerShareAmount = amount,
			};
			sec.CashDividends.Add(cashDividend);

			// Find the accounts with a position in this security on the day before the ex-dividend date.
			List<Account> accts = FinLogic.GetAllAccounts();
			foreach (Account acct in accts)
			{
				Console.WriteLine($"[{acct.Id:D4}] {acct.Name} - {acct.Institution} : "
					+ (acct.Closed ? "Closed " : "Open "));

				// Go through the Events, looking for ShareActivities that are before the dividend.
				decimal qty = 0;
				foreach (Event ev in acct.Events)
				{
					foreach (Activity act in ev.Activities)
					{
						if (act is SharesInActivity shIn)
						{
							Console.WriteLine($"  SharesIn  {shIn.Date} : {shIn.Quantity} shares of Security[{shIn.SecurityId}], cost basis {shIn.CostBasis}");
							if (shIn.SecurityId == sec.Id && shIn.Date.Date < exdivDate.Date)
							{
								Console.WriteLine($"    Match! Add {shIn.Quantity} shares to the running total.");
								qty += shIn.Quantity;
							}
							else
							{
								Console.WriteLine($"    Not a match!");
							}
						}

						if (act is SharesOutActivity shOut)
						{
							Console.WriteLine($"  SharesOut {shOut.Date} : {shOut.Quantity} shares of Security[{shOut.SecurityId}], assigned to {shOut.LotAssignments.Count} lots");
							if (shOut.SecurityId == sec.Id && shOut.Date.Date < exdivDate.Date)
							{
								Console.WriteLine($"    Match! Subtract {shOut.Quantity} shares from the running total.");
								qty -= shOut.Quantity;
							}
							else
							{
								Console.WriteLine($"    Not a match!");
							}
						}
					}
				}

				// Was there a dividend paid to this account?
				if (qty > 0)
				{
					Console.WriteLine($"This account had {qty} shares of {sec.Symbol} before the ex-div date.");
					decimal divValue = qty * amount;
					Console.WriteLine($"This resulted in a dividend payment of {divValue:C2} on {paymentDate}.");

					// TODO: The Account needs an Event (CashActivity) to represent the Global Dividend.
					// What is the connection between CashDividend and this new Event?
					Event divEvent = CreateCashDividendEvent(acct, paymentDate, divValue);
					cashDividend.Events.Add(divEvent);
				}
			}

			using (var context = new FinanceModelContext())
			{
				foreach (Account acct in accts)
					context.Attach(acct);
				context.Attach(sec);
				numChanges = context.SaveChanges();
			}

			return numChanges;
		}

		private int EditCashDividend(Security sec)
		{
			int numChanges = 0;

			return numChanges;
		}

		private int DeleteCashDividend(Security sec, Int64 divId)
		{
			int numChanges = 0;

			// Find the CashDividend in the Security, if it exists.
			CashDividend div = sec.CashDividends.Find(cd => cd.Id == divId);
			if (div == null)
			{
				Console.WriteLine($"ERROR: CashDividend [{divId}] is not associated with this security!");
				return numChanges;
			}

			using (var context = new FinanceModelContext())
			{
				context.Attach(div);

				// Make sure we have the entire Dividend.
				// We already have Security.
				// Fetch Events.
				context.Entry(div).Collection(d => d.Events).Load();
				foreach (Event ev in div.Events)
				{
					// Fetch Account.
					context.Entry(ev).Reference(e => e.Account).Load();
					// We already have Dividend.
					// Fetch Activities. There should only be a single CashActivity,
					// which does not have any dependent entities to load.
					context.Entry(ev).Collection(e => e.Activities).Load();
				}


				// Remove the CashDividend from the Security.
				context.Attach(sec);
				sec.CashDividends.Remove(div);

				// Delete the Events associated with this CashDividend,
				// as well as from the Accounts that they are in.
				foreach (Event ev in div.Events)
				{
					ev.Account.Events.Remove(ev);
				}
				div.Events.Clear();

				numChanges = context.SaveChanges();
			}
			return numChanges;
		}

		private int EditSecurity(Security sec)
		{
			int numChanges = 0;

			return numChanges;
		}

		private void RecordCashOnlyEvent(Account account, string type)
		{
			// Validate type is one we support.
			if (type != "Deposit" &&
				type != "Withdrawal")
				return;

			string userInput;
			DateTime date;
			decimal amount;

			// Get the date.
			bool validDate;
			do
			{
				Console.WriteLine($"Enter the date of the {type}: ");
				userInput = Console.ReadLine();
				validDate = DateTime.TryParse(userInput, out date);
				if (!validDate)
					Console.WriteLine("ERROR: Not a valid date!");
			} while (!validDate);

			// Get the amount.
			bool validAmount;
			do
			{
				Console.WriteLine();
				Console.WriteLine($"Enter the amount of the {type}: ");
				userInput = Console.ReadLine();
				validAmount = Decimal.TryParse(userInput, out amount);
				if (!validAmount)
					Console.WriteLine("ERROR: Not a decimal number!");
				else if ((type == "Deposit") && amount < 0)
				{
					Console.WriteLine($"ERROR: The amount of a {type} must not be less than zero!");
					validAmount = false;
				}
				else if ((type == "Withdrawal") && amount > 0)
				{
					Console.WriteLine($"ERROR: The amount of a {type} must not be greater than zero!");
					validAmount = false;
				}
			} while (!validAmount);

			// Print summary of Event and ask for confirmation.
			Console.WriteLine($"Here is the {type} event you have specified:");
			Console.WriteLine($"Date:   {date}");
			Console.WriteLine($"Amount: {amount}");
			bool validResponse = false;
			bool recordEvent = false;
			do
			{
				Console.WriteLine($"Do you wish to record this {type} event to the database? [Y/n]");
				userInput = Console.ReadLine();
				switch (userInput.ToUpper())
				{
					case "":
					case "Y":
						recordEvent = true;
						validResponse = true;
						break;
					case "N":
						validResponse = true;
						break;
				}
			} while (!validResponse);
			if (recordEvent)
			{
				Event ev = new Event(type) { Date = date };
				CashActivity ca = new CashActivity()
				{
					Amount = amount,
					Note = type,
				};

				ev.Activities.Add(ca);
				account.Events.Add(ev);

				using (var db = new FinanceModelContext())
				{
					// Tell the db that it knows the account.
					db.Attach(account);

					// There is no need to add to the Account's collection
					// within the scope of the context. The change tracker
					// will see that a new element has been added.
					//account.Events.Add(ev);

					var num = db.SaveChanges();
					Console.WriteLine($"Number of records changed: {num}");
				}
			}
		}

		private void RecordSecurityEvent(Account account, string type)
		{
			// Validate type is one we support.
			if (type != "Buy")
				return;

			string userInput;
			bool validInput = true;
			DateTime date;
			decimal amount;
			decimal qty;
			Int64 secId;
			string secSymbol = "<none>";

			// Get the date.
			do
			{
				Console.WriteLine();
				Console.WriteLine($"Enter the date of the {type}: ");
				userInput = Console.ReadLine();
				validInput = DateTime.TryParse(userInput, out date);
				if (!validInput)
					Console.WriteLine("ERROR: Not a valid date!");
			} while (!validInput);

			// Get the cash change amount.
			do
			{
				Console.WriteLine();
				Console.WriteLine($"Enter the cash change amount of the {type}: ");
				userInput = Console.ReadLine();
				validInput = Decimal.TryParse(userInput, out amount);
				if (!validInput)
					Console.WriteLine("ERROR: Not a decimal number!");
				else if ((type == "Sell") && amount < 0)
				{
					Console.WriteLine($"ERROR: The amount of a {type} must not be less than zero!");
					validInput = false;
				}
				else if ((type == "Buy") && amount > 0)
				{
					Console.WriteLine($"ERROR: The amount of a {type} must not be greater than zero!");
					validInput = false;
				}
			} while (!validInput);

			// Get the security id.
			do
			{
				Console.WriteLine();
				Console.WriteLine($"Enter the ID of the security for the {type}: ");
				userInput = Console.ReadLine();
				validInput = Int64.TryParse(userInput, out secId);
				if (!validInput)
				{
					Console.WriteLine("ERROR: Not a record ID!");
					continue;
				}

				// Look up the Security object.
				Security sec = GetSecurityFromId(secId);
				if (sec == null)
				{
					Console.WriteLine("ERROR: No Security with that ID found in the database!");
					validInput = false;
					continue;
				}
				secSymbol = sec.Symbol;
			} while (!validInput);

			// Get the quantity.
			do
			{
				Console.WriteLine();
				Console.WriteLine($"Enter the number of shares of {secSymbol} for the {type}: ");
				userInput = Console.ReadLine();
				validInput = Decimal.TryParse(userInput, out qty);
				if (!validInput)
					Console.WriteLine("ERROR: Not a decimal number!");
				else if (qty < 0)
				{
					Console.WriteLine($"ERROR: The quantity of a {type} must not be less than zero!");
					validInput = false;
				}
			} while (!validInput);

			// Print summary of Event and ask for confirmation.
			Console.WriteLine($"Here is the {type} event you have specified:");
			Console.WriteLine($"Date:   {date}");
			Console.WriteLine($"Quantity: {qty}");
			Console.WriteLine($"Symbol: {secSymbol}");
			Console.WriteLine($"Cash Change Amount: {amount}");
			bool recordEvent = false;
			do
			{
				Console.WriteLine($"Do you wish to record this {type} event to the database? [Y/n]");
				userInput = Console.ReadLine();
				switch (userInput.ToUpper())
				{
					case "":
					case "Y":
						recordEvent = true;
						break;
					case "N":
						recordEvent = false;
						break;
					default:
						validInput = false;
						break;
				}
			} while (!validInput);
			if (recordEvent)
			{
				Event ev = new Event(type) { Date = date };

				CashActivity ca = new CashActivity()
				{
					Amount = amount,
					Note = type,
				};
				ev.Activities.Add(ca);

				SharesActivity sa;
				if (type == "Buy")
				{
					sa = new SharesInActivity(secId, qty, amount);
				}
				else
				{
					sa = new SharesOutActivity(secId, qty);
					//TODO: Create the LotAssignment objects.

				}
				ev.Activities.Add(sa);

				account.Events.Add(ev);

				using (var db = new FinanceModelContext())
				{
					// Tell the db that it knows the account.
					db.Attach(account);

					// There is no need to add to the Account's collection
					// within the scope of the context. The change tracker
					// will see that a new element has been added.
					//account.Events.Add(ev);

					var num = db.SaveChanges();
					Console.WriteLine($"Number of records changed: {num}");
				}
			}
		}

		private void ActivityTest()
		{
			Int64 acctId;
			using (var context = new FinanceModelContext())
			{
				int num;
				Random rng = new Random();

				// Create a new Security.
				Security sec = new Security()
				{
					Symbol = "XYZ",
					Name = "Xcellent Zebras " + rng.Next(10000).ToString(),
				};
				Console.WriteLine("Add a new Security.");
				context.Securities.Add(sec);
				num = context.SaveChanges();
				// Append the ID to the symbol so that we can differentiate them.
				sec.Symbol = sec.Symbol + "_" + sec.Id.ToString();
				num = context.SaveChanges();

				// Create a new Account object.
				Account acct = new Account()
				{
					Institution = "The Bank Of Activity Testing",
					Name = Guid.NewGuid().ToString(),
					Closed = false,
				};
				Console.WriteLine("Add a new Account.");
				context.Accounts.Add(acct);

				//
				// Event: Add some cash to the account.
				//
				Console.WriteLine("Put some cash into the Account.");
				CreateDepositEvent(acct, DateTime.Now - TimeSpan.FromDays(100), 10000.00M);
				num = context.SaveChanges();
				Console.WriteLine($"  Number of records changed: {num}");
				acctId = acct.Id;

				//
				// Event: Buy 100 shares of XYZ.
				//
				Console.WriteLine("Buy 100 shares of XYZ.");
				Event firstBuyEvent = CreateBuyEvent(
					acct,
					DateTime.Now - TimeSpan.FromDays(90),
					sec.Id,
					100,
					100 * 75);
				num = context.SaveChanges();
				Console.WriteLine($"  Number of records changed: {num}");
				SharesInActivity firstSharesIn = (SharesInActivity)firstBuyEvent.Activities[1];
				CashActivity firstCashOut = (CashActivity)firstBuyEvent.Activities[0];

				//
				// Event: Sell 25 shares of XYZ.
				//
				Console.WriteLine("Sell 25 shares of XYZ.");
				Event firstSellEvent =
					CreateSellEvent(acct, DateTime.Now - TimeSpan.FromDays(50), sec.Id, 25, 25 * 37);
				SharesOutActivity sellSharesOut = (SharesOutActivity)firstSellEvent.Activities[1];
				CashActivity sellCashIn = (CashActivity)firstSellEvent.Activities[0];

				// Assign all of these shares to the first/only lot of XYZ.
				LotAssignment la = new LotAssignment()
				{
					Quantity = 25,
					Proceeds = 37 * 25,
				};
				firstSharesIn.Lot.LotAssignments.Add(la);

				sellSharesOut.LotAssignments.Add(la);
				num = context.SaveChanges();
				Console.WriteLine($"  Number of records changed: {num}");

				//
				// Event: Sell 30 (more) shares of XYZ.
				//
				Console.WriteLine("Sell 30 shares of XYZ.");
				Event secondSellEvent =
					CreateSellEvent(acct, DateTime.Now - TimeSpan.FromDays(40), sec.Id, 30, 30 * 11);
				sellSharesOut = (SharesOutActivity)secondSellEvent.Activities[1];
				sellCashIn = (CashActivity)secondSellEvent.Activities[0];

				// Assign all of these shares to the first/only lot of XYZ.
				la = new LotAssignment()
				{
					Quantity = sellSharesOut.Quantity,
					Proceeds = 11 * sellSharesOut.Quantity,
				};
				firstSharesIn.Lot.LotAssignments.Add(la);

				sellSharesOut.LotAssignments.Add(la);
				num = context.SaveChanges();
				Console.WriteLine($"  Number of records changed: {num}");

				//
				// Event: Buy 500 (more) shares of XYZ.
				//
				var secondBuyEvent = CreateBuyEvent(
					acct,
					DateTime.Now - TimeSpan.FromDays(30),
					sec.Id,
					500,
					500 * 6);
				num = context.SaveChanges();
				Console.WriteLine($"  Number of records changed: {num}");
				SharesInActivity secondSharesIn = (SharesInActivity)secondBuyEvent.Activities[1];
				CashActivity secondCashOut = (CashActivity)secondBuyEvent.Activities[0];

				//
				// Event: Sell 155 (more) shares of XYZ.
				//
				decimal numShares = 155;
				decimal sharePrice = 17.50M;
				decimal totalProceeds = numShares * sharePrice;
				Console.WriteLine($"Sell {numShares} shares of XYZ.");
				Event thirdSellEvent =
					CreateSellEvent(acct, DateTime.Now - TimeSpan.FromDays(25), sec.Id, numShares, totalProceeds);
				sellSharesOut = (SharesOutActivity)thirdSellEvent.Activities[1];
				sellCashIn = (CashActivity)thirdSellEvent.Activities[0];

				// Assign these shares such that it uses up the first lot then
				// puts the excess in the second lot.
				decimal firstLotShares = firstSharesIn.Lot.GetQuantityRemaining();
				decimal secondLotShares = numShares - firstLotShares;
				// First Lot
				la = new LotAssignment()
				{
					Quantity = firstLotShares,
					Proceeds = sharePrice * firstLotShares,
				};
				firstSharesIn.Lot.LotAssignments.Add(la);
				sellSharesOut.LotAssignments.Add(la);
				// Second Lot
				la = new LotAssignment()
				{
					Quantity = secondLotShares,
					Proceeds = totalProceeds - (sharePrice * firstLotShares),
				};
				secondSharesIn.Lot.LotAssignments.Add(la);
				sellSharesOut.LotAssignments.Add(la);
				num = context.SaveChanges();
				Console.WriteLine($"  Number of records changed: {num}");

				//
				// Event: Buy 1000 shares of XYZ.
				//
				var thirdBuyEvent = CreateBuyEvent(acct, DateTime.Now - TimeSpan.FromDays(20), sec.Id, 1000, 1000 * 4.40M);
				num = context.SaveChanges();
				Console.WriteLine($"  Number of records changed: {num}");
			}

			// Print out the contents of the database with respect to the Account we created.
			using (var context = new FinanceModelContext())
			{
				Account acct = context.Accounts.Find(acctId);
				// Need to load the Events. This is "shallow" so Activities will be loaded later.
				context.Entry(acct).Collection(a => a.Events).Load();
				Console.WriteLine($"Our Account: {acct.Name} @ {acct.Institution}");
				Console.WriteLine($"  Number of Events: {acct.Events.Count}.");

				Console.WriteLine("Events");
				Console.WriteLine("==========");
				foreach (Event ev in acct.Events)
				{
					// Load the Activities and their dependencies.
					context.Entry(ev).Collection(e => e.Activities).Load();
					// Need to manually walk the dependency graph for Events
					// because there is no way to .Include entities that are
					// subclasses when some of those subclasses would require
					// their own .ThenInclude (as far as I know).
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
					ListActivities(ev);
				}
			}

			using (var context = new FinanceModelContext())
			{
				Console.WriteLine("Security Lots");
				Console.WriteLine("=============");
				// With the current schema, we do not have a Position table. Therefore we need
				// to get to the Lots via the SharesInActivities.
				List<SharesInActivity> actSharesIn =
					//context.Activities
					context.SharesInActivities
					.Include(act => act.Event)
					.Where(act => act.Event.AccountId == acctId)
					//.OfType<SharesInActivity>()
					.Include(sia => sia.Lot)
						.ThenInclude(l => l.LotAssignments)
					.ToList();
				// For now we will just print the raw Lots. A better way would be to go through the collection
				// and figure out the positions so that the Lots could be displayed in per-security groups.
				foreach (SharesInActivity act in actSharesIn)
				{
					Lot lot = act.Lot;
					Console.WriteLine($"[{lot.Id:D3}] " +
						$"Quantity: {lot.GetQuantityRemaining()} of {lot.SharesInActivity.Quantity} - " +
						$"Cost Basis: {lot.GetCostBasisRemaining():C2} of {lot.SharesInActivity.CostBasis} - " +
						$"NumLotAssigns: {lot.LotAssignments.Count}");
					if (lot.LotAssignments.Count > 0)
					{
						foreach (LotAssignment assign in lot.LotAssignments)
						{
							Console.WriteLine($"     [{assign.Id:D3}] Qty: {assign.Quantity} - Proceeds: {assign.Proceeds:C2} From SharesOut: {assign.SharesOutActivityId}");
						}
					}
				}
			}

		}

		/// <summary>
		/// Record a deposit event.
		/// </summary>
		/// <param name="account"></param>
		/// <param name="date"></param>
		/// <param name="amount">The amount of money to deposit; must not be negative.</param>
		/// <returns></returns>
		public Event CreateDepositEvent(Account account, DateTime date, decimal amount)
		{
			// A deposit, by definition, is additive to the cash balance of the account.
			// Therefore, the amount cannot be negative.
			// Allow a deposit of 0 just in case there is some weird use case for it.
			if (amount < 0)
				throw new ArgumentOutOfRangeException();

			// Create the Event.
			Event ev = new Event() { Date = date, Type = "Deposit", Note = "CreateDepositEvent()" };

			// Create the CashInActivity.
			Console.WriteLine("CDE: Add a CashInActivity.");
			CashActivity cashIn = new CashActivity()
			{
				Amount = amount,
			};
			ev.Activities.Add(cashIn);

			account.Events.Add(ev);

			return ev;
		}

		public Event CreateBuyEvent(Account account, DateTime date, Int64 secId, decimal qty, decimal basis)
		{
			// Create the Event.
			Event ev = new Event() { Date = date, Type = "Buy", Note = "CreateBuyEvent()" };

			// Create the CashActivity.
			Console.WriteLine("CBE: Add a CashActivity.");
			CashActivity cashOut = new CashActivity()
			{
				Amount = -1 * basis, // amount is negative for CashOut
			};
			ev.Activities.Add(cashOut);

			// Create the SharesInActivity.
			Console.WriteLine("CBE: Add a SharesInActivity.");
			SharesInActivity sharesIn = new SharesInActivity(secId, qty, basis);
			ev.Activities.Add(sharesIn);

			account.Events.Add(ev);

			return ev;
		}

		/// <summary>
		/// The first Activity is Cash; the second Activity is SharesOut.
		/// </summary>
		/// <param name="account"></param>
		/// <param name="date"></param>
		/// <param name="secId"></param>
		/// <param name="qty">Must be positive.</param>
		/// <param name="proceeds">Must be positive.</param>
		/// <returns></returns>
		public Event CreateSellEvent(Account account, DateTime date, Int64 secId, decimal qty, decimal proceeds)
		{
			// Create the Event.
			Event ev = new Event() { Date = date, Type = "Sell", Note = "CreateSellEvent()" };

			// Create the CashActivity.
			Console.WriteLine("CSE: Add a CashActivity.");
			CashActivity cashIn = new CashActivity()
			{
				Amount = proceeds,
			};
			ev.Activities.Add(cashIn);

			// Create the SharesOutActivity.
			Console.WriteLine("CSE: Add a SharesOutActivity.");
			SharesOutActivity sharesOut = new SharesOutActivity(secId, qty);
			ev.Activities.Add(sharesOut);

			account.Events.Add(ev);

			return ev;
		}

		/// <summary>
		/// Record a dividend event.
		/// </summary>
		/// <param name="account"></param>
		/// <param name="date"></param>
		/// <param name="amount">The amount of the dividend; must not be negative.</param>
		/// <returns></returns>
		public Event CreateCashDividendEvent(Account account, DateTime date, decimal amount)
		{
			// A dividend, by definition, is additive to the cash balance of the account.
			// Therefore, the amount cannot be negative.
			if (amount <= 0)
				throw new ArgumentOutOfRangeException();

			// Create the Event.
			Event ev = new Event() { Date = date, Type = "CashDividend", Note = "CreateCashDividendEvent" };

			// Create the CashInActivity.
			Console.WriteLine("CCDE: Add a CashInActivity.");
			CashActivity cashIn = new CashActivity()
			{
				Amount = amount,
			};
			ev.Activities.Add(cashIn);

			account.Events.Add(ev);

			return ev;
		}

		private void ListAllEvents()
		{
			List<Event> events;
			using (var db = new FinanceModelContext())
			{
				// Grab all of the dependent entities before grabbing the Events.
				// Due to the use of subclasses, we can't use .Include for everything.
				List<Activity> activities = db.Activities.ToList();
				List<Lot> lots = db.Lots.ToList();
				List<LotAssignment> assignments = db.LotAssignments.ToList();
				events = db.Events.ToList();
			}

			Console.WriteLine($"Events ({events.Count,3} objects)");
			Console.WriteLine($"====================");
			foreach (Event ev in events)
			{
				Console.WriteLine($"[{ev.Id,3}] Acct: {ev.AccountId,3} Date: {ev.Date:yyyy-MM-dd} Type: {ev.Type,7} Activities: {ev.Activities.Count,3} {ev.Note}");
			}
		}

		/// <summary>
		/// Prints the activities. Assumes that all dependents are already loaded.
		/// </summary>
		/// <param name="ev"></param>
		private void ListActivities(Event ev)
		{
			foreach (Activity act in ev.Activities)
			{
				Console.Write($"[{act.EventId:D3} : {act.Id:D3}] Event: {ev.Type,7} {act.Date:yyyy-MM-dd}  ");
				if (act is CashActivity cashAct)
				{
					Console.WriteLine($"CashChange {cashAct.Amount:C2}");
				}
				else if (act is SharesInActivity siAct)
				{
					//context.Entry(siAct).Reference(a => a.Lot).Load();
					//Security sec = context.Securities.Find(siAct.SecurityId);
					Security sec = GetSecurityFromId(siAct.SecurityId);
					Console.WriteLine($"SharesIn   {siAct.Quantity} shares of {sec.Symbol}, cost basis {siAct.CostBasis}, lot {siAct.Lot.Id:D3}");
				}
				else if (act is SharesOutActivity soAct)
				{
					//Security sec = context.Securities.Find(soAct.SecurityId);
					Security sec = GetSecurityFromId(soAct.SecurityId);
					//context.Entry(soAct).Collection(soa => soa.LotAssignments).Load();
					Console.WriteLine($"SharesOut {soAct.Quantity} shares of {sec.Symbol}, assigned to {soAct.LotAssignments.Count} lots");
				}
				else
				{
					Console.WriteLine("ERROR: Activity type not recognized!");
				}
			}
		}
		private void ListAllActivities()
		{
			List<Activity> acts;
			using (var db = new FinanceModelContext())
			{
				acts = db.Activities
					.Include(act => act.Event)
					.ToList();
			}

			Console.WriteLine("All Activities");
			Console.WriteLine("==============");
			foreach (Activity act in acts)
			{
				Console.Write($"[{act.Id:D3}] {act.Date}  ");
				Console.WriteLine($"for Event [{act.EventId:D3}] ");
				if (act is CashActivity cashAct)
				{
					Console.WriteLine($"     Cash    {cashAct.Amount:C2}");
				}
				else if (act is SharesInActivity siAct)
				{
					Security sec = GetSecurityFromId(siAct.SecurityId);
					Console.WriteLine($"     SharesIn  {siAct.Quantity} shares of {sec.Symbol}[{sec.Id}], cost basis {siAct.CostBasis}");
				}
				else if (act is SharesOutActivity soAct)
				{
					Security sec = GetSecurityFromId(soAct.SecurityId);
					//context.Entry(soAct).Collection(soa => soa.LotAssignments).Load();
					Console.WriteLine($"     SharesOut {soAct.Quantity} shares of {sec.Symbol}[{sec.Id}], assigned to {soAct.LotAssignments.Count} lots");
				}
				else
				{
					Console.WriteLine("ERROR: Activity type not recognized!");
				}
			}
		}

		private Security GetSecurityFromId(Int64 secId, FinanceModelContext context = null)
		{
			Security sec;
			if (context != null)
				sec = context.Securities.Find(secId);
			else
			{
				using (var db = new FinanceModelContext())
				{
					sec = db.Securities.Find(secId);
				}
			}
			return sec;
		}
	}
}
