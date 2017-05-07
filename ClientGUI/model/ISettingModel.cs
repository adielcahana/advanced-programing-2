using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGUI.view_model
{
    interface ISettingModel
    {
        string ServerIP { get; set; }
        int ServerPort { get; set; }
        int MazeRows { get; set; }
        int MazeCols { get; set; }
        int SearchAlgorithm { get; set; }
        void SaveSettings();
    }
}
