using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Server
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        private Controller controller;

        public Server(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }

        public void Start()
        {
            IPEndPoint ep = new
            IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            listener.Start();
            while (true)
            {
                Console.WriteLine("Waiting for connections...");
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Got new connection");
                    ch.HandleClient(client);
                }
                catch (SocketException)
                { }
                Console.WriteLine("Connection Handeld");
            }
            Console.WriteLine("Server stopped");
            Console.ReadLine();
        }

        public void Stop()
        {
            listener.Stop();
        }
    }
}
