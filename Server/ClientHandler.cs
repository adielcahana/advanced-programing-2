using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    class ClientHandler : IClientHandler
    {
        private Controller controller;

        public ClientHandler(Controller controller)
        {
            this.controller = controller;
        }

        public void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            new Task(() =>
            {
                {
                    while (true)
                    {
                        string commandLine = reader.ReadLine();
                        Console.WriteLine(commandLine);
                        string result = Console.ReadLine();
//                        controller.ExecuteCommand(commandLine);
                        writer.WriteLine(result);
                        writer.Flush();
                        if (result.Equals("close"))
                        {
                            break;
                        }
                    }
                }
            }).Start();
                stream.Close();
                reader.Close();
                writer.Close();
                client.Close();
        }

        private void CloseConnection(TcpClient client)
        {
            client.Close();
        }
    }
}
