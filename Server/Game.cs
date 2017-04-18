using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace Server
{
    class Game
    {
        private TcpClient _player1;
        private TcpClient _player2;
        private Position _player1Position;
        private Position _player2Position;
        private string _name;
        private Maze _maze;

        public Game(string name, Maze maze, TcpClient player1)
        {
            _maze = maze;
            _name = name;
            _player1 = player1;
            _player1Position = maze.InitialPos;

        }

        public void AddPlayer(TcpClient player2)
        {
            _player2 = player2;
            _player2Position = _maze.InitialPos;
        }

        public bool isRunning()
        {
            Position goal = _maze.GoalPos;
            return !(_player1Position.Equals(goal) || _player2Position.Equals(goal));
        }

        private void PlayOneTurn(TcpClient player, Position playerPosition)
        {
            using (NetworkStream stream = player.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string move = reader.ReadLine();
            }
        }

        public void Start()
        {
            new Task(() =>
            {
                {
                    while (isRunning)
                    {
                        PlayOneTurn(_player1, _player1Position);
                        PlayOneTurn(_player2, _player2Position);
                    }
                    if (_player1Position.Equals(_maze.GoalPos))
                    {
                        using (NetworkStream stream = _player1.GetStream())
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine("you won");
                            writer.Flush();
                        }
                    }
                    else
                    {
                        using (NetworkStream stream = _player2.GetStream())
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine("you won");
                            writer.Flush();
                        }
                    }
                }
                _player1.Close();
                _player2.Close();
            }).Start();
        }
    }
}
