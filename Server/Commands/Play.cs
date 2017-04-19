using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    class Play : ICommand
    {
        private string gameName;
        private string direcion;
        public Play(string name)
        {
            gameName = name;
        }
        public string Execute(string[] args, TcpClient client = null)
        {
            string move = args[0];
            setDirection(move);
            return toJSON();
        }
        
        private void setDirection(string move)
        {
            direcion = move;
        }

        private string toJSON()
        {
            JObject play = new JObject();
            play["Name"] = gameName;
            play["Direction"] = direcion;
            return play.ToString();
        }
    }
}
