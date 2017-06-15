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
            JObject obj = JObject.Parse(list);
            return obj;
        }
    }
}
