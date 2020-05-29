using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples.FinanceModel
{
	public class QuarterlyReport
	{
		#region Schema
		public Int64 Id { get; set; }
		// Principal entity (1:N)
		public Int64 SecurityId { get; set; }
		public Security Security { get; set; }
		public int Quarter { get; set; }
		/// <summary>
		/// The fiscal year. Note that this might not match the year of the
		/// first/fourth quarter if the fiscal year crosses a calendar year
		/// boundary.
		/// </summary>
		public int Year { get; set; }
		/// <summary>
		/// The last day of the quarter. Note that the year might not match
		/// Year, because some companies have weird fiscal years.
		/// </summary>
		public DateTime QuarterEndDate { get; set; }
		// Dependent entities (1:N)
		public List<FinancialResult> Results { get; set; }
		#endregion

		public QuarterlyReport()
		{
			Results = new List<FinancialResult>();
		}

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is QuarterlyReport theObj)
			{
				if (Id != theObj.Id ||
				// Check only ID of principal entity to avoid recursion.
				SecurityId != theObj.SecurityId ||
				Quarter != theObj.Quarter ||
				Year != theObj.Year ||
				// Only the Date matters; Time is "don't care".
				QuarterEndDate.Date != theObj.QuarterEndDate.Date ||
				Results.Count != theObj.Results.Count)
					return false;

				// Check the Results. Note that the two Lists
				// do not need to be in the same order.
				foreach (FinancialResult result in Results)
				{
					FinancialResult target = theObj.Results
						.Find(fr => fr.Id == result.Id);
					if (target == null || target != result)
						return false;
				}
				return true;
			}
			return false;
		}

		public static bool operator ==(QuarterlyReport teeter, QuarterlyReport totter)
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

		public static bool operator !=(QuarterlyReport teeter, QuarterlyReport totter)
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
