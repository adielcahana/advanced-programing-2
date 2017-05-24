using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json;

namespace ClientGUI.model
{
    public class MultiPlayerModel : PlayerModel
    {
        protected readonly MultiPlayerModel _model;
        public event EventHandler<Maze> NewMaze;
        public event EventHandler<Position> PlayerMoved;
        public event EventHandler<Position> OtherPlayerMoved;
        public event EventHandler<string> FinishGame;
        private Client.Client _client;
        private int _clientId;

        private string _joinName;
        public string JoinName
        {
            get
            {
                return _joinName;
            }
            set
            {
                _joinName = value;
            }
        }


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

        public void StartGame()
        {
            _client.Initialize();
            string msg = CreateStartMessage();
            _client.Send(msg);
            string answer = _client.Recieve();
            if (answer.Equals("name: " + MazeName + " alredy taken"))
            {
                FinishGame(this, answer);
            }
            else
            {
                _maze = MazeLib.Maze.FromJSON(answer);
                _playerPos = _maze.InitialPos;
                _otherPlayerPos = _maze.InitialPos;
                _clientId = 0;
                new Task(() => Listen()).Start();
                NewMaze(this, _maze);
            }
        }

        public void JoinGame()
        {
            _client.Initialize();
            string msg = CreateJoinMessage();
            _client.Send(msg);
            string answer = _client.Recieve();
            if (_joinName == null || answer.Equals("the name: " + _joinName + " does not exist"))
            {
                FinishGame(this, "the name: " + _joinName + " does not exist");
            }
            else if(answer.Equals("game: " + _joinName + " is full"))
            {
                FinishGame(this, answer);
            }
            else
            {
                _maze = MazeLib.Maze.FromJSON(answer);
                Rows = _maze.Rows;
                Cols = _maze.Cols;
                _playerPos = _maze.InitialPos;
                _otherPlayerPos = _maze.InitialPos;
                _clientId = 1;
                new Task(() => Listen()).Start();
                NewMaze(this, _maze);

            }
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
                    {
                        PlayerPos = ChangePosition(move.MoveDirection, PlayerPos);
                        if (PlayerPos.Equals(_maze.GoalPos))
                        {
                            CloseGame();
                            FinishGame(this, "You Win!");
                        }
                    }
                    else
                    {
                        OtherPlayerPos = ChangePosition(move.MoveDirection, OtherPlayerPos);
                        if (OtherPlayerPos.Equals(_maze.GoalPos))
                        {
                            FinishGame(this, "You Lose!");
                        }
                    }
                }
                catch
                {
                    if (answer.Contains("close"))
                    {
                        _client.Close();
                        if (!PlayerPos.Equals(_maze.GoalPos) && !OtherPlayerPos.Equals(_maze.GoalPos))
                        {
                            FinishGame(this, "The Game Over!");
                        }
                        break;
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
            return "join " + _joinName;
        }

        public void Move(Direction direction)
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

        public void CloseGame()
        {
            _client.Send("close");
        }
    }
}
