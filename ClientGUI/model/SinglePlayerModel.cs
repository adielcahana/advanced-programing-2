using System;
using System.Windows;
using ClientGUI.view;
using Ex1;
using MazeLib;

namespace ClientGUI.model
{
    public class SinglePlayerModel : PlayerModel
    {
		public event EventHandler<Maze> NewMaze;
		public event EventHandler<Position> PlayerMoved;

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
            PlayerPos = ChangePosition(direction, PlayerPos);
		    if (PlayerPos.Equals(_maze.GoalPos) )
		    {
		        Finish();
		    }
		}

		public void RestartGame()
		{
			PlayerPos = _maze.InitialPos;
		}

		public void GenerateMaze()
        {
			Client.Client client = new Client.Client(_port, _ip);
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
			Client.Client client = new Client.Client(_port, _ip);
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

        public void Finish()
        {
            MessageWindow win = new MessageWindow("You Win!");
            win.Ok.Click += delegate (object sender1, RoutedEventArgs e1)
            {
                win.Close();
                new MainWindow().Show();
            };
            win.Cancel.Click += delegate (object sender1, RoutedEventArgs e1)
            {
                win.Close();
            };
            win.Show();
        }
    }
}
