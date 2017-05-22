using System;
using System.Windows;

namespace ClientGUI.view
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
            Menus.SinglePlayerMenu singlePlayer = new Menus.SinglePlayerMenu();
            Close();
			singlePlayer.Show();
		}

        private void MultiPlayerClick(object sender, RoutedEventArgs e)
        {
            Menus.MultiPlayerMenu multiPlayer = new Menus.MultiPlayerMenu();
			Close();
			multiPlayer.Show();
		}

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            Menus.SettingsMenu settings = new Menus.SettingsMenu();
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
