using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// interface of the controller
    /// </summary>
    interface IController
    {
        /// <summary>
        /// exectue methods according the arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client that send the command.</param>
        /// <returns>
        /// the answer of the execute </returns>
        string ExecuteCommand(string commandLine, TcpClient client = null);
    }
}
