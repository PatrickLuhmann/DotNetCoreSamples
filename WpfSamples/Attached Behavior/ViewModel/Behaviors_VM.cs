using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using WpfSamples.Attached_Behavior.Model;

namespace WpfSamples.Attached_Behavior.ViewModel
{
	public class Behaviors_VM : INotifyPropertyChanged
	{
		public string Salutation { get; set; } = "Welcome to Behaviors_VM";

		private string _DialogStatus = "<No Status>";
		public string DialogStatus
		{
			get => _DialogStatus;
			set
			{
				if (_DialogStatus != value)
				{
					_DialogStatus = value;
					NotifyPropertyChanged("DialogStatus");
				}
			}
		}

		public ReadOnlyCollection<ThingWithTwoValues> TopLevel { get; set; }

		public string SearchString { get; set; }

		private string _SelectedFile = "<No File Selected>";
		public string SelectedFile
		{
			get { return _SelectedFile; }
			set
			{
				if (_SelectedFile != value)
				{
					_SelectedFile = value;
					NotifyPropertyChanged("SelectedFile");
				}
			}
		}

		// This is used for showing a simple dialog window.
		private bool _DialogVisible = false;
		public bool DialogVisible
		{
			get
			{
				return _DialogVisible;
			}
			set
			{
				if (_DialogVisible != value)
				{
					_DialogVisible = value;
					NotifyPropertyChanged("DialogVisible");
				}
			}
		}

		// This is a better implementation for a dialog window backed by a ViewModel.
		// A property change on DialogBox triggers the DialogBox behavior, because
		// that is how we defined the behavior at the top of the main window.
		private object _DialogBox = null;
		public object DialogBox
		{
			get
			{
				return _DialogBox;
			}
			set
			{
				if (_DialogBox != value)
				{
					_DialogBox = value;
					NotifyPropertyChanged("DialogBox");
				}
			}
		}

		// This is a better implementation for a system dialog window backed by a ViewModel.
		// A property change on SelectFileDialogBox triggers the SelectFileDialogBox behavior, because
		// that is how we defined the behavior at the top of the main window.
		// TODO: Should this be generic so that any dialog will work? Or is it okay to be specific,
		//       with the knowledge that there could be many of these?
		private SelectFileDialogBox_VM _SelectFileDialogBox = null;
		public SelectFileDialogBox_VM SelectFileDialogBox
		{
			get => _SelectFileDialogBox;
			set
			{
				if (_SelectFileDialogBox != value)
				{
					_SelectFileDialogBox = value;
					NotifyPropertyChanged("SelectFileDialogBox");
				}
			}
		}

		private ThingWithTwoValues RootItem;

		#region Commands
		public ICommand SearchTreeCmd { get { return new SearchTreeCommand(); } }
		private class SearchTreeCommand : ICommand
		{
			public bool CanExecute(object parameter)
			{
				return true;
			}

			// TODO: What is this meant to be used for?
			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				Behaviors_VM bvm = (Behaviors_VM)parameter;
				if (bvm != null)
					bvm.SearchTree();
			}
		}
		public void SearchTree()
		{
			ThingWithTwoValues target = FindMatch(SearchString, RootItem);
			if (target == null)
				return;

			target.IsExpanded = true;

			target.IsSelected = true;
		}

		public ICommand ToggleDialogCmd { get { return new ToggleDialogCommand(); } }
		private class ToggleDialogCommand : ICommand
		{
			public bool CanExecute(object parameter)
			{
				return true;
			}

			// TODO: What is this meant to be used for?
			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				Behaviors_VM bvm = (Behaviors_VM)parameter;
				if (bvm != null)
					bvm.DialogVisible = !bvm.DialogVisible;
			}
		}

		public ICommand DialogBoxCmd { get { return new DialogBoxCommand(); } }
		private class DialogBoxCommand : ICommand
		{
			public bool CanExecute(object parameter)
			{
				return true;
			}

			// TODO: What is this meant to be used for?
			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				if (parameter is Behaviors_VM bvm)
				{
					bvm.DialogStatus = "About to display DialogBox";
					DialogBox_VM dbvm = new DialogBox_VM("Look Ma, I'm a star!");
					dbvm.OnOk = (_dbvm) =>
					{
						bvm.DialogStatus = $"User gave us {_dbvm.Input1} and {_dbvm.Input2}.";

						bvm.DialogBox = null;
					};
					// Put any additional VM init code here.

					// This assignment causes the dialog box to be displayed.
					// This is a blocking statement.
					bvm.DialogBox = dbvm;

					// The dialog has been closed (or killed or maybe some other things as well).
					// TODO: How do we determine the reason for the closure? I.E. OK vs. Cancel.
					System.Diagnostics.Debug.WriteLine($"DailogBox_VM.Input1: {dbvm.Input1}");
					System.Diagnostics.Debug.WriteLine($"DailogBox_VM.Input2: {dbvm.Input2}");
					//bvm.DialogStatus = "Just displayed DialogBox";

					// We are done with the dialog box so set its property to null.
					// This is needed in cases where the view can lose focus and then regain focuse,
					// such as with a tab control. In this case, the app will re-read the properties,
					// see that this is not null, and execute the set accessor, which will pop the dialog
					// up again even though the user didn't explicitly trigger it.
					bvm.DialogBox = null;
				}
			}
		}

		public ICommand SelectFileDialogBoxCmd { get { return new SelectFileDialogBoxCommand(); } }
		private class SelectFileDialogBoxCommand : ICommand
		{
			public bool CanExecute(object parameter)
			{
				return true;
			}

			// TODO: What is this meant to be used for?
			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				Behaviors_VM bvm = (Behaviors_VM)parameter;
				if (bvm != null)
				{
					bvm.SelectFileDialogBox = new SelectFileDialogBox_VM("Choose, but choose wisely.");
					if (bvm.SelectFileDialogBox.Result)
						bvm.SelectedFile = bvm.SelectFileDialogBox.FullFilename;
				}
			}
		}
		#endregion

		private ThingWithTwoValues FindMatch(string text, ThingWithTwoValues item)
		{
			if (item.Value1ContainsText(text))
				return item;

			foreach (var child in item.Children)
			{
#if false
				if (child.Value1ContainsText(text))
					return child;
#endif

				ThingWithTwoValues target = FindMatch(text, child);
				if (target != null)
					return target;
			}

			return null;
		}

		public Behaviors_VM()
		{
			Salutation = "Behaviors_VM [DT]";
			RootItem = new ThingWithTwoValues();
			TopLevel = new ReadOnlyCollection<ThingWithTwoValues>(
				new ThingWithTwoValues[] { RootItem });
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
