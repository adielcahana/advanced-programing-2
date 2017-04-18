using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

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
            return JObject.Parse(list).ToString();
        }
    }
}
