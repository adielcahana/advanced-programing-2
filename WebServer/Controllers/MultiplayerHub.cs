using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MazeLib;
using MazeMC;
using MazeMC.Models;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebServer.Models;

namespace WebServer
{
	public class MultiplayerHub : Hub
	{
		private static MultiPlayerModel model = new MultiPlayerModel();
		private static bool firstConnection = true;

		public MultiplayerHub()
		{
			if (firstConnection)
			{
				model.NewState += new MultiplayerModel.OnNewState(delegate(string gameName, string player1, string player2)
				{
					string move = model.GetState(gameName, player1);
					JObject obj = JObject.Parse(move);
					Clients.Client(player1).newState(obj);
					Clients.Client(player2).newState(obj);
				});
				firstConnection = false;
			}
		}
		
		public void CreateList()
		{
			string list = model.CreateList();
			List<string> gamesList = JsonConvert.DeserializeObject<List<string>>(list);
			JObject obj = new JObject();
			obj["games"] = JToken.FromObject(gamesList);
			Clients.Client(Context.ConnectionId).list(obj);
		}

		public void StartGame(string name, int row, int col)
		{
			Maze maze = model.NewGame(name, row, col, getClientId());
			JObject obj = JObject.Parse(maze.ToJSON());
			Clients.Client(Context.ConnectionId).createGame(obj);
			//push new game to all clients
			string list = model.CreateList();
			List<string> gamesList = JsonConvert.DeserializeObject<List<string>>(list);
			obj = new JObject();
			obj["games"] = JToken.FromObject(gamesList);
			Clients.All.list(obj);
		}

		public void JoinGame(string name)
		{
			Maze maze = model.JoinGame(name, getClientId());
			JObject obj = JObject.Parse(maze.ToJSON());
			Clients.Client(Context.ConnectionId).createGame(obj);
		}

		public void FinishGame(string name)
		{
			model.FinishGame(name, getClientId());
		}

		public void AddMove(string name, string direction)
		{
			model.AddMove(name, direction, getClientId());
		}

		private string getClientId()
		{
			return Context.ConnectionId;
		}
	}
}