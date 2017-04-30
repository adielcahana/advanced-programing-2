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
        public static readonly DependencyProperty MazeProperty = DependencyProperty.Register("Maze", typeof(Maze),
            typeof(MazeBoard), new UIPropertyMetadata(mazeChanged));
        public MazeBoard()
        {
            InitializeComponent();    
        }

        private static void mazeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeBoard board = (MazeBoard)d;
            board.DrawMaze();
        }

        private void DrawMaze()
        {
            Rectangle rect;
            int left = 0;
            int top = 0;
            int i = 0;
            for (int row = 0; row < _maze.Rows; row++)
            {
                for (int col = 0; col < _maze.Cols; col++)
                {
                    rect = new Rectangle();
                    rect.Height = 20;
                    rect.Width = 20;
                    rect.Stroke = Brushes.Black;
                    if (_maze[row, col] == CellType.Free)
                    {
                        rect.Fill = Brushes.Black;
                    }
                    else
                    {
                        rect.Fill = Brushes.White;
                    }
                    Canvas.SetLeft(rect, left);
                    Canvas.SetTop(rect, top);
                    left += 21;
                }
                top += 21;
            }
        }
    }
}
