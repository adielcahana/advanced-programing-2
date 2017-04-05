using System;
using System.Text;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace advanced_programing_2
{
    class MazeAdapterTest
    {
        static void Main(string[] args)
        {
            int col = 10;
            int row = 10;
            DFSMazeGenerator generator = new DFSMazeGenerator();
            Maze maze = generator.Generate(col, row);
            ISearchable<Position> adapter = new MazeAdapter(maze);
            ISearcher<Position> Searcher = new BestFirstSearch<Position>();
            ISolution<Position> sol = Searcher.Search(adapter);
            string maze_string = maze.ToString();

            StringBuilder sb = new StringBuilder();
            State<Position> state = sol.Get();
            for (int i = 0; i < maze_string.Length; i++)
            {
                if (state.Data.Row * (col + 1) + state.Data.Col == i)
                {
                    sb.Append('$');
                }
                else
                {
                    sb.Append(maze_string[i]);
                }
            }
            Console.WriteLine(maze_string);
            Console.WriteLine(sb.ToString());
        }
    }
}
