using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    class Player : IClientHandler
    {
        private IController _gameController;
        public Player(IController game)
        {
            _gameController = game;
        }
        public void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            string output = null;

            new Task(() => //get moves
            {
                do
                {
                    string input = reader.ReadLine();
                    _gameController.ExecuteCommand(input, client);
                } while (!output.Equals("close"));
            }).Start();

            new Task(() => // send moves
            {
                {
                    do
                    {
                        output = _gameController.getMessage();
                        writer.Write(output);
                        writer.Flush();
                    } while (!output.Equals("close"));
                }
            }).Start();
        }
    }
}
