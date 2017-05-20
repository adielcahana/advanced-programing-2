using System;
using System.ComponentModel;
using ClientGUI.model;
using Ex1;
using MazeLib;
using System.Text;
using System.Windows;

namespace ClientGUI.view_model
{
    public class SinglePlayerViewModel :INotifyPropertyChanged
    {
        private readonly SinglePlayerModel _model;
		public event PropertyChangedEventHandler PropertyChanged;
		private Direction _lastMove;

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

		private StringBuilder _mazeSrl;
		public string MazeSrl
		{
			get
			{
				return _mazeSrl.ToString();
			}
		}

		private int _rows;
		public int Rows
		{
			get { return _rows; }
			set
			{
				_rows = value;
				OnPropertyChanged("Rows");
			}
		}

		private int _cols;
		public int Cols
		{
			get { return _cols; }
			set
			{
				_cols = value;
				OnPropertyChanged("Cols");
			}
		}

		public SinglePlayerViewModel(SinglePlayerModel model)
        {
            _model = model;
			_lastMove = Direction.Right;
			_model.newMaze += new EventHandler<Maze>(delegate (Object sender, Maze e) {
				Rows = e.Rows;
				Cols = e.Cols;
				_mazeSrl = new StringBuilder(e.ToString());
				_mazeSrl[e.InitialPos.Row * (Cols + 2) + e.InitialPos.Col] = '2';
				OnPropertyChanged("MazeSrl");
			});

			_model.PlayerMoved += new EventHandler<Position>(delegate (Object sender, Position e) {
				_mazeSrl = new StringBuilder(((SinglePlayerModel)sender).Maze);
				switch (_lastMove)
				{
					case Direction.Right:
						_mazeSrl[e.Row * (Cols + 2) + e.Col] = '2';
						break;
					case Direction.Left:
						_mazeSrl[e.Row * (Cols + 2) + e.Col] = '3';
						break;
				}
				
				OnPropertyChanged("MazeSrl");
			});
		}

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public void GenerateMaze()
        {
            _model.GenerateMaze();
        }

        public MazeSolution SolveMaze()
        {
            return _model.SolveMaze();
        }

		public void RestartGame()
		{
			_model.RestartGame();
		}

		public void Move(Direction direction)
		{
			if (direction == Direction.Right || direction == Direction.Left)
			{
				_lastMove = direction;
			}
			_model.Move(direction);
		}
    }
}
