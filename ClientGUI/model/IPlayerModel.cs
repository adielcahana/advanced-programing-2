using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace ClientGUI.model
{
    interface IPlayerModel
    {
        string MazeName { get; set; }
        int Rows { get; set; }
        int Cols { get; set; }
        string Maze { get;}
        Position ChangePosition(Direction direction, Position playerPosition);
        bool IsValidMove(Direction direction, Position playerPosition);

    }
}
