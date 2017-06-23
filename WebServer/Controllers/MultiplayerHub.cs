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
				model.GameStart += new MultiplayerModel.OnGameStart(delegate (string player1, string player2)
				{
					Clients.Client(player1).startGame();
				});
                model.GameFinish += new MultiplayerModel.OnGameFinish(delegate (string gameName, string player1, string player2)
                {
                    JObject obj = new JObject
                    {
                        ["msg"] = "You Won!"
                    };
                    Clients.Client(player1).finishGame(obj);
                    obj = new JObject
                    {
                        ["msg"] = "You Lose!"
                    };
                    Clients.Client(player2).finishGame(obj);
                    model.SetFinishMessageSent(gameName);
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
			JObject obj;
			Maze maze = model.NewGame(name, row, col, getClientId());
			if (maze == null)
			{
				obj = new JObject
				{
					["msg"] = "name already exist"
				};
			}
			else
			{
				obj = JObject.Parse(maze.ToJSON());
				//push new game to all clients lists
				string list = model.CreateList();
				List<string> gamesList = JsonConvert.DeserializeObject<List<string>>(list);
				JObject gamesListObj = new JObject();
				gamesListObj["games"] = JToken.FromObject(gamesList);
				Clients.All.list(gamesListObj);
			}
			Clients.Client(Context.ConnectionId).initGame(obj);
		}

		public void JoinGame(string name)
		{
			JObject obj;
			Maze maze = model.JoinGame(name, getClientId());
			if (maze == null)
			{
				obj = new JObject
				{
					["msg"] = "game already full"
				};
			}
			else
			{
				obj = JObject.Parse(maze.ToJSON());
				//remove game name from clients list
				string list = model.CreateList();
				List<string> gamesList = JsonConvert.DeserializeObject<List<string>>(list);
				JObject gamesListObj = new JObject();
				gamesListObj["games"] = JToken.FromObject(gamesList);
				Clients.All.list(gamesListObj);
			}
			Clients.Client(Context.ConnectionId).initGame(obj);
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