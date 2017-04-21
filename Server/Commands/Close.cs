using System.Net.Sockets;

namespace Server.Commands
{
    /// <summary>
    /// implement close command
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    class Close : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel _model;

        /// <summary>
        /// constructor of the <see cref="Close"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public Close(IModel model)
        {
            this._model = model;
        }

        /// <summary>
        /// exectue close command according the arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client that send the command.</param>
        /// <returns>
        /// string of close
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
