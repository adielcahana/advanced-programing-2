using System;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace advanced_programing_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int col = 1000;
            int row = 1000;

            DFSMazeGenerator generator = new DFSMazeGenerator();
            Maze maze = generator.Generate(col, row);

            //string mazeString = maze.ToString();
            //Console.WriteLine(mazeString);

            ISearchable<Position> adapter = new MazeAdapter(maze);
            ISearcher<Position> BfsSearcher = new BestFirstSearch<Position>();
            ISolution<Position> sol = BfsSearcher.Search(adapter);

            int BfsNumOfStases = BfsSearcher.GetNumberOfNodesEvaluated();

            /*StringBuilder sb = new StringBuilder(mazeString);

            foreach (State<Position> s in sol)
            {
                sb[s.Data.Row * (col + 2) + s.Data.Col] = '$';
            }

            Console.WriteLine(sb.ToString());
            */
            ISearcher<Position> DfsSearcher = new DepthFirstSearch<Position>();
            sol = DfsSearcher.Search(adapter);

            int DfsNumOfStases = DfsSearcher.GetNumberOfNodesEvaluated();
            /*
            sb = new StringBuilder(mazeString);

            foreach (State<Position> s in sol)
            {
                sb[s.Data.Row * (col + 2) + s.Data.Col] = '$';
            }

            Console.WriteLine(sb.ToString());
            */
            Console.WriteLine("number of BFS states:" + BfsNumOfStases);
            Console.WriteLine("number of DFS states:" + DfsNumOfStases);

            Console.Read();
        }
    }
}