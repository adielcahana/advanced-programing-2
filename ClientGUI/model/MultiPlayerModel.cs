using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json;

namespace ClientGUI.model
{
	/// <summary>
	/// Multiplayer model
	/// </summary>
	/// <seealso cref="ClientGUI.model.PlayerModel" />
	public class MultiPlayerModel : PlayerModel
	{
		/// <summary>
		/// Occurs when [new maze].
		/// </summary>
		public event EventHandler<Maze> NewMaze;
		/// <summary>
		/// Occurs when [player moved].
		/// </summary>
		public event EventHandler<Position> PlayerMoved;
		/// <summary>
		/// Occurs when [other player moved].
		/// </summary>
		public event EventHandler<Position> OtherPlayerMoved;
		/// <summary>
		/// Occurs when [finish game].
		/// </summary>
		public event EventHandler<string> FinishGame;
		/// <summary>
		/// The client
		/// </summary>
		private Client.Client _client;
		/// <summary>
		/// The client identifier
		/// </summary>
		private int _clientId;
		/// <summary>
		/// The join name from the combo box
		/// </summary>
		private string _joinName;
		/// <summary>
		/// Gets or sets the name of the join.
		/// </summary>
		/// <value>
		/// The name of the join.
		/// </value>
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
		/// <summary>
		/// Initializes a new instance of the <see cref="MultiPlayerModel" /> class.
		/// </summary>
		public MultiPlayerModel()
		{
			_mazeName = "name";
			Rows = Properties.Settings.Default.MazeRows;
			Cols = Properties.Settings.Default.MazeCols;
			_client = new Client.Client(_port, _ip);
		}
		/// <summary>
		/// The player position
		/// </summary>
		private Position _playerPos;
		/// <summary>
		/// Gets or sets the player position.
		/// </summary>
		/// <value>
		/// The player position.
		/// </value>
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
		/// <summary>
		/// The other player position
		/// </summary>
		private Position _otherPlayerPos;
		/// <summary>
		/// Gets or sets the other player position.
		/// </summary>
		/// <value>
		/// The other player position.
		/// </value>
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
		/// <summary>
		/// Starts the game.
		/// </summary>
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
		/// <summary>
		/// Joins the game.
		/// </summary>
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
			else if (answer.Equals("game: " + _joinName + " is full"))
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
		/// <summary>
		/// Listens this instance.
		/// </summary>
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
		/// <summary>
		/// Creates the start message.
		/// </summary>
		/// <returns></returns>
		public string CreateStartMessage()
		{
			return "start " + _mazeName + " " + _rows.ToString() + " " + _cols.ToString();
		}
		/// <summary>
		/// Creates the join message.
		/// </summary>
		/// <returns></returns>
		public string CreateJoinMessage()
		{
			return "join " + _joinName;
		}
		/// <summary>
		/// Moves the specified direction.
		/// </summary>
		/// <param name="direction">The direction.</param>
		public void Move(Direction direction)
		{
			string msg = "play " + direction.ToString();
			_client.Send(msg);
		}
		/// <summary>
		/// Creates the list.
		/// </summary>
		/// <returns></returns>
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
		/// <summary>
		/// Closes the game.
		/// </summary>
		public void CloseGame()
		{
			_client.Send("close");
		}
	}
}
