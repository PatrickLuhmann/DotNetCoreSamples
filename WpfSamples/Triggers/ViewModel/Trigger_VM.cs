using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfSamples.Triggers.ViewModel
{
	public class Trigger_VM : INotifyPropertyChanged
	{
		#region Properties
		public string Title { get; set; }

		public List<string> ComboList1 { get; set; }
		private string _SelectedCombo1;
		public string SelectedCombo1
		{
			get => _SelectedCombo1;
			set
			{
				_SelectedCombo1 = value;
				NotifyPropertyChanged("SelectedCombo1");
			}
		}

		// Item C
		private string _CDate;
		public string CDate
		{
			get => _CDate;
			set
			{
				_CDate = value;
				NotifyPropertyChanged("CDate");
			}
		}

		private string _CQuantity;
		public string CQuantity
		{
			get => _CQuantity;
			set
			{
				_CQuantity = value;
				NotifyPropertyChanged("CQuantity");
			}
		}

		private string _CAmount;
		public string CAmount
		{
			get => _CAmount;
			set
			{
				_CAmount = value;
				NotifyPropertyChanged("CAmount");
			}
		}
		#endregion

		public Trigger_VM()
		{
			Title = "Welcome to the Trigger Sample";

			ComboList1 = new List<string>
			{
				"Item A",
				"Item B",
				"Item C",
			};
			SelectedCombo1 = ComboList1[0];
		}

		#region NotifyPropertyChanged
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
}
