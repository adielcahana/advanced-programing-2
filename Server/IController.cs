using System.Net.Sockets;

namespace Server
{
    interface IController
    {
        string ExecuteCommand(string commandLine, TcpClient client = null);
    }
}
