using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfSamples
{
	public class Main_VM : INotifyPropertyChanged
	{
		private string _Salutation;
		public string Salutation
		{
			get => _Salutation;
			set
			{
				if (_Salutation != value)
				{
					_Salutation = value;
					NotifyPropertyChanged("Salutation");
				}
			}
		}

		public Main_VM()
		{
			Salutation = "This is a temporary salutation.";

			using (StreamReader read = new StreamReader("Assets/Salutation_Main.txt"))
			{
				Salutation = read.ReadToEnd();
			}
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
}
