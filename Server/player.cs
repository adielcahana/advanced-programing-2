using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
