using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using MazeLib;

namespace Server
{
    class Controller
    {
        private Dictionary<string, ICommand> commands;
        private Dictionary<string, Game> games;

        private MazeModel model;
        public Controller()
        {
            //model = new Model();
            commands = new Dictionary<string, ICommand>();
            games = new Dictionary<string, Game>();
            //commands.Add("generate", new GenerateMazeCommand(model));
            // more commands...
        }
        public string ExecuteCommand(string commandLine)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return null;
            //return command.Execute(args);
        }

        public string NewGame(String name, Maze maze, TcpClient player1)
        {
            games.Add(name, new Game(name, maze, player1));
            return "waiting for player2";
        }

        public string JoinGame(String name, TcpClient player2)
        {
            Game game;
            if (games.TryGetValue(name, out game))
            {
                game.AddPlayer(player2);
                game.Start();
                return "starting game";
            }
            return "the name: " + name + "does not exist";
        }
    }
}