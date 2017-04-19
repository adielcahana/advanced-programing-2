﻿using Server.Commands;
using System.Collections.Generic;

namespace Server
{
    class ServerController : Controller
    {
        private MazeModel _model;
        public ServerController(MazeModel model)
        {
            _model = model;
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new Generate(model));
            commands.Add("solve", new Solve(model));
            commands.Add("start", new Start(model, this));
            commands.Add("join", new Join(model));
            commands.Add("list", new List(model));
        }
    }
}