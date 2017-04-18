﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using MazeLib;

namespace Server.Commands
{
    class Start : ICommand
    {
        private MazeModel _model;

        public Start(MazeModel model, Controller controller)
        {
            _model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);

            return _model.NewGame(name, rows, cols, client);
        }
    }
}
