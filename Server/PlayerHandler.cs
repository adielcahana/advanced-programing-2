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
                        output = _gameController.getState();
                        writer.Write(output);
                        writer.Flush();
                    } while (!output.Equals("close"));
                }
            }).Start();
        }
    }
}
