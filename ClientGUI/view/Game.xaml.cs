using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ClientGUI;
using MazeLib;

namespace ClientGui
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private MainWindow _main;
        private Client.Client _client;
        private Maze _maze;
        private MazeBoard _board;
        private Position _playerPos;
        public Game(Maze maze, MainWindow main)
        {
            InitializeComponent();
            _main = main;
            _maze = maze;
            _board = new MazeBoard(_maze, Canvas);
            _playerPos = _maze.InitialPos;

            Rectangle rect = new Rectangle();
            rect.Height = 20;
            rect.Width = 20;
            rect.Stroke = Brushes.Black;
            rect.Fill = Brushes.Blue;
            Canvas.SetLeft(rect, _playerPos.Row * 21);
            Canvas.SetTop(rect, _playerPos.Row * 21);
            Canvas.Children.Add(rect);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int row = _playerPos.Row;
            int col = _playerPos.Col;
            Canvas = new Canvas();
            if (e.Key == Key.Down)
            {
                if (_maze.Rows > row + 1 && _maze[row + 1,col] == CellType.Free)
                {
                    _playerPos = new Position(row+1,col);
                }
            }
            else if (e.Key == Key.Up)
            {
                if (0 >= row - 1 && _maze[row - 1, col] == CellType.Free)
                {
                    _playerPos = new Position(row - 1, col);
                }
            }
            else if (e.Key == Key.Right)
            {
                if (_maze.Cols > col + 1 && _maze[row, col + 1] == CellType.Free)
                {
                    _playerPos = new Position(row, col + 1);
                }
            }
            else if (e.Key == Key.Left)
            {
                if (0 >= col - 1 && _maze[row, col - 1] == CellType.Free)
                {
                    _playerPos = new Position(row, col - 1);
                }
            }
            _board.DrawMaze();
            Rectangle rect = new Rectangle();
            rect.Height = 20;
            rect.Width = 20;
            rect.Stroke = Brushes.Black;
            rect.Fill = Brushes.Blue;
            Canvas.SetLeft(rect,_playerPos.Row * 21);
            Canvas.SetTop(rect, 100 + _playerPos.Row * 21);
            Canvas.Children.Add(rect);
        }

        private void Restart_game_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Game game = new Game(_maze, _main);
            game.Show();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
            _main.Show();
        }
    }
}
