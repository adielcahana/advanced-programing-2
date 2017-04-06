using System;
using System.Text;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace advanced_programing_2
{
    internal class MazeAdapterTest
    {
        private static void Main(string[] args)
        {
            int col = 10;
            int row = 10;

            DFSMazeGenerator generator = new DFSMazeGenerator();
            Maze maze = generator.Generate(col, row);

            string mazeString = maze.ToString();
            Console.WriteLine(mazeString);

            ISearchable<Position> adapter = new MazeAdapter(maze);
            ISearcher<Position> searcher = new BestFirstSearch<Position>();
            ISolution<Position> sol = searcher.Search(adapter);

            StringBuilder sb = new StringBuilder(mazeString);

            foreach (State<Position> s in sol)
                sb[s.Data.Row * (col + 2) + s.Data.Col] = '$';

            Console.WriteLine(sb.ToString());
            Console.Read();
        }
    }
}