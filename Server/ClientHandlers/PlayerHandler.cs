using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// encapsulate a player handeling
    /// </summary>
    /// <seealso cref="IClientHandler" />
    class PlayerHandler : IClientHandler
    {
        private GameController _gameController;
        
        public PlayerHandler(GameController game)
        {
            _gameController = game;
        }

        /// <summary>
        /// Handles the player.
        /// </summary>
        /// <param name="client">The client.</param>
        public void HandleClient(TcpClient client)
        {
            string output = "";

            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            // read requsets from the client and procces them
            Task read = new Task(() => 
            {
                string input = "";
                string execute = "";
                do
                {
                    try
                    {
                        input = reader.ReadLine();
                        execute = _gameController.ExecuteCommand(input, client);
                    }
                    catch (Exception e) { }
                    // while the requst isn't closing request
                } while (!execute.Equals("close"));
            });

            // send the client the game state until a closing state is reached
            Task write = new Task(() =>
            {
                {
                    do
                    {
                        output = _gameController.getState(client);
                        writer.WriteLine(output);
                        writer.Flush();
                    } while (!output.Equals("close"));

                    stream.Close();
                    reader.Close();
                    writer.Close();
                    client.Close();
                }
            });

            read.Start();
            write.Start();
        }
    }
}
