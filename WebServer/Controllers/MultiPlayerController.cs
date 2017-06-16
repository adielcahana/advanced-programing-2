using MazeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class MultiPlayerController : ApiController
    {
        private static MultiPlayerModel model = new MultiPlayerModel();

        [HttpGet]
        [Route("MultiPlayer")]
        public JObject CreateList()
        {
            string list = model.CreateList();
            List<string> gamesList = JsonConvert.DeserializeObject<List<string>>(list);
            JObject obj = new JObject();
            obj["games"] = JToken.FromObject(gamesList);
            return obj;
        }

        [HttpGet]
        [Route("MultiPlayer/{name}/{row}/{col}/{username}")]
        public JObject StartGame(string name, int row, int col, string username)
        {
            Maze maze = model.NewGame(name, row, col, username);
            JObject obj = JObject.Parse(maze.ToJSON());
            return obj;
        }

        [HttpGet]
        [Route("MultiPlayer/{name}/{username}")]
        public JObject JoinGame(string name, string username)
        {
            Maze maze = model.JoinGame(name, username);
            JObject obj = JObject.Parse(maze.ToJSON());
            return obj;
        }
    }
}
