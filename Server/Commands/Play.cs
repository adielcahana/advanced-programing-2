using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
namespace Server.Commands
{
    /// <summary>
    /// play command
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    class Play : ICommand
    {
        private string _gameName;
        private GameController _gameController;
        private string Direction {set ; get;}

        /// <summary>
        /// Initializes a new instance of the <see cref="Play"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="gameController">The game controller.</param>
        public Play(string name, GameController gameController)
        {
            _gameName = name;
            _gameController = gameController;
        }

        /// <summary>
        /// exectue the command according the arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client that send the command.</param>
        /// <returns>
        /// the answer to the client
        /// </returns>
        public string Execute(string[] args, TcpClient client = null)
        {
            Direction = args[0];
            _gameController.addMove(Direction, client);
            return null;
        }
    }
}
