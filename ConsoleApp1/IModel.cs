using MazeLib;
using SearchAlgorithmsLib;

namespace Server
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
