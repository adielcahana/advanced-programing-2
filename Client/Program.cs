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
                    answer = "";
                    Console.Write("Please enter a command: ");
                    string command = Console.ReadLine();
                    writer.WriteLine(command);
                    writer.Flush();
                    // Get result from server
                    answer = reader.ReadToEnd();
                    Console.Write(answer);
                } while (!answer.Equals("close"));
            }
            client.Close();
        }
    }
}
