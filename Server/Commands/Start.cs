﻿using System.Net.Sockets;

namespace Server.Commands
{
    class Start : ICommand
    {
        private IModel _model;

        public Start(IModel model)
        {
            _model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            if(args.Length != 3)
            {
                return "wrong arguments";
            }
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);

            return _model.NewGame(name, rows, cols, client);
        }
    }
}
