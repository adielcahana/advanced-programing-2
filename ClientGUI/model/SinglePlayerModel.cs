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

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public Maze GenerateMaze()
        {
			Client.Client client = new Client.Client();
			client.Initialize();
			string msg = CreateGenerateMessage();
            client.Send(msg);
            string answer = client.Recieve();
			client.Close();
            return Maze.FromJSON(answer);
        }

        public string CreateGenerateMessage()
        {
            return "generate " + _mazeName + " " + _rows.ToString() + " " + _cols.ToString();
		}

        public MazeSolution SolveMaze()
        {
			Client.Client client = new Client.Client();
			client.Initialize();
			string msg = CreateSolveMessage();
            client.Send(msg);
            string answer = client.Recieve();
			client.Close();
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
