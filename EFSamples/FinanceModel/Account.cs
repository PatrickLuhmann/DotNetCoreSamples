using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFSamples.FinanceModel
{
	public class Account
	{
		#region Schema
		public Int64 Id { get; set; }

		// Dependent entities (1:N)
		public List<Event> Events { get; set; }

		public string Institution { get; set; }
		public string Name { get; set; }
		public bool Closed { get; set; }
		#endregion

		#region Unmapped Properties
		// TODO: Can we remove Account.Cash?
		[NotMapped]
		public decimal Cash { get; set; }

		// TODO: Move this to a VM? Just keep deriving it?
		//[NotMapped]
		//public List<Position> Positions { get; set; }
		#endregion

		public decimal GetCashAmount()
		{
			decimal amount = 0;
			foreach (Event ev in Events)
			{
				foreach (Activity act in ev.Activities)
				{
					// The Amount property is positive or negative
					// depending on whether cash is being added or
					// removed from the account. Thus there is no
					// need to check the Type of the Event.
					if (act is CashActivity cash)
						amount += cash.Amount;
				}
			}
			return amount;
		}

		public Account()
		{
			Id = 0;
			Cash = 0.0M;
			Closed = false;
			Events = new List<Event>();
			//Positions = new List<Position>();
		}

		#region Equality
		public override bool Equals(object obj)
		{
			if (!(obj is Account))
				return false;

			Account theObj = (Account)obj;

			if (Id != theObj.Id ||
				// Check Count here; whole list will be checked later.
				Events.Count != theObj.Events.Count ||
				// Check Count here; whole list will be checked later.
				//Positions.Count != theObj.Positions.Count ||
				Institution != theObj.Institution ||
				Name != theObj.Name ||
				Closed != theObj.Closed ||
				Cash != theObj.Cash)
				return false;


			// Check the Events. Note that the two Lists
			// do not need to be in the same order.
			foreach (Event ev in Events)
			{
				Event target = theObj.Events.Find(e => e.Id == ev.Id);
				if (target == null || target != ev)
					return false;
			}

#if false
			// The Positions are derived from the other
			// attributes, so it isn't critical that we
			// do a complete compare. Just having the same
			// ones (based on associated Security) should suffice.
			foreach (var pos in Positions)
			{
				Position target = theObj.Positions
					.Find(p => p.SecurityId == pos.SecurityId);
				if (target == null)
					return false;
			}
#endif

			return true;
		}

		public static bool operator ==(Account teeter, Account totter)
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

		public static bool operator !=(Account teeter, Account totter)
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
