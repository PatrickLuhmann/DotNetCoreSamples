using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples.FinanceModel
{
	public class LotAssignment
	{
		#region Schema
		public Int64 Id { get; set; }
		// Principal entity (1:N)
		public Int64 SharesOutActivityId { get; set; }
		public SharesOutActivity SharesOutActivity { get; set; }
		// Principal entity (1:N)
		public Int64 LotId { get; set; }
		public Lot Lot { get; set; }
		public decimal Quantity { get; set; }
		public decimal Proceeds { get; set; }
		// Can't rely on math for this due to possible rounding errors.
		// The app will figure this out.
		public decimal CostBasis { get; set; }
		#endregion

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is LotAssignment assignObj)
			{
				if (Id != assignObj.Id ||
					// Check only ID of principal entity to avoid recursion.
					SharesOutActivityId != assignObj.SharesOutActivityId ||
					// Check only ID of principal entity to avoid recursion.
					LotId != assignObj.LotId ||
					Quantity != assignObj.Quantity ||
					Proceeds != assignObj.Proceeds)
					return false;

				return true;
			}
			return false;
		}

		public static bool operator ==(LotAssignment teeter, LotAssignment totter)
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

		public static bool operator !=(LotAssignment teeter, LotAssignment totter)
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
