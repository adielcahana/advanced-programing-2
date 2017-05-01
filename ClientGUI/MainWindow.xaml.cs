using System;
using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SinglePlayerMenu _singlePlayer;
        private readonly MultiPlayerMenu _multiPlayer;
        private readonly SettingsMenu _settings;
        public MainWindow()
        {
            _singlePlayer = new SinglePlayerMenu();
            _multiPlayer = new MultiPlayerMenu();
            _settings = new SettingsMenu();
            InitializeComponent();
        }

        private void SinglePlayerClick(object sender, RoutedEventArgs e)
        {
            Hide();
            _singlePlayer.Show();
        }

        private void MultiPlayerClick(object sender, RoutedEventArgs e)
        {
            Hide();
            _multiPlayer.Show();
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            Hide();
            _settings.Show();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
            Environment.Exit(0);
        }
    }
}
