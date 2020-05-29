using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples.FinanceModel
{
	public enum ResultType
	{
		NotValid = 0,
		DilutedEPS,
		DilutedSharesOutstanding,
		NetCashFromOperations,
		PropertyPlantAndEquipment,
		LongTermDebt,
	}

	public class FinancialResult
	{
		#region Schema
		public Int64 Id { get; set; }
		// Principal entity (1:N)
		public Int64 QuarterlyReportId { get; set; }
		public QuarterlyReport QuarterlyReport { get; set; }
		/// <summary>
		/// The type of result. EX: "Diluted EPS", "Net Cash Provided By Operations"
		/// </summary>
		public ResultType Type { get; set; }
		public decimal? Value { get; set; }
		/// <summary>
		/// The number of results Value includes. Most results will be 1,
		/// because they are complete in themselves. Other types, like for FCF
		/// and sometimes EPS, will use 1-4 based on the quarter.
		/// </summary>
		public int Cumulative { get; set; }
		#endregion

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is FinancialResult theObj)
			{
				if (Id != theObj.Id ||
					// Check only ID of principal entity to avoid recursion.
					QuarterlyReportId != theObj.QuarterlyReportId ||
					Type != theObj.Type ||
					Value != theObj.Value ||
					Cumulative != theObj.Cumulative)
					return false;

				return true;
			}

			return false;
		}

		public static bool operator ==(FinancialResult teeter, FinancialResult totter)
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

		public static bool operator !=(FinancialResult teeter, FinancialResult totter)
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
