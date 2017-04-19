using System;
using System.Collections.Generic;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using MazeLib;
using Ex1;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace Server
{
    internal enum Algorithm
    {
        BFS,
        DFS
    }

    internal class MazeModel : IModel
    {
        private readonly ISearcher<Position>[] _algorithms;
        private readonly DFSMazeGenerator _generator;
        private readonly Dictionary<string, Maze> _mazes;
        private Dictionary<string, Game> _games;

        private readonly Dictionary<string, MazeSolution> _solutions;

        public MazeModel()
        {
            _mazes = new Dictionary<string, Maze>();
            _games = new Dictionary<string, Game>();
            _solutions = new Dictionary<string, MazeSolution>();
            _generator = new DFSMazeGenerator();
            _algorithms = new ISearcher<Position>[2];
            _algorithms[0] = new BestFirstSearch<Position>();
            _algorithms[1] = new DepthFirstSearch<Position>();
        }

        public Maze GenerateMaze(string name, int row, int col)
        {
            if (_mazes.ContainsKey(name))
                return null;
            Maze maze;
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
            if(_games.Count == 0)
            {
                return "no games avaliable\n";
            }
            List<string> names = new List<string>(_games.Keys.Count);
            foreach (string name in _games.Keys)
            {
                names.Add(name);
            }   
            return JsonConvert.SerializeObject(names, Formatting.Indented);
        }

        public string NewGame(String name, int rows, int cols, TcpClient player1)
        {
            Maze maze = new Maze(rows, cols);
            Game game = new Game(name, maze, this);
            game.AddPlayer(player1);
            _games.Add(name, game);

            game.initialize();
            game.Start();

            return maze.ToJSON();
        }

        public string JoinGame(String name, TcpClient player2)
        {
            Game game;
            if (_games.TryGetValue(name, out game))
            {
                game.AddPlayer(player2);
                return game.Maze.ToJSON();
            }
            return "the name: " + name + "does not exist";
        }

        public void finishGame(string name, TcpClient client)
        {
            Game game;
            _games[name].Finish();
            _games.Remove(name);
        }
    }
}
