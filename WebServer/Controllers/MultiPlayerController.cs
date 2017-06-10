using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class MultiPlayerController : ApiController
    {
	    private static MultiPlayerModel model = new MultiPlayerModel();

	    [HttpGet]
	    [Route("MultyPlayer/list")]
	    public JObject CreateList()
	    {
		    List<string> names = model.CreateList();
			JObject obj = JObject.Parse(JsonConvert.SerializeObject(names, Formatting.Indented));
		    return obj;
	    }

	    [HttpGet]
	    [Route("MultyPlayer/list")]
	    public JObject CreateList()
	    {
		    List<string> names = model.CreateList();
		    JObject obj = JObject.Parse(JsonConvert.SerializeObject(names, Formatting.Indented));
		    return obj;
	    }

	}
}