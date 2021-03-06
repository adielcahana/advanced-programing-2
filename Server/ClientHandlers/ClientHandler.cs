﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using MessagingLib;
using Server.Controllers;

namespace Server.ClientHandlers
{
    /// <summary>
    ///     the server main client handler
    /// </summary>
    /// <seealso cref="IClientHandler" />
    internal class ClientHandler : IClientHandler
    {
        private readonly IController _controller;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientHandler" /> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public ClientHandler(IController controller)
        {
            _controller = controller;
        }

        /// <summary>
        ///     Handles the client by answering a single request and closing the connection.
        /// </summary>
        /// <param name="client">The client.</param>
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                NetworkStream stream = client.GetStream();
                MessageReader reader = new MessageReader(new StreamReader(stream));
                MessageWriter writer = new MessageWriter(new StreamWriter(stream));
                {
                    string commandLine = reader.ReadMessage();
                    Console.WriteLine(commandLine);
                    string result = _controller.ExecuteCommand(commandLine, client);
                    writer.WriteMessage(result);
                    /*if the requset was for a new game,
                     * than the client handeling responsability goes to the game,
                     * therefore the connection isb't closed.
                     */

                    if (!(commandLine.Contains("start") || commandLine.Contains("join")))
                    {
                        client.Close();
                        stream.Close();
                        reader.Close();
                        writer.Close();
                    }
                }
            }).Start();
        }
    }
}