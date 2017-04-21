using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    /// <summary>
    /// implement join command
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    class Join : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel _model;

        /// <summary>
        /// constructor of the <see cref="Join"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public Join(IModel model)
        {
            this._model = model;
        }

        /// <summary>
        /// exectue join command according the arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client that send the command.</param>
        /// <returns>
        /// the maze details
        /// </returns>
        public string Execute(string[] args, TcpClient client = null)
        {
            if (args.Length != 1)
            {
                return "wrong arguments";
            }
            string name = args[0];
            return _model.JoinGame(name, client);
        }
    }
}
