using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
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
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            Task read = new Task(() => //get moves
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
                } while (!execute.Equals("close"));
            });

            Task write = new Task(() => // send moves
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
