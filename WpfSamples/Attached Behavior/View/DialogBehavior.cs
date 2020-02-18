using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WpfSamples.Attached_Behavior.ViewModel;

namespace WpfSamples.Attached_Behavior.View
{
	public static class DialogBehavior
	{
		#region DialogVisible Behavior
		//
		// BEHAVIOR: DialogVisible
		//

		// Define an attached property to be used with the attached behavior.
		// The attached property is named DialogVisible.
		public static readonly DependencyProperty DialogVisibleProperty =
			DependencyProperty.RegisterAttached(
				"DialogVisible", // name of attached property
				typeof(bool), // type of attached property
				typeof(DialogBehavior), // owner type? the attached behavior we are defining; always the class we are defined in???
				new PropertyMetadata(false, OnDialogVisibleChanged)); // default metadata????

		// Required Get for an attached property; name = "Get" + property name.
		// The return type is known to be bool because that is how we defined the property, above.
		public static bool GetDialogVisible(DependencyObject source)
		{
			return (bool)source.GetValue(DialogVisibleProperty);
		}

		// Required Set for an attached property; name = "Set" + property name.
		// The type of 'value' is known to be bool because that is how we defined the property, above.
		public static void SetDialogVisible(DependencyObject source, bool value)
		{
			source.SetValue(DialogVisibleProperty, value);
		}

		static void OnDialogVisibleChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
		{
			if (depObj is Window parent && e.NewValue is bool doShow)
			{
				new CustomDialogWindow { DataContext = parent.DataContext }.ShowDialog();
			}
		}
		#endregion

		#region DialogBox Behavior
		//
		// BEHAVIOR: DialogBox
		//

		private static DialogBox_View CurrentDialogBoxView;

		public static readonly DependencyProperty DialogBoxProperty =
			DependencyProperty.RegisterAttached(
				"DialogBox",
				typeof(object),
				typeof(DialogBehavior),
				new PropertyMetadata(null, OnDialogBoxChanged));

		// Required Get for an attached property; name = "Get" + property name.
		// The return type is known to be object because that is how we defined the property, above.
		public static object GetDialogBox(DependencyObject source)
		{
			return (object)source.GetValue(DialogBoxProperty);
		}

		// Required Set for an attached property; name = "Set" + property name.
		// The type of 'value' is known to be object because that is how we defined the property, above.
		public static void SetDialogBox(DependencyObject source, object value)
		{
			source.SetValue(DialogBoxProperty, value);
		}

		static void OnDialogBoxChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
		{
			// depObj is the View class that declares the behavior (BehaviorMainWindow in this case).
			if (depObj == null || e.NewValue == null)
				return;

			// Look in the resource dictionary for an item with the key of type DialogBox_VM.
			// This will return us a new object of type DialogBox_View because that is what
			// we put in the <Application.Resources> section of App.xaml.
			// e.NewValue is the DialogBox_VM object that was created in DialogBoxCommand.Execute().
			var resource = Application.Current.TryFindResource(e.NewValue.GetType());
			if (resource is DialogBox_View dlg)
			{
				// Give the dialog its DataContext (the corresponding VM).
				DialogBox_VM dbvm = (DialogBox_VM)e.NewValue;
				dlg.DataContext = dbvm;

				// Define the callback for when the VM signals that the dialog is closing.
				dbvm.DialogClosing += (sender, args) =>
				{
					// POC
					CurrentDialogBoxView.Close();
				};

				// Callback for the Window is closing.
				dlg.Closing += (sender, args) =>
				{
					// stuff
					System.Diagnostics.Debug.WriteLine("Callback: DailogBox_View.Closing");
				};
				// Callback for the Window is closed.
				dlg.Closed += (sender, args) =>
				{
					// stuff
					System.Diagnostics.Debug.WriteLine("Callback: DailogBox_View.Closed");
				};

				// POC: Get the dialog to close without the collection handling from the sample.
				//      This feels like a hack so even if it works, look for something better.
				CurrentDialogBoxView = dlg;

				// This assumes all dialogs are modal. Otherwise we would invoke Show().
				dlg.ShowDialog();
			}
		}
		#endregion

		#region SelectFileDialogBox Behavior
		//
		// BEHAVIOR: SelectFileDialogBox
		//

		public static readonly DependencyProperty SelectFileDialogBoxProperty =
			DependencyProperty.RegisterAttached(
				"SelectFileDialogBox",
				typeof(object),
				typeof(DialogBehavior),
				new PropertyMetadata(null, OnSelectFileDialogBoxChanged));

		public static object GetSelectFileDialogBox(DependencyObject source)
		{
			return (object)source.GetValue(SelectFileDialogBoxProperty);
		}

		public static void SetSelectFileDialogBox(DependencyObject source, object value)
		{
			source.SetValue(SelectFileDialogBoxProperty, value);
		}

		static void OnSelectFileDialogBoxChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
		{
			// depObj is apparently the parent and of type Window (or UserControl. presumably).
			if (depObj == null || e.NewValue == null)
				return;

			// Look in the resource dictionary for an item with the key of type SelectFileDialogBox_VM.
			// This will return us a new object of type SelectFileDialogBoxShim because that is what
			// we put in the <Application.Resources> section of App.xaml.
			// e.NewValue is the SelectFileDialogBox_VM object that was created in SelectFileDialogBoxCommand.Execute().
			var resource = Application.Current.TryFindResource(e.NewValue.GetType());
			if (resource is SelectFileDialogBoxShim shim)
			{
				SelectFileDialogBox_VM theVM = (SelectFileDialogBox_VM)e.NewValue;
				shim.Show(theVM);
			}
		}
		#endregion
	}
}
