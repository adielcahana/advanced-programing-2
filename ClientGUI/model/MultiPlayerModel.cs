using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using MazeLib;
using Newtonsoft.Json;
using static Client.Client;

namespace ClientGUI.model
{
    public class MultiPlayerModel
    {
        private Maze _maze;
        private string _mazeName;
        private int _rows;
        private int _cols;
        private int _otherRows;
        private int _otherCols;
        public event EventHandler<Maze> NewMaze;
        public event EventHandler<Position> PlayerMoved;
        private Client.Client _client;
        private int _clientId;

        public MultiPlayerModel()
        {
            _mazeName = "name";
            Rows = Properties.Settings.Default.MazeRows;
            Cols = Properties.Settings.Default.MazeCols;
            _client = new Client.Client();
        }

        public string Maze
        {
            get
            {
                return _maze.ToString();
            }
        }


        public string MazeName
        {
            get
            {
                return _mazeName;
            }
            set
            {
                _mazeName = value;
            }
        }

        public int Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                _rows = value;
            }
        }

        public int Cols
        {
            get
            {
                return _cols;
            }
            set
            {
                _cols = value;
            }
        }
        public int OtherRows
        {
            get
            {
                return _otherRows;
            }
            set
            {
                _otherRows = value;
            }
        }

        public int OtherCols
        {
            get
            {
                return _otherCols;
            }
            set
            {
                _otherCols = value;
            }
        }

        private Position _playerPos;
        public Position PlayerPos
        {
            get
            {
                return _playerPos;
            }
            set
            {
                _playerPos = value;
                PlayerMoved(this, _playerPos);

            }
        }

        private Position _otherPlayerPos;
        public Position OtherPlayerPos
        {
            get
            {
                return _otherPlayerPos;
            }
            set
            {
                _otherPlayerPos = value;
                PlayerMoved(this, _otherPlayerPos);
            }
        }

        public void Move(Direction direction)
        {
            int row = PlayerPos.Row;
            int col = PlayerPos.Col;
            if (IsValidMove(direction))
            {
                switch (direction)
                {
                    case Direction.Up:
                        PlayerPos = new Position(row - 1, col);
                        break;
                    case Direction.Down:
                        PlayerPos = new Position(row + 1, col);
                        break;
                    case Direction.Right:
                        PlayerPos = new Position(row, col + 1);
                        break;
                    case Direction.Left:
                        PlayerPos = new Position(row, col - 1);
                        break;
                    default:
                        throw new Exception("wrond argument in Move");
                }
            }
            else
            {
                PlayerPos = PlayerPos;
            }
        }
        private bool IsValidMove(Direction direction)
        {
            int row = PlayerPos.Row;
            int col = PlayerPos.Col;
            try
            {
                switch (direction)
                {
                    case Direction.Up:
                        return _maze[row - 1, col] == CellType.Free;
                    case Direction.Down:
                        return _maze[row + 1, col] == CellType.Free;
                    case Direction.Right:
                        return _maze[row, col + 1] == CellType.Free;
                    case Direction.Left:
                        return _maze[row, col - 1] == CellType.Free;
                    default:
                        throw new Exception("wrond argument in IsValidMove");
                }
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public void StartGame()
        {
            _client.Initialize();
            string msg = CreateStartMessage();
            _client.Send(msg);
            string answer = _client.Recieve();
            _maze = MazeLib.Maze.FromJSON(answer);
            _playerPos = _maze.InitialPos;
            _clientId = 0;
            new Task(() => Listen()).Start();
            NewMaze(this, _maze);
        }

        public void JoinGame()
        {
            _client.Initialize();
            string msg = CreateJoinMessage();
            _client.Send(msg);
            string answer = _client.Recieve();
            _maze = MazeLib.Maze.FromJSON(answer);
            _playerPos = _maze.InitialPos;
            _clientId = 1;
            new Task(() => Listen()).Start();
            NewMaze(this, _maze);
        }

        public void Listen()
        {
            while (true)
            {
                string answer = _client.Recieve();
                try
                {
                    // check if it's a move
                    Move move = ClientGUI.Move.FromJson(answer);
                    if (move.ClientId != _clientId)
                        Move(move.MoveDirection);
                }
                catch
                {
                    if (answer.Contains("close"))
                    {
                        
                    }
                }
            }
        }

        public string CreateStartMessage()
        {
            return "start " + _mazeName + " " + _rows.ToString() + " " + _cols.ToString();
        }
        public string CreateJoinMessage()
        {
            return "join " + _mazeName;
        }

        public void SendMoveMassege(Direction direction)
        {
            string msg = "play " + direction.ToString();
            _client.Send(msg);
        }

        public List<string> CreateList()
        {
            Client.Client client = new Client.Client();
            client.Initialize();
            client.Send("list");
            string msg = client.Recieve();
            client.Close();
            return JsonConvert.DeserializeObject<List<string>>(msg);
        }
    }
}
