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

namespace ClientGUI.controls
{
	/// <summary>
	/// Interaction logic for MenuUserControl.xaml
	/// </summary>
	public partial class MenuUserControl : UserControl
	{
		//public int Rows
		//{
		//	get { return (int)GetValue(RowsProperty); }
		//	set { SetValue(RowsProperty, value); }
		//}
		//// Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
		//public static readonly DependencyProperty RowsProperty =
		//	DependencyProperty.Register("Rows", typeof(int), typeof(MenuUserControl), new PropertyMetadata(0));
		
		//public int Cols
		//{
		//	get { return (int)GetValue(ColsProperty); }
		//	set { SetValue(ColsProperty, value); }
		//}
		//// Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
		//public static readonly DependencyProperty ColsProperty =
		//	DependencyProperty.Register("Cols", typeof(int), typeof(MenuUserControl), new PropertyMetadata(0));

		//public string Name
		//{
		//	get { return (string)GetValue(nameProperty); }
		//	set { SetValue(nameProperty, value); }
		//}
		//// Using a DependencyProperty as the backing store for name.  This enables animation, styling, binding, etc...
		//public static readonly DependencyProperty nameProperty =
		//	DependencyProperty.Register("Name", typeof(string), typeof(MenuUserControl), new PropertyMetadata(0));

		public MenuUserControl()
		{
			InitializeComponent();
		}
	}
}
