using System.Windows;
using ClientGUI.model;

namespace ClientGUI.view.Menus
{
    /// <summary>
    /// Interaction logic for SettingsMenu.xaml
    /// </summary>
    public partial class SettingsMenu : Window
    {
        private SettingViewModel _vm;

        public SettingsMenu()
        {
            InitializeComponent();
            _vm = new SettingViewModel(new SettingsModel());
            DataContext = _vm;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
			new view.MainWindow().Show();
		}

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _vm.SaveSetting();
            Close();
			new view.MainWindow().Show();
		}
    }
}
