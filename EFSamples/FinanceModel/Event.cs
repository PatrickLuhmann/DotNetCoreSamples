using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFSamples.FinanceModel
{
	public class Event
	{
		#region Schema
		public Int64 Id { get; set; }
		
		// Principal entity (1:N)
		public Int64 AccountId { get; set; }
		public Account Account { get; set; }

		// Principal entity (1:N)
		// Optional: Not all Events are associated with a Dividend.
		public Int64? DividendId { get; set; }
		public CashDividend Dividend { get; set; }

		// Dependent entities (1:N)
		public List<Activity> Activities { get; set; }
		public DateTime Date { get; set; }
		public string Type { get; set; }
		public string Note { get; set; }
		#endregion

		public DateTime GetDate()
		{
			if (Activities[0] == null)
				return new DateTime().Date;
			else
				return Activities[0].Date;
		}

		public Event()
		{
			Activities = new List<Activity>();
			Type = "Unknown";
			Note = "Unknown";
		}

		public Event(string type) : this()
		{
			Type = type;
		}

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is Event theObj)
			{
				if (Id != theObj.Id ||
					// Check only ID of principal entity to avoid recursion.
					AccountId != theObj.AccountId ||
					// Check only ID of principal entity to avoid recursion.
					DividendId != theObj.DividendId ||
					// Check Count here; whole list will be checked later.
					Activities.Count != theObj.Activities.Count ||
					Type != theObj.Type ||
					Note != theObj.Note)
					return false;

				// Check the Activities. Note that the two Lists
				// do not need to be in the same order.
				foreach (Activity act in Activities)
				{
					Activity target = theObj.Activities.Find(a => a.Id == act.Id);
					if (target == null || target != act)
						return false;
				}
				return true;
			}
			return false;
		}

		public static bool operator ==(Event teeter, Event totter)
		{
			// NOTE: Can't check for null using == because that would be recursive.
			if (Object.ReferenceEquals(teeter, null))
			{
				if (Object.ReferenceEquals(totter, null))
					return true; // null == null, so technically true.
				else
					return false;
			}

			return teeter.Equals(totter);
		}

		public static bool operator !=(Event teeter, Event totter)
		{
			return !(teeter == totter);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}
}
