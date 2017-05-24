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
        
        private void btnSinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            Menus.SinglePlayerMenu singlePlayer = new Menus.SinglePlayerMenu();
            Close();
            singlePlayer.Show();
        }

        private void btnMultiPlayer_Click(object sender, RoutedEventArgs e)
        {
            Menus.MultiPlayerMenu multiPlayer = new Menus.MultiPlayerMenu();
            Close();
            multiPlayer.Show();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Menus.SettingsMenu settings = new Menus.SettingsMenu();
            Close();
            settings.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Environment.Exit(0);
        }
    }
}
