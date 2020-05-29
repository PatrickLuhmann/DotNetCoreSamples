using EFSamples.FinanceModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleSamples.Finance_Sample
{
	public class FinanceLogic
	{
		static public Account GetAccount(Int64 id)
		{
			using (var context = new FinanceModelContext())
			{
				Account acct = context.Accounts.Find(id);
				if (acct == null)
					return null;

				// Fetch the Account's collections from the database.
				context.Entry(acct).Collection(a => a.Events).Load();
				// We need to manually walk the dependency graph.
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

		static public List<Account> GetAllAccounts(bool openOnly = false)
		{
			List<Account> accounts;
			using (var context = new FinanceModelContext())
			{
				if (openOnly)
					accounts = context.Accounts.Where(a => a.Closed == false).ToList();
				else
					accounts = context.Accounts.ToList();

				for (int i = 0; i < accounts.Count; i++)
					accounts[i] = GetAccount(accounts[i].Id);
			}
			return accounts;
		}
	}
}
