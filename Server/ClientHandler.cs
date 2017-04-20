using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    class ClientHandler : IClientHandler
    {
        private IController controller;

        public ClientHandler(IController controller)
        {
            this.controller = controller;
        }

        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);
                {
                    string commandLine = reader.ReadLine();
                    Console.WriteLine(commandLine);
                    string result = controller.ExecuteCommand(commandLine, client);
                    writer.WriteLine(result);
                    writer.Flush();
                    if(!commandLine.Contains("start") && commandLine.Contains("join"))
                    {
                        stream.Close();
                        reader.Close();
                        writer.Close();
                    }
                }
            }).Start();
        }
    }
}
