using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ClientGUI.view;

namespace ClientGUI.controls
{
	/// <summary>
	/// Interaction logic for MazeBoard.xaml
	/// </summary>
	public partial class MazeBoard : UserControl
	{
		private readonly Rectangle _player;

		public string Maze
		{
			get { return (string)GetValue(MazeProperty); }
			set { SetValue(MazeProperty, value); }
		}

		public static readonly DependencyProperty MazeProperty =
			DependencyProperty.Register("Maze", typeof(string), typeof(MazeBoard), new PropertyMetadata(OnMazePropertyChanged));

		private static void OnMazePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((MazeBoard)d).RefreshMaze();
		}

		public int Rows
		{
			get { return (int)GetValue(RowsProperty); }
			set { SetValue(RowsProperty, value); }
		}

		public static readonly DependencyProperty RowsProperty =
			DependencyProperty.Register("Rows", typeof(int), typeof(MazeBoard));

		public int Cols
		{
			get { return (int)GetValue(ColsProperty); }
			set { SetValue(ColsProperty, value); }
		}

		public static readonly DependencyProperty ColsProperty =
			DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard));

	    public bool Finish
	    {
	        get { return (bool)GetValue(FinishProperty); }
	        set { SetValue(FinishProperty, value); }
	    }

	    public static readonly DependencyProperty FinishProperty =
	        DependencyProperty.Register("Finish", typeof(bool), typeof(MazeBoard), new PropertyMetadata(FinishMazePropertyChanged));

	    private static void FinishMazePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	    {
	        ((MazeBoard)d).CloseMaze();
	    }

        public MazeBoard()
		{
			InitializeComponent();
			_player = new Rectangle();
			_player.Stroke = Brushes.Gray;
		}

	    public void CloseMaze()
	    {
            new MainWindow().Show();
	        Window.GetWindow(this).Close();
        }

        public void RefreshMaze()
		{
			_player.Height = Canvas.Height / Rows;
			_player.Width = Canvas.Width / Cols;
			double left = 0;
			double top = 0;
			bool newLine = false;
			Canvas.Children.Remove(_player);
			foreach (char c in Maze)
			{
				switch (c)
				{
					case '*':
					case '#':
					case '0':
					case '1':
						left += _player.Width;
						break;
					case '2':
						_player.Fill = (ImageBrush)Resources["DaveRight"];
						Canvas.SetLeft(_player, left);
						Canvas.SetTop(_player, top);
						Panel.SetZIndex(_player, 1);
						Canvas.Children.Add(_player);
						return;
					case '3':
						_player.Fill = (ImageBrush)Resources["DaveLeft"];
						Canvas.SetLeft(_player, left);
						Canvas.SetTop(_player, top);
						Panel.SetZIndex(_player, 1);
						Canvas.Children.Add(_player);
						return;
					default:
						if (Char.IsWhiteSpace(c) && newLine == false)
						{
							newLine = true;
							left = 0;
							top += _player.Height;
						}
						else
						{
							newLine = false;
						}
						continue;
				}
			}
		}

		public void DrawMaze()
		{
			Canvas.Children.Clear();
			double left = 0;
			double top = 0;
			bool newLine = false;
			foreach (char c in Maze)
			{
				Rectangle rect = new Rectangle();
				rect.Stroke = Brushes.Gray;
				rect.Height = Canvas.Height / Rows;
				rect.Width = Canvas.Width / Cols;
				switch (c)
				{
					case '*':
						rect.Fill = Brushes.Black;
						break;
					case '#':
						rect.Fill = (ImageBrush)Resources["Goal"];
						break;
					case '0':
						rect.Fill = Brushes.Black;
						break;
					case '1':
						rect.Fill = (ImageBrush)Resources["Wall"];
						break;
					case '2':
						rect.Fill = Brushes.Black;
						break;
					case '3':
						rect.Fill = Brushes.Black;
						break;
					default:
						if (Char.IsWhiteSpace(c) && newLine == false)
						{
							newLine = true;
							left = 0;
							top += rect.Height;
						}
						else
						{
							newLine = false;
						}
						continue;
				}
				Canvas.SetLeft(rect, left);
				Canvas.SetTop(rect, top);
				Canvas.Children.Add(rect);
				left += rect.Width;
			}
		}

		private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (Maze != null)
			{
				DrawMaze();
				RefreshMaze();
			}
		}
	}
}