﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfSamples.List_Sorting.ViewModel
{
	public class Sorting_VM : INotifyPropertyChanged
	{
		public string Salutation { get; set; }

		public ObservableCollection<int> Integers { get; set; }

		private bool IntegersAscActive = false;
		private bool IntegersDescActive = false;

		public Sorting_VM()
		{
			Salutation = "Welcome to the list sorting sample!";

			Random rng = new Random();

			Integers = new ObservableCollection<int>();
			for (int i = 0; i < 100; i++)
				Integers.Add(rng.Next(0, 101));
		}

		#region Commands
		public ICommand SortIntegersAscendingCmd { get { return new SortIntAscCommand(); } }
		private class SortIntAscCommand : ICommand
		{
			public bool CanExecute(object parameter)
			{
				return true;
			}

			// TODO: What is this meant to be used for?
			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				if (parameter is Sorting_VM svm)
					svm.SortIntegersAscending();
			}
		}
		public void SortIntegersAscending()
		{
			ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(Integers);
			if (!IntegersAscActive)
				lcv.CustomSort = new SortIntegersAscending();
			else
				lcv.CustomSort = null;

			IntegersAscActive = !IntegersAscActive;
			IntegersDescActive = false;
		}

		public ICommand SortIntegersDescendingCmd { get { return new SortIntDescCommand(); } }
		private class SortIntDescCommand : ICommand
		{
			public bool CanExecute(object parameter)
			{
				return true;
			}

			// TODO: What is this meant to be used for?
			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				if (parameter is Sorting_VM svm)
					svm.SortIntegersDescending();
			}
		}
		public void SortIntegersDescending()
		{
			ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(Integers);
			if (!IntegersDescActive)
				lcv.CustomSort = new SortIntegersDescending();
			else
				lcv.CustomSort = null;

			IntegersDescActive = !IntegersDescActive;
			IntegersAscActive = false;
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

	public class SortIntegersDescending : IComparer
	{
		public int Compare(object x, object y)
		{
			if (x is int xx && y is int yy)
			{
				if (xx < yy)
					return 1;
				else
					return -1;
			}

			throw new ArgumentException("This Compare requires two integers.");
		}
	}
}