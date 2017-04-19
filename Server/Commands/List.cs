using System.Net.Sockets;

namespace Server.Commands
{
    class List : ICommand
    {
        private IModel model;

        public List(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client = null)
        {
            return model.CreateList();
        }

    }
}
