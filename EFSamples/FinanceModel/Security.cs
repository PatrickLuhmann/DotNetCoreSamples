using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples.FinanceModel
{
	public class Security
	{
		#region Schema
		public Int64 Id { get; set; }

		// Dependent entities (1:N)
		public List<Price> Prices { get; set; }

		// Dependent entities (1:N)
		public List<CashDividend> CashDividends { get; set; }

		// TODO: Implement StockSplit as a Dependent entity.
		//public List<StockSplit> StockSplits { get; set; }

		// Dependent entities (1:N)
		public List<QuarterlyReport> QuarterlyReports { get; set; }
		public string Name { get; set; }
		public string Symbol { get; set; }
		public bool Retired { get; set; }
		#endregion

		#region Public Methods
		public Price GetPrice(DateTime date)
		{
			Price price = Prices.Find(p => p.Date == date);
			return price;
		}
		#endregion

		public Security()
		{
			Prices = new List<Price>();
			CashDividends = new List<CashDividend>();
			QuarterlyReports = new List<QuarterlyReport>();
			Retired = false;
		}

		#region Equality
		public override bool Equals(object obj)
		{
			if (obj is Security theObj)
			{
				if (Id != theObj.Id ||
				// Check Count here; whole list will be checked later.
				Prices.Count != theObj.Prices.Count ||
				// Check Count here; whole list will be checked later.
				CashDividends.Count != theObj.CashDividends.Count ||
				// Check Count here; whole list will be checked later.
				QuarterlyReports.Count != theObj.QuarterlyReports.Count ||
				Name != theObj.Name ||
				Symbol != theObj.Symbol ||
				Retired != theObj.Retired)
					return false;

				// Check the Prices. Note that the two Lists
				// do not need to be in the same order.
				foreach (Price price in Prices)
				{
					Price target = theObj.Prices.Find(p => p.Id == price.Id);
					if (target == null || target != price)
						return false;
				}

				// Check the Dividends. Note that the two Lists
				// do not need to be in the same order.
				foreach (CashDividend div in CashDividends)
				{
					CashDividend target = theObj.CashDividends.Find(d => d.Id == div.Id);
					if (target == null || target != div)
						return false;
				}

				// Check the QuarterlyReports. Note that the two Lists
				// do not need to be in the same order.
				foreach (QuarterlyReport report in QuarterlyReports)
				{
					QuarterlyReport target = theObj.QuarterlyReports
						.Find(qr => qr.Id == report.Id);
					if (target == null || target != report)
						return false;
				}
				return true;
			}
			return false;
		}

		public static bool operator ==(Security teeter, Security totter)
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

		public static bool operator !=(Security teeter, Security totter)
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
