using System;
using Ex1;
using MazeLib;

namespace ClientGUI.model
{
    public class SinglePlayerModel : PlayerModel
    {
		public event EventHandler<Maze> NewMaze;
		public event EventHandler<Position> PlayerMoved;

		public SinglePlayerModel()
        {
            _mazeName = "name";
            Rows = Properties.Settings.Default.MazeRows;
            Cols = Properties.Settings.Default.MazeCols;
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

		public void RestartGame()
		{
			PlayerPos = _maze.InitialPos;
		}

		public void GenerateMaze()
        {
			Client.Client client = new Client.Client();
			client.Initialize();
			string msg = CreateGenerateMessage();
            client.Send(msg);
            string answer = client.Recieve();
			client.Close();
			_maze = MazeLib.Maze.FromJSON(answer);
			_playerPos = _maze.InitialPos;
			NewMaze(this, _maze);
		}

        private string CreateGenerateMessage()
        {
            return "generate " + _mazeName + " " + _rows.ToString() + " " + _cols.ToString();
		}

        public MazeSolution SolveMaze()
        {
			Client.Client client = new Client.Client();
			client.Initialize();
			string msg = CreateSolveMessage();
            client.Send(msg);
            string answer = client.Recieve();
			client.Close();
			return MazeSolution.FromJson(answer);
        }

        private string CreateSolveMessage()
        {
            int algorithm = Properties.Settings.Default.SearchAlgorithm;
            return "solve " + _mazeName + " " + algorithm.ToString();
        }
    }
}
