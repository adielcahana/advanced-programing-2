using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    class Close : ICommand
    {
        private Controller _controller;

        public Close(Controller controller)
        {
            this._controller = controller;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            _controller.finishGame(name, client);
            return "close";
        }
    }
}
