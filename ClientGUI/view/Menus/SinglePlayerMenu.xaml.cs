using System.Windows;
using ClientGUI.model;
using ClientGUI.view_model;

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
		private void Back(object sender, RoutedEventArgs e)
		{
		    new MainWindow().Show();
            Close();
		}

		private void Window_Closed(object sender, System.EventArgs e)
		{
			if (!_gameStarted)
			{
				new view.MainWindow().Show();
			}
		}

		private void StartGame_Click(object sender, RoutedEventArgs e)
		{
			Games.SinglePlayerGame game = new Games.SinglePlayerGame(_viewModel);
			_viewModel.GenerateMaze();
			game.Show();
			game.Start();
			_gameStarted = true;
			Close();
        }
	}
}
