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
using System.Windows.Shapes;
using ClientGUI.view_model;
using MazeLib;
using System.ComponentModel;

namespace ClientGUI.view
{
	/// <summary>
	/// Interaction logic for SinglePlayerGame.xaml
	/// </summary>
	public partial class SinglePlayerGame : Window
	{
		public SinglePlayerViewModel _vm;
		private Maze Maze { get; set; }

		public SinglePlayerGame(SinglePlayerViewModel vm)
		{
			InitializeComponent();
			_vm = vm;
			DataContext = _vm;
			Board.DataContext = _vm;
		}

		public void Start()
		{
			Board.DrawMaze();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			
			if (e.Key == Key.Down)
			{
				_vm.Move("down");
			}
			else if (e.Key == Key.Up)
			{
				_vm.Move("up");
			}
			else if (e.Key == Key.Right)
			{
				_vm.Move("right");
			}
			else if (e.Key == Key.Left)
			{
				_vm.Move("left");
			}
			Board.DrawMaze();
		}
	}
}
