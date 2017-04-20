﻿using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading.Tasks;
using MazeLib;
using System.Collections.Generic;
using Server.Commands;

namespace Server
{
    class GameController : Controller
    {
        private MazeModel _model;

        private List<TcpClient> _players;
        private List<Position> _positions;
        private ConcurrentQueue<Move> _moves;
        private ConcurrentQueue<Move> _changes;
        public static Dictionary<string, Direction> directions;

        private bool _gameFinished;
        private bool _canContinue;
        private int lastReaderIndex;

        private string _name;
        public Maze Maze { get; }

        public GameController(string name, Maze maze, MazeModel model)
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
            commands.Add("play", new Play(_name, this));
            commands.Add("close", new Close(model));

            _canContinue = false;
            _gameFinished = false;
            lastReaderIndex = -1;
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
            PlayerHandler player = new PlayerHandler(this);
            while (!_canContinue)
            {
                System.Threading.Thread.Sleep(10);
            }
            player.HandleClient(_players[0]);
            player.HandleClient(_players[1]);
        }

        public void addMove(string direction, TcpClient client)
        {
            Direction dir;
            directions.TryGetValue(direction, out dir);
            int clientID = 0;
            if (client == _players[0])
            {
                clientID = 0;
            }
            else
            {
                clientID = 1;
            }
            _moves.Enqueue(new Move(dir, _name, clientID));
        }

        public string getState(TcpClient playerClient)
        {
            int indexOfClient = _players.IndexOf(playerClient);
            while (_changes.Count == 0 || lastReaderIndex == indexOfClient)
            {
                System.Threading.Thread.Sleep(10);
            }

            Move move;
            lock (this)
            {
                if (lastReaderIndex == -1)
                {
                    _changes.TryPeek(out move);
                    lastReaderIndex = indexOfClient;
                }
                else
                {
                    _changes.TryDequeue(out move);
                    lastReaderIndex = -1;
                }
            }

            if (move.ClientId == -1)
            {
                return "close";
            }

            return move.ToJSON();
        }

        public void Finish(TcpClient player)
        {
            _gameFinished = true;
        }

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
                    _changes.Enqueue(new Move(Direction.Up, null, -1));
                }
            }).Start();
        }
    }
}
