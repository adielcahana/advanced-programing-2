namespace ClientGUI.model
{
    interface ISettingModel
    {
        string ServerIp { get; set; }
        int ServerPort { get; set; }
        int MazeRows { get; set; }
        int MazeCols { get; set; }
        int SearchAlgorithm { get; set; }
        void SaveSettings();
    }
}
