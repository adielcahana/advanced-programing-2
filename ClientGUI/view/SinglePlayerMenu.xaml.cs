using System.Windows;
using ClientGUI.model;
using ClientGUI.view_model;
using MazeLib;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class SinglePlayerMenu : Window
    {
        private MainWindow _main;
        private bool _gameStarted;
        private Error _error;
        private SinglePlayerViewModel _viewModel;
        public SinglePlayerMenu(MainWindow main)
        {
            InitializeComponent();
            _viewModel = new SinglePlayerViewModel(new SinglePlayerModel());
            _main = main;
            _gameStarted = false;
            _error = new Error();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
            _main.Show();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            if (!_gameStarted)
            {
                _main.Show();
            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            Maze maze = _viewModel.GenerateMaze();
            /*Game game = new Game();*/
            _gameStarted = true;
            /*game.Show();*/
            Close();
        }
    }
}
