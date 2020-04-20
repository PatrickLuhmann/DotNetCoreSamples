using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFSamples.FinanceModel
{
	public class Lot
	{
		#region Schema
		public Int64 Id { get; set; }
		// Principal entity (1:1)
		public Int64 SharesInActivityId { get; set; }
		public SharesInActivity SharesInActivity { get; set; }
		// Dependent entities (1:N)
		public List<LotAssignment> LotAssignments { get; set; }
		#endregion

		public decimal GetQuantityRemaining()
		{
			decimal sum = SharesInActivity.Quantity;
			foreach (var assign in LotAssignments)
				sum -= assign.Quantity;
			return sum;
		}

		public decimal GetCostBasisRemaining()
		{
			decimal cbr = SharesInActivity.CostBasis;
			foreach (var assign in LotAssignments)
				cbr -= assign.CostBasis;
			return cbr;
		}

		public Lot()
		{
			LotAssignments = new List<LotAssignment>();
		}

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is Lot lotObj)
			{
				if (Id != lotObj.Id ||
					// Check only ID of principal entity to avoid recursion.
					SharesInActivityId != lotObj.SharesInActivityId ||
					// Check Count here; whole list will be checked later.
					LotAssignments.Count != lotObj.LotAssignments.Count)
					return false;

				foreach (LotAssignment assign in LotAssignments)
				{
					LotAssignment target = lotObj.LotAssignments.Find(la => la.Id == assign.Id);
					if (target == null || target != assign)
						return false;
				}

				return true;
			}
			return false;
		}

		public static bool operator ==(Lot teeter, Lot totter)
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

		public static bool operator !=(Lot teeter, Lot totter)
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
