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
            DependencyProperty.Register("Maze", typeof(string), typeof(MazeBoard));

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

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard));

        public MazeBoard()
        {
            InitializeComponent();
        }

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
				rect.Height = Canvas.Height/Rows;
				rect.Width = Canvas.Width/Cols;
				rect.Stroke = Brushes.Black;
				switch (c)
				{
					case '*':
						rect.Fill = Brushes.Lime;
						break;
					case '#':
						rect.Fill = Brushes.Red;
						break;
					case '0':
						rect.Fill = Brushes.White;
						break;
					case '1':
						rect.Fill = Brushes.Black;
						break;
					case '2':
						rect.Fill = Brushes.Blue;
						break;
					default:
						if (Char.IsWhiteSpace(c) && newLine == false)
						{
							newLine = true;
							left = 0;
							top += rect.Height + 1;
						} else
						{
							newLine = false;
						}
						continue;
				}
				Canvas.SetLeft(rect, left);
				Canvas.SetTop(rect, top);
				Canvas.Children.Add(rect);
				left += rect.Width + 1;
			}
        }
    }
}
