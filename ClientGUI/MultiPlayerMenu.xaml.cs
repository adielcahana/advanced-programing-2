using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MultiPlayerMenu.xaml
    /// </summary>
    public partial class MultiPlayerMenu : Window
    {
        private readonly MainWindow _main;
        public MultiPlayerMenu(MainWindow main)
        {
            _main = main;
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
            _main.Show();
        }
    }
}
