﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientGUI.model;
using MazeLib;

namespace ClientGUI.view_model
{
    class SinglePlayerViewModel : ClientViewModel
    {
        private readonly SinglePlayerModel _model;
        private string _mazeName;
        private int _rows;
        private int _cols;
        public SinglePlayerViewModel(SinglePlayerModel model)
        {
            _model = model;
        }

        protected override void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public Maze GenerateMaze()
        {
            return _model.GenerateMaze();
        }

        public string MazeName
        {
            get { return _mazeName; }
            set
            {
                if (_mazeName != value)
                {
                    _mazeName = value;
                    OnPropertyChanged("MazeName");
                }
            }
        }

        public int rows
        {
            get { return _rows; }
            set
            {
                if (_rows != value)
                {
                    _rows = value;
                    OnPropertyChanged("rows");
                }
            }
        }

        public int cols
        {
            get { return _rows; }
            set
            {
                if (_cols != value)
                {
                    _cols = value;
                    OnPropertyChanged("cols");
                }
            }
        }
    }
}
