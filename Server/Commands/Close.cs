using System.Net.Sockets;

namespace Server.Commands
{
    /// <summary>
    /// close game command.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    class Close : ICommand
    {
        private IModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="Close"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public Close(IModel model)
        {
            this._model = model;
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
            if (args.Length != 1)
            {
                return "wrong arguments";
            }
            string name = args[0];
            _model.finishGame(name, client);
            return "close";
        }
    }
}
