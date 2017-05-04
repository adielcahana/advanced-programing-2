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
using System.Windows.Shapes;
using Client;
using ClientGUI;
using MazeLib;
using Client;

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
        public Game(string name, int row, int col, MainWindow main)
        {
            InitializeComponent();
            _main = main;
            _client = new Client.Client();
            _client.Initialize();
            _client.Send("generate " + name + " " + row + " " + col);
            string mazeSrl = _client.Recieve();
            _client.Close();
            _maze = Maze.FromJSON(mazeSrl);
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
    }
}
