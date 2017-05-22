using ClientGUI.view_model;
using System.Windows;
using System.Windows.Input;
using MazeLib;

namespace ClientGUI.view.Games
{
    /// <summary>
    /// Interaction logic for MultiPlayerGame.xaml
    /// </summary>
    public partial class MultiPlayerGame : Window
    {

        private readonly MultiPlayerViewModel _vm;

        public MultiPlayerGame(MultiPlayerViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
            MyBoard.DataContext = _vm;
            OtherBoard.DataContext = _vm;
        }

        public void Start()
        {
            MyBoard.DrawMaze();
            MyBoard.RefreshMaze();
            OtherBoard.DrawMaze();
            OtherBoard.RefreshMaze();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    _vm.Move(Direction.Down);
                    break;
                case Key.Up:
                    _vm.Move(Direction.Up);
                    break;
                case Key.Right:
                    _vm.Move(Direction.Right);
                    break;
                case Key.Left:
                    _vm.Move(Direction.Left);
                    break;
            }
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow msg = new MessageWindow("are you sure you want to go back to the main menu?");
            msg.Ok.Click += delegate (object sender1, RoutedEventArgs e1)
            {

                msg.Close();
                Close();
                new MainWindow().Show();
            };
            msg.Cancel.Click += delegate (object sender1, RoutedEventArgs e1)
            {
                msg.Close();
                Show();
            };
            Hide();
            msg.Show();
        }
    }
}
