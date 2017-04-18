using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using MazeLib;

namespace Server.Commands
{
    class Start : ICommand
    {
        private MazeModel _model;
        private Controller _controller;

        public Start(MazeModel model, Controller controller)
        {
            _model = model;
            _controller = controller;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);

            Maze maze = _model.GenerateMaze(name, rows, cols);
            return _controller.NewGame(name, maze, client);
        }
    }
}
