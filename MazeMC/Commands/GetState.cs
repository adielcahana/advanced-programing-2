using System.Net.Sockets;
using MazeMC.Models;

namespace MazeMC.Commands
{
    class GetState : ICommand
    {
        private IModel _model;

        public GetState(IModel model)
        {
            _model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            //args[0] == game name
            return _model.GetState(args[0], client);
        }
    }
}
