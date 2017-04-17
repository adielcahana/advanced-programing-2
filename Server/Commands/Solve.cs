using System.Net.Sockets;
using advanced_programing_2;

namespace Server.Commands
{
    internal class Solve : ICommand
    {
        private readonly MazeModel model;

        public Solve(MazeModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            bool type;
            if (bool.TryParse(args[1], out type))
            {
                Algorithm alg = type ? Algorithm.BFS : Algorithm.DFS;
                MazeSolution solution = model.SolveMaze(name, alg);
                return solution.ToJSON();
            }
            //TODO: handle a case when the parsing fails
            return null;
        }
    }
}