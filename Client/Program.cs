using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            string answer = null;
            string command = null;
            do
            {
                TcpClient client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("You are connected");
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    answer = "";
                    Console.Write("Please enter a command: ");
                    command = Console.ReadLine();
                    writer.WriteLine(command);
                    writer.Flush();
                    // Get result from server
                    answer = reader.ReadToEnd();
                    Console.Write(answer);
                }
                if (command.Equals("start") || command.Equals("join"))
                {
                    command = clientMultipleGame(client);
                }
                client.Close();
            } while (!answer.Equals("close"));
        }

        private static string clientMultipleGame(TcpClient client)
        {
            string answer = null;
            string command = null;
            do
            {
                Console.WriteLine("start multiple game\n");
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    answer = "";
                    Console.Write("Please enter a command: ");
                    command = Console.ReadLine();
                    writer.WriteLine(command);
                    writer.Flush();
                    // Get result from server
                    answer = reader.ReadToEnd();
                    Console.Write(answer);
                }
            } while (!answer.Equals("close"));
            return command;
        }
    }
}
