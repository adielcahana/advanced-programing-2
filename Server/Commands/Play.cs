using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
namespace Server.Commands
{
    class Play : ICommand
    {
        private string _gameName;
        private GameController _gameController;
        private string Direction {set ; get;}

        public Play(string name, GameController gameController)
        {
            _gameName = name;
            _gameController = gameController;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            Direction = args[0];
            _gameController.addMove(Direction, client);
            return null;
        }


    }
}
