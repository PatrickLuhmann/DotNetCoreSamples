using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WpfSamples.List_Sorting.Model
{
	public class LedgerItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public decimal Value { get; set; }
		public int Quantity { get; set; }
	}

	public class LedgerSorter : IComparer
	{
		private readonly string Field;
		private readonly bool Asc;

		public int Compare(object x, object y)
		{
			if (x is LedgerItem xx && y is LedgerItem yy)
			{
				switch (Field)
				{
					case "Id":
						if (xx.Id > yy.Id)
							return Asc ? 1 : -1;
						else if (xx.Id < yy.Id)
							return Asc ? -1 : 1;
						else
							return 0;
					case "Name":
						if (String.Compare(xx.Name, yy.Name) > 0)
							return Asc ? 1 : -1;
						else if (String.Compare(xx.Name, yy.Name) < 0)
							return Asc ? -1 : 1;
						else
							return 0;
					case "Date":
						if (xx.Date > yy.Date)
							return Asc ? 1 : -1;
						else if (xx.Date < yy.Date)
							return Asc ? -1 : 1;
						else
							return 0;
					case "Value":
						if (xx.Value > yy.Value)
							return Asc ? 1 : -1;
						else if (xx.Value < yy.Value)
							return Asc ? -1 : 1;
						else
							return 0;
					case "Quantity":
						if (xx.Quantity > yy.Quantity)
							return Asc ? 1 : -1;
						else if (xx.Quantity < yy.Quantity)
							return Asc ? -1 : 1;
						else
							return 0;
					default:
						throw new ArgumentException("Field not recognized.");
				}
			}

			throw new ArgumentException("This Compare requires two LedgerItem objects.");
		}

		public LedgerSorter(string field, bool asc)
		{
			Field = field;
			Asc = asc;
		}
	}
}
