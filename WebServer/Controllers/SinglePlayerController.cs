using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MazeAdapterLib;
using MazeLib;
using MazeMC.Models;
using Newtonsoft.Json.Linq;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class SinglePlayerController : ApiController
    {
	    private SinglePlayerModel model;

	    public SinglePlayerController()
	    {
		    model = new SinglePlayerModel();
	    }

	    [HttpGet]
	    public JObject GenerateMaze(string name, int row, int col)
	    {
		    Maze maze = model.GenerateMaze(name, row, col);
			JObject obj = JObject.Parse(maze.ToJSON());
		    return obj;
	    }

	    [HttpPost]
	    public JObject SolveMaze(string name, Algorithm algorithm)
	    {
		    MazeSolution solution = model.SolveMaze(name, algorithm);
		    JObject obj = JObject.Parse(solution.ToJson());
		    return obj;
	    }
	}
}