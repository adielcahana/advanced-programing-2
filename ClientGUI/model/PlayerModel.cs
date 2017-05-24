using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace ClientGUI.model
{
    public abstract class PlayerModel : IPlayerModel
    {
        protected string _mazeName;
        protected int _rows;
        protected int _cols;
        protected Maze _maze;
        protected string _ip;
        protected int _port;

        public PlayerModel()
        {
            _ip = Properties.Settings.Default.ServerIP;
            _port = Properties.Settings.Default.ServerPort;
            _mazeName = "name";
            Rows = Properties.Settings.Default.MazeRows;
            Cols = Properties.Settings.Default.MazeCols;
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
        public string Maze
        {
            get
            {
                return _maze.ToString();
            }
        }

        public Position ChangePosition(Direction direction, Position playerPosition)
        {
            int row = playerPosition.Row;
            int col = playerPosition.Col;
            if (IsValidMove(direction, playerPosition))
            {
                switch (direction)
                {
                    case Direction.Up:
                        playerPosition = new Position(row - 1, col);
                        break;
                    case Direction.Down:
                        playerPosition = new Position(row + 1, col);
                        break;
                    case Direction.Right:
                        playerPosition = new Position(row, col + 1);
                        break;
                    case Direction.Left:
                        playerPosition = new Position(row, col - 1);
                        break;
                    default:
                        throw new Exception("wrond argument in Move");
                }
            }
            else
            {
                playerPosition = playerPosition;
            }
            return playerPosition;
        }

        public bool IsValidMove(Direction direction, Position playerPosition)
        {
            int row = playerPosition.Row;
            int col = playerPosition.Col;
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

    }
}
