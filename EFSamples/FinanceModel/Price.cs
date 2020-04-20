using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples.FinanceModel
{
	public class Price
	{
		#region Schema
		public Int64 Id { get; set; }
		// Principal entity (1:N)
		public Int64 SecurityId { get; set; }
		public Security Security { get; set; }
		public DateTime Date { get; set; }
		public decimal Value { get; set; }
		public int SplitAdjustmentNumerator { get; set; } = 1;
		public int SplitAdjustmentDenominator { get; set; } = 1;
		#endregion

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is Price theObj)
			{
				if (Id != theObj.Id ||
				// Check only ID of principal entity to avoid recursion.
				SecurityId != theObj.SecurityId ||
				// Only the Date matters; Time is "don't care".
				Date.Date != theObj.Date.Date ||
				Value != theObj.Value ||
				SplitAdjustmentNumerator != theObj.SplitAdjustmentNumerator ||
				SplitAdjustmentDenominator != theObj.SplitAdjustmentDenominator)
					return false;

				return true;
			}
			return false;
		}

		public static bool operator ==(Price teeter, Price totter)
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

		public static bool operator !=(Price teeter, Price totter)
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
