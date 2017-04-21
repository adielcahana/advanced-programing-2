using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// interface of the client handler
    /// </summary>
    interface IClientHandler
    {
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        void HandleClient(TcpClient client);
    }
}
