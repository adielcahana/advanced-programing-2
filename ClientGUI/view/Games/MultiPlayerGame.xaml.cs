using ClientGUI.view_model;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MazeLib;

namespace ClientGUI.view.Games
{

    /// <summary>
    /// Interaction logic for MultiPlayerGame.xaml
    /// </summary>
    public partial class MultiPlayerGame : Window
    {

        public MultiPlayerViewModel _vm;
        private DispatcherTimer _timer;
        public MultiPlayerGame(MultiPlayerViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            _timer = null;
            DataContext = _vm;
            MyBoard.DataContext = _vm;
            OtherBoard.DataContext = _vm;
        }

        public void Start()
        {
            MyBoard.DrawMaze();
            OtherBoard.DrawMaze();
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
            new MainWindow().Show();
            Close();
        }
    }
}
