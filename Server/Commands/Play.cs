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
        private string _gameName;
        private string Direction {set ; get;}

        public Play(string name)
        {
            _gameName = name;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            Direction = args[0];
            return toJSON();
        }

        private string toJSON()
        {
            JObject play = new JObject();
            play["Name"] = _gameName;
            play["Direction"] = Direction;
            return play.ToString();
        }
    }
}
