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
            this.ResizeMode = ResizeMode.CanMinimize;
            InitializeComponent();
        }

        private void SinglePlayerClick(object sender, RoutedEventArgs e)
        {
            SinglePlayerMenu singlePlayer = new SinglePlayerMenu(this);
            Hide();
            singlePlayer.Show();
        }

        private void MultiPlayerClick(object sender, RoutedEventArgs e)
        {
            MultiPlayerMenu multiPlayer = new MultiPlayerMenu(this);
            Hide();
            multiPlayer.Show();
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            SettingsMenu settings = new SettingsMenu(this);
            Hide();
            settings.Show();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
            Environment.Exit(0);
        }
    }
}
