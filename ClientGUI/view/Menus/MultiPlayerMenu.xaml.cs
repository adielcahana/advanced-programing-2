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

        private void btnJoinGame_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.JoinName != null)
            {
                MultiPlayerGame game = new MultiPlayerGame(_viewModel);
                _viewModel.JoinGame();
                _gameStarted = true;
                Close();
            }
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow msg = new MessageWindow("wait to second player...");
            Hide();
            msg.Cancel.Click += delegate (object sender1, RoutedEventArgs e1)
            {
                _viewModel.FinishGame();
                msg.Close();
            };
            msg.Show();
            MultiPlayerGame game = new MultiPlayerGame(_viewModel);
            _viewModel.StartGame();
            msg.Close();
            _gameStarted = true;
            Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new view.MainWindow().Show();
        }
    }
}
