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
			_vm.NewMaze += NewMaze;
			Board.DataContext = _vm;
		}

		private void NewMaze(object obj, PropertyChangedEventArgs args){
			Maze = (obj as SinglePlayerViewModel).Maze;
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			int row = _vm.PlayerPos.Row;
			int col = _vm.PlayerPos.Col;
			if (e.Key == Key.Down)
			{ 
				if (Maze.Rows > row + 1 && Maze[row + 1, col] == CellType.Free)
				{
					_vm.PlayerPos = new Position(row + 1, col);
				}
			}
			else if (e.Key == Key.Up)
			{
				if (row - 1 >= 0 && Maze[row - 1, col] == CellType.Free)
				{
					_vm.PlayerPos = new Position(row - 1, col);
				}
			}
			else if (e.Key == Key.Right)
			{
				if (Maze.Cols > col + 1 && Maze[row, col + 1] == CellType.Free)
				{
					_vm.PlayerPos = new Position(row, col + 1);
				}
			}
			else if (e.Key == Key.Left)
			{
				if (col - 1 >= 0 && Maze[row, col - 1] == CellType.Free)
				{
					_vm.PlayerPos = new Position(row, col - 1);
				}
			}
			Board.DrawMaze();
		}
	}
}
