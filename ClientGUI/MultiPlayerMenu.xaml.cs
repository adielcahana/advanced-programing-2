﻿using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MultiPlayerMenu.xaml
    /// </summary>
    public partial class MultiPlayerMenu : Window
    {
        public MultiPlayerMenu()
        {
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
