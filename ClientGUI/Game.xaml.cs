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
            _board = new MazeBoard(_maze);
            
        }
    }
}
