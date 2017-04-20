using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            string answer = null;
            string command = null;
            while(true)
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
                    Console.Write(answer + "\n");
                }
                if (command.Contains("start") || command.Contains("join"))
                {
                    command = clientMultipleGame(client);
                }
                client.Close();
            };
        }

        private static string clientMultipleGame(TcpClient client)
        {
            string answer = null;
            string command = null;
            Console.WriteLine("start multiple game\n");
            //NetworkStream stream = client.GetStream();
            //StreamReader reader = new StreamReader(stream);
            //StreamWriter writer = new StreamWriter(stream);
            new Task(() =>
            {
                do
                {
                    answer = "";
                    answer = reader.ReadToEnd();
                    Console.Write(answer);
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
            stream.Close();
            reader.Close();
            writer.Close();
            return command;
        }
    }
}
