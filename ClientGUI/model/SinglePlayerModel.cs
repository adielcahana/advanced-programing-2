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
    public class SinglePlayerModel //: INotifyPropertyChanged
    {
		private Maze _maze;
		private string _mazeName;
        private int _rows;
        private int _cols;
		//public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler<Maze> newMaze;
		public event EventHandler<Position> PlayerMoved;

		public SinglePlayerModel()
        {
            _mazeName = "name";
            Rows = Properties.Settings.Default.MazeRows;
            Cols = Properties.Settings.Default.MazeCols;
        }

		private Position _playerPos;
		public Position PlayerPos
		{
			get
			{
				return _playerPos;
			}
			set
			{
				_playerPos = value;
				PlayerMoved(this, _playerPos);
			}
		}

		public string Maze
		{
			get
			{
				return _maze.ToString();
			}
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
               //NotifyPropertyChanged("MazeName");
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
                //NotifyPropertyChanged("Rows");
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
                //NotifyPropertyChanged("Cols");
            }
        }

		//public void NotifyPropertyChanged(string propName)
		//{
		//	this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		//}

		public void Move(Direction direction)
		{
			int row = PlayerPos.Row;
			int col = PlayerPos.Col;
			if (IsValidMove(direction))
			{
				switch (direction)
				{
					case Direction.Up:
						PlayerPos = new Position(row - 1, col);
						break;
					case Direction.Down:						
						PlayerPos = new Position(row + 1, col);
						break;
					case Direction.Right:
						PlayerPos = new Position(row, col + 1);
						break;
					case Direction.Left:
						PlayerPos = new Position(row, col - 1);
						break;
					default:
						throw new Exception("wrond argument in Move");
				}
			}
			else
			{
				PlayerPos = PlayerPos;
			}
			
		}

		public void RestartGame()
		{
			PlayerPos = _maze.InitialPos;
		}

		private bool IsValidMove(Direction direction)
		{
			int row = PlayerPos.Row;
			int col = PlayerPos.Col;
			try
			{
				switch (direction)
				{
					case Direction.Up:
						return _maze[row - 1, col] == CellType.Free;
					case Direction.Down:
						return _maze[row + 1, col] == CellType.Free;
					case Direction.Right:
						return _maze[row, col + 1] == CellType.Free;
					case Direction.Left:
						return _maze[row, col - 1] == CellType.Free;
					default:
						throw new Exception("wrond argument in IsValidMove");
				}
			}
			catch (IndexOutOfRangeException)
			{
				return false;
			}
		}

		public void GenerateMaze()
        {
			Client.Client client = new Client.Client();
			client.Initialize();
			string msg = CreateGenerateMessage();
            client.Send(msg);
            string answer = client.Recieve();
			client.Close();
			_maze = MazeLib.Maze.FromJSON(answer);
			_playerPos = _maze.InitialPos;
			newMaze(this, _maze);
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
