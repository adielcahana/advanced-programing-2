using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;
using Ex1;
using MazeLib;

namespace ClientGUI.model
{
    class SinglePlayerModel : INotifyPropertyChanged
    {
        private string _mazeName;
        private int _rows;
        private int _cols;
        public event EventHandler<Maze> newMaze;

        public SinglePlayerModel()
        {
            _mazeName = "name";
            _rows = Properties.Settings.Default.MazeRows;
            _cols = Properties.Settings.Default.MazeCols;
            _client = new Client.Client();
            _client.Initialize();
        }
        public string MazeName
        {
            get
            {
                return _mazeName;
            }
            set
            {
                _mazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }

        public int Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                _rows = value;
                NotifyPropertyChanged("Rows");
            }
        }

        public int Cols
        {
            get
            {
                return _cols;
            }
            set
            {
                _cols = value;
                NotifyPropertyChanged("Cols");
            }
        }

        private readonly Client.Client _client;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public Maze GenerateMaze()
        {
            string msg = CreateGenerateMessage();
            _client.Send(msg);
            string answer = _client.Recieve();
            return Maze.FromJSON(answer);
        }

        public string CreateGenerateMessage()
        {
            string msg = "generate";
            msg += " " + _mazeName;
            msg += " " + _rows.ToString();
            msg += " " + _cols.ToString();
            return msg;
        }

        public MazeSolution SolveMaze()
        {
            string msg = CreateSolveMessage();
            _client.Send(msg);
            string answer = _client.Recieve();
            return MazeSolution.FromJson(answer);
        }

        public string CreateSolveMessage()
        {
            int algorithm = Properties.Settings.Default.SearchAlgorithm;
            string msg = "solve";
            msg += " " + _mazeName;
            msg += " " + algorithm.ToString();
            return msg;
        }

    }
}
