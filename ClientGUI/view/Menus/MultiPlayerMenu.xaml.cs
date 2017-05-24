using System.Windows;
using ClientGUI.model;
using ClientGUI.view.Games;
using ClientGUI.view_model;

namespace ClientGUI.view.Menus
{
    /// <summary>
    /// Interaction logic for MultiPlayerMenu.xaml
    /// </summary>
    public partial class MultiPlayerMenu : Window
    {
        /// <summary>
        /// check if the game start
        /// </summary>
        private bool _gameStarted;
        /// <summary>
        /// The view model
        /// </summary>
        private MultiPlayerViewModel _viewModel;

        /// <summary>
        /// constructor of the <see cref="MultiPlayerMenu"/> class.
        /// </summary>
        public MultiPlayerMenu()
        {
            InitializeComponent();
            _viewModel = new MultiPlayerViewModel(new MultiPlayerModel());
            DataContext = _viewModel;
            _gameStarted = false;
        }

        /// <summary>
        /// Handles the Click event of the btnJoinGame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnJoinGame_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.JoinName != null)
            {
                MultiPlayerGame game = new MultiPlayerGame(_viewModel);
                _viewModel.JoinGame();
                _gameStarted = true;
                Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnStartGame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow msg = new MessageWindow("wait to second player...");
            msg.Cancel.Click += delegate (object sender1, RoutedEventArgs e1)
            {
                _viewModel.FinishGame();
                msg.Close();
            };
            Hide();
            // wait until the second player join
            msg.Show();
            MultiPlayerGame game = new MultiPlayerGame(_viewModel);
            _viewModel.StartGame();
            msg.Close();
            _gameStarted = true;
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new view.MainWindow().Show();
        }
    }
}
