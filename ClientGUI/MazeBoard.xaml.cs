using MazeLib;
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

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        private Maze _maze;
        private Canvas _canvas;
        public static readonly DependencyProperty MazeProperty = DependencyProperty.Register("Maze", typeof(Maze),
            typeof(MazeBoard), new UIPropertyMetadata(mazeChanged));

        public MazeBoard(Maze maze, Canvas c)
        {
            InitializeComponent();
            _maze = maze;
            _canvas = c;
            DrawMaze();
        }

        private static void mazeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeBoard board = (MazeBoard)d;
            board.DrawMaze();
        }

        public void DrawMaze()
        {
            Rectangle rect;
            int left = 0;
            int top = 100;
            
            for (int row = 0; row < _maze.Rows; row++)
            {
                for (int col = 0; col < _maze.Cols; col++)
                {
                    rect = new Rectangle();
                    rect.Height = 20;
                    rect.Width = 20;
                    rect.Stroke = Brushes.Black;
                    if (row == _maze.InitialPos.Row && col == _maze.InitialPos.Col)
                    {
                        rect.Fill = Brushes.Lime;
                    }
                    else if (row == _maze.GoalPos.Row && col == _maze.GoalPos.Col)
                    {
                        rect.Fill = Brushes.Red;
                    }
                    else if (_maze[row, col] == CellType.Free)
                    {
                        rect.Fill = Brushes.Black;
                    }
                    else
                    {
                        rect.Fill = Brushes.White;
                    }
                    Canvas.SetLeft(rect, left);
                    Canvas.SetTop(rect, top);
                    _canvas.Children.Add(rect);
                    left += 21;
                }
                left = 0;
                top += 21;
            }
        }
    }
}
