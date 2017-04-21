using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    class Join : ICommand
    {
        private IModel _model;

        public Join(IModel model)
        {
            this._model = model;
        }
        public string Execute(string[] args, TcpClient client = null)
        {
            if (args.Length != 1)
            {
                return "wrong arguments";
            }
            string name = args[0];
            return _model.JoinGame(name, client);
        }
    }
}
