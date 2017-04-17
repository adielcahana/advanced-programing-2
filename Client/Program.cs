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
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string answer = null;
                do {
                    Console.Write("Please enter a command: ");
                    string command = Console.ReadLine();
                    writer.Write(command);
                    // Get result from server
                    answer = reader.ReadLine();
                    Console.WriteLine(answer);
                } while (answer.Equals("close"));
            }
            client.Close();
        }
    }
}
