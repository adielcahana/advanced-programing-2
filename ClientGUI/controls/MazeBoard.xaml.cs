﻿using System;
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
using MazeLib;

namespace ClientGUI.view
{
	/// <summary>
	/// Interaction logic for MazeBoard.xaml
	/// </summary>
	public partial class MazeBoard : UserControl
	{
		public string Maze
		{
			get { return (string)GetValue(MazeProperty); }
			set { SetValue(MazeProperty, value); }
		}

		public static readonly DependencyProperty MazeProperty =
			DependencyProperty.Register("Maze", typeof(string), typeof(MazeBoard), new PropertyMetadata(onMazePropertyChanged));

		private static void onMazePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((MazeBoard)d).DrawMaze();
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
		}

		//public void Draw()
		//{
		//	Canvas.Children.Remove(player);
		//	player.Height = Canvas.Height / Rows;
		//	player.Width = Canvas.Width / Cols;
		//	Canvas.SetLeft(player, left);
		//	Canvas.SetTop(player, top);
		//	Canvas.Children.Add(player);
		//}

		public void DrawMaze()
		{
			Canvas.Children.Clear();
			Rectangle rect;
			double left = 0;
			double top = 0;
			bool newLine = false;
			foreach (char c in Maze)
			{
				rect = new Rectangle();
				rect.Height = Canvas.Height / Rows;
				rect.Width = Canvas.Width / Cols;
				rect.Stroke = Brushes.Black;
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
						rect.Fill = (ImageBrush)Resources["DaveRight"];
						break;
					case '3':
						rect.Fill = (ImageBrush)Resources["DaveLeft"];
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