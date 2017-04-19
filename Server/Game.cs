using System.Collections.Concurrent;
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

        private List<TcpClient> _players;
        private List<Position> _positions;
        private ConcurrentQueue<Move> _moves;
        private ConcurrentQueue<Move> _changes;
        public static Dictionary<string, Direction> directions;

        private bool _gameFinished;
        private bool _canContinue;

        private string _name;
        public Maze Maze { get; }

        public Game(string name, Maze maze, MazeModel model)
        {
            directions = new Dictionary<string, Direction>();
            directions.Add(Direction.Up.ToString(), Direction.Up);
            directions.Add(Direction.Down.ToString(), Direction.Down);
            directions.Add(Direction.Right.ToString(), Direction.Right);
            directions.Add(Direction.Left.ToString(), Direction.Left);

            _players = new List<TcpClient>();
            _positions = new List<Position>();
            _moves = new ConcurrentQueue<Move>();
            _changes = new ConcurrentQueue<Move>();

            _name = name;
            _model = model;

            Maze = maze;
            commands = new Dictionary<string, ICommand>();
            commands.Add("play", new Play(_name));
            commands.Add("close", new Close(model));

            _canContinue = false;
            _gameFinished = false;
        }

        public override string ExecuteCommand(string commandLine, TcpClient client = null)
        {
            string move = base.ExecuteCommand(commandLine, client);
            _moves.Enqueue(new Move(directions[move], _name, _players.IndexOf(client)));
            return null;
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

        public void Finish(TcpClient player)
        {
            _gameFinished = true;
        }

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

        public void Start()
        {
            new Task(() =>
            {
                Move move;
                while (!_gameFinished)
                {
                    while (_moves.IsEmpty)
                    {
                        System.Threading.Thread.Sleep(10);
                    }
                    if (_moves.TryDequeue(out move))
                    {
                        int row = _positions[move.ClientId].Row;
                        int col = _positions[move.ClientId].Col;
                        switch (move.MoveDirection)
                        {
                            case (Direction.Up):
                                _positions[move.ClientId] = new Position(row - 1, col);
                                break;
                            case (Direction.Down):
                                _positions[move.ClientId] = new Position(row + 1, col);
                                break;
                            case (Direction.Right):
                                _positions[move.ClientId] = new Position(row, col + 1);
                                break;
                            case (Direction.Left):
                                _positions[move.ClientId] = new Position(row, col - 1);
                                break;
                        }
                        _changes.Enqueue(move);
                    }
                    _changes.Enqueue(new Move( Direction.Up , null, -1));
                }
            }).Start();
        }
    }
}
