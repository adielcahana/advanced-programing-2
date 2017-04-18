using System.Net.Sockets;

namespace Server.Commands
{
    class List : ICommand
    {
        private MazeModel model;

        public List(MazeModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client = null)
        {
            string list = model.CreateList();
        }

    }
}
