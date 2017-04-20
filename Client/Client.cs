using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        public void start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            string answer = null;
            string command = null;
            while(true)
            {
                TcpClient client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("You are connected");
                stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                {
                    answer = "";
                    Console.Write("Please enter a command: ");
                    command = Console.ReadLine();
                    writer.WriteLine(command);
                    writer.Flush();
                    // Get result from server
                    answer = reader.ReadLine();
                    Console.WriteLine(answer);
                }
                if (command.Contains("start") || command.Contains("join"))
                {
                    clientMultipleGame(client);
                }
                stream.Close();
                reader.Close();
                writer.Close();
                client.Close();
            };
        }

        private void clientMultipleGame(TcpClient client)
        {
            string answer = "";
            string command = "";
            Console.WriteLine("start multiple game\n");
            new Task(() =>
            {
                do
                {
                    answer = "";
                    answer = reader.ReadLine();
                    Console.WriteLine(answer);
                } while (!answer.Equals("close"));
                }).Start();
            do
            {
                Console.Write("Please enter a command: ");
                command = Console.ReadLine();
                writer.WriteLine(command);
                writer.Flush();
                // Get result from server
            } while (!answer.Equals("close"));
        }
    }
}
