using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfSamples.Attached_Behavior.Model
{
	public class ThingWithTwoValues : INotifyPropertyChanged
	{
		public string Value1 { get; set; }
		public string Value2 { get; set; }
		public ThingWithTwoValues Parent { get; set; } = null;

		private bool _IsExpanded;
		public bool IsExpanded
		{
			get
			{
				return _IsExpanded;
			}
			set
			{
				if (_IsExpanded != value)
				{
					_IsExpanded = value;
					NotifyPropertyChanged("IsExpanded");
				}

				// TreeView doesn't do full expansion, so we have to do it manually.
				if (_IsExpanded && Parent != null)
					Parent.IsExpanded = true;
			}
		}

		private bool _IsSelected;
		public bool IsSelected
		{
			get
			{
				return _IsSelected;
			}
			set
			{
				if (_IsSelected != value)
				{
					_IsSelected = value;
					NotifyPropertyChanged("IsSelected");
				}
			}
		}

		private static int NumItems = 0;

		public ReadOnlyCollection<ThingWithTwoValues> Children { get; set; }

		public bool Value1ContainsText(string text)
		{
			// Validate input and target.
			if (String.IsNullOrEmpty(text) || String.IsNullOrEmpty(Value1))
				return false;

			// Get the index of text; -1 means text is not present.
			int loc = Value1.IndexOf(text);
			if (loc >= 0)
				return true;
			else
				return false;
		}

		public ThingWithTwoValues()
		{
			// This is me.
			Value1 = Guid.NewGuid().ToString();
			Value2 = Guid.NewGuid().ToString();

			NumItems++;

			ThingWithTwoValues[] children;
			if (NumItems < 100)
			{
				// This is my children.
				Random rng = new Random();
				int numChildren = rng.Next(0, 3);
				if (NumItems == 1)
					numChildren = 5;
				children = new ThingWithTwoValues[numChildren];
				for (int i = 0; i < numChildren; i++)
					children[i] = new ThingWithTwoValues { Parent = this };
				Children = new ReadOnlyCollection<ThingWithTwoValues>(children);
			}
			else
			{
				children = new ThingWithTwoValues[0];
				Children = new ReadOnlyCollection<ThingWithTwoValues>(children);
			}
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
