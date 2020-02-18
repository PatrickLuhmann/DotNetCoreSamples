using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using WpfSamples.Attached_Behavior.ViewModel;

namespace WpfSamples.Attached_Behavior.View
{
	public class SelectFileDialogBoxShim
	{
		public void Show(SelectFileDialogBox_VM sfvm)
		{
			// Create the file dialog.
			// TODO: There is also a SaveFileDialog. Do I need to implement them both?
			OpenFileDialog dlg = new OpenFileDialog();

			// Set the attributes of the dialog.
			dlg.Title = sfvm.Title;
			// ... other stuff as needed.

			// Show me, don't tell me!
			var result = dlg.ShowDialog();

			sfvm.Result = (result != null) && result.Value;
			sfvm.FullFilename = dlg.FileName;
			// ... other stuff as needed.
		}
	}
}
