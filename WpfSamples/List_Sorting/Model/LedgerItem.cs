using System;
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
}
