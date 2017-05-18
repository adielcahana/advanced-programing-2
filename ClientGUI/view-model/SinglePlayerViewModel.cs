using System.ComponentModel;
using ClientGUI.model;
using Ex1;
using MazeLib;
using System.Threading.Tasks;
using System.Text;

namespace ClientGUI.view_model
{
    public class SinglePlayerViewModel : ClientViewModel
    {
		public event PropertyChangedEventHandler NewMaze;
		private readonly SinglePlayerModel _model;
		private Maze _maze;
		public Maze Maze
		{
			get
			{
				return _maze;
			}
			private set
			{
				_maze = value;
			}
		}

		private string _mazeSrl;
		public string MazeSrl
		{
			get
			{
				return _mazeSrl;
			}
			set
			{
				StringBuilder mazeSrl = new StringBuilder(Maze.ToString());
				mazeSrl[PlayerPos.Row * Maze.Cols + PlayerPos.Col + 2 * PlayerPos.Row] = '2';
				_mazeSrl = mazeSrl.ToString();
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
				MazeSrl = "defualt";
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
					//OnPropertyChanged("MazeName");
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
					//OnPropertyChanged("rows");
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
					//OnPropertyChanged("cols");
				}
			}
		}

		public SinglePlayerViewModel(SinglePlayerModel model)
        {
            _model = model;
        }

		protected void OnPropertyChanged(string name)
		{
			NewMaze?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public void GenerateMaze()
        {
			//new Task(() =>
			//{
				Maze = _model.GenerateMaze();
				PlayerPos = Maze.InitialPos;
				OnPropertyChanged("maze");
			//}).Start();
        }

        public MazeSolution SolveMaze()
        {
            return _model.SolveMaze();
        }
    }
}
