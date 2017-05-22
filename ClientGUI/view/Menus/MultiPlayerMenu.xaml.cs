using System.Windows;
using ClientGUI.model;
using ClientGUI.view.Games;
using ClientGUI.view_model;

namespace ClientGUI.view.Menus
{
    /// <summary>
    /// Interaction logic for MultiPlayerMenu.xaml
    /// </summary>
    public partial class MultiPlayerMenu : Window
    {
        private bool _gameStarted;
        private MultiPlayerViewModel _viewModel;
        public MultiPlayerMenu()
        {
            InitializeComponent();
            _viewModel = new MultiPlayerViewModel(new MultiPlayerModel());
            DataContext = _viewModel;
            _gameStarted = false;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
			new view.MainWindow().Show();
		}

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow msg = new MessageWindow("wait to second player...");
            Hide();
            msg.Ok.Click += delegate (object sender1, RoutedEventArgs e1) { };
            msg.Cancel.Click += delegate (object sender1, RoutedEventArgs e1)
            {
                msg.Close();
                _viewModel.Close();
            };
            msg.Show();
            MultiPlayerGame game = new MultiPlayerGame(_viewModel);
            _viewModel.StartGame();
            msg.Close();
            game.Show();
            game.Start();
            _gameStarted = true;
            Close();
        }

        private void JoinGame_Click(object sender, RoutedEventArgs e)
        {
            MultiPlayerGame game = new MultiPlayerGame(_viewModel);
            _viewModel.JoinGame();
            game.Show();
            game.Start();
            _gameStarted = true;
            Close();
        }
    }
}
