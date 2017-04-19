using Ex1;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface IModel
    {
        Maze GenerateMaze(string name, int row, int col);
        MazeSolution SolveMaze(string name, Algorithm algorithm);
        string NewGame(String name, int rows, int cols, TcpClient player1);
        string JoinGame(String name, TcpClient player2);
        string finishGame(string name, TcpClient client);
    }
}
