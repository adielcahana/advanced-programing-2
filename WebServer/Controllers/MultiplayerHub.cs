using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MazeLib;
using MazeMC;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebServer.Models;

namespace WebServer
{
	public class MultiplayerHub : Hub
	{
		private static MultiPlayerModel model = new MultiPlayerModel();

		public JObject CreateList()
		{
			string list = model.CreateList();
			List<string> gamesList = JsonConvert.DeserializeObject<List<string>>(list);
			JObject obj = new JObject();
			obj["games"] = JToken.FromObject(gamesList);
			return obj;
		}

		public JObject StartGame(string name, int row, int col)
		{
			Maze maze = model.NewGame(name, row, col, getClientId());
			JObject obj = JObject.Parse(maze.ToJSON());
			return obj;
		}

		public JObject JoinGame(string name)
		{
			Maze maze = model.JoinGame(name, getClientId());
			JObject obj = JObject.Parse(maze.ToJSON());
			return obj;
		}

		public void FinishGame(string name)
		{
			model.FinishGame(name, getClientId());
		}

		public JObject AddMove(string name, string direction)
		{
			model.AddMove(name, direction, getClientId());
			string state = model.GetState(name, getClientId());
			JObject obj = JObject.Parse(state);
			return obj;
		}

		private string getClientId()
		{
			return Context.ConnectionId;
		}
	}
}