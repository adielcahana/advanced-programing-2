using System.Windows;
using ClientGUI.model;

namespace ClientGUI.view.Menus
{
	/// <summary>
	/// Interaction logic for SinglePlayer.xaml
	/// </summary>
	public partial class SinglePlayerMenu : Window
	{
		private bool _gameStarted;
		private SinglePlayerViewModel _viewModel;
		public SinglePlayerMenu()
		{
			InitializeComponent();
			_viewModel = new SinglePlayerViewModel(new SinglePlayerModel());
			DataContext = _viewModel;
			_gameStarted = false;
		}

		private void Window_Closed(object sender, System.EventArgs e)
		{
			if (!_gameStarted)
			{
				new view.MainWindow().Show();
			}
		}

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            Games.SinglePlayerGame game = new Games.SinglePlayerGame(_viewModel);
            _viewModel.GenerateMaze();
            _gameStarted = true;
            Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
