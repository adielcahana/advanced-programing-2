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
            Name = name;
        }

        public string ToJSON()
        {
            JObject play = new JObject();
            play["Name"] = Name;
            play["Direction"] = MoveDirection.ToString();
            play["ID"] = ClientId;
            return play.ToString();
        }

        public string moveToJSON()
        {
            JObject play = new JObject();
            play["Name"] = Name;
            play["Direction"] = MoveDirection.ToString();
            return play.ToString();
        }

        public static Move FromJSON(string str)
        {
            moves = new Dictionary<string, Direction>();
            moves.Add(Direction.Up.ToString(), Direction.Up);
            moves.Add(Direction.Down.ToString(), Direction.Down);
            moves.Add(Direction.Right.ToString(), Direction.Right);
            moves.Add(Direction.Left.ToString(), Direction.Left);
            JObject json = JObject.Parse(str);
            string name = (string) json["Name"];
            int clientId = (int) json["ID"];
            Direction direction = moves[(string)json["Direction"]];
            return new Move(direction, name, clientId);
        }
    }
}
