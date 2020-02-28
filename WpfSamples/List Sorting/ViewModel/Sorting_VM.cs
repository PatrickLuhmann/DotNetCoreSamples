using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;

namespace WpfSamples.List_Sorting.ViewModel
{
	public class Sorting_VM : INotifyPropertyChanged
	{
		public string Salutation { get; set; }

		public ObservableCollection<int> Integers { get; set; }

		public Sorting_VM()
		{
			Salutation = "Welcome to the list sorting sample!";

			Random rng = new Random();

			Integers = new ObservableCollection<int>();
			for (int i = 0; i < 100; i++)
				Integers.Add(rng.Next(0, 101));

			ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(Integers);
			lcv.CustomSort = new SortIntegersAscending();
		}
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion
	}

	public class SortIntegersAscending : IComparer
	{
		public int Compare(object x, object y)
		{
			if (x is int xx && y is int yy)
			{
				if (xx > yy)
					return 1;
				else
					return -1;
			}

			throw new ArgumentException("This Compare requires two integers.");
		}
	}
}
