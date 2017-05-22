using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using ClientGUI.model;
using MazeLib;

namespace ClientGUI.view_model
{
    public class MultiPlayerViewModel
    {
        private readonly MultiPlayerModel _model;
        public event PropertyChangedEventHandler PropertyChanged;
        private Direction _lastMove;

        private ObservableCollection<String> _gamesList;
        public ObservableCollection<String> GamesList
        {
            get { return _gamesList; }
            set
            {
                if (_gamesList != value)
                {
                    _gamesList = value;
                    OnPropertyChanged("GamesList");
                }
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

        public MultiPlayerViewModel(MultiPlayerModel model)
        {
            _model = model;
            _gamesList = _model.CreateList();
            _lastMove = Direction.Right;
            _model.NewMaze += new EventHandler<Maze>(delegate (Object sender, Maze e) {
	            _mazeSrl = new StringBuilder(e.ToString()) {[e.InitialPos.Row * (Cols + 2) + e.InitialPos.Col] = '2'};
	            OnPropertyChanged("MazeSrl");
            });

            _model.PlayerMoved += new EventHandler<Position>(delegate (Object sender, Position e) {
                _mazeSrl = new StringBuilder(((MultiPlayerModel)sender).Maze);
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

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void Move(Direction direction)
        {
            if (direction == Direction.Right || direction == Direction.Left)
            {
                _lastMove = direction;
            }
            _model.SendMoveMassege(direction);
        }

        public void StartGame()
        {
            _model.StartGame();
        }

        public void JoinGame()
        {
            _model.JoinGame();
        }

        public void Close()
        {
            _model.Close();
        }
    }
}
