using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Ex1;
using System.Windows.Threading;

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
			switch (e.Key)
			{
				case Key.Down:
					_vm.Move(Direction.Down);
					break;
				case Key.Up:
					_vm.Move(Direction.Up);
					break;
				case Key.Right:
					_vm.Move(Direction.Right);
					break;
				case Key.Left:
					_vm.Move(Direction.Left);
					break;
			}
			Board.DrawMaze();
		}

		private void RestartGame_Click(object sender, RoutedEventArgs e)
		{
			MessageWindow msg = new MessageWindow("are you sure you want to restart the game?");
			msg.Ok.Click += delegate (object sender1, RoutedEventArgs e1)
			{
				_vm.RestartGame();
				Board.DrawMaze();
				msg.Close();
				Show();
			};
			msg.Cancel.Click += delegate (object sender1, RoutedEventArgs e1)
			{
				msg.Close();
				Show();
			};
			Hide();
			msg.Show();
		}

		private void Menu_Click(object sender, RoutedEventArgs e)
		{
			MessageWindow msg = new MessageWindow("are you sure you want to go back to the main menu?");
			msg.Ok.Click += delegate (object sender1, RoutedEventArgs e1)
			{
				msg.Close();
				Close();
				new MainWindow().Show();
			};
			msg.Cancel.Click += delegate (object sender1, RoutedEventArgs e1)
			{
				msg.Close();
				Show();
			};
			Hide();
			msg.Show();
		}

		private void SolveMaze_Click(object sender, RoutedEventArgs e)
		{
			KeyDown -= Window_KeyDown;
			MazeSolution solution = _vm.SolveMaze();
			IEnumerator<Direction> directions = solution.GetEnumerator();
			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1/60);
			timer.Tick += delegate(object sender1, EventArgs e1)
			{
				if (directions.MoveNext())
				{
					switch (directions.Current)
					{
						case Direction.Down:
							_vm.Move(Direction.Down);
							break;
						case Direction.Up:
							_vm.Move(Direction.Up);
							break;
						case Direction.Right:
							_vm.Move(Direction.Right);
							break;
						case Direction.Left:
							_vm.Move(Direction.Left);
							break;
					}
					Board.DrawMaze();
				} 
				else
				{
					timer.Stop();
					KeyDown += Window_KeyDown;
				}
			};
			_vm.RestartGame();
			Board.DrawMaze();
			timer.Start();
		}
	}
}
