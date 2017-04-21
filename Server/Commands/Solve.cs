using System.Net.Sockets;
using Ex1;

namespace Server.Commands
{
    /// <summary>
    /// implement solve command
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    internal class Solve : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private readonly IModel model;

        /// <summary>
        /// constructor of the <see cref="Solve"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public Solve(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// exectue solve command according the arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client that send the command.</param>
        /// <returns>
        /// the maze solution
        /// </returns>
        public string Execute(string[] args, TcpClient client = null)
        {
            if (args.Length != 2)
            {
                return "wrong arguments";
            }
            string name = args[0];
            bool type;
            // parse the algorithm and send to model to solve
            if (!bool.TryParse(args[1], out type))
            {
                Algorithm alg = type ? Algorithm.DFS : Algorithm.BFS;
                MazeSolution solution = model.SolveMaze(name, alg);
                return solution.ToJSON();
            }
            return null;
        }
    }
}