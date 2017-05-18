using System.Windows;
using ClientGUI.model;
using ClientGUI.view;
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
        private SinglePlayerViewModel _viewModel;
        public SinglePlayerMenu(MainWindow main)
        {
            InitializeComponent();
            _viewModel = new SinglePlayerViewModel(new SinglePlayerModel());
			DataContext = _viewModel;
            _main = main;
            _gameStarted = false;
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
			SinglePlayerGame game = new SinglePlayerGame(_viewModel);
			_viewModel.GenerateMaze();
			game.Show();
			_gameStarted = true;
            Close();
        }
    }
}
