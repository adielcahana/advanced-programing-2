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
        private static Dictionary<string, Move> moves;
        public Play()
        {
            moves = new Dictionary<string, Move>();
           /* moves.Add("up", new Move("up"));
            moves.Add("down", new Move("down"));
            moves.Add("right", new Move("right"));
            moves.Add("leftt", new Move("left"));*/
        }
        public string Execute(string[] args, TcpClient client = null)
        {
            throw new NotImplementedException();
        }
    }
}
