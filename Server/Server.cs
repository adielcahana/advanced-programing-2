using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// the server class, manage all the tcp connection
    /// </summary>
    class Server
    {
        /// <summary>
        /// The port to listen
        /// </summary>
        private int port;
        /// <summary>
        /// The tcp listener
        /// </summary>
        private TcpListener listener;
        /// <summary>
        /// The client handler type
        /// </summary>
        private IClientHandler ch;

        /// <summary>
        /// constructor of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="ch">The client handler.</param>
        public Server(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }

        /// <summary>
        /// Start the server.
        /// </summary>
        public void Start()
        {
            // initialize
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings[0]),
                Int32.Parse(ConfigurationManager.AppSettings[1]));
            listener = new TcpListener(ep);
            listener.Start();
            while (true)
            {
                try
                {
                    // get new connection with client
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Got new connection");
                    // give to the client handler to maanage the communication with the client
                    ch.HandleClient(client);
                }
                catch (SocketException)
                { }
                Console.WriteLine("Connection Handeld");
            }
        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
    }
}
