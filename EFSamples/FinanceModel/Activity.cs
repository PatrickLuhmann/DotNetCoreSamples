using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFSamples.FinanceModel
{
	public class Activity
	{
		#region Schema
		public Int64 Id { get; set; }
		// Principal entity (1:N)
		public Int64 EventId { get; set; }
		public Event Event { get; set; }
		public string Note { get; set; }
		#endregion

		// Date can only be set via the parent Event.
		[NotMapped]
		public DateTime Date
		{
			get => Event.Date;
		}

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is Activity theObj)
			{
				if (Id != theObj.Id ||
					// Check only ID of principal entity to avoid recursion.
					EventId != theObj.EventId ||
					// Only the Date matters; Time is "don't care".
					Date.Date != theObj.Date.Date ||
					Note != theObj.Note)
					return false;
				return true;
			}
			return false;
		}

		public static bool operator ==(Activity teeter, Activity totter)
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

		public static bool operator !=(Activity teeter, Activity totter)
		{
			return !(teeter == totter);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}

	public class CashActivity : Activity
	{
		#region Schema
		/// <summary>
		/// Positive means "cash in", negative means "cash out".
		/// </summary>
		public decimal Amount { get; set; }
		#endregion

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is CashActivity theObj)
			{
				if (Amount != theObj.Amount ||
					!base.Equals(obj))
					return false;
				return true;
			}
			return false;
		}

		public static bool operator ==(CashActivity teeter, CashActivity totter)
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

		public static bool operator !=(CashActivity teeter, CashActivity totter)
		{
			return !(teeter == totter);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}

	public class SharesActivity : Activity
	{
		#region Schema
		public decimal Quantity { get; set; }

		// This is not a formal relationship, just a simple reference.
		public Int64 SecurityId { get; set; }
		#endregion

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is SharesActivity theObj)
			{
				if (Quantity != theObj.Quantity ||
					SecurityId != theObj.SecurityId ||
					!base.Equals(obj))
					return false;
				return true;
			}
			return false;
		}

		public static bool operator ==(SharesActivity teeter, SharesActivity totter)
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

		public static bool operator !=(SharesActivity teeter, SharesActivity totter)
		{
			return !(teeter == totter);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}

	public class SharesInActivity : SharesActivity
	{
		#region Schema
		public decimal CostBasis { get; set; }
		// Dependent entity (1:1)
		public Lot Lot { get; set; }
		#endregion

		public SharesInActivity()
		{
			Lot = new Lot();
		}

		public SharesInActivity(Int64 secId, decimal qty, decimal basis) : this()
		{
			SecurityId = secId;
			Quantity = qty;
			CostBasis = basis;
		}

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is SharesInActivity theObj)
			{
				if (CostBasis != theObj.CostBasis||
					Lot != theObj.Lot ||
					!base.Equals(obj))
					return false;
				return true;
			}
			return false;
		}

		public static bool operator ==(SharesInActivity teeter, SharesInActivity totter)
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

		public static bool operator !=(SharesInActivity teeter, SharesInActivity totter)
		{
			return !(teeter == totter);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}

	public class SharesOutActivity : SharesActivity
	{
		#region Schema
		public decimal Proceeds { get; set; }
		// A SharesOutActivity creates a collection of LotAssignments.
		public List<LotAssignment> LotAssignments { get; set; }
		#endregion

		public SharesOutActivity()
		{
			LotAssignments = new List<LotAssignment>();
		}

		/// <summary>
		/// Creates the object, but does not create LotAssignment objects.
		/// </summary>
		/// <param name="date"></param>
		/// <param name="secId"></param>
		/// <param name="qty"></param>
		/// <param name="basis"></param>
		public SharesOutActivity(Int64 secId, decimal qty) : this()
		{
			SecurityId = secId;
			Quantity = qty;
		}

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is SharesOutActivity theObj)
			{
				if (Proceeds != theObj.Proceeds ||
					LotAssignments.Count != theObj.LotAssignments.Count ||
					!base.Equals(obj))
					return false;

				// Check the LotAssignments. Note that the two Lists
				// do not need to be in the same order.
				foreach (LotAssignment assign in LotAssignments)
				{
					LotAssignment target = theObj.LotAssignments.Find(la => la.Id == assign.Id);
					if (target == null || target != assign)
						return false;
				}
				return true;
			}
			return false;
		}

		public static bool operator ==(SharesOutActivity teeter, SharesOutActivity totter)
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

		public static bool operator !=(SharesOutActivity teeter, SharesOutActivity totter)
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
