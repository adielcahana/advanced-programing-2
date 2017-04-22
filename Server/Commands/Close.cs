using System.Net.Sockets;

namespace Server.Commands
{
    class Close : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel _model;

        public Close(IModel model)
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
            _model.finishGame(name, client);
            return "close";
        }
    }
}
