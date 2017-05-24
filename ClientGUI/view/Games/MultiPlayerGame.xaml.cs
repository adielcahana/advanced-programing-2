using ClientGUI.view_model;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ClientGUI.view.Menus;
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
            
            Binding binding = new Binding();
            binding.Path = new PropertyPath("Start");
            binding.Source = vm;
            BindingOperations.SetBinding(this, StartProperty, binding);

            binding = new Binding();
            binding.Path = new PropertyPath("Finish");
            binding.Source = vm;
            BindingOperations.SetBinding(this, FinishProperty, binding);

            binding = new Binding();
            binding.Path = new PropertyPath("FinishMessage");
            binding.Source = vm;
            BindingOperations.SetBinding(this, FinishMessageProperty, binding);

            MyBoard.DataContext = _vm;
            OtherBoard.DataContext = _vm;
        }

        public void StartGame()
        {
            Show();
            MyBoard.DrawMaze();
            MyBoard.RefreshMaze();
            OtherBoard.DrawMaze();
            OtherBoard.RefreshMaze();
        }

        public bool Finish
        {
            get { return (bool)GetValue(FinishProperty); }
            set { SetValue(FinishProperty, value); }
        }

        public static readonly DependencyProperty FinishProperty =
            DependencyProperty.Register("Finish", typeof(bool), typeof(MultiPlayerGame), new PropertyMetadata(FinishPropertyChanged));

        public string FinishMessage
        {
            get { return (string)GetValue(FinishMessageProperty); }
            set { SetValue(FinishMessageProperty, value); }
        }

        public static readonly DependencyProperty FinishMessageProperty =
            DependencyProperty.Register("FinishMessage", typeof(string), typeof(MultiPlayerGame));

        public bool Start
        {
            get { return (bool)GetValue(StartProperty); }
            set { SetValue(FinishProperty, value); }
        }

        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(bool), typeof(MultiPlayerGame), new PropertyMetadata(StartPropertyChanged));

        private static void StartPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ((MultiPlayerGame)d).StartGame();
            }
        }

        private static void FinishPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MultiPlayerGame)d).FinishGame();
        }

        private void FinishGame()
        {
            MessageWindow message;
            if (Finish == true && Start == true)
            {
                message = new MessageWindow(FinishMessage);
                message.Ok.Click += delegate (object sender1, RoutedEventArgs e1)
                {
                    Close();
                    message.Hide();
                    new MainWindow().Show();
                    message.Close();
                };
                message.Cancel.Click += delegate (object sender1, RoutedEventArgs e1)
                {
                    message.Close();
                };
                message.Show();
            }
            else if (Finish == true && Start == false)
            {
                Close();
                message = new MessageWindow(FinishMessage);
                message.Ok.Click += delegate (object sender1, RoutedEventArgs e1)
                {
                    message.Hide();
                    new MultiPlayerMenu().Show();
                    message.Close();
                };
                message.Cancel.Click += delegate (object sender1, RoutedEventArgs e1)
                {
                    message.Hide();
                    new MultiPlayerMenu().Show();
                    message.Close();
                };
                message.Show();
            }
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

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow msg = new MessageWindow("are you sure you want to go back to the main menu?");
            msg.Ok.Click += delegate (object sender1, RoutedEventArgs e1)
            {

                msg.Close();
                _vm.FinishGame();
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
