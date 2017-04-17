using System.Net.Sockets;

namespace Server
{
    interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
