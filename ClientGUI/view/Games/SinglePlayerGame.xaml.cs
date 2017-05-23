using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using ClientGUI.view_model;
using Ex1;
using MazeLib;

namespace ClientGUI.view.Games
{
	/// <summary>
	/// Interaction logic for SinglePlayerGame.xaml
	/// </summary>
	public partial class SinglePlayerGame : Window
	{
		private SinglePlayerViewModel _vm;
		private DispatcherTimer _timer;

		public bool Finish
		{
			get { return (bool)GetValue(FinishProperty); }
			set { SetValue(FinishProperty, value); }
		}

		public static readonly DependencyProperty FinishProperty =
			DependencyProperty.Register("Finish", typeof(bool), typeof(SinglePlayerGame), new PropertyMetadata(FinishPropertyChanged));

		public bool Start
		{
			get { return (bool)GetValue(StartProperty); }
			set { SetValue(FinishProperty, value); }
		}

		public static readonly DependencyProperty StartProperty =
			DependencyProperty.Register("Start", typeof(bool), typeof(SinglePlayerGame), new PropertyMetadata(StartPropertyChanged));

		public SinglePlayerGame(SinglePlayerViewModel vm)
		{
			InitializeComponent();
			_vm = vm;
			_timer = null;
			DataContext = _vm;
			Board.DataContext = _vm;

			Binding binding = new Binding();
			binding.Path = new PropertyPath("Start");
			binding.Source = vm;  
			BindingOperations.SetBinding(this ,StartProperty ,binding);

			binding = new Binding();
			binding.Path = new PropertyPath("Finish");
			binding.Source = vm;
			BindingOperations.SetBinding(this, FinishProperty, binding);
		}

		private static void StartPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	    {
		    if ((bool) e.NewValue)
		    {
			    ((SinglePlayerGame)d).StartGame();
			}
		}

		private static void FinishPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((SinglePlayerGame)d).FinishGame();
		}

		private void FinishGame()
		{
			MessageWindow message;
			FinishGame();
			if (Finish == true && Start == true)
			{
				message = new MessageWindow("You won");
				message.Ok.Click += delegate (object sender1, RoutedEventArgs e1)
				{
					Close();
					message.Close();
				};
				message.Cancel.Click += delegate (object sender1, RoutedEventArgs e1)
				{
					message.Close();
				};
				message.Show();
			}
			else if (Finish == true && Start == false)
			{
				Close();
				message = new MessageWindow("Name already exist");
				message.Ok.Click += delegate (object sender1, RoutedEventArgs e1)
				{
					message.Close();
				};
				message.Cancel.Click += delegate (object sender1, RoutedEventArgs e1)
				{
					message.Close();
				};
				message.Show();
			}
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
		}

		private void RestartGame_Click(object sender, RoutedEventArgs e)
		{
			MessageWindow msg = new MessageWindow("are you sure you want to restart the game?");
			if (_timer != null && _timer.IsEnabled)
			{
				_timer.Stop();
			    KeyDown += Window_KeyDown;
			}
			msg.Ok.Click += delegate (object sender1, RoutedEventArgs e1)
			{
				_vm.RestartGame();
				msg.Close();
				Show();
			};
			msg.Cancel.Click += delegate (object sender1, RoutedEventArgs e1)
			{
				msg.Close();
				Show();
			    if (_timer != null)
			    {
			        KeyDown -= Window_KeyDown;
                    _timer.Start();
			    }
            };
			Hide();
			msg.Show();
		}

		private void StartGame()
		{
			Board.DrawMaze();
			Board.RefreshMaze();
		}

//		public void CloseMaze()
//	    {
//            Clos
//        }

		private void Menu_Click(object sender, RoutedEventArgs e)
		{
			MessageWindow msg = new MessageWindow("are you sure you want to go back to the main menu?");
		    if (_timer != null && _timer.IsEnabled)
		    {
		        _timer.Stop();
		        KeyDown += Window_KeyDown;
		    }
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
			    if (_timer != null)
			    {
			        KeyDown -= Window_KeyDown;
                    _timer.Start();
			    }
            };
			Hide();
			msg.Show();
		}

		private void SolveMaze_Click(object sender, RoutedEventArgs e)
		{
			KeyDown -= Window_KeyDown;
			MazeSolution solution = _vm.SolveMaze();
			_vm.RestartGame();
			IEnumerator<Direction> directions = solution.GetEnumerator();
			_timer = new DispatcherTimer();
			_timer.Interval = TimeSpan.FromSeconds(0.1);
			_timer.Tick += delegate(object sender1, EventArgs e1)
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
				} 
				else
				{
					_timer.Stop();
					KeyDown += Window_KeyDown;
				    _timer = null;
				}
			};
			_timer.Start();
		}
	}
}
