using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class SinglePlayerMenu : Window
    {
        public SinglePlayerMenu()
        {
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
