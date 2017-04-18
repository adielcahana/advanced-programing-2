using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
            new Task(() => //get moves
            {
                {
                    string commandLine = reader.ReadLine();
                    _gameController.ExecuteCommand(commandLine, client);
                }
            }).Start();
            new Task(() => // send moves
            {
                {
                    string commandLine = writer.WriteLine("fssfsfsfffff");
                    Console.WriteLine(commandLine);
                    string result = _gameController.ExecuteCommand(commandLine, client);
                    writer.Write(result);
                    writer.Flush();
                }
            }).Start();
        }
    }
}
