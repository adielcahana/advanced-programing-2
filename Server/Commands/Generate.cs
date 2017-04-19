using System.Net.Sockets;
using MazeLib;

namespace Server.Commands
{
    internal class Generate : ICommand
    {
        private readonly IModel model;

        public Generate(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);

            Maze maze = model.GenerateMaze(name, rows, cols);
            if(maze == null)
            {
                return "name: " + name + " already taken\n";
            }
            return maze.ToJSON();
        }
    }
}