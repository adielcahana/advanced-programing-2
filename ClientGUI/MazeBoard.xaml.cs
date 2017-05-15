using System.Data.Common;
using MazeLib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ClientGui;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        public Game Game { get; set; }
        public Maze Maze
        {
            get { return (Maze)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(Maze), typeof(MazeBoard), new PropertyMetadata(0));

        private int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
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

        public Position PlayerPos
        {
            get { return (Position)GetValue(PlayerPosProperty); }
            set { SetValue(PlayerPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerPosProperty =
            DependencyProperty.Register("PlayerPos", typeof(Position), typeof(MazeBoard), new PropertyMetadata(PlayerMoved));

        public Position GoalPos
        {
            get { return (Position)GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoalPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoalPosProperty =
            DependencyProperty.Register("GoalPos", typeof(Position), typeof(MazeBoard), new PropertyMetadata(0));


        public MazeBoard()
        {
            InitializeComponent();
            DataContext = Game;
        }

        private static void PlayerMoved(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeBoard board = (MazeBoard)d;
            board.DrawMaze();
        }

        private void DrawMaze()
        {
            Canvas.Children.Clear();
            int left = 0;
            int top = 100;
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Height = 20;
                    rect.Width = 20;
                    rect.Stroke = Brushes.Black;
                    if (row == PlayerPos.Row && col == PlayerPos.Col)
                    {
                        rect.Fill = Brushes.Blue;
                    }
                    else if (row == GoalPos.Row && col == GoalPos.Col)
                    {
                        rect.Fill = Brushes.Red;
                    }
                    else if (Maze[row, col] == CellType.Free)
                    {
                        rect.Fill = Brushes.Black;
                    }
                    else
                    {
                        rect.Fill = Brushes.White;
                    }
                    Canvas.SetLeft(rect, left);
                    Canvas.SetTop(rect, top);
                    Canvas.Children.Add(rect);
                    left += 21;
                }
                left = 0;
                top += 21;
            }
        }
    }
}
