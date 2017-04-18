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
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
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
                client.Close();
            }).Start();
        }
    }
}
