using System.Net.Sockets;

namespace Server
{
    internal interface ICommand
    {
        string Execute(string[] args, TcpClient client = null);
    }
}