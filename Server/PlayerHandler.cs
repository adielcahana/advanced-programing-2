using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    class PlayerHandler : IClientHandler
    {
        private GameController _gameController;
        public PlayerHandler(GameController game)
        {
            _gameController = game;
        }

        public void HandleClient(TcpClient client)
        {
            string output = "";
            NetworkStream stream = client.GetStream();
            new Task(() => //get moves
            {
                StreamReader reader = new StreamReader(stream);
                do
                {
                    string input = reader.ReadLine();
                    _gameController.ExecuteCommand(input, client);
                } while (!output.Equals("close"));
            }).Start();

            new Task(() => // send moves
            {
                StreamWriter writer = new StreamWriter(stream);
                do
                {
                    output = _gameController.getState(client);
                    writer.WriteLine(output);
                    writer.Flush();
                } while (!output.Equals("close"));
            }).Start();
        }
    }
}
