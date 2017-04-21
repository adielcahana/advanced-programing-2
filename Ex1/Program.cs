using System;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1
{
    /// <summary>
    /// main class: create maze and solve it with BFS and DFS algorithms
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // the maze size 100x100
            int col = 100;
            int row = 100;

            // create maze with DFSMazeGenerator
            DFSMazeGenerator generator = new DFSMazeGenerator();
            Maze maze = generator.Generate(col, row);

            // print the maze
            string mazeString = maze.ToString();
            Console.WriteLine(mazeString);

            // adapt the maze and solve it with BFS
            ISearchable<Position> adapter = new MazeAdapter(maze);
            ISearcher<Position> BfsSearcher = new BestFirstSearch<Position>();
            ISolution<Position> sol = BfsSearcher.Search(adapter);

            int BfsNumOfStases = BfsSearcher.GetNumberOfNodesEvaluated();

            // solve the maze with DFS
            ISearcher<Position> DfsSearcher = new DepthFirstSearch<Position>();
            sol = DfsSearcher.Search(adapter);

            int DfsNumOfStases = DfsSearcher.GetNumberOfNodesEvaluated();

            // print the num of evalueted nodes for BFS and DFS
            Console.WriteLine("number of BFS states:" + BfsNumOfStases);
            Console.WriteLine("number of DFS states:" + DfsNumOfStases);

            Console.Read();
        }
    }
}
