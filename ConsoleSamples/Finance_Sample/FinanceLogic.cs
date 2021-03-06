﻿using EFSamples.FinanceModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleSamples.Finance_Sample
{
	public class FinanceLogic
	{
		private readonly string DbFilename;

		public Account GetAccount(Int64 id)
		{
			using (var context = new FinanceModelContext(DbFilename))
			{
				Account acct = context.Accounts
					.Where(a => a.Id == id)
					.Include(a => a.Events)
						.ThenInclude(ev => ev.Dividend)
						// TODO: Do we get the Security here?
						// If so, do we also get its Prices and QuarterlyReports?
						// Where does it end? If we are getting everything then
						// why not just load the entire database into memory at the beginning?
					.Single();
				if (acct == null)
					return null;

				// We need to manually walk the Activities dependency graph
				// because they are composed of subclasses, which cannot be
				// fetched with a single .Include statement.
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

				return acct;
			}
		}

		/// <summary>
		/// Gets the Account objects from the database.
		/// This is a "shallow" fetch, in that the dependent entites are not retrieved.
		/// </summary>
		/// <param name="openOnly">Determines if closed accounts should be included.</param>
		/// <returns></returns>
		public List<Account> GetAllAccounts(bool openOnly = false)
		{
			List<Account> accounts;
			using (var context = new FinanceModelContext(DbFilename))
			{
				if (openOnly)
					accounts = context.Accounts.Where(a => a.Closed == false).ToList();
				else
					accounts = context.Accounts.ToList();
			}
			return accounts;
		}

		public FinanceLogic(string filename)
		{
			DbFilename = filename;
		}
	}
}
