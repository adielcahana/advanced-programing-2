using ClientGUI.view_model;
using System.ComponentModel;

namespace ClientGUI
{
    class SettingViewModel : INotifyPropertyChanged
	{
        private readonly ISettingModel _model;

		public event PropertyChangedEventHandler PropertyChanged;

		public SettingViewModel(ISettingModel model)
        {
            _model = model;
        }
        public string ServerIP
        {
            get { return _model.ServerIP; }
            set
            {
               _model.ServerIP = value;
                OnPropertyChanged("ServerIP");
            }
        }

        public int ServerPort
        {
            get { return _model.ServerPort; }
            set
            {
                _model.ServerPort = value;
                OnPropertyChanged("ServerPort");
            }
        }

        public int MazeRows
        {
            get { return _model.MazeRows; }
            set
            {
                _model.MazeRows = value;
                OnPropertyChanged("MazeRows");
            }
        }

        public int MazeCols
        {
            get { return _model.MazeCols; }
            set
            {
                _model.MazeCols = value;
                OnPropertyChanged("MazeCols");
            }
        }

        public int SearchAlgorithm
        {
            get { return _model.SearchAlgorithm; }
            set
            {
                _model.SearchAlgorithm = value;
                OnPropertyChanged("SearchAlgorithm");
            }
        }

		protected void OnPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		public void SaveSetting()
        {
            _model.SaveSettings();
        }
    }
}
