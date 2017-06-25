﻿using System;
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
		private static MultiplayerModel model = new MultiplayerModel();
        private UserContext db = new UserContext();
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
                model.GameFinish += new MultiplayerModel.OnGameFinish(delegate (string gameName, string winner, string loser)
                {
                    string winnerUsername = model.GetUsernameById(gameName, winner);
                    JObject obj = new JObject
                    {
                        ["msg"] = "You Won!"
                    };
                    Clients.Client(winner).finishGame(obj);
                    Rank winnerRank = db.Ranks.Find(winnerUsername);
//                    db.Ranks.Remove(winnerRank);
                    winnerRank.GamesWon++;
//                    db.Ranks.Add(winnerRank);
                    string loserUsername = model.GetUsernameById(gameName, loser);
                    obj = new JObject
                    {
                        ["msg"] = "You Lose!"
                    };
                    Clients.Client(loser).finishGame(obj);
                    Rank loserRank = db.Ranks.Find(loserUsername);
//                    db.Ranks.Remove(loserRank);
                    loserRank.GamesLost++;
//                    db.Ranks.Add(loserRank);
                    model.SetFinishMessageSent(gameName);
                    db.SaveChanges();
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

		public void StartGame(string name, int row, int col, string username)
		{
			JObject obj;
			Maze maze = model.NewGame(name, row, col, getClientId(), username);
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

		public void JoinGame(string name, string username)
		{
			JObject obj;
			Maze maze = model.JoinGame(name, getClientId(), username);
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