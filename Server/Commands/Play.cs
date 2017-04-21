using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
namespace Server.Commands
{
    /// <summary>
    /// implement play command
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    class Play : ICommand
    {
        /// <summary>
        /// The game name
        /// </summary>
        private string _gameName;
        /// <summary>
        /// The game controller
        /// </summary>
        private GameController _gameController;

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        private string Direction {set ; get;}

        /// <summary>
        /// constructor of the <see cref="Play"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="gameController">The game controller.</param>
        public Play(string name, GameController gameController)
        {
            _gameName = name;
            _gameController = gameController;
        }

        /// <summary>
        /// exectue play command according the arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client that send the command.</param>
        /// <returns>
        /// null
        /// </returns>
        public string Execute(string[] args, TcpClient client = null)
        {
            Direction = args[0];
            _gameController.addMove(Direction, client);
            return null;
        }


    }
}
