using System.ComponentModel;
using ClientGUI.model;
using Ex1;
using MazeLib;

namespace ClientGUI.view_model
{
    class SinglePlayerViewModel : ClientViewModel
    {
        private readonly SinglePlayerModel _model;

        public SinglePlayerViewModel(SinglePlayerModel model)
        {
            _model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public Maze GenerateMaze()
        {
            return _model.GenerateMaze();
        }

        public MazeSolution SolveMaze()
        {
            return _model.SolveMaze();
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
                    OnPropertyChanged("rows");
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
                    OnPropertyChanged("cols");
                }
            }
        }
    }
}
