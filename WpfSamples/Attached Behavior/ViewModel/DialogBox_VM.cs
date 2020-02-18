using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace WpfSamples.Attached_Behavior.ViewModel
{
	public class DialogBox_VM : INotifyPropertyChanged
	{
		public string Salutation { get; set; } = "This is DialogBox_VM";
		public string Input1 { get; set; }
		public string Input2 { get; set; }

		public Action<DialogBox_VM> OnOk { get; set; }

		public event EventHandler DialogClosing;

		#region Commands
		public ICommand OkCmd { get { return new OkCommand(); } }
		private class OkCommand : ICommand
		{
			public bool CanExecute(object parameter)
			{
				return true;
			}

			// TODO: What is this meant to be used for?
			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				if (parameter is DialogBox_VM dbvm)
				{
					// Trigger the action that the creator registered.
					dbvm.OnOk?.Invoke(dbvm);

					// We could check dbvm to see if we should really close the dialog box,
					// or keep it open so that the user can fix whatever problem prevents
					// the dialog box from closing.
					dbvm.DialogClosing?.Invoke(dbvm, new EventArgs());
				}
			}
		}

		public ICommand CancelCmd { get { return new CancelCommand(); } }
		private class CancelCommand : ICommand
		{
			public bool CanExecute(object parameter)
			{
				return true;
			}

			// TODO: What is this meant to be used for?
			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				if (parameter is DialogBox_VM dbvm)
				{
					// What do we do to Cancel-close this dialog box?
					// Raise an event in our VM that the behavior monitors?

					// First, any input provided by the user is discarded.
					dbvm.Input1 = "";
					dbvm.Input2 = "";

					dbvm.DialogClosing?.Invoke(dbvm, new EventArgs());
				}
			}
		}
		#endregion

		public DialogBox_VM(string sal)
		{
			Salutation = sal;
		}

		public DialogBox_VM()
		{
			Salutation = "DialogBox_VM[DT]";
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
