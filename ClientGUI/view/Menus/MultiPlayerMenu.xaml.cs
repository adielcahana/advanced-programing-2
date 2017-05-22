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

        private void ChooseGame_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            MultiPlayerGame game = new MultiPlayerGame(_viewModel);
            _viewModel.StartGame();
            game.Show();
            game.Start();
            _gameStarted = true;
            Close();
        }

        private void JoinGame_Click(object sender, RoutedEventArgs e)
        {
            MultiPlayerGame game = new MultiPlayerGame(_viewModel);
            _viewModel.StartGame();
            game.Show();
            game.Start();
            _gameStarted = true;
            Close();
        }
    }
}
