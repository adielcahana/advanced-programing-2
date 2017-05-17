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

        public string MyMaze
        {
            get { return (string)GetValue(MyMazeProperty); }
            set { SetValue(MyMazeProperty, value); }
        }
         
        public static readonly DependencyProperty MyMazeProperty =
            DependencyProperty.Register("MyMaze", typeof(string), typeof(MazeBoard));

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeBoard), new PropertyMetadata(0));

        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard), new PropertyMetadata(0));

        public MazeBoard()
        {
            InitializeComponent();
        }

        private static void mazeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeBoard board = (MazeBoard)d;
            board.DrawMaze();
        }

        public void DrawMaze()
        {
            //Rectangle rect;
            //int left = 0;
            //int top = 100;

            //for (int row = 0; row < MyMaze.Rows; row++)
            //{
            //    for (int col = 0; col < MyMaze.Cols; col++)
            //    {
            //        rect = new Rectangle();
            //        rect.Height = 20;
            //        rect.Width = 20;
            //        rect.Stroke = Brushes.Black;
            //        if (row == MyMaze.InitialPos.Row && col == MyMaze.InitialPos.Col)
            //        {
            //            rect.Fill = Brushes.Lime;
            //        }
            //        else if (row == MyMaze.GoalPos.Row && col == MyMaze.GoalPos.Col)
            //        {
            //            rect.Fill = Brushes.Red;
            //        }
            //        else if (MyMaze[row, col] == CellType.Free)
            //        {
            //            rect.Fill = Brushes.Black;
            //        }
            //        else
            //        {
            //            rect.Fill = Brushes.White;
            //        }
            //        Canvas.SetLeft(rect, left);
            //        Canvas.SetTop(rect, top);
            //        Canvas.Children.Add(rect);
            //        left += 21;
            //    }
            //    left = 0;
            //    top += 21;
            //}
        }
    }
}
