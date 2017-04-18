﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Server.Commands;

namespace Server
{
    class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private MazeModel _model;

        public Controller(MazeModel model)
        {
            this._model = model;
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new Generate(model));
            commands.Add("solve", new Solve(model));
            commands.Add("start", new Start(model, this));
            commands.Add("join", new Join(model));
            commands.Add("list", new List(model));
        }

        public string ExecuteCommand(string commandLine, TcpClient client = null)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found\n";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }
    }
}