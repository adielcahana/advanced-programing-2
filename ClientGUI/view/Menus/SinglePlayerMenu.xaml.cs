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
		private bool _gameStarted;
		private SinglePlayerViewModel _viewModel;
	    private MainWindow _main;
		public SinglePlayerMenu(MainWindow main)
		{
			InitializeComponent();
		    _main = main;
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
				new MainWindow().Show();
			}
		}

		private void StartGame_Click(object sender, RoutedEventArgs e)
		{
			SinglePlayerGame game = new SinglePlayerGame(_viewModel);
			_viewModel.GenerateMaze();
			game.Show();
			game.Start();
			_gameStarted = true;
			Close();
		    _main.Show();
        }
	}
}
