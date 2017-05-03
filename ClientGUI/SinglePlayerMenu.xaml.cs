using System.Windows;
using ClientGui;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class SinglePlayerMenu : Window
    {
        private MainWindow _main;
        private bool _gameStarted;
        public SinglePlayerMenu(MainWindow main)
        {
            InitializeComponent();
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
            Game game = new Game(InsertName.Text, int.Parse(InsertRows.Text), int.Parse(InsertCols.Text), _main);
            _gameStarted = true;
            game.Show();
            Close();
        }
    }
}
