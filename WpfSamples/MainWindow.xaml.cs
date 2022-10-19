using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfSamples.Attached_Behavior.View;
using WpfSamples.Triggers.View;

namespace WpfSamples
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Behavior_Button_Click(object sender, RoutedEventArgs e)
		{
			var wnd = new Behaviors_View();
			wnd.Show();
		}

		private void Trigger_Button_Click(object sender, RoutedEventArgs e)
		{
			var wnd = new Trigger_View();
			wnd.Show();
		}
	}
}
