using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfSamples.Attached_Behavior.ViewModel
{
	public class SelectFileDialogBox_VM : INotifyPropertyChanged
	{
		public string Title { get; set; }

		public bool Result { get; set; }

		public string FullFilename { get; set; }

		public SelectFileDialogBox_VM(string title)
		{
			Title = title;
		}

		public SelectFileDialogBox_VM()
		{
			Title = "SelectFileDialogBox_VM[DT]";
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
