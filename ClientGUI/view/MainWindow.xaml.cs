using System;
using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.CanMinimize;
        }

        private void SinglePlayerClick(object sender, RoutedEventArgs e)
        {
            SinglePlayerMenu singlePlayer = new SinglePlayerMenu();
            Close();
			singlePlayer.Show();
		}

        private void MultiPlayerClick(object sender, RoutedEventArgs e)
        {
            MultiPlayerMenu multiPlayer = new MultiPlayerMenu();
			Close();
			multiPlayer.Show();
		}

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            SettingsMenu settings = new SettingsMenu();
			Close();
			settings.Show();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
            Environment.Exit(0);
        }
    }
}
