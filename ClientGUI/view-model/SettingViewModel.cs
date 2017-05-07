using ClientGUI.view_model;

namespace ClientGUI
{
    class SettingViewModel : ClientViewModel
    {
        private readonly ISettingModel _model;

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
                NotifyPropertyChanged("ServerIP");
            }
        }

        public int ServerPort
        {
            get { return _model.ServerPort; }
            set
            {
                _model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }

        public int MazeRows
        {
            get { return _model.MazeRows; }
            set
            {
                _model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        public int MazeCols
        {
            get { return _model.MazeCols; }
            set
            {
                _model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        public int SearchAlgorithm
        {
            get { return _model.SearchAlgorithm; }
            set
            {
                _model.SearchAlgorithm = value;
                NotifyPropertyChanged("SearchAlgorithm");
            }
        }
        private void NotifyPropertyChanged(string note)
        {

        }

        public void SaveSetting()
        {
            _model.SaveSettings();
        }
    }
}
