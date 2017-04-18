using System.Net.Sockets;

namespace Server.Commands
{
    class Close : ICommand
    {
        private MazeModel _model;

        public Close(MazeModel model)
        {
            this._model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            _model.finishGame(name, client);
            return "close";
        }
    }
}
