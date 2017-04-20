using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace Server
{
    class Move
    {
        public Direction MoveDirection { get; set; }
        private string Name { get; set; }
        public int ClientId { get; set; }
        public static Dictionary<string, Direction> moves;

        public Move(Direction moveDirection, string name, int clientId = -1)
        {
            moves = new Dictionary<string, Direction>();
            moves.Add(Direction.Up.ToString(), Direction.Up);
            moves.Add(Direction.Down.ToString(), Direction.Down);
            moves.Add(Direction.Right.ToString(), Direction.Right);
            moves.Add(Direction.Left.ToString(), Direction.Left);

            MoveDirection = moveDirection;
            ClientId = clientId;
            Name = Name;
        }

        public string ToJSON()
        {
            JObject play = new JObject();
            play["Name"] = Name;
            play["Direction"] = MoveDirection.ToString();
            play["Id"] = ClientId;
            return play.ToString();
        }

        public static Move FromJSON(string str)
        {
            JObject json = JObject.Parse(str);
            string name = (string) json["Name"];
            int clientId = (int) json["Id"];
            return new Move(moves[(string) json["Directon"]], name, clientId);
        }
    }
}
