using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientGUI.model;
using MazeLib;

namespace ClientGUI.view_model
{
    public class MultiPlayerViewModel
    {
        private readonly MultiPlayerModel _model;
        public event PropertyChangedEventHandler PropertyChanged;
        private Direction _lastMove;

        private List<String> _gamesList;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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

        public int OtherRows
        {
            get { return _model.OtherRows; }
            set
            {
                _model.OtherRows = value;
                OnPropertyChanged("OtherRows");
            }
        }

        public int OtherCols
        {
            get { return _model.OtherCols; }
            set
            {
                _model.OtherCols = value;
                OnPropertyChanged("OtherCols");
            }
        }


        public MultiPlayerViewModel(MultiPlayerModel model)
        {
            _model = model;
            List<string> gamesList = _model.CreateList();
            _lastMove = Direction.Right;
            _model.NewMaze += new EventHandler<Maze>(delegate (Object sender, Maze e) {
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
    }
}
