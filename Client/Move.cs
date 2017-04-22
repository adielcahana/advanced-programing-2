using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace Client
{
    /// <summary>
    /// gives serialization abilities to move msg
    /// </summary>
    class Move
    {
        public Direction MoveDirection { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        /// <summary>
        /// moves dictionary from string to Direction
        /// </summary>
        public static Dictionary<string, Direction> moves;

        /// <summary>
        /// Initializes a new instance of the <see cref="Move"/> class.
        /// </summary>
        /// <param name="moveDirection">The move direction.</param>
        /// <param name="name">The name.</param>
        /// <param name="clientId">The client identifier.</param>
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

        /// <summary>
        /// serialize Move obj to json string.
        /// </summary>
        /// <returns>
        /// json represantation of Move.
        /// </returns>
        public string ToJSON()
        {
            JObject play = new JObject();
            play["Name"] = Name;
            play["Direction"] = MoveDirection.ToString();
            play["ID"] = ClientId;
            return play.ToString();
        }
        /// <summary>
        /// deserialize Move From json string.
        /// </summary>
        /// <param name="str">json represantation of MazeSolution.</param>
        /// <returns>
        /// Move object
        /// </returns>
        public static Move FromJSON(string str)
        {
            JObject json = JObject.Parse(str);
            string name = (string) json["Name"];
            return new Move(moves[(string) json["Directon"]], name);
        }
    }
}
