using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using MazeLib;
using System.Collections.Generic;
using Server.Commands;
using System.Linq;

namespace Server
{
    class Game : IController
    {
        private MazeModel _model;
        private Dictionary<string, ICommand> commands;
        private int finish;

        private int playersInTheGame;

        private TcpClient _player1;
        private TcpClient _player2;

        private Position _player1Position;
        private Position _player2Position;

        private string _name;
        public Maze _maze { get; }

        public Game(string name, Maze maze, TcpClient player1, MazeModel model)
        {
            _model = model;
            commands = new Dictionary<string, ICommand>();
            finish = 0;
            playersInTheGame = 1;
            _player1 = player1;
            _maze = maze;
            _name = name;
            _player1Position = maze.InitialPos;
            commands.Add("play", new Play(_name));
            commands.Add("close", new Close(model));
        }

        public string ExecuteCommand(string commandLine, TcpClient client = null)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found\n";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            // send command to second player
            return command.Execute(args, client);
        }

        public void AddPlayer(TcpClient player2)
        {
            _player2 = player2;
            _player2Position = _maze.InitialPos;
        }

        public bool waitToSecondPlayer()
        {
            return _player2 == null;
        }

        /*public bool isRunning()
        {
            if(finish != 0)
            {
                return false;
            }
            Position goal = _maze.GoalPos;
            return !(_player1Position.Equals(goal) || _player2Position.Equals(goal));
        }*/

        /*public void finishGame(TcpClient player)
        {
            if (player == _player1)
            {
                finish = 1;
            }
            else
            {
                finish = 2;
            }
        }*/

        /*private void Play(TcpClient player, Position playerPosition)
        {
            new Task(() =>
            {
                string move = null;
                using (NetworkStream stream = _player1.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                do
                {
                    move = reader.ReadLine();
                } while (move.Equals("close"));
            }).Start();
            
        }*/

        /*public void Start()
        {
            new Task(() =>
            {
                {
                    while (isRunning())
                    {
                        Play(_player1, _player1Position);
                        Play(_player2, _player2Position);
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
                    else if (_player2Position.Equals(_maze.GoalPos))
                    {
                        using (NetworkStream stream = _player2.GetStream())
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine("you won");
                            writer.Flush();
                        }
                    }
                    else if (finish == 1)
                    {
                        using (NetworkStream stream = _player2.GetStream())
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine("close");
                            writer.Flush();
                        }
                    }
                    else
                    {
                        using (NetworkStream stream = _player1.GetStream())
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine("close");
                            writer.Flush();
                        }
                    }
                }
                _player1.Close();
                _player2.Close();
            }).Start();
        }*/
    }
}
