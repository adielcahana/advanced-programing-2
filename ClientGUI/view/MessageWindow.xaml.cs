using System.Windows;

namespace ClientGUI.view
{
    /// <summary>
    /// Interaction logic for Error.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
		public MessageWindow(string msg)
        {
            InitializeComponent();
			Message.Text = msg;
        }
    }
}
