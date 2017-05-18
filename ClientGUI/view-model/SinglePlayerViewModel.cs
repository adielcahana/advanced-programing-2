using System.ComponentModel;
using ClientGUI.model;
using Ex1;
using MazeLib;
using System.Threading.Tasks;
using System.Text;
using System;

namespace ClientGUI.view_model
{
    public class SinglePlayerViewModel : ClientViewModel, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		private readonly SinglePlayerModel _model;
		public Maze Maze { get; set; }

		public string MazeSrl
		{
			get
			{
				StringBuilder mazeSrl = new StringBuilder(Maze.ToString());
				mazeSrl[PlayerPos.Row * Maze.Cols + PlayerPos.Col + 2 * PlayerPos.Row] = '2';
				return mazeSrl.ToString();
			}
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
				OnPropertyChanged("MazeSrl");
			}
		}

		public string MazeName
		{
			get { return _model.MazeName; }
			set
			{
				if (_model.MazeName != value)
				{
					_model.MazeName = value;
					OnPropertyChanged("MazeName");
				}
			}
		}

		public int Rows
		{
			get { return _model.Rows; }
			set
			{
				if (_model.Rows != value)
				{
					_model.Rows = value;
					OnPropertyChanged("Rows");
				}
			}
		}

		public int Cols
		{
			get { return _model.Cols; }
			set
			{
				if (_model.Cols != value)
				{
					_model.Cols = value;
					OnPropertyChanged("Cols");
				}
			}
		}

		public SinglePlayerViewModel(SinglePlayerModel model)
        {
            _model = model;
        }


		protected void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public void Move(string direction)
		{
			int row = PlayerPos.Row;
			int col = PlayerPos.Col;
			if (direction.Equals("down"))
			{
				if (IsValidMove(row + 1, col))
				{
					PlayerPos = new Position(row + 1, col);
				}
			}
			else if (direction.Equals("up"))
			{
				if (IsValidMove(row - 1, col))
				{
					PlayerPos = new Position(row - 1, col);
				}
			}
			else if (direction.Equals("right"))
			{
				if (IsValidMove(row, col + 1))
				{
					PlayerPos = new Position(row, col + 1);
				}
			}
			else if (direction.Equals("left"))
			{
				if (IsValidMove(row, col - 1))
				{
					PlayerPos = new Position(row, col - 1);
				}
			}
		}

		private bool IsValidMove(int row, int col)
		{
			try
			{
				return Maze[row, col] == CellType.Free;
			}
			catch (IndexOutOfRangeException e)
			{
				return false;
			}
		}

		public void GenerateMaze()
        {
			Maze = _model.GenerateMaze();
			PlayerPos = Maze.InitialPos;
        }

        public MazeSolution SolveMaze()
        {
            return _model.SolveMaze();
        }
    }
}
