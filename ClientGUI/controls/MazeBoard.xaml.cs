using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ClientGUI.controls
{
	/// <summary>
	/// Interaction logic for MazeBoard.xaml
	/// </summary>
	public partial class MazeBoard : UserControl
	{
		private Rectangle player;

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

		public MazeBoard()
		{
			InitializeComponent();
			player = new Rectangle();
			player.Stroke = Brushes.Gray;
		}

		private void RefreshMaze()
		{
			player.Height = Canvas.Height / Rows;
			player.Width = Canvas.Width / Cols;
			double left = 0;
			double top = 0;
			bool newLine = false;
			Canvas.Children.Remove(player);
			foreach (char c in Maze)
			{
				switch (c)
				{
					case '*':
					case '#':
					case '0':
					case '1':
						left += player.Width;
						break;
					case '2':
						player.Fill = (ImageBrush)Resources["DaveRight"];
						Canvas.SetLeft(player, left);
						Canvas.SetTop(player, top);
						Panel.SetZIndex(player, 1);
						Canvas.Children.Add(player);
						return;
					case '3':
						player.Fill = (ImageBrush)Resources["DaveLeft"];
						Canvas.SetLeft(player, left);
						Canvas.SetTop(player, top);
						Panel.SetZIndex(player, 1);
						Canvas.Children.Add(player);
						return;
					default:
						if (Char.IsWhiteSpace(c) && newLine == false)
						{
							newLine = true;
							left = 0;
							top += player.Height;
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
			player.Height = Canvas.Height / Rows;
			player.Width = Canvas.Width / Cols;
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
	}
}