using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json;

namespace ClientGUI.model
{
    public class MultiPlayerModel : PlayerModel
    {
        public event EventHandler<Maze> NewMaze;
        public event EventHandler<Position> PlayerMoved;
        public event EventHandler<Position> OtherPlayerMoved;
        private Client.Client _client;
        private int _clientId;

        public MultiPlayerModel()
        {
            _mazeName = "name";
            Rows = Properties.Settings.Default.MazeRows;
            Cols = Properties.Settings.Default.MazeCols;
            _client = new Client.Client(_port, _ip);
        }

        private Position _playerPos;
        private Position PlayerPos
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
        private Position OtherPlayerPos
        {
            get
            {
                return _otherPlayerPos;
            }
            set
            {
                _otherPlayerPos = value;
                OtherPlayerMoved(this, _otherPlayerPos);
            }
        }

        public void Move(Direction direction)
        {
            int row = PlayerPos.Row;
            int col = PlayerPos.Col;
            if (IsValidMove(direction, PlayerPos))
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

        public void MoveOther(Direction direction)
        {
            int row = OtherPlayerPos.Row;
            int col = OtherPlayerPos.Col;
            if (IsValidMove(direction, OtherPlayerPos))
            {
                switch (direction)
                {
                    case Direction.Up:
                        OtherPlayerPos = new Position(row - 1, col);
                        break;
                    case Direction.Down:
                        OtherPlayerPos = new Position(row + 1, col);
                        break;
                    case Direction.Right:
                        OtherPlayerPos = new Position(row, col + 1);
                        break;
                    case Direction.Left:
                        OtherPlayerPos = new Position(row, col - 1);
                        break;
                    default:
                        throw new Exception("wrond argument in Move");
                }
            }
            else
            {
                OtherPlayerPos = OtherPlayerPos;
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
            _otherPlayerPos = _maze.InitialPos;
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
            Rows = _maze.Rows;
            Cols = _maze.Cols;
            _playerPos = _maze.InitialPos;
            _otherPlayerPos = _maze.InitialPos;
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
                    if (move.ClientId == _clientId)
                        Move(move.MoveDirection);
                    else
                    {
                        MoveOther(move.MoveDirection);
                    }
                }
                catch
                {
                    if (answer.Contains("close"))
                    {
                        Close();                
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

        public ObservableCollection<string> CreateList()
        {
            Client.Client client = new Client.Client(_port, _ip);
            client.Initialize();
            client.Send("list");
            string answer = client.Recieve();
            client.Close();
            if (!answer.Equals("no games avaliable"))
            {
                return JsonConvert.DeserializeObject<ObservableCollection<string>>(answer);
            }
            return null;
        }

        public void Close()
        {
            _client.Close();
        }
    }
}
