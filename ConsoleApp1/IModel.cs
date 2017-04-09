using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace ConsoleApp1
{
    enum Algorithm
    {
        BFS = 0, DFS = 1
    }

    interface IModel
    {
        Maze GenerateMaze(string name, int row, int col);
        ISolution<T> SolveMaze<T>(string name, Algorithm algorithm);
    }
}
