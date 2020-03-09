using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using WpfSamples.List_Sorting.Model;

namespace WpfSamples.List_Sorting.ViewModel
{
	public class Sorting_VM : INotifyPropertyChanged
	{
		public string Salutation { get; set; }

		public ObservableCollection<int> Integers { get; set; }

		public ObservableCollection<LedgerItem> LedgerItems { get; set; }

		private bool IntegersAscActive = false;
		private bool IntegersDescActive = false;

		private Dictionary<string, int> LedgerSortState;

		public Sorting_VM()
		{
			Salutation = "Welcome to the list sorting sample!";

			Random rng = new Random();

			Integers = new ObservableCollection<int>();
			for (int i = 0; i < 100; i++)
				Integers.Add(rng.Next(0, 101));

			LedgerItems = new ObservableCollection<LedgerItem>();
			for (int i = 0; i < 100; i++)
			{
				LedgerItems.Add(new LedgerItem
				{
					Id = i,
					Name = Guid.NewGuid().ToString(),
					Date = DateTime.Now - TimeSpan.FromDays(rng.Next(0, 1000)),
					Value = rng.Next(1, 10000) * (decimal)rng.NextDouble(),
					Quantity = rng.Next(1, 100),
				});
			}

			LedgerSortState = new Dictionary<string, int>();
			LedgerSortState.Add("Id", 0);
			LedgerSortState.Add("Name", 0);
			LedgerSortState.Add("Date", 0);
			LedgerSortState.Add("Value", 0);
			LedgerSortState.Add("Quantity", 0);
			SortLedger("Id");
		}

		private void SortIntegers(bool asc)
		{
			ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(Integers);

			if (asc)
			{
				if (!IntegersAscActive)
					lcv.CustomSort = new IntegerSorter(true);
				else
					lcv.CustomSort = null;

				IntegersAscActive = !IntegersAscActive;
				IntegersDescActive = false;
			}
			else
			{
				if (!IntegersDescActive)
					lcv.CustomSort = new IntegerSorter(false);
				else
					lcv.CustomSort = null;

				IntegersDescActive = !IntegersDescActive;
				IntegersAscActive = false;
			}
		}

		private void SortLedger(string field)
		{
			ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(LedgerItems);

			if (LedgerSortState[field] == 0)
			{
				// Not currently being sorted by this field, so sort
				// it ASC and mark the others as being not sorted currently.
				foreach (var entry in LedgerSortState.Keys.OfType<object>().ToArray())
					LedgerSortState[(string)entry] = 0;
				LedgerSortState[field] = 1;
				lcv.CustomSort = new LedgerSorter(field, true);
			}
			else if (LedgerSortState[field] == 1)
			{
				// Currently being sorted ASC by this field, so sort it DESC.
				LedgerSortState[field] = -1;
				lcv.CustomSort = new LedgerSorter(field, false);
			}
			else if (LedgerSortState[field] == -1)
			{
				// Currently being sorted DESC by this field, so
				// remove sorting altogether.
				LedgerSortState[field] = 0;
				lcv.CustomSort = null;
			}
			else
				throw new ArgumentException("The value for this key is invalid.");
		}

		#region Commands
		public ICommand SortIntegersCmd { get { return new SortIntCommand(this); } }
		private class SortIntCommand : ICommand
		{
			private readonly Sorting_VM Svm = null;
			public bool CanExecute(object parameter)
			{
				return true;
			}

			// TODO: What is this meant to be used for?
			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				if (parameter is string direction)
				{
					if (direction == "Desc")
						Svm.SortIntegers(false);
					else if (direction == "Asc")
						Svm.SortIntegers(true);
				}
			}
			public SortIntCommand(Sorting_VM svm)
			{
				Svm = svm;
			}
		}

		public ICommand SortLedgerCmd { get { return new SortLedgerCommand(this); } }
		private class SortLedgerCommand : ICommand
		{
			private readonly Sorting_VM Svm = null;
			public bool CanExecute(object parameter)
			{
				return true;
			}

			// TODO: What is this meant to be used for?
			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				if (parameter is string field)
				{
					Svm.SortLedger(field);
				}
			}
			public SortLedgerCommand(Sorting_VM svm)
			{
				Svm = svm;
			}
		}
		#endregion

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

	public class IntegerSorter : IComparer
	{
		private readonly bool Ascending = true;

		public int Compare(object x, object y)
		{
			if (x is int xx && y is int yy)
			{
				if (Ascending)
				{
					if (xx > yy)
						return 1;
					else
						return -1;
				}
				else
				{
					if (xx < yy)
						return 1;
					else
						return -1;
				}
			}

			throw new ArgumentException("This Compare requires two integers.");
		}

		public IntegerSorter(bool asc)
		{
			Ascending = asc;
		}
	}
}
