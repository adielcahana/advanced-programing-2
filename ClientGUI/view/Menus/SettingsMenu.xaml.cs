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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Properties.Settings.Default.Reload();
            new MainWindow().Show();
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _vm.SaveSetting();
            Hide();
            new MainWindow().Show();
            Close();
        }
    }
}