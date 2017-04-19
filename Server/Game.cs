using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using MazeLib;
using System.Collections.Generic;
using Server.Commands;
using System.Linq;

namespace Server
{
    class Game : Controller
    {
        private MazeModel _model;
        private Dictionary<string, ICommand> commands;
        private int finish;

        private List<TcpClient> _players;
        private List<Position> _positions;
        private Stack<string> _moves;

        private bool _canContinue;

        private string _name;
        public Maze Maze { get; }

        public Game(string name, Maze maze, MazeModel model)
        {
            _players = new List<TcpClient>();
            _positions = new List<Position>();
            _moves = new Stack<string>();

            _name = name;
            _model = model;

            Maze = maze;
            commands = new Dictionary<string, ICommand>();
            commands.Add("play", new Play(_name));
            commands.Add("close", new Close(model));

            _canContinue = false;
        }

        public string ExecuteCommand(string commandLine, TcpClient client = null)
        {
            string move = base.ExecuteCommand(commandLine, client);
        }

        public void AddPlayer(TcpClient client)
        {
            _players.Add(client);
            _positions.Add(Maze.InitialPos);
            if (_players.Count == 2)
            {
                _canContinue = true;
            }
        }

        public void initialize()
        {
            Player player = new Player(this);
            while (!_canContinue)
            {
                System.Threading.Thread.Sleep(10);
            }
            player.HandleClient(_players[0]);
            player.HandleClient(_players[1]);
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
