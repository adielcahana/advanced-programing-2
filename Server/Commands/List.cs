using System.Net.Sockets;

namespace Server.Commands
{
    /// <summary>
    /// implement list command
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    class List : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        /// <summary>
        /// constructor of the <see cref="List"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public List(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// exectue list command according the arguments.
        /// </summary>
        /// <param name="args">The arguments (not expect to args).</param>
        /// <param name="client">The client that send the command.</param>
        /// <returns>
        /// list of the active games
        /// </returns>
        public string Execute(string[] args, TcpClient client = null)
        {
            return model.CreateList();
        }

    }
}
