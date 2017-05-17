using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;
using MazeLib;

namespace ClientGUI.model
{
    public class SinglePlayerModel
    {
        private string _mazeName;
        private int _rows;
        private int _cols;

        private readonly Client.Client _client;

        public SinglePlayerModel()
        {
            _client = new Client.Client();
            _client.Initialize();
        }

        public Maze GenerateMaze()
        {
            string msg = CreateGenerateMessage();
            _client.Send(msg);
            string answer = _client.Recieve();
            return Maze.FromJSON(msg);
        }

        public string CreateGenerateMessage()
        {
            string msg = "generate";
            msg += " " + _mazeName;
            msg += " " + _rows.ToString();
            msg += " " + _cols.ToString();
            return msg;
        }
    }
}
