using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using MazeAdapterLib;
using MazeGeneratorLib;
using MazeLib;
using Newtonsoft.Json;
using MazeMC.Controllers;
using SearchAlgorithmsLib;
using SearchAlgorithmsLib.Algorithms;

namespace MazeMC.Models
{
	public class MultiplayerModel
	{
		public delegate void OnGameStart(string player1, string player2);
		public event OnGameStart GameStart;
		public delegate void OnNewState(string name, string player1, string player2);
		public event OnNewState NewState;
		private readonly DFSMazeGenerator _generator;

		/// <summary>
		///     The mazes cache
		/// </summary>
		private readonly Dictionary<string, Maze> _mazes;

		/// <summary>
		///     The solutions cache
		/// </summary>
		private readonly Dictionary<string, MazeSolution> _solutions;

		/// <summary>
		///     The multiplayer games cache
		/// </summary>
		private readonly Dictionary<string, Game> _games;

		public MultiplayerModel()
		{
			_mazes = new Dictionary<string, Maze>();
			_games = new Dictionary<string, Game>();
			_solutions = new Dictionary<string, MazeSolution>();
			_generator = new DFSMazeGenerator();
		}

		/// <summary>
		///     Creates list of active game.
		/// </summary>
		/// <returns>
		///     list of games names
		/// </returns>
		public string CreateList()
		{

			//if (_games.Count == 0)
			//   return "no games avaliable";
			List<string> names = new List<string>(_games.Keys.Count);
			foreach (string name in _games.Keys)
			{
				if (!_games[name].IsStarted()) names.Add(name);
			}
			names.Add("game1");
			names.Add("game2");
			names.Add("game3");

			return JsonConvert.SerializeObject(names, Formatting.Indented);
		}

		/// <summary>
		///     create new game.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="rows">The rows.</param>
		/// <param name="cols">The cols.</param>
		/// <param name="playerId">clientId.</param>
		/// <returns>
		///     the maze detailes
		/// </returns>
		public Maze NewGame(string name, int rows, int cols, string playerId)
		{
			// check if the game exist
			if (_games.ContainsKey(name))
				return null;
			// else create new game
			Maze maze = _generator.Generate(rows, cols);
			maze.Name = name;
			Game game = new Game(maze, this);
			game.NewState += new Game.OnNewState(delegate (string gameName, string player1, string player2)
			{
				NewState(gameName, player1, player2);
			});
			game.GameStart += new Game.OnGameStart(delegate (string player1, string player2)
			{
				GameStart(player1, player2);
			});
			_games.Add(name, game);
			game.AddPlayer(playerId);
			//game.Initialize();
			game.Start();
			return maze;
		}

		/// <summary>
		///     player 2 join the game.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="player2">The player2.</param>
		/// <returns>
		///     the maze detailes
		/// </returns>
		public Maze JoinGame(string name, string player2)
		{
			Game game;
			if (_games.TryGetValue(name, out game))
			{
				if (game.IsStarted())
				{
					return null;
				}
				game.AddPlayer(player2);
				return game.Maze;
			}
			return null;
		}

		/// <summary>
		///     notify the game to finish.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="client">The clientId.</param>
		public void FinishGame(string name, string client)
		{
			_games[name].Finish();
			while (!_games[name].BothFinish())
			{
				System.Threading.Thread.Sleep(10);
			}
			_games.Remove(name);
		}

		/// <summary>
		/// Adds move to the given game
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="direction">The direction.</param>
		/// <param name="clientId">The clientId.</param>
		/// <returns></returns>
		public Move AddMove(string name, string direction, string clientId)
		{
			return _games[name].AddMove(direction, clientId);
		}

		/// <summary>
		/// Get state from given game
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="clientId">The clientId.</param>
		/// <returns></returns>
		public string GetState(string name, string clientId)
		{
			return _games[name].GetState(clientId);
		}
	}
}
