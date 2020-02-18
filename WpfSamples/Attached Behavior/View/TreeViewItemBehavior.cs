using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfSamples.Attached_Behavior.View
{
	// From the sample by Josh Smith: https://www.codeproject.com/articles/28959/introduction-to-attached-behaviors-in-wpf 
	public static class TreeViewItemBehavior
	{
		// Define an attached property to be used with the attached behavior.
		// The attached property is named IsBroughtIntoViewWhenSelected.
		public static readonly DependencyProperty IsBroughtIntoViewWhenSelectedProperty =
			DependencyProperty.RegisterAttached(
				"IsBroughtIntoViewWhenSelected", // name of attached property
				typeof(bool), // type of attached property
				typeof(TreeViewItemBehavior), // owner type? the attached behavior we are defining; always the class we are defined in???
				new UIPropertyMetadata(false, OnIsBroughtIntoViewWhenSelectedChanged)); // default metadata????

		// Required Get for an attached property; name = "Get" + property name.
		// The return type is known to be bool because that is how we defined the property, above.
		public static bool GetIsBroughtIntoViewWhenSelected(TreeViewItem treeViewItem)
		{
			return (bool)treeViewItem.GetValue(IsBroughtIntoViewWhenSelectedProperty);
		}

		// Required Set for an attached property; name = "Set" + property name.
		// The type of 'value' is known to be bool because that is how we defined the property, above.
		public static void SetIsBroughtIntoViewWhenSelected(TreeViewItem treeViewItem, bool value)
		{
			treeViewItem.SetValue(IsBroughtIntoViewWhenSelectedProperty, value);
		}

		static void OnIsBroughtIntoViewWhenSelectedChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
		{
			TreeViewItem item = (TreeViewItem)depObj;
			if (item == null)
				return;

			if (e.NewValue is bool == false)
				return;

			if ((bool)e.NewValue)
				item.Selected += OnTreeViewItemSelected;
			else
				item.Selected -= OnTreeViewItemSelected;

			// POC - not sure what this means; need to learn about app resources/dictionary/whatever.
			var resource = Application.Current.TryFindResource(e.NewValue.GetType());
		}

		static void OnTreeViewItemSelected(object sender, RoutedEventArgs e)
		{
			// Only proceed for the actual selected item, not its ancestors.
			if (!Object.ReferenceEquals(sender, e.OriginalSource))
				return;

			TreeViewItem item = (TreeViewItem)e.OriginalSource;
			item?.BringIntoView();
		}
	}
}
