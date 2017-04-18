using System;
using System.Collections.Generic;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using MazeLib;
using Ex1;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;

namespace Server
{
    internal enum Algorithm
    {
        BFS,
        DFS
    }

    internal class MazeModel
    {
        private readonly ISearcher<Position>[] _algorithms;
        private readonly DFSMazeGenerator _generator;
        private readonly Dictionary<string, Maze> _mazes;

        private readonly Dictionary<string, MazeSolution> _solutions;

        public MazeModel()
        {
            _mazes = new Dictionary<string, Maze>();
            _solutions = new Dictionary<string, MazeSolution>();
            _generator = new DFSMazeGenerator();
            _algorithms = new ISearcher<Position>[2];
            _algorithms[0] = new BestFirstSearch<Position>();
            _algorithms[1] = new DepthFirstSearch<Position>();
        }

        public Maze GenerateMaze(string name, int row, int col)
        {
            Maze maze;
            if (_mazes.TryGetValue(name, out maze))
                return maze;
            maze = _generator.Generate(col, row);
            maze.Name = name;
            _mazes.Add(name, maze);
            return maze;
        }

        public MazeSolution SolveMaze(string name, Algorithm algorithm)
        {
            MazeSolution solution;
            if (_solutions.TryGetValue(name, out solution))
                return solution;
            Maze maze;
            if (_mazes.TryGetValue(name, out maze))
            {
                ISearchable<Position> adapter = new MazeAdapter(maze);
                ISolution<Position> sol = _algorithms[(int) algorithm].Search(adapter);
                solution = new MazeSolution(name, sol, _algorithms[(int) algorithm].GetNumberOfNodesEvaluated());
                _solutions.Add(name, solution);
                return solution;
            }
            Console.WriteLine("the maze: " + name + "does not exist.");
            //TODO: handle a case when the maze does not exist
            return null;
        }

        public string CreateList()
        {
            if(_mazes.Count == 0)
            {
                return "no games avaliable";
            }
            StringBuilder buildList = new StringBuilder();
            foreach (var item in _mazes)
            {
                buildList.Append(item.Key + " ");
            }
            return buildList.ToString();
        }
    }
}