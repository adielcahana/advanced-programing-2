﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for SettingsMenu.xaml
    /// </summary>
    public partial class SettingsMenu : Window
    {
        private readonly MainWindow _main;

        public SettingsMenu(MainWindow main)
        {
            InitializeComponent();
            _main = main;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
            _main.Show();
        }
    }
}
