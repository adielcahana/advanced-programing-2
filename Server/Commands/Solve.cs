using System.Net.Sockets;
using Ex1;

namespace Server.Commands
{
    internal class Solve : ICommand
    {
        private readonly IModel model;

        public Solve(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            if (args.Length != 2)
            {
                return "wrong arguments";
            }
            string name = args[0];
            bool type;
            if (!bool.TryParse(args[1], out type))
            {
                Algorithm alg = type ? Algorithm.DFS : Algorithm.BFS;
                MazeSolution solution = model.SolveMaze(name, alg);
                return solution.ToJSON();
            }
            //TODO: handle a case when the parsing fails
            return null;
        }
    }
}