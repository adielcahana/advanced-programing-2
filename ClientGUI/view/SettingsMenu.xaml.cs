using System.Windows;
using ClientGUI.view_model;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for SettingsMenu.xaml
    /// </summary>
    public partial class SettingsMenu : Window
    {
        private readonly MainWindow _main;
        private SettingViewModel _vm;

        public SettingsMenu(MainWindow main)
        {
            _vm = new SettingViewModel(new ApplicationSettingsModel()); 
            InitializeComponent();
            _main = main;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
            _main.Show();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _vm.SaveSetting();
        }
    }
}
