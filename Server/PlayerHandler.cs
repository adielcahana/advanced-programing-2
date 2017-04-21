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
            string input = "";
            string output = "";
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            Thread thread = null;

            Task read = new Task(() => //get moves
            {
                thread = Thread.CurrentThread;
                do
                {
                    try
                    {
                        input = reader.ReadLine();
                        _gameController.ExecuteCommand(input, client);
                        System.Threading.Thread.Sleep(500);
                    }
                    catch (Exception e) { }
                } while (!output.Equals("close"));
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
                    thread.Abort();
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
