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
        private Maze Maze { get; set; }
        private Position _playerPos;

        public Game()
        {
            InitializeComponent();
            DataContext = Board;
            _playerPos = Maze.InitialPos;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int row = _playerPos.Row;
            int col = _playerPos.Col;
            if (e.Key == Key.Down)
            {
                if (Maze.Rows > row + 1 && Maze[row + 1,col] == CellType.Free)
                {
                    _playerPos = new Position(row+1,col);
                }
            }
            else if (e.Key == Key.Up)
            {
                if (0 >= row - 1 && Maze[row - 1, col] == CellType.Free)
                {
                    _playerPos = new Position(row - 1, col);
                }
            }
            else if (e.Key == Key.Right)
            {
                if (Maze.Cols > col + 1 && Maze[row, col + 1] == CellType.Free)
                {
                    _playerPos = new Position(row, col + 1);
                }
            }
            else if (e.Key == Key.Left)
            {
                if (0 >= col - 1 && Maze[row, col - 1] == CellType.Free)
                {
                    _playerPos = new Position(row, col - 1);
                }
            }
        }
    }
}
