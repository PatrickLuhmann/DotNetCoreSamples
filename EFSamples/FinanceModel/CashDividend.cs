using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples.FinanceModel
{
	public class CashDividend
	{
		#region Schema
		public Int64 Id { get; set; }

		// Principal Entity
		public Int64 SecurityId { get; set; }
		public Security Security { get; set; }

		// Dependent Entity (1:N)
		public List<Event> Events { get; set; }

		public DateTime ExDivDate { get; set; }
		public DateTime PaymentDate { get; set; }
		public decimal PerShareAmount { get; set; }
		#endregion

		public CashDividend()
		{
			Events = new List<Event>();
		}

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is CashDividend theObj)
			{
				if (Id != theObj.Id ||
				// Check only ID of principal entity to avoid recursion.
				SecurityId != theObj.SecurityId ||
				// Check Count here; whole list will be checked later.
				Events.Count != theObj.Events.Count ||
				// Only the Date matters; Time is "don't care".
				ExDivDate.Date != theObj.ExDivDate.Date ||
				// Only the Date matters; Time is "don't care".
				PaymentDate.Date != theObj.PaymentDate.Date ||
				PerShareAmount != theObj.PerShareAmount)
					return false;

				// Check the Events. Note that the two Lists
				// do not need to be in the same order.
				foreach (Event ev in Events)
				{
					Event target = theObj.Events.Find(e => e.Id == ev.Id);
					if (target == null || target != ev)
						return false;
				}
				return true;
			}
			return false;
		}

		public static bool operator ==(CashDividend teeter, CashDividend totter)
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

		public static bool operator !=(CashDividend teeter, CashDividend totter)
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
