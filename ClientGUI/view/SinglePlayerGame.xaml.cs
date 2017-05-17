using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClientGUI.view_model;
using MazeLib;

namespace ClientGUI.view
{
    /// <summary>
    /// Interaction logic for SinglePlayerGame.xaml
    /// </summary>
    public partial class SinglePlayerGame : Window
    {
        public SinglePlayerViewModel _vm;

        public SinglePlayerGame(SinglePlayerViewModel vm)
        {
            _vm = vm;
            DataContext = _vm;
            InitializeComponent();
        }
    }
}
