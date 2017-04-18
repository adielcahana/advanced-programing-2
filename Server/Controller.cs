using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using MazeLib;
using Server.Commands;

namespace Server
{
    class Controller
    {
        private Dictionary<string, ICommand> commands;
        private Dictionary<string, Game> games;

        private MazeModel _model;
        public Controller(MazeModel model)
        {
            this._model = model;
            commands = new Dictionary<string, ICommand>();
            games = new Dictionary<string, Game>();
            commands.Add("generate", new Generate(model));
            commands.Add("solve", new Solve(model));
            commands.Add("start", new Start(model, this));
            commands.Add("join", new Join(this));
            commands.Add("list", new List(model));
            commands.Add("play", new Play(this));
            commands.Add("close", new Close(this));
        }

        public string ExecuteCommand(string commandLine, TcpClient client = null)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }

        public string NewGame(String name, Maze maze, TcpClient player1)
        {
            Game game = new Game(name, maze, player1);
            games.Add(name, game);
            while (game.waitToSecondPlayer())
            {
                System.Threading.Thread.Sleep(10);
            }
            return maze.ToJSON();
        }

        public string JoinGame(String name, TcpClient player2)
        {
            Game game;
            if (games.TryGetValue(name, out game))
            {
                if (!game.waitToSecondPlayer())
                {
                    return "game: " + name + "already play";
                }
                game.AddPlayer(player2);
                game.Start();
                return game._maze.ToJSON();
            }
            return "the name: " + name + "does not exist";
        }

        public string finishGame(string name, TcpClient client)
        {
            Game game;
            if (games.TryGetValue(name, out game))
            {
                game.finishGame(client);
                return "close";
            }
            return "the name: " + name + "does not exist";
        }
    }
}