using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using ClientGUI.model;
using MazeLib;

namespace ClientGUI.view_model
{
    public class MultiPlayerViewModel : INotifyPropertyChanged
    {
        private readonly MultiPlayerModel _model;
        public event PropertyChangedEventHandler PropertyChanged;
        private Direction _lastMove;
        private Direction _otherLastMove;

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
        
        public string JoinName
        {
            get { return _model.JoinName; }
            set
            {
                _model.JoinName = value;
                OnPropertyChanged("JoinName");
            }
        }

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

        private string _finishMessage;
        public string FinishMessage
        {
            get { return _finishMessage; }
            set
            {
                _finishMessage = value;
                OnPropertyChanged("FinishMessage");
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

        private StringBuilder _otherMazeSrl;
        public string OtherMazeSrl
        {
            get
            {
                if (_otherMazeSrl == null)
                {
                    return null;
                }
                return _otherMazeSrl.ToString();
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
            _otherLastMove = Direction.Right;
            _model.NewMaze += new EventHandler<Maze>(delegate (Object sender, Maze e) {
                if (e != null)
                {
                    _mazeSrl =
                        new StringBuilder(e.ToString()) {[e.InitialPos.Row * (Cols + 2) + e.InitialPos.Col] = '2'};
                    OnPropertyChanged("MazeSrl");
                    _otherMazeSrl =
                        new StringBuilder(e.ToString()) {[e.InitialPos.Row * (Cols + 2) + e.InitialPos.Col] = '2'};
                    OnPropertyChanged("OtherMazeSrl");
                    Start = true;
                }
                else
                {
                    Finish = true;
                }
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
            _model.OtherPlayerMoved += new EventHandler<Position>(delegate (Object sender, Position e) {
                _otherMazeSrl = new StringBuilder(((MultiPlayerModel)sender).Maze);
                switch (_otherLastMove)
                {
                    case Direction.Right:
                        _otherMazeSrl[e.Row * (Cols + 2) + e.Col] = '2';
                        break;
                    case Direction.Left:
                        _otherMazeSrl[e.Row * (Cols + 2) + e.Col] = '3';
                        break;
                }
                OnPropertyChanged("OtherMazeSrl");
            });
            _model.FinishGame += new EventHandler<string>(delegate (Object sender, string e)
            {
                FinishMessage = e;
                Finish = true;
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
            _model.Move(direction);
        }

        public void StartGame()
        {
            _model.StartGame();
        }

        public void JoinGame()
        {
            _model.JoinGame();
        }

        public void FinishGame()
        {
            _model.CloseGame();
        }
    }
}
