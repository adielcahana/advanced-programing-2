using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using ClientGUI.model;
using ClientGUI.view;
using Ex1;
using MazeLib;

namespace ClientGUI
{
    public class SinglePlayerViewModel :INotifyPropertyChanged
    {
        private readonly SinglePlayerModel _model;
		public event PropertyChangedEventHandler PropertyChanged;
		private Direction _lastMove;

	    private bool _finish;
		public bool Finish
        {
            get { return _finish; }
            set
            {
	            _finish = value;
				OnPropertyChanged("Finish");
            }
        }

	    private bool _start;
	    public bool Start
	    {
		    get { return _start; }
		    set
		    {
			    _start = value;
				OnPropertyChanged("Start");
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

		private StringBuilder _mazeSrl;
		public string MazeSrl
		{
			get
			{
			    if (_mazeSrl == null)
			    {
			        return null;
			    } 
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
	        _start = false;
	        _finish = false;
			_model.NewMaze += new EventHandler<Maze>(delegate (Object sender, Maze e) {
				if (e != null)
				{
					_mazeSrl = new StringBuilder(e.ToString());
					_mazeSrl[e.InitialPos.Row * (Cols + 2) + e.InitialPos.Col] = '2';
					OnPropertyChanged("MazeSrl");
					Start = true;
				}
				else
				{
					Finish = true;
				}
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

            _model.FinishGame += new EventHandler<string>(delegate(Object sender, string e)
            {
	            Finish = true;
            });
        }

        private void OnPropertyChanged(string name)
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