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

		public int Rows
		{
			get { return _model.Rows; }
			set
			{
				_model.Rows = value;
				OnPropertyChanged("Rows");
			}
		}

		public int Cols
		{
			get { return _model.Cols; }
			set
			{
				_model.Cols = value;
				OnPropertyChanged("Cols");
			}
		}

		public SinglePlayerViewModel(SinglePlayerModel model)
        {
            _model = model;
			_lastMove = Direction.Right;
			_model.newMaze += new EventHandler<Maze>(delegate (Object sender, Maze e) {
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
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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